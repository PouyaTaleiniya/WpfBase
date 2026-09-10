using MaterialDesignColors;
using MaterialDesignThemes.Wpf;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using XControlComponents.Tools;
using XControlComponents.Tools.Controls;
using XControlHelper;

namespace XControlComponents.Controls;


public partial class XDate : UserControl
{
    private bool IsEnternalEntered { get; set; }

    //Border  
    private double _WidthEntered;
    private double _HeightEntered;

    //Label
    private double _LabelWidthEntered;
    private double _LabelFontSizeEntered;

    //Text 
    private double _TextFontSizeEntered;
    private double _PlaceHolderFontSizeEntered;

    public XDate()
    {
        InitializeComponent();
        DateDialogHost.Tag = Guid.NewGuid().ToString();

        //Border 
        _Height = XDateDefaults.Height;
        _Width = XDateDefaults.Width;
        _WidthAuto = XDateDefaults.WidthAuto;
        _RequiredVisibility = XDateDefaults.RequiredVisibility;
        _RequiredColor = XDateDefaults.RequiredColor;
        _BorderBrushFocused = XDateDefaults.BorderBrushFocused;
        _BorderBrushColor = XDateDefaults.BorderBrushColor;
        _BorderBrushColorFocused = XDateDefaults.BorderBrushColorFocused;

        //Label
        _LabelVisibility = true;
        _LabelTitle = XDateDefaults.LabelTitle;
        _LabelFontSize = XDateDefaults.LabelFontSize;
        _LabelOpacity = XDateDefaults.LabelOpacity;
        _LabelColor = XDateDefaults.LabelColor;
        _LabelGridWidth = XDateDefaults.LabelGridWidth;
        _LabelVerticalAlignment = XDateDefaults.LabelVerticalAlignment;
        _LabelPadding = XDateDefaults.LabelPadding;
        _LabelTop = XDateDefaults.LabelTop;
        _LabelTopHeight = XDateDefaults.LabelTopHeight;
    }

    #region  Border 
    private double? _height;
    public double? _Height
    {
        get => _height;
        set
        {
            _height = value;

            if (_height == null)
                _height = XDateDefaults.Height;

            if (_height < XDateDefaults.Height)
                _height = XDateDefaults.Height;

            if (!IsEnternalEntered)
                _HeightEntered = _height.Value;

            GR_DateBox.Height = _height.Value;
        }
    }

    private double? _width;
    public double? _Width
    {
        get => _width;
        set
        {
            _width = value;

            if (_width == null)
                _width = XDateDefaults.Width;

            _WidthEntered = _width.Value;
            GR_DateBox.Width = _width.Value;
        }
    }

    private double? _fontSize;
    public double? _FontSize
    {
        get => _fontSize;
        set
        {
            _fontSize = value;

            if (_fontSize == null)
                _FontSize = XDateDefaults.FontSize;

            if (_FontSize < 10)
                _FontSize = 10;

            IsEnternalEntered = true;
            _LabelFontSize = _FontSize == XDateDefaults.FontSize ? _LabelFontSizeEntered : _FontSize;
            //_TextFontSize = _FontSize == XDateDefaults.FontSize ? _TextFontSizeEntered : _FontSize;
            //_PlaceHolderFontSize = _FontSize == XDateDefaults.FontSize ? _PlaceHolderFontSizeEntered : _FontSize;
            IsEnternalEntered = false;
        }
    }

    private bool _isRequired;
    public bool _IsRequired
    {
        get => _isRequired;
        set
        {
            _isRequired = value;
            Lb_Required.Visibility = _isRequired && _requiredVisibility.Value ? Visibility.Visible : Visibility.Hidden;
        }
    }

    private bool? _requiredVisibility;
    public bool? _RequiredVisibility
    {
        get => _requiredVisibility;
        set
        {
            _requiredVisibility = value;

            if (_requiredVisibility == null)
                _requiredVisibility = XDateDefaults.RequiredVisibility;

            _IsRequired = _isRequired;
        }
    }

    private Brush _requiredColor;
    public Brush _RequiredColor
    {
        get => _requiredColor;
        set
        {
            _requiredColor = value;
            if (_requiredColor == null)
                _requiredColor = XDateDefaults.RequiredColor;

            Lb_Required.Foreground = _requiredColor;
        }
    }

    private bool? _borderBrushFocused;
    public bool? _BorderBrushFocused
    {
        get => _borderBrushFocused;
        set
        {
            _borderBrushFocused = value;
            if (_borderBrushFocused == null)
                _borderBrushFocused = XDateDefaults.BorderBrushFocused;
        }
    }

    private Brush _borderBrushColor;
    public Brush _BorderBrushColor
    {
        get => _borderBrushColor;
        set
        {
            _borderBrushColor = value;
            if (_borderBrushColor == null)
                _borderBrushColor = XDateDefaults.BorderBrushColor;
            BR_Date.BorderBrush = _borderBrushColor;
        }
    }

    private Brush _borderBrushColorFocused;
    public Brush _BorderBrushColorFocused
    {
        get => _borderBrushColorFocused;
        set
        {
            _borderBrushColorFocused = value;

            if (_borderBrushColorFocused == null)
                _borderBrushColorFocused = XDateDefaults.BorderBrushColorFocused;

            var getColorTransparent = XElementHelper.GetColor(Colors.Transparent);
            if (_borderBrushColorFocused.FillStringSafe() == getColorTransparent.FillStringSafe())
                _borderBrushColorFocused = _borderBrushColor;
        }
    }
    #endregion

    #region Label
    private double? _labelGridWidth;
    public double? _LabelGridWidth
    {
        get => _labelGridWidth;
        set
        {
            _labelGridWidth = value;

            //Label Size
            if (_labelGridWidth == null)
                _labelGridWidth = XDateDefaults.LabelGridWidth;

            if (_labelGridWidth > 0 && _labelGridWidth < 60)
                _labelGridWidth = 60;

            if (_labelGridWidth > 0)
                _LabelWidthEntered = _labelGridWidth.Value;

            //Label Content
            if (_labelGridWidth > 0 && _labelTitle.IsNullOrEmpty())
                _LabelTitle = XDateDefaults.LabelTitle;

            GR_Col_Lable.Width = new GridLength(_labelGridWidth.Value, GridUnitType.Pixel);

            if (_labelGridWidth == 0 && BR_Date.Padding.Left == 0)
            {
                BR_Date.Padding = new Thickness(10, 0, 5, 0);
            }

            HandleLabelTop();
        }
    }

    private double? _labelFontSize;
    public double? _LabelFontSize
    {
        get => _labelFontSize;
        set
        {
            _labelFontSize = value;

            if (_labelFontSize == null)
                _labelFontSize = XDateDefaults.LabelFontSize;

            if (_labelFontSize < 10)
                _labelFontSize = 10;

            if (!IsEnternalEntered)
                _LabelFontSizeEntered = _labelFontSize.Value;

            Lb_Content.FontSize = _labelFontSize.Value;
        }
    }

    private double? _labelOpacity;
    public double? _LabelOpacity
    {
        get => _labelOpacity;
        set
        {
            _labelOpacity = value;
            if (_labelOpacity == null)
                _labelOpacity = XDateDefaults.LabelOpacity;

            if (_labelOpacity < 0.1)
                _labelOpacity = 0.1;

            Lb_Content.Opacity = _labelOpacity.Value;
        }
    }

    private double? _labelTopHeight;
    public double? _LabelTopHeight
    {
        get => _labelTopHeight;
        set
        {
            _labelTopHeight = value;
            if (_labelTopHeight == null || _labelTopHeight.Value < 20)
                _labelTopHeight = XDateDefaults.LabelTopHeight;

            HandleLabelTop();
        }
    }

    private bool? _labelTop;
    public bool? _LabelTop
    {
        get => _labelTop;
        set
        {
            _labelTop = value;
            if (_labelTop == null)
                _labelTop = XDateDefaults.LabelTop;

            if (!_labelVisibility.Value || _LabelGridWidth.Value <= 0)
                return;

            HandleLabelTop();
        }
    }

    private bool? _labelVisibility;
    public bool? _LabelVisibility
    {
        get => _labelVisibility;
        set
        {
            _labelVisibility = value;
            if (!_labelVisibility.HasValue || (_labelVisibility.HasValue && _labelVisibility.Value))
            {
                if (_labelVisibility == null)
                    _labelVisibility = true;

                if (_LabelWidthEntered > 0)
                    _LabelGridWidth = _LabelWidthEntered;
                else
                    _LabelGridWidth = XDateDefaults.LabelGridWidth;
            }
            else
                _LabelGridWidth = 0;
        }
    }

    private string _labelTitle;
    public string _LabelTitle
    {
        get => _labelTitle;
        set
        {
            _labelTitle = value;
            if (_labelTitle.IsNullOrEmpty() && _labelGridWidth > 0)
                _labelTitle = XDateDefaults.LabelTitle;
            Lb_Content.Content = _LabelTitle;
        }
    }

    private Brush _labelColor;
    public Brush _LabelColor
    {
        get => _labelColor;
        set
        {
            _labelColor = value;
            if (_labelColor == null)
                _labelColor = XDateDefaults.LabelColor;
            Lb_Content.Foreground = _labelColor;
        }
    }

    private VerticalAlignment? _labelVerticalAlignment;
    public VerticalAlignment? _LabelVerticalAlignment
    {
        get => _labelVerticalAlignment;
        set
        {
            _labelVerticalAlignment = value;
            if (_labelVerticalAlignment == null)
                _labelVerticalAlignment = XDateDefaults.LabelVerticalAlignment;

            Lb_Content.VerticalAlignment = _labelVerticalAlignment.Value;
            Lb_Required.VerticalAlignment = _labelVerticalAlignment.Value;
        }
    }

    private Thickness? _labelPadding;
    public Thickness? _LabelPadding
    {
        get => _labelPadding;
        set
        {
            _labelPadding = value;
            if (_labelPadding == null)
                _labelPadding = XDateDefaults.LabelPadding;

            Lb_Content.Padding = _labelPadding.Value;
        }
    }

    private bool? _widthAuto;
    public bool? _WidthAuto
    {
        get => _widthAuto;
        set
        {
            _widthAuto = value;

            if (_widthAuto == null)
                _widthAuto = XDateDefaults.WidthAuto;

            if (_widthAuto.Value)
                GR_DateBox.ClearValue(WidthProperty);
            else
            {
                if (_WidthEntered > 0)
                    _Width = _WidthEntered;
                else
                    _WidthEntered = XDateDefaults.Width;
            }
        }
    }
    #endregion

    #region Text
    private double? _textFontSize;
    public double? _TextFontSize
    {
        get => _textFontSize;
        set
        {
            _textFontSize = value;

            if (_textFontSize == null)
                _textFontSize = XDateDefaults.TextFontSize;

            if (_textFontSize < 10)
                _textFontSize = 10;

            if (!IsEnternalEntered)
                _TextFontSizeEntered = _textFontSize.Value;

            DT_Date.FontSize = _textFontSize.Value;
        }
    }

    private double? _textOpacity;
    public double? _TextOpacity
    {
        get => _textOpacity;
        set
        {
            _textOpacity = value;
            if (_textOpacity == null)
                _textOpacity = XDateDefaults.TextOpacity;

            if (_textOpacity < 0.1)
                _textOpacity = 0.1;

            DT_Date.Opacity = _textOpacity.Value;
        }
    }

    private Brush _textColor;
    public Brush _TextColor
    {
        get => _textColor;
        set
        {
            _textColor = value;
            if (_textColor == null)
                _textColor = XDateDefaults.TextColor;
            DT_Date.Foreground = _textColor;
        }
    }

    private Brush _textBackgroundColor;
    public Brush _TextBackgroundColor
    {
        get => _textBackgroundColor;
        set
        {
            _textBackgroundColor = value;
            if (_textBackgroundColor == null)
                _textBackgroundColor = XDateDefaults.BackgroundColor;

            DT_Date.Background = _textBackgroundColor;
        }
    }

    private Brush _textCaretColor;
    public Brush _TextCaretColor
    {
        get => _textCaretColor;
        set
        {
            _textCaretColor = value;
            if (_textCaretColor == null)
                _textCaretColor = XDateDefaults.TextCaretColor;
            //DT_Date.CaretBrush = _textCaretColor;
        }
    }

    private Thickness? _textPadding;
    public Thickness? _TextPadding
    {
        get => _textPadding;
        set
        {
            _textPadding = value;
            if (_textPadding == null)
                _textPadding = XDateDefaults.TextPadding;

            DT_Date.Padding = _textPadding.Value;
        }
    }
    #endregion

    #region PlaceHolder
    private double? _placeHolderOpacity;
    public double? _PlaceHolderOpacity
    {
        get => _placeHolderOpacity;
        set
        {
            _placeHolderOpacity = value;
            if (_placeHolderOpacity == null)
                _placeHolderOpacity = XDateDefaults.PlaceHolderOpacity;

            if (_placeHolderOpacity < 0.1)
                _placeHolderOpacity = 0.1;

            Lb_PlaceHolder.Opacity = _placeHolderOpacity.Value;
        }
    }

    private string _placeHolder;
    public string _PlaceHolder
    {
        get => _placeHolder;
        set
        {
            _placeHolder = value;
            Lb_PlaceHolder.Content = _PlaceHolder;
        }
    }

    private double? _placeHolderFontSize;
    public double? _PlaceHolderFontSize
    {
        get => _placeHolderFontSize;
        set
        {
            _placeHolderFontSize = value;

            if (_placeHolderFontSize == null)
                _placeHolderFontSize = XDateDefaults.PlaceHolderFontSize;

            if (_placeHolderFontSize < 10)
                _placeHolderFontSize = 10;

            if (!IsEnternalEntered)
                _PlaceHolderFontSizeEntered = _placeHolderFontSize.Value;

            Lb_PlaceHolder.FontSize = _placeHolderFontSize.Value;
        }
    }

    private Brush _placeHolderColor;
    public Brush _PlaceHolderColor
    {
        get => _placeHolderColor;
        set
        {
            _placeHolderColor = value;
            if (_placeHolderColor == null)
                Lb_PlaceHolder.ClearValue(ForegroundProperty);
            else
                Lb_PlaceHolder.Foreground = _placeHolderColor;
        }
    }
    #endregion

    private void UserControl_Loaded(object sender, RoutedEventArgs e)
    {
        Application.Current.Resources["PrimaryColorKey"] = XAppMethods.Color_Blue_0D47A1();

        var getWindow = Window.GetWindow(this);
        if (getWindow != null)
        {
            getWindow.MouseRightButtonDown += (s, k) =>
            {
                if (DateDialogHost.IsOpen)
                    CloseCalendare();
            };

            getWindow.MouseLeftButtonDown += (s, k) =>
            {
                if (DateDialogHost.IsOpen)
                {
                    var getClickedElement = k.OriginalSource as FrameworkElement;

                    var getBorder = XElementHelper.FindParentByName<Border>(getClickedElement, "BR_Date");
                    if (getBorder != null)
                        return;

                    if (DateDialogHost.IsOpen)
                        CloseCalendare();
                }
            };

            getWindow.PreviewMouseDown += (s, k) =>
            {
                if (Txt_Data.IsFocused)
                {
                    GR_DateBox.Focus();
                }
            };
        }
        DT_Date.SelectedDate = XDataConverter.StringToDateTime("6/16/2025", "M/D/Y");

        MyCalendar.Resources.MergedDictionaries.Clear();
        MyCalendar.Resources.MergedDictionaries.Add(new BundledTheme()
        {
            BaseTheme = BaseTheme.Light,
            PrimaryColor = PrimaryColor.DeepPurple,
            SecondaryColor = SecondaryColor.Lime,
        });
    }

    private void HandleLabelTop()
    {
        var Height = _HeightEntered;

        if (_labelVisibility.Value || _LabelGridWidth.Value > 0)
        {
            if (_labelTop.HasValue && _labelTopHeight.HasValue && _labelTop.Value)
            {
                Height = Height + _labelTopHeight.Value;
                GR_Row_Label.Height = new GridLength(_labelTopHeight.Value, GridUnitType.Pixel);
                Lb_Content.Padding = new Thickness(0);

                //GR_Content
                Grid.SetRow(GR_Content, 0);
                Grid.SetColumnSpan(GR_Content, 2);

                //BR_Text
                Grid.SetColumn(BR_Date, 1);
                Grid.SetColumnSpan(BR_Date, 2);
            }
            else
                DisableLabelTop();
        }
        else
            DisableLabelTop();

        IsEnternalEntered = true;
        _Height = Height;
        IsEnternalEntered = false;

        void DisableLabelTop()
        {
            Lb_Content.Padding = new Thickness(10, 0, 0, 0);
            GR_Row_Label.Height = new GridLength(0);

            //GR_Content
            Grid.SetRow(GR_Content, 1);
            Grid.SetColumnSpan(GR_Content, 1);

            //BR_Text
            Grid.SetColumn(BR_Date, 2);
            Grid.SetColumnSpan(BR_Date, 1);
        }
    }

    private void DatePicker_CalendarOpened(object sender, RoutedEventArgs e)
    {

    }

    private void DatePicker_CalendarClosed(object sender, RoutedEventArgs e)
    {
        //if (sender is DatePicker dp && dp.Template.FindName("PART_Text", dp) is DatePickerText tb)
        //{
        //    tb.Select(0, 0);  
        //}
    }

    private void Txt_Data_PreviewTextInput(object sender, TextCompositionEventArgs e)
    {
        //var textBox = sender as TextBox;

        //if (_inputType == XInputTypes.Int)
        //{
        //    e.Handled = XDataConverter.Regex_Int(e.Text);
        //    return;
        //}

        //if (_inputType == XInputTypes.Decimal)
        //{
        //    string proposedText = textBox.Text.Insert(textBox.SelectionStart, e.Text);
        //    e.Handled = XDataConverter.Regex_Decimal(proposedText, _decimalPoint.Value);
        //    return;
        //}
    }

    private void Txt_Data_PreviewKeyDown(object sender, KeyEventArgs e)
    {
        if (e.Key == Key.Space)
        {
            e.Handled = true;
        }

        if (e.Key == Key.Escape)
        {
            GR_DateBox.Focus();
        }

        //Paste (Ctrl+V)
        if ((Keyboard.Modifiers & ModifierKeys.Control) == ModifierKeys.Control && e.Key == Key.V)
        {
            e.Handled = true;
        }
    }

    private void Txt_Data_Pasting(object sender, DataObjectPastingEventArgs e)
    {
        e.CancelCommand();
    }

    private void Txt_Data_GotFocus(object sender, RoutedEventArgs e)
    {
        if (_borderBrushFocused.Value)
        {
            BR_Date.BorderBrush = _borderBrushColorFocused;
        }
    }

    private void Txt_Data_LostFocus(object sender, RoutedEventArgs e)
    {
        if (_borderBrushFocused.Value)
        {
            BR_Date.BorderBrush = _borderBrushColor;
        }
    }

    private void Label_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
    {
        XPopupManager.CloseAllDialogHosts();
        XPopupManager.RegisterDialogHost(DateDialogHost);
    }

    private void MyCalendar_SelectedDatesChanged(object sender, SelectionChangedEventArgs e)
    {
        var x = sender as Calendar;
        Txt_Data.Text = x.SelectedDate.Value.ToString("yyyy/MM/dd");
        CloseCalendare();
    }

    private void CloseCalendare()
    {
        XPopupManager.CloseDialogHostByTag(DateDialogHost.Tag.FillStringSafe());
    }
}

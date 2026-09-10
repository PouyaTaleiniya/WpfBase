using MaterialDesignThemes.Wpf;
using System.ComponentModel;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;
using XControlHelper;

namespace XControlComponents.Controls;

/// <summary>
/// Interaction logic for XTextBox.xaml
/// </summary>
public partial class XTextBox1 : UserControl
{
    private bool IsEnternalEntered { get; set; }

    //Border Box 
    private double _WidthEntered;
    private double _HeightEntered;
    private bool _IsFocused { get; set; }

    //Label
    private double _LabelWidthEntered;
    private double _LabelFontSizeEntered;

    //Text Box
    private double _TextFontSizeEntered;
    private double _PlaceHolderFontSizeEntered;

    [EditorBrowsable(EditorBrowsableState.Always)]
    public delegate void _TextChangedEventHandler(object sender, object Data);
    public event _TextChangedEventHandler _TextChanged;

    public XTextBox1()
    {
        InitializeComponent();

        //Border Box
        _Height = XTextBoxDefaults.Height;
        _Width = XTextBoxDefaults.Width;
        _WidthAuto = XTextBoxDefaults.WidthAuto;
        _RequiredVisibility = XTextBoxDefaults.RequiredVisibility;
        _BackgroundColor = XTextBoxDefaults.BackgroundColor;
        _RequiredColor = XTextBoxDefaults.RequiredColor;
        _BorderBrushFocused = XTextBoxDefaults.BorderBrushFocused;
        _BorderBrushColor = XTextBoxDefaults.BorderBrushColor;
        _BorderBrushColorFocused = XTextBoxDefaults.BorderBrushColorFocused;
        _ElementType = XElementTypes.Normal;

        //Label
        _LabelVisibility = true;
        _LabelTitle = XTextBoxDefaults.LabelTitle;
        _LabelFontSize = XTextBoxDefaults.LabelFontSize;
        _LabelOpacity = XTextBoxDefaults.LabelOpacity;
        _LabelColor = XTextBoxDefaults.LabelColor;
        _LabelGridWidth = XTextBoxDefaults.LabelGridWidth;
        _LabelVerticalAlignment = XTextBoxDefaults.LabelVerticalAlignment;
        _LabelPadding = XTextBoxDefaults.LabelPadding;
        _LabelTop = XTextBoxDefaults.LabelTop;
        _LabelTopHeight = XTextBoxDefaults.LabelTopHeight;

        //Text Box
        _TextBoxFontSize = XTextBoxDefaults.TextBoxFontSize;
        _TextBoxColor = XTextBoxDefaults.TextBoxColor;
        _TextBoxCaretColor = XTextBoxDefaults.TextBoxCaretColor;
        _TextBoxSelectionColor = XTextBoxDefaults.TextBoxSelectionColor;
        _TextBoxOpacity = XTextBoxDefaults.TextBoxOpacity;
        _TextBoxPadding = XTextBoxDefaults.TextBoxPadding;

        //Place Holder
        _PlaceHolderFontSize = XTextBoxDefaults.PlaceHolderFontSize;
        _PlaceHolderOpacity = XTextBoxDefaults.PlaceHolderOpacity;
        _PlaceHolder = XTextBoxDefaults.PlaceHolder;

        // Multi Line
        _MultiLine = XTextBoxDefaults.MultiLine;
        _MultiLineHeight = XTextBoxDefaults.MultiLineHeight;

        //Max Length
        _MaxLengthGridWidth = XTextBoxDefaults.MaxLengthGridWidth;
        _MaxLengthOpacity = XTextBoxDefaults.MaxLengthOpacity;
        _MaxLengthMargin = XTextBoxDefaults.MaxLengthMargin;
        _MaxLengthOutbox = XTextBoxDefaults.MaxLengthOutbox;
        _MaxLengthColor = XTextBoxDefaults.MaxLengthColor;
        _MaxLengthVisibility = false;

        //Value
        _TriggerValueChanged = XTextBoxDefaults.TriggerValueChanged;
        _isNumberGenerate = XTextBoxDefaults.IsNumberGenerate;
        _InputType = XTextBoxDefaults.InputType;
        _DecimalPoint = XTextBoxDefaults.DecimalPoint;
        _AllowPaste = XTextBoxDefaults.AllowPaste;
        _AllowImojiPaste = XTextBoxDefaults.AllowImojiPaste;
    }

    #region  Border Box
    private double? _height;
    public double? _Height
    {
        get => _height;
        set
        {
            _height = value;

            if (_height == null)
                _height = XTextBoxDefaults.Height;

            if (_height < XTextBoxDefaults.Height)
                _height = XTextBoxDefaults.Height;

            if (!IsEnternalEntered)
                _HeightEntered = _height.Value;

            GR_TextBox.Height = _height.Value;
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
                _width = XTextBoxDefaults.Width;

            _WidthEntered = _width.Value;
            GR_TextBox.Width = _width.Value;
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
                _FontSize = XTextBoxDefaults.FontSize;

            if (_FontSize < 10)
                _FontSize = 10;

            IsEnternalEntered = true;
            _LabelFontSize = _FontSize == XTextBoxDefaults.FontSize ? _LabelFontSizeEntered : _FontSize;
            _TextBoxFontSize = _FontSize == XTextBoxDefaults.FontSize ? _TextFontSizeEntered : _FontSize;
            _PlaceHolderFontSize = _FontSize == XTextBoxDefaults.FontSize ? _PlaceHolderFontSizeEntered : _FontSize;
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
                _requiredVisibility = XTextBoxDefaults.RequiredVisibility;

            _IsRequired = _isRequired;
        }
    }

    private Brush _backgroundColor;
    public Brush _BackgroundColor
    {
        get => _backgroundColor;
        set
        {
            _backgroundColor = value;
            if (_backgroundColor == null)
                _backgroundColor = XTextBoxDefaults.BackgroundColor;

            HandleBackground();
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
                _requiredColor = XTextBoxDefaults.RequiredColor;

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
                _borderBrushFocused = XTextBoxDefaults.BorderBrushFocused;
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
                _borderBrushColor = XTextBoxDefaults.BorderBrushColor;
            BR_TextBox.BorderBrush = _borderBrushColor;
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
                _borderBrushColorFocused = XTextBoxDefaults.BorderBrushColorFocused;

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
                _labelGridWidth = XTextBoxDefaults.LabelGridWidth;

            if (_labelGridWidth > 0 && _labelGridWidth < 60)
                _labelGridWidth = 60;

            if (_labelGridWidth > 0)
                _LabelWidthEntered = _labelGridWidth.Value;

            //Label Content
            if (_labelGridWidth > 0 && _labelTitle.IsNullOrEmpty())
                _LabelTitle = XTextBoxDefaults.LabelTitle;

            GR_Col_Lable.Width = new GridLength(_labelGridWidth.Value, GridUnitType.Pixel);

            if (_labelGridWidth == 0 && Txt_Data.Padding.Left == 0)
            {
                Txt_Data.Padding = new Thickness(10, 0, 5, 0);
            }

            HandleBorder();
            HandleMultiLine();
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
                _labelFontSize = XTextBoxDefaults.LabelFontSize;

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
                _labelOpacity = XTextBoxDefaults.LabelOpacity;

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
                _labelTopHeight = XTextBoxDefaults.LabelTopHeight;

            HandleMultiLine();
        }
    }

    private XElementTypes? _elementType;
    public XElementTypes? _ElementType
    {
        get => _elementType;
        set
        {
            _elementType = value;
            if (_elementType == null)
                _elementType = XTextBoxDefaults.ElementType;

            if (!_labelVisibility.HasValue)
                return;

            HandleBackground();
            HandleMultiLine();
            HandleBorder();
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
                _labelTop = XTextBoxDefaults.LabelTop;

            if (!_labelVisibility.Value || _LabelGridWidth.Value <= 0)
                return;

            HandleBorder();
            HandleMultiLine();
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
                    _LabelGridWidth = XTextBoxDefaults.LabelGridWidth;
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
                _labelTitle = XTextBoxDefaults.LabelTitle;
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
                _labelColor = XTextBoxDefaults.LabelColor;
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
                _labelVerticalAlignment = XTextBoxDefaults.LabelVerticalAlignment;

            Lb_Content.VerticalAlignment = _labelVerticalAlignment.Value;
            Lb_Required.VerticalAlignment = _labelVerticalAlignment.Value;
        }
    }

    private Brush _labelBackgroundColor;
    public Brush _LabelBackgroundColor
    {
        get => _labelBackgroundColor;
        set
        {
            _labelBackgroundColor = value;
            HandleBackground();
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
                _labelPadding = XTextBoxDefaults.LabelPadding;

            if (_labelTop.HasValue && !_labelTop.Value)
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
                _widthAuto = XTextBoxDefaults.WidthAuto;

            if (_widthAuto.Value)
                GR_TextBox.ClearValue(WidthProperty);
            else
            {
                if (_WidthEntered > 0)
                    _Width = _WidthEntered;
                else
                    _WidthEntered = XTextBoxDefaults.Width;
            }
        }
    }
    #endregion

    #region TextBox
    private double? _textBoxFontSize;
    public double? _TextBoxFontSize
    {
        get => _textBoxFontSize;
        set
        {
            _textBoxFontSize = value;

            if (_textBoxFontSize == null)
                _textBoxFontSize = XTextBoxDefaults.TextBoxFontSize;

            if (_textBoxFontSize < 10)
                _textBoxFontSize = 10;

            if (!IsEnternalEntered)
                _TextFontSizeEntered = _textBoxFontSize.Value;

            Txt_Data.FontSize = _textBoxFontSize.Value;
        }
    }

    private double? _textBoxOpacity;
    public double? _TextBoxOpacity
    {
        get => _textBoxOpacity;
        set
        {
            _textBoxOpacity = value;
            if (_textBoxOpacity == null)
                _textBoxOpacity = XTextBoxDefaults.TextBoxOpacity;

            if (_textBoxOpacity < 0.1)
                _textBoxOpacity = 0.1;

            Txt_Data.Opacity = _textBoxOpacity.Value;
        }
    }

    private Brush _textBoxColor;
    public Brush _TextBoxColor
    {
        get => _textBoxColor;
        set
        {
            _textBoxColor = value;
            if (_textBoxColor == null)
                _textBoxColor = XTextBoxDefaults.TextBoxColor;
            Txt_Data.Foreground = _textBoxColor;
        }
    }

    private Brush _textBoxBackgroundColor;
    public Brush _TextBoxBackgroundColor
    {
        get => _textBoxBackgroundColor;
        set
        {
            _textBoxBackgroundColor = value;
            HandleBackground();
        }
    }

    private Brush _textBoxCaretColor;
    public Brush _TextBoxCaretColor
    {
        get => _textBoxCaretColor;
        set
        {
            _textBoxCaretColor = value;
            if (_textBoxCaretColor == null)
                _textBoxCaretColor = XTextBoxDefaults.TextBoxCaretColor;

            Txt_Data.CaretBrush = _textBoxCaretColor;
        }
    }

    private Brush _textBoxSelectionColor;
    public Brush _TextBoxSelectionColor
    {
        get => _textBoxSelectionColor;
        set
        {
            _textBoxSelectionColor = value;
            if (_textBoxSelectionColor == null)
                _textBoxSelectionColor = XTextBoxDefaults.TextBoxSelectionColor;

            Txt_Data.SelectionBrush = _textBoxSelectionColor;
        }
    }

    private Thickness? _textBoxPadding;
    public Thickness? _TextBoxPadding
    {
        get => _textBoxPadding;
        set
        {
            _textBoxPadding = value;
            if (_textBoxPadding == null)
                _textBoxPadding = XTextBoxDefaults.TextBoxPadding;

            Txt_Data.Padding = _textBoxPadding.Value;
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
                _placeHolderOpacity = XTextBoxDefaults.PlaceHolderOpacity;

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
                _placeHolderFontSize = XTextBoxDefaults.PlaceHolderFontSize;

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

    #region Multi Line
    private bool? _multiLine;
    public bool? _MultiLine
    {
        get => _multiLine;
        set
        {
            _multiLine = value;

            if (_multiLine == null)
                _multiLine = XTextBoxDefaults.MultiLine;

            HandleMultiLine();
            HandleBorder();
        }
    }

    private double? _multiLineHeight;
    public double? _MultiLineHeight
    {
        get => _multiLineHeight;
        set
        {
            _multiLineHeight = value;

            if (_multiLineHeight == null || _multiLineHeight.Value < 40)
                _multiLineHeight = XTextBoxDefaults.MultiLineHeight;

            HandleMultiLine();
        }
    }
    #endregion

    #region MaxLength
    private int? _maxLength;
    public int? _MaxLength
    {
        get => _maxLength;
        set
        {
            _maxLength = value;
            if (_maxLength == null)
                _maxLength = 0;
            Txt_Data.MaxLength = _maxLength.Value;
        }
    }

    private double? _maxLengthOpacity;
    public double? _MaxLengthOpacity
    {
        get => _maxLengthOpacity;
        set
        {
            _maxLengthOpacity = value;
            if (_maxLengthOpacity == null)
                _maxLengthOpacity = XTextBoxDefaults.MaxLengthOpacity;

            if (_maxLengthOpacity < 0.1)
                _maxLengthOpacity = 0.1;

            Txt_MaxLength.Opacity = _maxLengthOpacity.Value;
        }
    }

    private bool _maxLengthVisibility;
    public bool _MaxLengthVisibility
    {
        get => _maxLengthVisibility;
        set
        {
            _maxLengthVisibility = value;
            if (_maxLength <= 0)
                _maxLengthVisibility = false;

            HandleBorder();
        }
    }

    private double? _maxLengthGridWidth;
    public double? _MaxLengthGridWidth
    {
        get => _maxLengthGridWidth;
        set
        {
            _maxLengthGridWidth = value;
            if (_maxLengthGridWidth == null)
                _maxLengthGridWidth = XTextBoxDefaults.MaxLengthGridWidth;

            HandleBorder();
        }
    }

    private bool? _maxLengthOutbox;
    public bool? _MaxLengthOutbox
    {
        get => _maxLengthOutbox;
        set
        {
            _maxLengthOutbox = value;
            if (_maxLengthOutbox == null)
                _maxLengthOutbox = XTextBoxDefaults.MaxLengthOutbox;

            HandleBorder();
        }
    }

    private Brush _maxLengthColor;
    public Brush _MaxLengthColor
    {
        get => _maxLengthColor;
        set
        {
            _maxLengthColor = value;
            if (_maxLengthColor == null)
                _maxLengthColor = XTextBoxDefaults.MaxLengthColor;

            Txt_MaxLength.Foreground = _maxLengthColor;
        }
    }

    private Thickness? _maxLengthMargin;
    public Thickness? _MaxLengthMargin
    {
        get => _maxLengthMargin;
        set
        {
            _maxLengthMargin = value;
            if (_maxLengthMargin == null)
                _maxLengthMargin = XTextBoxDefaults.MaxLengthMargin;

            Txt_MaxLength.Margin = _maxLengthMargin.Value;
        }
    }
    #endregion

    #region Values
    private XInputTypes? _inputType;
    public XInputTypes? _InputType
    {
        get => _inputType;
        set
        {
            _inputType = value;
            if (_inputType == null)
                _inputType = XTextBoxDefaults.InputType;

            HandleData();
        }
    }

    private XDecimalPoints? _decimalPoint;
    public XDecimalPoints? _DecimalPoint
    {
        get => _decimalPoint;
        set
        {
            _decimalPoint = value;
            if (_decimalPoint == null)
                _decimalPoint = XTextBoxDefaults.DecimalPoint;

            HandleData();
        }
    }

    private bool? _allowPaste;
    public bool? _AllowPaste
    {
        get => _allowPaste;
        set
        {
            _allowPaste = value;
            if (_allowPaste == null)
                _allowPaste = false;
        }
    }

    private bool? _allowImojiPaste;
    public bool? _AllowImojiPaste
    {
        get => _allowImojiPaste;
        set
        {
            _allowImojiPaste = value;
            if (_allowImojiPaste == null)
                _allowImojiPaste = false;
        }
    }

    private bool? _triggerValueChanged;
    public bool? _TriggerValueChanged
    {
        get => _triggerValueChanged;
        set
        {
            if (_triggerValueChanged == null)
                _triggerValueChanged = false;
            _triggerValueChanged = XTextBoxDefaults.TriggerValueChanged;
        }
    }

    private bool? _isNumberGenerate;
    public bool? _IsNumberGenerate
    {
        get => _isNumberGenerate;
        set
        {
            _isNumberGenerate = value;
            if (_isNumberGenerate == null)
                _isNumberGenerate = XTextBoxDefaults.IsNumberGenerate;

            HandleData();
        }
    }

    public object Value
    {
        get
        {
            if (_inputType == XInputTypes.Int)
                return IntData;
            if (_inputType == XInputTypes.Decimal)
                return DecimalData;
            return Data;
        }
        set
        {
            if (!_triggerValueChanged.Value && _TextChanged != null)
                Txt_Data.TextChanged -= Txt_Data_TextChanged;

            if (!value.IsNullOrEmpty())
            {
                if (_inputType == XInputTypes.Int)
                    Txt_Data.Text = value.IntParse().FillStringSafe();
                else if (_inputType == XInputTypes.Decimal)
                    Txt_Data.Text = value.DecimalParse(_decimalPoint.Value).FillStringSafe();
                else
                    Txt_Data.Text = value.FillStringSafe();
            }
            else
                Txt_Data.Text = string.Empty;

            if (!_triggerValueChanged.Value && _TextChanged != null)
                Txt_Data.TextChanged += Txt_Data_TextChanged;
        }
    }

    public string Data
    {
        get => Txt_Data.Text.FillStringSafe();
    }

    public int IntData
    {
        get => Data.IntParse();
    }

    public decimal DecimalData
    {
        get => Data.DecimalParse();
    }
    #endregion

    private void UserControl_Loaded(object sender, RoutedEventArgs e)
    {
        var getWindow = Window.GetWindow(this);
        if (getWindow != null)
        {
            getWindow.PreviewMouseDown += (s, k) =>
            {
                if (Txt_Data.IsFocused)
                {
                    GR_TextBox.Focus();
                }
            };
        }
    }

    private void HandleBorder()
    {
        if (_elementType == XElementTypes.Floating)
            HandleFloatingModeBorder();
        else if (_elementType == XElementTypes.Border)
            HandleLabelModeBorder();
        else
            HandleNormalModeBorder();
    }

    private void HandleNormalModeBorder()
    {
        //Reset Borders
        BR_Label.BorderThickness = new Thickness(0);
        BR_TextBox.BorderThickness = new Thickness(1, 1, 0, 1);
        BR_MaxLength.BorderThickness = new Thickness(0, 1, 1, 1);
        TextFieldAssist.SetUnderlineBrush(Txt_Data, XElementHelper.GetColor(Colors.Transparent));

        if (_maxLengthVisibility)
        {
            if (!_labelTop.Value || !_labelVisibility.Value)
            {
                if (_maxLengthOutbox.HasValue || _multiLine.Value)
                {
                    BR_TextBox.BorderThickness = _maxLengthOutbox.Value || _multiLine.Value ? new Thickness(1) : new Thickness(1, 1, 0, 1);
                    BR_MaxLength.BorderThickness = _maxLengthOutbox.Value || _multiLine.Value ? new Thickness(0) : new Thickness(0, 1, 1, 1);
                    BR_MaxLength.Background = _maxLengthOutbox.Value || _multiLine.Value ? XElementHelper.GetColor(Colors.Transparent) : _textBoxBackgroundColor;
                }
                else
                {
                    BR_TextBox.BorderThickness = new Thickness(1, 1, 0, 1);
                    BR_MaxLength.BorderThickness = new Thickness(0, 1, 1, 1);
                    BR_MaxLength.Background = _textBoxBackgroundColor;
                }
            }
            else
            {
                BR_TextBox.BorderThickness = new Thickness(1);
                BR_MaxLength.BorderThickness = new Thickness(0);
                BR_MaxLength.Background = XElementHelper.GetColor(Colors.Transparent);
            }

            BR_MaxLength.Visibility = Visibility.Visible;
            GR_Col_Counter.Width = new GridLength(_maxLengthGridWidth.Value, GridUnitType.Pixel);
        }
        else
        {
            BR_TextBox.BorderThickness = new Thickness(1);
            if (_labelTop.HasValue && !_labelTop.Value)
            {
                GR_Col_Counter.Width = new GridLength(0, GridUnitType.Pixel);
                BR_MaxLength.Visibility = Visibility.Visible;
            }
            else
                BR_MaxLength.Visibility = Visibility.Hidden;
        }
    }

    private void HandleLabelModeBorder()
    {
        var BorderRight = _LabelVisibility.HasValue && _LabelVisibility.Value ? 0 : 1;

        //Max Length Visible
        GR_Col_Counter.Width = new GridLength(_maxLengthVisibility ? _maxLengthGridWidth.Value : 0, GridUnitType.Pixel);

        //Reset Borders
        BR_Label.BorderThickness = new Thickness(1, 1, 0, 1);
        BR_TextBox.BorderThickness = new Thickness(BorderRight, 1, 1, 1);
        BR_MaxLength.BorderThickness = new Thickness(0, 1, 1, 1);
        TextFieldAssist.SetUnderlineBrush(Txt_Data, XElementHelper.GetColor(Colors.Transparent));

        if (_labelTop.HasValue && _labelTop.Value && _LabelVisibility.HasValue && _LabelVisibility.Value)
        {
            var IsCoverMaxLength = !_maxLengthVisibility || (_maxLengthVisibility && !_MaxLengthOutbox.Value);
            var IsCoverBorder = !_maxLengthVisibility || (_maxLengthVisibility && _MaxLengthOutbox.Value);

            if (_maxLengthVisibility)
            {
                BR_Label.BorderThickness = new Thickness(1, 1, _MaxLengthOutbox.Value ? 1 : 0, 0);
                BR_MaxLength.BorderThickness = _MaxLengthOutbox.Value ? new Thickness(0) : new Thickness(0, 1, 1, 0);
                BR_MaxLength.Background = _maxLengthOutbox.Value ? XElementHelper.GetColor(Colors.Transparent) : _backgroundColor;
            }
            else
            {
                BR_Label.BorderThickness = new Thickness(1, 1, 1, 0);
                BR_MaxLength.BorderThickness = new Thickness(0);
                BR_MaxLength.Background = XElementHelper.GetColor(Colors.Transparent);
            }

            BR_TextBox.BorderThickness = new Thickness(1, 0, 1, 1);
            return;
        }

        if (_multiLine.HasValue && _multiLine.Value && _maxLengthVisibility)
        {
            BR_TextBox.BorderThickness = new Thickness(BorderRight, 1, 1, 1);
            BR_MaxLength.BorderThickness = new Thickness(0);
            BR_MaxLength.Background = XElementHelper.GetColor(Colors.Transparent);
            return;
        }

        if (_maxLengthVisibility)
        {
            if (_maxLengthOutbox.HasValue && _maxLengthOutbox.Value)
            {
                BR_TextBox.BorderThickness = new Thickness(BorderRight, 1, 1, 1);
                BR_MaxLength.BorderThickness = new Thickness(0);
                BR_MaxLength.Background = XElementHelper.GetColor(Colors.Transparent);
            }
            else
            {
                BR_TextBox.BorderThickness = new Thickness(BorderRight, 1, 0, 1);
                BR_MaxLength.BorderThickness = new Thickness(0, 1, 1, 1);
                BR_MaxLength.Background = _backgroundColor;
            }
        }
        else
        {
            BR_TextBox.BorderThickness = new Thickness(BorderRight, 1, 1, 1);
        }
    }

    private void HandleFloatingModeBorder()
    {
        //Reset Borders
        BR_Label.BorderThickness = new Thickness(0, 0, 0, 0);
        BR_TextBox.BorderThickness = new Thickness(0, 0, 0, 1);
        BR_MaxLength.BorderThickness = new Thickness(0, 0, 0, 1);


        if (_elementType == XElementTypes.Floating)
        {
            TextFieldAssist.SetUnderlineBrush(Txt_Data, _IsFocused ? _borderBrushColorFocused : _borderBrushColor);
            BR_MaxLength.BorderBrush = _IsFocused ? _borderBrushColorFocused : _borderBrushColor;
        }

        if (_maxLengthVisibility)
        {
            if (_maxLengthOutbox.HasValue)
            {
                BR_MaxLength.BorderThickness = _maxLengthOutbox.Value ? new Thickness(0) : new Thickness(0, 0, 0, _IsFocused ? 2.5 : 1);
            }
            else
            {
                BR_MaxLength.BorderThickness = new Thickness(0, 0, 0, _IsFocused ? 2.5 : 1);
            }
            GR_Col_Counter.Width = new GridLength(_maxLengthGridWidth.Value, GridUnitType.Pixel);
        }
        else
            GR_Col_Counter.Width = new GridLength(0, GridUnitType.Pixel);
    }

    private void HandleData()
    {
        Value = string.Empty;
        if (!_isNumberGenerate.Value)
            return;

        if (!_inputType.HasValue)
            return;

        if (_inputType == XInputTypes.Int || _inputType == XInputTypes.Decimal)
        {
            if (_inputType == XInputTypes.Decimal)
                Value = 0;
            else if (_decimalPoint.HasValue)
                Value = "0".DecimalParse(_decimalPoint.Value);
        }
    }

    private void HandleMultiLine()
    {
        if (_elementType == XElementTypes.Floating)
            HandleNonLabel();
        else if (_elementType == XElementTypes.Border)
            HandleLabelMultiLine();
        else
            HandleNonLabelMultiLine();
    }

    private void HandleNonLabelMultiLine()
    {
        var Height = _HeightEntered;

        if (_labelVisibility.Value || _LabelGridWidth.Value > 0)
        {
            if (_labelTop.HasValue && _labelTopHeight.HasValue && _labelTop.Value)
            {
                Height = Height + _labelTopHeight.Value;
                GR_Row_Label.Height = new GridLength(_labelTopHeight.Value, GridUnitType.Pixel);
                Lb_Content.Padding = new Thickness(3, 0, 0, 0);

                //BR_Label
                Grid.SetRow(BR_Label, 0);
                Grid.SetColumnSpan(BR_Label, 2);

                //BR_TextBox
                Grid.SetColumn(BR_TextBox, 1);
                Grid.SetColumnSpan(BR_TextBox, 3);

                //MaxLength
                Grid.SetRow(BR_MaxLength, 0);
            }
            else
                DisableLabelTop();
        }
        else
            DisableLabelTop();

        if (_multiLine.HasValue && _multiLineHeight.HasValue)
        {
            if (_multiLine.Value)
            {
                Height = Height + _multiLineHeight.Value;
                Txt_Data.VerticalContentAlignment = VerticalAlignment.Top;
                Txt_Data.VerticalScrollBarVisibility = ScrollBarVisibility.Auto;
                Txt_Data.HorizontalScrollBarVisibility = ScrollBarVisibility.Disabled;
                Txt_Data.TextWrapping = TextWrapping.Wrap;
                Txt_Data.Height = Height - GR_Row_Label.Height.Value;
            }
            else
            {
                DisableMultiLine();
            }
        }
        else
        {
            DisableMultiLine();
        }

        IsEnternalEntered = true;
        _Height = Height;
        IsEnternalEntered = false;

        void DisableLabelTop()
        {
            Lb_Content.Padding = _labelPadding.HasValue ? _labelPadding.Value : new Thickness(3, 0, 0, 0);
            GR_Row_Label.Height = new GridLength(0);

            //BR_Label
            Grid.SetRow(BR_Label, 1);
            Grid.SetColumnSpan(BR_Label, 1);

            //BR_TextBox
            Grid.SetColumn(BR_TextBox, 2);
            Grid.SetColumnSpan(BR_TextBox, 1);

            //MaxLength
            Grid.SetRow(BR_MaxLength, 1);
        }

        void DisableMultiLine()
        {
            Txt_Data.ClearValue(HeightProperty);
            Txt_Data.VerticalContentAlignment = VerticalAlignment.Center;
            Txt_Data.VerticalScrollBarVisibility = ScrollBarVisibility.Hidden;
            Txt_Data.HorizontalScrollBarVisibility = ScrollBarVisibility.Hidden;
            Txt_Data.TextWrapping = TextWrapping.NoWrap;
        }
    }

    private void HandleLabelMultiLine()
    {
        var Height = _HeightEntered;

        if (_labelVisibility.Value || _LabelGridWidth.Value > 0)
        {
            if (_labelTop.HasValue && _labelTopHeight.HasValue && _labelTop.Value)
            {
                Height = Height + _labelTopHeight.Value;
                GR_Row_Label.Height = new GridLength(_labelTopHeight.Value, GridUnitType.Pixel);
                Lb_Content.Padding = new Thickness(3, 0, 0, 0);

                if (Txt_Data.Padding.Left == 0)
                {
                    Txt_Data.Padding = new Thickness(10, 0, 5, 0);
                }

                var IsCoverMaxLength = !_maxLengthVisibility || (_maxLengthVisibility && !_MaxLengthOutbox.Value);

                //GR_Content
                Grid.SetRow(BR_Label, 0);
                Grid.SetColumnSpan(BR_Label, IsCoverMaxLength ? 3 : 2);

                //BR_TextBox
                Grid.SetColumn(BR_TextBox, 1);
                Grid.SetColumnSpan(BR_TextBox, IsCoverMaxLength ? 3 : 2);

                //BR_MaxLength
                Grid.SetRow(BR_MaxLength, 0);
            }
            else
                DisableLabelTop();
        }
        else
            DisableLabelTop();

        if (_multiLine.HasValue && _multiLineHeight.HasValue)
        {
            if (_multiLine.Value)
            {
                Height = Height + _multiLineHeight.Value;
                Txt_Data.VerticalContentAlignment = VerticalAlignment.Top;
                Txt_Data.VerticalScrollBarVisibility = ScrollBarVisibility.Auto;
                Txt_Data.HorizontalScrollBarVisibility = ScrollBarVisibility.Disabled;
                Txt_Data.TextWrapping = TextWrapping.Wrap;
                Txt_Data.Height = Height - GR_Row_Label.Height.Value;
            }
            else
            {
                DisableMultiLine();
            }
        }
        else
        {
            DisableMultiLine();
        }

        IsEnternalEntered = true;
        _Height = Height;
        IsEnternalEntered = false;

        void DisableLabelTop()
        {
            var Height = _HeightEntered;
            Lb_Content.Padding = _labelPadding.HasValue ? _labelPadding.Value : new Thickness(10, 0, 0, 0);
            GR_Row_Label.Height = new GridLength(0);

            //GR_Content
            Grid.SetRow(BR_Label, 1);
            Grid.SetColumnSpan(BR_Label, 1);

            //BR_TextBox
            Grid.SetColumn(BR_TextBox, 2);
            Grid.SetColumnSpan(BR_TextBox, 1);

            //BR_MaxLength
            Grid.SetRow(BR_MaxLength, 1);

            IsEnternalEntered = true;
            _Height = Height;
            IsEnternalEntered = false;
        }

        void DisableMultiLine()
        {
            Txt_Data.ClearValue(HeightProperty);
            Txt_Data.VerticalContentAlignment = VerticalAlignment.Center;
            Txt_Data.VerticalScrollBarVisibility = ScrollBarVisibility.Hidden;
            Txt_Data.HorizontalScrollBarVisibility = ScrollBarVisibility.Hidden;
            Txt_Data.TextWrapping = TextWrapping.NoWrap;
        }
    }

    private void HandleNonLabel()
    {
        var Height = _HeightEntered;
        Lb_Content.Padding = _labelPadding.HasValue ? _labelPadding.Value : new Thickness(0, 0, 0, 0);
        GR_Row_Label.Height = new GridLength(0);

        //GR_Content
        Grid.SetRow(BR_Label, 1);
        Grid.SetColumnSpan(BR_Label, 1);

        //BR_TextBox
        Grid.SetRow(BR_TextBox, 1);
        Grid.SetColumn(BR_TextBox, 2);
        Grid.SetColumnSpan(BR_TextBox, 1);

        //BR_MaxLength
        Grid.SetRow(BR_MaxLength, 1);

        IsEnternalEntered = true;
        _Height = Height;
        IsEnternalEntered = false;

        //DisableMultiLine
        Txt_Data.ClearValue(HeightProperty);
        Txt_Data.VerticalContentAlignment = VerticalAlignment.Center;
        Txt_Data.VerticalScrollBarVisibility = ScrollBarVisibility.Hidden;
        Txt_Data.HorizontalScrollBarVisibility = ScrollBarVisibility.Hidden;
        Txt_Data.TextWrapping = TextWrapping.NoWrap;
    }

    private void HandleBackground()
    {
        var TextColor = XElementHelper.GetColor(Colors.White);

        //Label
        BR_Label.ClearValue(BackgroundProperty);

        if (_elementType == XElementTypes.Floating)
        {
            BR_TextBox.Background = XElementHelper.GetColor(Colors.Transparent);
            return;
        }

        if (_elementType == XElementTypes.Border)
        {
            var LabelBackgroundColor = TextColor;
            if (_labelBackgroundColor != null)
                LabelBackgroundColor = _labelBackgroundColor;
            else if (_backgroundColor != null)
                LabelBackgroundColor = _backgroundColor;

            BR_Label.Background = LabelBackgroundColor;
        }

        //Text Box
        var TextBackgroundColor = TextColor;
        if (_textBoxBackgroundColor != null)
            TextBackgroundColor = _textBoxBackgroundColor;
        else if (_backgroundColor != null)
            TextBackgroundColor = _backgroundColor;

        BR_TextBox.Background = TextBackgroundColor;

        //Max Legnth
        HandleMaxLengthBackground();
    }

    private void HandleMaxLengthBackground()
    {
        var TextBackgroundColor = XElementHelper.GetColor(Colors.White);
        if (_textBoxBackgroundColor != null)
            TextBackgroundColor = _textBoxBackgroundColor;
        else if (_backgroundColor != null)
            TextBackgroundColor = _backgroundColor;

        if (!_maxLengthVisibility)
        {
            BR_MaxLength.Background = XElementHelper.GetColor(Colors.Transparent);
            return;
        }

        if (_elementType == XElementTypes.Floating)
        {
            BR_MaxLength.Background = XElementHelper.GetColor(Colors.Transparent);
            return;
        }

        if (_elementType == XElementTypes.Border)
        {
            if (_maxLengthOutbox.HasValue && _maxLengthOutbox.Value)
            {
                BR_MaxLength.Background = XElementHelper.GetColor(Colors.Transparent);
                return;
            }

            if (_multiLine.HasValue && _multiLine.Value)
            {
                BR_MaxLength.Background = XElementHelper.GetColor(Colors.Transparent);
                return;
            }

            BR_MaxLength.Background = TextBackgroundColor;

            return;
        }



        if (_maxLengthOutbox.HasValue)
            BR_MaxLength.Background = _maxLengthOutbox.Value ? XElementHelper.GetColor(Colors.Transparent) : TextBackgroundColor;
        else
            BR_MaxLength.Background = TextBackgroundColor;
    }

    private void Txt_Data_PreviewTextInput(object sender, TextCompositionEventArgs e)
    {
        var textBox = sender as TextBox;

        if (_inputType == XInputTypes.Int)
        {
            e.Handled = XDataConverter.Regex_Int(e.Text);
            return;
        }

        if (_inputType == XInputTypes.Decimal)
        {
            string proposedText = textBox.Text.Insert(textBox.SelectionStart, e.Text);
            e.Handled = XDataConverter.Regex_Decimal(proposedText, _decimalPoint.Value);
            return;
        }
    }

    private void Txt_Data_PreviewKeyDown(object sender, KeyEventArgs e)
    {
        if (e.Key == Key.Space)
        {
            e.Handled = _inputType == XInputTypes.Int || _inputType == XInputTypes.Decimal;
        }

        if (e.Key == Key.Escape)
        {
            GR_TextBox.Focus();
        }

        //Paste (Ctrl+V)
        if (!_allowPaste.Value)
        {
            if ((Keyboard.Modifiers & ModifierKeys.Control) == ModifierKeys.Control && e.Key == Key.V)
            {
                e.Handled = true;
            }
        }
    }

    private void Txt_Data_Pasting(object sender, DataObjectPastingEventArgs e)
    {
        if (e.DataObject.GetDataPresent(typeof(string)))
        {
            var textBox = sender as TextBox;

            if (!_allowImojiPaste.Value)
            {
                var pastedUnicodeText = e.DataObject.GetData(DataFormats.UnicodeText) as string;
                if (XDataConverter.ContainsEmoji(pastedUnicodeText))
                {
                    e.CancelCommand();
                    return;
                }
            }

            var pastedText = (string)e.DataObject.GetData(typeof(string));
            if (_inputType == XInputTypes.Int)
            {
                if (XDataConverter.Regex_Int(pastedText))
                    e.CancelCommand();
                return;
            }

            if (_inputType == XInputTypes.Decimal)
            {
                var proposedText = textBox.Text.Insert(textBox.SelectionStart, pastedText);
                if (XDataConverter.Regex_Decimal(proposedText, _decimalPoint.Value))
                    e.CancelCommand();
                return;
            }
        }
        else
        {
            e.CancelCommand();
        }
    }

    private void Txt_Data_GotFocus(object sender, RoutedEventArgs e)
    {
        if (_borderBrushFocused.Value)
        {
            _IsFocused = true;
            if (_elementType == XElementTypes.Floating)
            {
                HandleFloatingModeBorder();
                return;
            }

            BR_Label.BorderBrush = _borderBrushColorFocused;
            BR_TextBox.BorderBrush = _borderBrushColorFocused;
            BR_MaxLength.BorderBrush = _borderBrushColorFocused;
        }
    }

    private void Txt_Data_LostFocus(object sender, RoutedEventArgs e)
    {
        if (_borderBrushFocused.Value)
        {
            _IsFocused = false;
            if (_elementType == XElementTypes.Floating)
            {
                HandleFloatingModeBorder();
                return;
            }

            BR_Label.BorderBrush = _borderBrushColor;
            BR_TextBox.BorderBrush = _borderBrushColor;
            BR_MaxLength.BorderBrush = _borderBrushColor;
        }
    }

    private void Txt_Data_TextChanged(object sender, TextChangedEventArgs e)
    {
        if (_TextChanged != null)
        {
            var textBox = sender as TextBox;
            _TextChanged(sender, textBox.Text);
        }
    }
}


public class CharCountWithMaxConverter : IMultiValueConverter
{
    public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
    {
        if (values.Length < 2)
            return "";

        int currentLength = (values[0] as string)?.Length ?? 0;
        int maxLength = values[1] is int i ? i : 0;

        return $"{currentLength} / {maxLength}";
    }

    public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        => throw new NotImplementedException();
}
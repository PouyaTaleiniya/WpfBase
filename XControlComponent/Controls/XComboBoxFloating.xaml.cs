using MaterialDesignThemes.Wpf;
using System.Collections;
using System.ComponentModel;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media;
using XControlComponents.Tools;
using XControlHelper;

namespace XControlComponents.Controls;

/// <summary>
/// Interaction logic for XComboBox.xaml
/// </summary>
public partial class XComboBoxFloating : UserControl
{
    private bool IsEnternalEntered { get; set; }
    private List<object> getAllDataSources { get; set; }
    private bool SearchMode { get; set; }

    //Border Box 
    private double _WidthEntered;

    //Label
    private double _LabelWidthEntered;
    private double _LabelFontSizeEntered;

    //Text Box
    private double _TextFontSizeEntered;
    private double _PlaceHolderFontSizeEntered;

    //Search
    private double _PlaceHolderSearchFontSizeEntered;
    private double _TextSearchFontSizeEntered;

    [EditorBrowsable(EditorBrowsableState.Always)]
    public delegate void _ValueChangedEventHandler(object sender, XComboBoxValueEventArgs Data);
    public event _ValueChangedEventHandler _ValueChanged;

    public XComboBoxFloating()
    {
        InitializeComponent();
        XComboBoxPopup.Tag = Guid.NewGuid().ToString();

        //Border Box
        _Height = XComboBoxDefaults.Height;
        _Width = XComboBoxDefaults.Width;
        _WidthAuto = XComboBoxDefaults.WidthAuto;
        _RequiredVisibility = XComboBoxDefaults.RequiredVisibility;
        _RequiredColor = XComboBoxDefaults.RequiredColor;
        _BorderBrushColor = XComboBoxDefaults.BorderBrushColor;
        _BorderBrushColorFocused = XComboBoxDefaults.BorderBrushColorFloating;
        _BorderBrushFocused = XComboBoxDefaults.BorderBrushFocused;
        _IsGenerateEmptyLabel = XComboBoxDefaults.IsGenerateEmptyLabel;

        //Popup
        _PopupBackgroundColor = XComboBoxDefaults.PopupBackgroundColor;
        _PopupBackgroundColorFocused = XComboBoxDefaults.PopupBackgroundColorFocused;
        _PopupBorderBrushColor = XComboBoxDefaults.PopupBorderBrushColor;
        _PopupTextColor = XComboBoxDefaults.PopupTextColor;
        _popupTextFontSize = XComboBoxDefaults.PopupTextFontSize;

        //Label
        _LabelVisibility = true;
        _LabelTitle = XComboBoxDefaults.LabelTitle;
        _LabelFontSize = XComboBoxDefaults.LabelFontSize;
        _LabelOpacity = XComboBoxDefaults.LabelOpacity;
        _LabelColor = XComboBoxDefaults.LabelColor;
        _LabelGridWidth = XComboBoxDefaults.LabelGridWidth;
        _LabelPadding = XComboBoxDefaults.LabelPadding;

        //Text
        _TextFontSize = XComboBoxDefaults.TextFontSize;
        _TextColor = XComboBoxDefaults.TextColor;
        _TextOpacity = XComboBoxDefaults.TextOpacity;
        _TextPadding = XComboBoxDefaults.TextPadding;

        //Place Holder
        _PlaceHolderFontSize = XComboBoxDefaults.PlaceHolderFontSize;
        _PlaceHolderOpacity = XComboBoxDefaults.PlaceHolderOpacity;
        _PlaceHolder = XComboBoxDefaults.PlaceHolder;

        //Search
        _TextSearchFontSize = XComboBoxDefaults.TextSearchFontSize;
        _TextSearchColor = XComboBoxDefaults.TextSearchColor;
        _TextSearchOpacity = XComboBoxDefaults.TextSearchOpacity;
        _TextSearchPadding = XComboBoxDefaults.TextSearchPadding;
        _BorderBrushSearchColor = XComboBoxDefaults.BorderBrushSearchColor;
        _BorderBrushSearchColorFocused = XComboBoxDefaults.BorderBrushSearchColorFocused;
        _BorderBrushSearchFocused = XComboBoxDefaults.BorderBrushSearchFocused;
        _IsEnableSearch = XComboBoxDefaults.IsEnableSearch;
        _EqualSearchMode = XComboBoxDefaults.EqualSearchMode;
        _CaseSensitiveSearchMode = XComboBoxDefaults.CaseSensitiveSearchMode;
        _PlaceHolderSearchFontSize = XComboBoxDefaults.PlaceHolderSearchFontSize;
        _PlaceHolderSearchOpacity = XComboBoxDefaults.PlaceHolderSearchOpacity;
        _PlaceHolderSearchText = XComboBoxDefaults.PlaceHolderSearchText;

        //Value
        _items = new List<string>();
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
                _height = XComboBoxDefaults.Height;

            if (_height < XComboBoxDefaults.Height)
                _height = XComboBoxDefaults.Height;

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
                _width = XComboBoxDefaults.Width;

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
                _FontSize = XComboBoxDefaults.FontSize;

            if (_FontSize < 10)
                _FontSize = 10;

            IsEnternalEntered = true;
            _LabelFontSize = _FontSize == XComboBoxDefaults.FontSize ? _LabelFontSizeEntered : _FontSize;
            _TextFontSize = _FontSize == XComboBoxDefaults.FontSize ? _TextFontSizeEntered : _FontSize;
            _PlaceHolderFontSize = _FontSize == XComboBoxDefaults.FontSize ? _PlaceHolderFontSizeEntered : _FontSize;
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
                _requiredVisibility = XComboBoxDefaults.RequiredVisibility;

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
                _requiredColor = XComboBoxDefaults.RequiredColor;

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
                _borderBrushFocused = XComboBoxDefaults.BorderBrushFocused;

            if (_borderBrushFocused.Value)
                TextFieldAssist.SetUnderlineBrush(Txt_Data, _borderBrushColorFocused);
            else
                TextFieldAssist.SetUnderlineBrush(Txt_Data, XElementHelper.GetColor(Colors.Transparent));
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
                _borderBrushColor = XComboBoxDefaults.BorderBrushColor;
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
                _borderBrushColorFocused = XComboBoxDefaults.BorderBrushColorFocused;

            var getColorTransparent = XElementHelper.GetColor(Colors.Transparent);
            if (_borderBrushColorFocused.FillStringSafe() == getColorTransparent.FillStringSafe())
                _borderBrushColorFocused = _borderBrushColor;

            TextFieldAssist.SetUnderlineBrush(Txt_Data, XAppMethods.Color_Blue_2196f3());
        }
    }

    private bool? _isGenerateEmptyLabel;
    public bool? _IsGenerateEmptyLabel
    {
        get => _isGenerateEmptyLabel;
        set
        {
            _isGenerateEmptyLabel = value;
            if (_isGenerateEmptyLabel == null)
                _isGenerateEmptyLabel = XComboBoxDefaults.IsGenerateEmptyLabel;

            if (_dataSource != null)
            {
                _DataSource = _dataSource;
                return;
            }

            if (_items != null && _items.Count() > 0)
            {
                _Items = _items;
                return;
            }
        }
    }
    #endregion

    #region Popup
    private Brush _popupBackgroundColor;
    public Brush _PopupBackgroundColor
    {
        get => _popupBackgroundColor;
        set
        {
            _popupBackgroundColor = value;
            if (_popupBackgroundColor == null)
                _popupBackgroundColor = XComboBoxDefaults.PopupBackgroundColor;

            BR_Popup.Background = _popupBackgroundColor;
        }
    }

    private Brush _popupBackgroundColorFocused;
    public Brush _PopupBackgroundColorFocused
    {
        get => _popupBackgroundColorFocused;
        set
        {
            _popupBackgroundColorFocused = value;
            if (_popupBackgroundColorFocused == null)
                _popupBackgroundColorFocused = XComboBoxDefaults.PopupBackgroundColorFocused;
        }
    }

    private Brush _popupBorderBrushColor;
    public Brush _PopupBorderBrushColor
    {
        get => _popupBorderBrushColor;
        set
        {
            _popupBorderBrushColor = value;
            if (_popupBorderBrushColor == null)
                _popupBorderBrushColor = XComboBoxDefaults.PopupBorderBrushColor;

            BR_Popup.BorderBrush = _popupBorderBrushColor;
        }
    }

    private Brush _popupTextColor;
    public Brush _PopupTextColor
    {
        get => _popupTextColor;
        set
        {
            _popupTextColor = value;
            if (_popupTextColor == null)
                _popupTextColor = XComboBoxDefaults.PopupTextColor;

            if (_dataSource != null || (_items != null && _items.Count() > 0))
            {
                var getAllLabels = XElementHelper.FindChilds<Label>(GR_Popup);
                if (getAllLabels.Count() > 0)
                {
                    getAllLabels.ForEach(item =>
                    {
                        ((Label)item).Foreground = _popupTextColor;
                    });
                }
            }
        }
    }

    private double? _popupTextFontSize;
    public double? _PopupTextFontSize
    {
        get => _popupTextFontSize;
        set
        {
            _popupTextFontSize = value;
            if (_popupTextFontSize == null)
                _popupTextFontSize = XComboBoxDefaults.PopupTextFontSize;

            if (_dataSource != null || (_items != null && _items.Count() > 0))
            {
                var getAllLabels = XElementHelper.FindChilds<Label>(GR_Popup);
                if (getAllLabels.Count() > 0)
                {
                    getAllLabels.ForEach(item =>
                    {
                        ((Label)item).FontSize = _popupTextFontSize.Value;
                    });
                }
            }
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
                _labelGridWidth = XComboBoxDefaults.LabelGridWidth;

            if (_labelGridWidth > 0 && _labelGridWidth < 60)
                _labelGridWidth = 60;

            if (_labelGridWidth > 0)
                _LabelWidthEntered = _labelGridWidth.Value;

            //Label Content
            if (_labelGridWidth > 0 && _labelTitle.IsNullOrEmpty())
                _LabelTitle = XComboBoxDefaults.LabelTitle;

            GR_Col_Lable.Width = new GridLength(_labelGridWidth.Value, GridUnitType.Pixel);

            if (_labelGridWidth == 0 && Txt_Data.Padding.Left == 0)
            {
                Txt_Data.Padding = new Thickness(10, 0, 5, 0);
            }
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
                _labelFontSize = XComboBoxDefaults.LabelFontSize;

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
                _labelOpacity = XComboBoxDefaults.LabelOpacity;

            if (_labelOpacity < 0.1)
                _labelOpacity = 0.1;

            Lb_Content.Opacity = _labelOpacity.Value;
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
                if (_LabelWidthEntered > 0)
                    _LabelGridWidth = _LabelWidthEntered;
                else
                    _LabelGridWidth = XComboBoxDefaults.LabelGridWidth;
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
                _labelTitle = XComboBoxDefaults.LabelTitle;
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
                _labelColor = XComboBoxDefaults.LabelColor;
            Lb_Content.Foreground = _labelColor;
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
                _labelPadding = XComboBoxDefaults.LabelPadding;

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
                _widthAuto = XComboBoxDefaults.WidthAuto;

            if (_widthAuto.Value)
                GR_TextBox.ClearValue(WidthProperty);
            else
            {
                if (_WidthEntered > 0)
                    _Width = _WidthEntered;
                else
                    _WidthEntered = XComboBoxDefaults.Width;
            }
        }
    }
    #endregion

    #region TextBox
    private double? _textFontSize;
    public double? _TextFontSize
    {
        get => _textFontSize;
        set
        {
            _textFontSize = value;

            if (_textFontSize == null)
                _textFontSize = XComboBoxDefaults.TextFontSize;

            if (_textFontSize < 10)
                _textFontSize = 10;

            if (!IsEnternalEntered)
                _TextFontSizeEntered = _textFontSize.Value;

            Txt_Data.FontSize = _textFontSize.Value;
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
                _textOpacity = XComboBoxDefaults.TextOpacity;

            if (_textOpacity < 0.1)
                _textOpacity = 0.1;

            Txt_Data.Opacity = _textOpacity.Value;
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
                _textColor = XComboBoxDefaults.TextColor;
            Txt_Data.Foreground = _textColor;
        }
    }

    private Brush _textBackgroundColor;
    public Brush _TextBoxBackgroundColor
    {
        get => _textBackgroundColor;
        set
        {
            _textBackgroundColor = value;
            if (_textBackgroundColor == null)
            {
                GR_Data.ClearValue(BackgroundProperty);
            }
            else
            {
                GR_Data.Background = _textBackgroundColor;
            }
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
                _textPadding = XComboBoxDefaults.TextPadding;

            Txt_Data.Padding = _textPadding.Value;
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
                _placeHolderOpacity = XComboBoxDefaults.PlaceHolderOpacity;

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
                _placeHolderFontSize = XComboBoxDefaults.PlaceHolderFontSize;

            if (_placeHolderFontSize < 10)
                _placeHolderFontSize = 10;

            if (!IsEnternalEntered)
                _PlaceHolderFontSizeEntered = _placeHolderFontSize.Value;

            Lb_PlaceHolder.FontSize = _placeHolderFontSize.Value;
        }
    }
    #endregion

    #region Search
    private double? _textSearchFontSize;
    public double? _TextSearchFontSize
    {
        get => _textSearchFontSize;
        set
        {
            _textSearchFontSize = value;

            if (_textSearchFontSize == null)
                _textSearchFontSize = XComboBoxDefaults.TextSearchFontSize;

            if (_textSearchFontSize < 10)
                _textSearchFontSize = 10;

            if (!IsEnternalEntered)
                _TextSearchFontSizeEntered = _textSearchFontSize.Value;

            Txt_Data.FontSize = _textSearchFontSize.Value;
        }
    }

    private double? _textSearchOpacity;
    public double? _TextSearchOpacity
    {
        get => _textSearchOpacity;
        set
        {
            _textSearchOpacity = value;
            if (_textSearchOpacity == null)
                _textSearchOpacity = XComboBoxDefaults.TextSearchOpacity;

            if (_textSearchOpacity < 0.1)
                _textSearchOpacity = 0.1;

            Txt_Data.Opacity = _textSearchOpacity.Value;
        }
    }

    private Brush _textSearchColor;
    public Brush _TextSearchColor
    {
        get => _textSearchColor;
        set
        {
            _textSearchColor = value;
            if (_textSearchColor == null)
                _textSearchColor = XComboBoxDefaults.TextSearchColor;
            Txt_Data.Foreground = _textSearchColor;
        }
    }

    private Thickness? _textSearchPadding;
    public Thickness? _TextSearchPadding
    {
        get => _textSearchPadding;
        set
        {
            _textSearchPadding = value;
            if (_textSearchPadding == null)
                _textSearchPadding = XComboBoxDefaults.TextSearchPadding;

            Txt_Data.Padding = _textSearchPadding.Value;
        }
    }

    private double? _fontSearchSize;
    public double? _FontSearchSize
    {
        get => _fontSearchSize;
        set
        {
            _fontSearchSize = value;

            if (_fontSearchSize == null)
                _FontSearchSize = XComboBoxDefaults.FontSearchSize;

            if (_FontSearchSize < 10)
                _FontSearchSize = 10;

            IsEnternalEntered = true;
            _TextSearchFontSize = _FontSearchSize == XComboBoxDefaults.FontSearchSize ? _TextSearchFontSizeEntered : _FontSearchSize;
            _PlaceHolderSearchFontSize = _FontSearchSize == XComboBoxDefaults.FontSearchSize ? _PlaceHolderSearchFontSizeEntered : _FontSearchSize;
            IsEnternalEntered = false;
        }
    }

    private bool? _borderBrushSearchFocused;
    public bool? _BorderBrushSearchFocused
    {
        get => _borderBrushSearchFocused;
        set
        {
            _borderBrushSearchFocused = value;
            if (_borderBrushSearchFocused == null)
                _borderBrushSearchFocused = XComboBoxDefaults.BorderBrushSearchFocused;

            if (_borderBrushSearchFocused.Value)
                TextFieldAssist.SetUnderlineBrush(Txt_Data_Search, _borderBrushSearchColorFocused);
            else
                TextFieldAssist.SetUnderlineBrush(Txt_Data_Search, XElementHelper.GetColor(Colors.Transparent));
        }
    }

    private bool? _isEnableSearch;
    public bool? _IsEnableSearch
    {
        get => _isEnableSearch;
        set
        {
            _isEnableSearch = value;
            if (_isEnableSearch == null)
                _isEnableSearch = XComboBoxDefaults.IsEnableSearch;

            HandleSearch();
        }
    }

    private bool? _equalSearchMode;
    public bool? _EqualSearchMode
    {
        get => _equalSearchMode;
        set
        {
            _equalSearchMode = value;
            if (_equalSearchMode == null)
                _equalSearchMode = XComboBoxDefaults.EqualSearchMode;
        }
    }

    private bool? _caseSensitiveSearchMode;
    public bool? _CaseSensitiveSearchMode
    {
        get => _caseSensitiveSearchMode;
        set
        {
            _caseSensitiveSearchMode = value;
            if (_caseSensitiveSearchMode == null)
                _caseSensitiveSearchMode = XComboBoxDefaults.CaseSensitiveSearchMode;
        }
    }

    private Brush _borderBrushSearchColor;
    public Brush _BorderBrushSearchColor
    {
        get => _borderBrushSearchColor;
        set
        {
            _borderBrushSearchColor = value;
            if (_borderBrushSearchColor == null)
                _borderBrushSearchColor = XComboBoxDefaults.BorderBrushSearchColor;
            BR_TextBox_Search.BorderBrush = _borderBrushSearchColor;
        }
    }

    private Brush _borderBrushSearchColorFocused;
    public Brush _BorderBrushSearchColorFocused
    {
        get => _borderBrushSearchColorFocused;
        set
        {
            _borderBrushSearchColorFocused = value;

            if (_borderBrushSearchColorFocused == null)
                _borderBrushSearchColorFocused = XComboBoxDefaults.BorderBrushSearchColorFocused;

            var getColorTransparent = XElementHelper.GetColor(Colors.Transparent);
            if (_borderBrushSearchColorFocused.FillStringSafe() == getColorTransparent.FillStringSafe())
                _borderBrushSearchColorFocused = _borderBrushSearchColor;

            TextFieldAssist.SetUnderlineBrush(Txt_Data_Search, _borderBrushSearchColorFocused);
        }
    }

    private double? _placeHolderSearchOpacity;
    public double? _PlaceHolderSearchOpacity
    {
        get => _placeHolderSearchOpacity;
        set
        {
            _placeHolderSearchOpacity = value;
            if (_placeHolderSearchOpacity == null)
                _placeHolderSearchOpacity = XComboBoxDefaults.PlaceHolderSearchOpacity;

            if (_placeHolderSearchOpacity < 0.1)
                _placeHolderSearchOpacity = 0.1;

            Lb_PlaceHolder_Search.Opacity = _placeHolderSearchOpacity.Value;
        }
    }

    private string _placeHolderSearchText;
    public string _PlaceHolderSearchText
    {
        get => _placeHolderSearchText;
        set
        {
            _placeHolderSearchText = value;
            Lb_PlaceHolder_Search.Content = _PlaceHolderSearchText;
        }
    }

    private double? _placeHolderSearchFontSize;
    public double? _PlaceHolderSearchFontSize
    {
        get => _placeHolderSearchFontSize;
        set
        {
            _placeHolderSearchFontSize = value;

            if (_placeHolderSearchFontSize == null)
                _placeHolderSearchFontSize = XComboBoxDefaults.PlaceHolderSearchFontSize;

            if (_placeHolderSearchFontSize < 10)
                _placeHolderSearchFontSize = 10;

            if (!IsEnternalEntered)
                _PlaceHolderSearchFontSizeEntered = _placeHolderSearchFontSize.Value;

            Lb_PlaceHolder_Search.FontSize = _placeHolderSearchFontSize.Value;
        }
    }
    #endregion

    #region Values  
    private bool? _triggerValueChanged;
    public bool? _TriggerValueChanged
    {
        get => _triggerValueChanged;
        set
        {
            if (_triggerValueChanged == null)
                _triggerValueChanged = false;
            _triggerValueChanged = XComboBoxDefaults.TriggerValueChanged;
        }
    }

    public object Value
    {
        get => Txt_Data.Tag.FillStringSafe();
        set
        {
            if (_dataSource != null)
            {
                getAllDataSources.ForEach(item =>
                {
                    var getTypes = item.GetType();
                    var getFields = getTypes.GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);

                    //key Field
                    var keyFieldName = $"<{_keyField}>k__BackingField";
                    var getkeyField = getFields.FirstOrDefault(a => a.Name == keyFieldName);
                    var getkeyFieldValue = getkeyField.GetValue(item);

                    //Display Field
                    var keyDisplayField = $"<{_displayField}>k__BackingField";
                    var getDisplayField = getFields.FirstOrDefault(a => a.Name == keyDisplayField);
                    var getDisplayFieldValue = getDisplayField.GetValue(item);

                    if (getkeyField.FillStringSafe() == value.FillStringSafe())
                    {
                        SetValueData(getkeyFieldValue, getDisplayFieldValue, _triggerValueChanged.Value);
                    }
                });
                return;
            }

            if (_items != null && _items.Count() > 0)
            {
                var getItem = _items.FirstOrDefault(a => a == value.FillStringSafe());
                if (getItem != null)
                    SetValueData(getItem, getItem, _triggerValueChanged.Value);
                return;
            }
        }
    }

    private object _dataSourceSelected;
    public object _DataSourceSelected
    {
        get
        {
            _dataSourceSelected = null;
            if (_dataSource != null)
            {
                getAllDataSources.ForEach(item =>
                {
                    if (_dataSourceSelected == null)
                    {
                        var getTypes = item.GetType();
                        var getFields = getTypes.GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);

                        //key Field
                        var keyFieldName = $"<{_keyField}>k__BackingField";
                        var getkeyField = getFields.FirstOrDefault(a => a.Name == keyFieldName);
                        var getkeyFieldValue = getkeyField.GetValue(item);

                        if (getkeyFieldValue.FillStringSafe() == getkeyFieldValue.FillStringSafe())
                            _dataSourceSelected = item;
                    }
                });

                return _dataSourceSelected;
            }

            if (_items != null && _items.Count() > 0)
            {
                _items.ForEach(item =>
                {
                    if (_dataSourceSelected == null)
                    {
                        if (item.FillStringSafe() == Value.FillStringSafe())
                        {
                            _dataSourceSelected = item;
                        }
                    }
                });
            }

            return _dataSourceSelected;
        }
    }
    #endregion

    #region Data Sources
    private string _keyField;
    public string _KeyField
    {
        set => _keyField = value;
    }

    private string _displayField;
    public string _DisplayField
    {
        get => _displayField;
        set => _displayField = value;
    }

    private object _dataSource;
    public object _DataSource
    {
        set
        {
            _dataSource = value;
            getAllDataSources = null;

            //Clear Grid Content
            GR_Popup.Children.Clear();
            GR_Popup.RowDefinitions.Clear();

            //Generate Empty Label
            if (_IsGenerateEmptyLabel.Value && !SearchMode)
                GenerateGridDataPanel(string.Empty, string.Empty, 0);

            if (_dataSource != null && !_keyField.IsNullOrEmpty() && !_displayField.IsNullOrEmpty())
            {
                var IndexRow = _isGenerateEmptyLabel.Value && !SearchMode ? 1 : 0;
                getAllDataSources = ((IEnumerable)_dataSource).Cast<object>().ToList();
                getAllDataSources.ForEach(item =>
                {
                    var getTypes = item.GetType();
                    var getFields = getTypes.GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);

                    //key Field
                    var keyFieldName = $"<{_keyField}>k__BackingField";
                    var getkeyField = getFields.FirstOrDefault(a => a.Name == keyFieldName);
                    var getkeyFieldValue = getkeyField.GetValue(item);

                    //Display Field
                    var keyDisplayField = $"<{_displayField}>k__BackingField";
                    var getDisplayField = getFields.FirstOrDefault(a => a.Name == keyDisplayField);
                    var getDisplayFieldValue = getDisplayField.GetValue(item);

                    if (getkeyField != null && getDisplayField != null)
                    {
                        if (IsAllowedGenerateLabel(getDisplayFieldValue))
                        {
                            GenerateGridDataPanel(getkeyFieldValue.FillStringSafe(), getDisplayFieldValue.FillStringSafe(), IndexRow);
                            IndexRow++;
                        }
                    }
                });

                HandleSearch();
            }
        }
    }

    private List<string> _items;
    public List<string> _Items
    {
        get => _items;
        set
        {
            _items = value;
            if (_items == null)
                _items = new List<string>();

            //Clear Grid Content
            GR_Popup.Children.Clear();
            GR_Popup.RowDefinitions.Clear();

            //Generate Empty Label
            if (_IsGenerateEmptyLabel.Value && !SearchMode)
                GenerateGridDataPanel(string.Empty, string.Empty, 0);

            if (_items.Count() > 0)
            {
                var IndexRow = _isGenerateEmptyLabel.Value && !SearchMode ? 1 : 0;
                _items.ForEach(item =>
                {
                    if (IsAllowedGenerateLabel(item))
                    {
                        GenerateGridDataPanel(item, item, IndexRow);
                        IndexRow++;
                    }
                });
            }

            HandleSearch();
        }
    }
    #endregion

    private void UserControl_Loaded(object sender, RoutedEventArgs e)
    {
        var getWindow = Window.GetWindow(this);
        if (getWindow != null)
        {
            getWindow.MouseRightButtonDown += (s, k) =>
            {
                if (XComboBoxPopup.IsOpen)
                    ClosePopup(XComboBoxPopup);
            };

            getWindow.MouseLeftButtonDown += (s, k) =>
            {
                if (XComboBoxPopup.IsOpen)
                {
                    var getClickedElement = k.OriginalSource as FrameworkElement;

                    var getBorder = XElementHelper.FindParentByName<Border>(getClickedElement, "BR_AngleDown");
                    if (getBorder != null)
                        return;

                    var getGrid = XElementHelper.FindParentByName<Grid>(getClickedElement, "GR_Data");
                    if (getGrid != null)
                        return;

                    if (XComboBoxPopup.IsOpen)
                        ClosePopup(XComboBoxPopup);
                }
            };
        }
    }

    private void Txt_Data_PreviewKeyDown(object sender, KeyEventArgs e)
    {
        //Paste (Ctrl+V)
        if ((Keyboard.Modifiers & ModifierKeys.Control) == ModifierKeys.Control && e.Key == Key.V)
        {
            e.Handled = true;
        }
    }

    private void GR_Data_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
    {
        HandlePopup();
    }

    private void Txt_Data_PreviewMouseDown(object sender, MouseButtonEventArgs e)
    {
        HandlePopup();
    }

    private void HandleSearch()
    {
        var IsDataSource = (_dataSource != null && getAllDataSources.Count() > 0) || (_items != null && _items.Count() > 0);
        GR_Row_Search.Height = new GridLength(_isEnableSearch.Value && IsDataSource ? 50 : 0, GridUnitType.Pixel);
    }

    private void HandlePopup()
    {
        if (XComboBoxPopup.IsOpen)
        {
            ClosePopup(XComboBoxPopup);
        }
        else
        {
            if (XPopupManager.XComboBoxPopupOpened != null && XPopupManager.XComboBoxPopupOpened.Tag != XComboBoxPopup.Tag)
                ClosePopup(XPopupManager.XComboBoxPopupOpened);

            XComboBoxPopup.Width = GR_Data.ActualWidth;
            XComboBoxPopup.IsOpen = true;
            XPopupManager.XComboBoxPopupOpened = XComboBoxPopup;
        }
    }

    private void ClosePopup(Popup xComboBoxPopup)
    {
        XPopupManager.ClosePopupByTag(xComboBoxPopup.Tag.ToString());
        BR_AngleDown.BorderBrush = _borderBrushColor;
        BR_AngleDown.BorderThickness = new Thickness(0, 0, 0, 1);
        XPopupManager.XComboBoxPopupOpened = null;
    }

    private async void XComboBoxPopup_Opened(object sender, EventArgs e)
    {
        if (_BorderBrushFocused.Value)
        {
            BR_TextBox.BorderBrush = _borderBrushColorFocused;

            await Task.Delay(130);
            Txt_Data.Focus();

            await Task.Delay(130);
            BR_AngleDown.BorderBrush = _borderBrushColorFocused;
            BR_AngleDown.BorderThickness = new Thickness(0, 0, 0, 2.5);
        }

        XPopupManager.RegisterPopup((Popup)sender);
    }

    private void XComboBoxPopup_Closed(object sender, EventArgs e)
    {

        if (_BorderBrushFocused.Value)
        {
            BR_TextBox.BorderBrush = _borderBrushColor;
            BR_AngleDown.BorderBrush = _borderBrushColor;
            BR_AngleDown.BorderThickness = new Thickness(0, 0, 0, 1);
        }
        GR_TextBox.Focus();

        if (_isEnableSearch.Value)
        {
            SearchMode = false;
            Txt_Data_Search.Text = string.Empty;

            if (_dataSource != null)
            {
                _DataSource = _dataSource;
                return;
            }

            if (_items != null && _items.Count() > 0)
            {
                _Items = _items;
                return;
            }
        }

    }

    #region GridData
    private void GenerateGridDataPanel(string KeyField, string DisplayField, int IndexRow)
    {
        GR_Popup.RowDefinitions.Add(new RowDefinition
        {
            Height = new GridLength(30)
        });

        var innerGrid = new Grid
        {
            Cursor = Cursors.Hand,
            Background = Brushes.Transparent
        };

        // رویدادها
        innerGrid.MouseEnter += GridData_MouseEnter;
        innerGrid.MouseLeave += GridData_MouseLeave;
        innerGrid.MouseLeftButtonDown += GridData_MouseLeftButtonDown;

        // ساخت لیبل
        var lbl = new Label
        {
            Content = DisplayField,
            Tag = KeyField,
            Padding = new Thickness(10, 0, 0, 0),
            VerticalAlignment = VerticalAlignment.Center,
            Foreground = _popupTextColor,
            FontSize = _popupTextFontSize.Value
        };

        // اضافه کردن لیبل به گرید
        innerGrid.Children.Add(lbl);

        // تعیین سطر
        Grid.SetRow(innerGrid, IndexRow);

        // اضافه کردن به گرید والد
        GR_Popup.Children.Add(innerGrid);
    }

    private void GridData_MouseEnter(object sender, MouseEventArgs e)
    {
        var getCurrentGrid = sender as Grid;
        getCurrentGrid.Background = _PopupBackgroundColorFocused;
    }

    private void GridData_MouseLeave(object sender, MouseEventArgs e)
    {
        var getCurrentGrid = sender as Grid;
        getCurrentGrid.ClearValue(BackgroundProperty);
    }

    private void GridData_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
    {
        var getElm = sender as Grid;
        var getLabel = XElementHelper.FindChild<Label>(getElm);
        SetValueData(getLabel.Tag.FillStringSafe(), getLabel.Content.FillStringSafe(), true);
    }

    private void SetValueData(object KeyField, object DisplayField, bool IsTriggerChanged)
    {
        Txt_Data.Text = DisplayField.FillStringSafe();
        Txt_Data.Tag = KeyField;
        ClosePopup(XComboBoxPopup);

        if (_ValueChanged != null && IsTriggerChanged)
        {
            _ValueChanged(this, new XComboBoxValueEventArgs
            {
                Content = Txt_Data.Text.FillStringSafe(),
                KeyField = Txt_Data.Tag.FillStringSafe(),
                DataSource = _DataSourceSelected
            });
        }
    }
    #endregion

    #region Search
    private void Txt_Data_Search_GotFocus(object sender, RoutedEventArgs e)
    {
        BR_AngleDown.BorderBrush = _borderBrushColorFocused;
        BR_AngleDown.BorderThickness = new Thickness(0, 0, 0, 1);
    }

    private void Txt_Data_Search_TextChanged(object sender, TextChangedEventArgs e)
    {
        try
        {
            if (!_isEnableSearch.Value || !XComboBoxPopup.IsOpen)
                return;

            var getElm = sender as TextBox;
            var getText = getElm.Text.FillStringSafe();
            SearchMode = !getText.IsNullOrEmpty();

            if (_dataSource != null)
            {
                _DataSource = _dataSource;
                return;
            }

            if (_items != null && _items.Count() > 0)
            {
                _Items = _items;
                return;
            }
        }
        catch (Exception)
        {

        }
    }

    private bool IsAllowedGenerateLabel(object DisplayField)
    {
        if (!SearchMode)
            return true;

        if (_equalSearchMode.Value)
        {
            if (_caseSensitiveSearchMode.Value)
                return DisplayField.FillStringSafe() == Txt_Data_Search.Text.FillStringSafe();
            else
                return DisplayField.FillStringSafe().ToLower() == Txt_Data_Search.Text.FillStringSafe().ToLower();
        }

        if (_caseSensitiveSearchMode.Value)
            return DisplayField.FillStringSafe().Contains(Txt_Data_Search.Text.FillStringSafe());
        else
            return DisplayField.FillStringSafe().ToLower().Contains(Txt_Data_Search.Text.FillStringSafe().ToLower());
    }
    #endregion 
}

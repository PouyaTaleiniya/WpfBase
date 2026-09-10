using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using XControlHelper;

namespace XControlComponents.Controls;

public partial class XCheckBox : UserControl
{
    private bool IsEnternalEntered { get; set; }

    //Label
    private double _LabelWidthEntered;
    private double _LabelFontSizeEntered;

    //Text Box
    private double _TextFontSizeEntered;
    private double _PlaceHolderFontSizeEntered;

    [EditorBrowsable(EditorBrowsableState.Always)]
    public delegate void _CheckedEventHandler(object sender, bool isChecked);
    public event _CheckedEventHandler _CheckedChanged;

    public XCheckBox()
    {
        InitializeComponent();

        //Label
        _LabelVisibility = true;
        _LabelTitle = XCheckBoxDefault.LabelTitle;
        _LabelFontSize = XCheckBoxDefault.LabelFontSize;
        _LabelOpacity = XCheckBoxDefault.LabelOpacity;
        _LabelColor = XCheckBoxDefault.LabelColor;

        //CheckBox
        _TriggerCheckedChanged = XCheckBoxDefault.TriggerCheckedChanged;
        _CheckBoxColor = XCheckBoxDefault.CheckBoxColor;
    }

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
                _labelGridWidth = XCheckBoxDefault.LabelGridWidth;

            if (_labelGridWidth > 0 && _labelGridWidth < 60)
                _labelGridWidth = 60;

            if (_labelGridWidth > 0)
                _LabelWidthEntered = _labelGridWidth.Value;

            //Label Content
            if (_labelGridWidth > 0 && _labelTitle.IsNullOrEmpty())
                _LabelTitle = XCheckBoxDefault.LabelTitle;

            GR_Col_Lable.Width = new GridLength(_labelGridWidth.Value, GridUnitType.Pixel);
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
                _labelFontSize = XCheckBoxDefault.LabelFontSize;

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
                _labelOpacity = XCheckBoxDefault.LabelOpacity;

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
                    _LabelGridWidth = XCheckBoxDefault.LabelGridWidth;
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
                _labelTitle = XCheckBoxDefault.LabelTitle;
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
                _labelColor = XCheckBoxDefault.LabelColor;
            Lb_Content.Foreground = _labelColor;
        }
    }
    #endregion

    #region CheckBox
    private bool? _triggerCheckedChanged;
    public bool? _TriggerCheckedChanged
    {
        get => _triggerCheckedChanged;
        set
        {
            _triggerCheckedChanged = value;
            if (_triggerCheckedChanged == null)
                _triggerCheckedChanged = XCheckBoxDefault.TriggerCheckedChanged;
        }
    }

    private Brush _checkBoxColor;
    public Brush _CheckBoxColor
    {
        get => _checkBoxColor;
        set
        {
            _checkBoxColor = value;
            if (_checkBoxColor == null)
                _checkBoxColor = XCheckBoxDefault.CheckBoxColor;
            Ch_Data.Background = _checkBoxColor;
        }
    }
    #endregion

    #region Values
    public bool Value
    {
        get => Ch_Data.IsChecked.Value;
        set
        {
            if (!_triggerCheckedChanged.Value && _CheckedChanged != null)
            {
                Ch_Data.Checked -= CheckBox_Checked;
                Ch_Data.Unchecked -= CheckBox_Unchecked;
            }

            Ch_Data.IsChecked = value;

            if (!_triggerCheckedChanged.Value && _CheckedChanged != null)
            {
                Ch_Data.Checked += CheckBox_Checked;
                Ch_Data.Unchecked += CheckBox_Unchecked;
            }
        }
    }
    #endregion

    private void CheckBox_Checked(object sender, RoutedEventArgs e)
    {
        if (_CheckedChanged != null)
        {
            _CheckedChanged(sender, true);
        }
    }

    private void CheckBox_Unchecked(object sender, RoutedEventArgs e)
    {
        if (_CheckedChanged != null)
        {
            _CheckedChanged(sender, false);
        }
    }
}

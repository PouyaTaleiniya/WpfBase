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

namespace XControlComponents.Controls.Data
{
    public abstract class XComboBoxDataControl : UserControl
    {
        protected List<object> getAllDataSources { get; set; }
        protected abstract Grid gR_Popup { get; }
        protected abstract Border bR_Popup { get; }
        protected abstract Border bR_TextBox_Search { get; }
        protected abstract RowDefinition gR_Row_Search { get; }
        protected abstract TextBox txt_Data { get; }
        protected abstract TextBox txt_Data_Search { get; }
        protected abstract Label lb_PlaceHolder_Search { get; }
        protected abstract Popup xComboBoxPopup { get; }

        protected abstract bool IsEnternalEntered { get; set; }
        protected bool SearchMode { get; set; }

        //Search
        private double _PlaceHolderSearchFontSizeEntered;
        private double _TextSearchFontSizeEntered;


        [EditorBrowsable(EditorBrowsableState.Always)]
        public delegate void _ValueChangedEventHandler(object sender, XComboBoxValueEventArgs Data);
        public event _ValueChangedEventHandler _ValueChanged;

        public XComboBoxDataControl()
        {

            _IsGenerateEmptyLabel = XComboBoxDefaults.IsGenerateEmptyLabel;

            //Popup
            _PopupBackgroundColor = XComboBoxDefaults.PopupBackgroundColor;
            _PopupBackgroundColorFocused = XComboBoxDefaults.PopupBackgroundColorFocused;
            _PopupBorderBrushColor = XComboBoxDefaults.PopupBorderBrushColor;
            _PopupTextColor = XComboBoxDefaults.PopupTextColor;
            _popupTextFontSize = XComboBoxDefaults.PopupTextFontSize;

            //Search
            _EqualSearchMode = XComboBoxDefaults.EqualSearchMode;
            _CaseSensitiveSearchMode = XComboBoxDefaults.CaseSensitiveSearchMode;
            _IsEnableSearch = XComboBoxDefaults.IsEnableSearch;

            //Value
            _items = new List<string>();

            Loaded += XComboBoxDataControl_Loaded;
        }

        private void XComboBoxDataControl_Loaded(object sender, RoutedEventArgs e)
        {

        }

        #region Border Box
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

                if (bR_Popup != null)
                    bR_Popup.Background = _popupBackgroundColor;
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

                bR_Popup.BorderBrush = _popupBorderBrushColor;
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
                    var getAllLabels = XElementHelper.FindChilds<Label>(gR_Popup);
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
                    var getAllLabels = XElementHelper.FindChilds<Label>(gR_Popup);
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

                txt_Data_Search.FontSize = _textSearchFontSize.Value;
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

                txt_Data_Search.Opacity = _textSearchOpacity.Value;
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
                txt_Data_Search.Foreground = _textSearchColor;
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

                txt_Data_Search.Padding = _textSearchPadding.Value;
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
                    TextFieldAssist.SetUnderlineBrush(txt_Data_Search, _borderBrushSearchColorFocused);
                else
                    TextFieldAssist.SetUnderlineBrush(txt_Data_Search, XElementHelper.GetColor(Colors.Transparent));
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
                bR_TextBox_Search.BorderBrush = _borderBrushSearchColor;
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

                TextFieldAssist.SetUnderlineBrush(txt_Data_Search, _borderBrushSearchColorFocused);
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

                lb_PlaceHolder_Search.Opacity = _placeHolderSearchOpacity.Value;
            }
        }

        private string _placeHolderSearchText;
        public string _PlaceHolderSearchText
        {
            get => _placeHolderSearchText;
            set
            {
                _placeHolderSearchText = value;
                lb_PlaceHolder_Search.Content = _PlaceHolderSearchText;
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

                lb_PlaceHolder_Search.FontSize = _placeHolderSearchFontSize.Value;
            }
        }

        public bool? _isEnableSearch;
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
        #endregion

        #region Value
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
            get => txt_Data.Tag.FillStringSafe();
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

                            if (getkeyFieldValue.FillStringSafe() == Value.FillStringSafe())
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

        protected object _dataSource;
        public object _DataSource
        {
            set
            {
                _dataSource = value;
                getAllDataSources = null;

                //Clear Grid Content
                gR_Popup.Children.Clear();
                gR_Popup.RowDefinitions.Clear();

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
                }

                HandleSearch();
            }
        }

        protected List<string> _items;
        public List<string> _Items
        {
            get => _items;
            set
            {
                _items = value;
                if (_items == null)
                    _items = new List<string>();

                //Clear Grid Content
                gR_Popup.Children.Clear();
                gR_Popup.RowDefinitions.Clear();

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


        private void HandleSearch()
        {
            var IsDataSource = (_dataSource != null && getAllDataSources.Count() > 0) || (_items != null && _items.Count() > 0);
            gR_Row_Search.Height = new GridLength(_isEnableSearch.Value && IsDataSource ? 50 : 0, GridUnitType.Pixel);
        }

        #region Grid Data
        private void GenerateGridDataPanel(string KeyField, string DisplayField, int IndexRow)
        {
            gR_Popup.RowDefinitions.Add(new RowDefinition
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
            gR_Popup.Children.Add(innerGrid);
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
            txt_Data.Text = DisplayField.FillStringSafe();
            txt_Data.Tag = KeyField;
            XPopupManager.ClosePopupByTag(xComboBoxPopup.Tag.ToString());

            if (_ValueChanged != null && IsTriggerChanged)
            {
                _ValueChanged(this, new XComboBoxValueEventArgs
                {
                    Content = txt_Data.Text.FillStringSafe(),
                    KeyField = txt_Data.Tag.FillStringSafe(),
                    DataSource = _DataSourceSelected
                });
            }
        }
        #endregion

        #region Search 
        private void Txt_Data_Search_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                if (!_isEnableSearch.Value || !xComboBoxPopup.IsOpen)
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
                    return DisplayField.FillStringSafe() == txt_Data_Search.Text.FillStringSafe();
                else
                    return DisplayField.FillStringSafe().ToLower() == txt_Data_Search.Text.FillStringSafe().ToLower();
            }

            if (_caseSensitiveSearchMode.Value)
                return DisplayField.FillStringSafe().Contains(txt_Data_Search.Text.FillStringSafe());
            else
                return DisplayField.FillStringSafe().ToLower().Contains(txt_Data_Search.Text.FillStringSafe().ToLower());
        }
        #endregion

    }
}

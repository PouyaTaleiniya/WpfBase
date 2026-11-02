using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Globalization;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using XControlComponents.Models;
using XControlComponents.Models.XGrid;
using XControlComponents.Tools;
using XControlComponents.Tools.Controls;

namespace XControlComponents.Controls
{
    /// <summary>
    /// Interaction logic for XGridView.xaml
    /// </summary>
    public partial class XGridView : UserControl
    {
        private List<object> getAllDataSources { get; set; }
        public XGridData xGridData { get; set; } = new XGridData();

        private bool SearchMode { get; set; }
        private bool IsInitializedDataSource { get; set; } = false;
        private bool IsInitializedRowData { get; set; }
        private bool IsVerticalScrollBarVisible = false;

        //Events
        [EditorBrowsable(EditorBrowsableState.Always)]
        public delegate void _RowDoubleClickEventHandler(object sender, object Data);
        public event _RowDoubleClickEventHandler RowDoubleClick;


        [EditorBrowsable(EditorBrowsableState.Always)]
        public delegate void _DataSourceInitializedEventHandler();
        public event _DataSourceInitializedEventHandler DataSourceInitialized;

        public XGridView()
        {
            InitializeComponent();

            //Border
            _BorderColor = XGridViewDefaults.BorderColor;
            _InnerBorderColor = XGridViewDefaults.InnerBorderColor;

            //Grid
            _GridMargin = XGridViewDefaults.GridMargin;
            _GridBackground = XGridViewDefaults.GridBackground;

            //Grid Header
            _HeaderHeight = XGridViewDefaults.HeaderHeight;
            _HeaderTxtForeground = XGridViewDefaults.HeaderTxtForeground;
            _HeaderTxtFontSize = XGridViewDefaults.HeaderTxtFontSize;
            _HeaderTxtFontWeight = XGridViewDefaults.HeaderTxtFontWeight;
            _HeaderTxtFontStyle = XGridViewDefaults.HeaderTxtFontStyle;
            _HeaderTxtSelectable = XGridViewDefaults.HeaderTxtSelectable;
            _HeaderTxtSelectedBrush = XGridViewDefaults.HeaderTxtSelectedBrush;
            _HeaderTxtSelectionOpacity = XGridViewDefaults.HeaderTxtSelectionOpacity;

            //Grid Row
            _RowHeight = XGridViewDefaults.RowHeight;
            _RowSelectedBackground = XGridViewDefaults.RowSelectedBackground;
            _RowSelectable = XGridViewDefaults.RowSelectable;
            _RowMultiSelectable = XGridViewDefaults.RowMultiSelectable;
            _RowTxtForeground = XGridViewDefaults.RowTxtForeground;
            _RowTxtFontSize = XGridViewDefaults.RowTxtFontSize;
            _RowTxtFontWeight = XGridViewDefaults.RowTxtFontWeight;
            _RowTxtFontStyle = XGridViewDefaults.RowTxtFontStyle;
            _RowTxtSelectable = XGridViewDefaults.RowTxtSelectable;
            _RowTxtSelectedBrush = XGridViewDefaults.RowTxtSelectedBrush;
            _RowTxtSelectionOpacity = XGridViewDefaults.RowTxtSelectionOpacity;
            _RowIndicator = XGridViewDefaults.RowIndicator;

            //NoData
            _NoDataForeground = XGridViewDefaults.NoDataForeground;
            _NoDataBackGround = XGridViewDefaults.NoDataBackGround;
            _NoDataText = XGridViewDefaults.NoDataText;
            _NoDataFontSize = XGridViewDefaults.NoDataFontSize;
        }

        #region User Control 
        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            HandleGridVisibility();
        }

        private void UserControl_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (Keyboard.FocusedElement is TextBox focusedTextBox)
            {
                DependencyObject clickedElement = e.OriginalSource as DependencyObject;
                if (clickedElement != null && !IsClickInsideTextBox(clickedElement, focusedTextBox))
                {
                    focusedTextBox.SelectionLength = 0;
                    Keyboard.ClearFocus();
                }
            }
        }

        private bool IsClickInsideTextBox(DependencyObject clickedElement, TextBox targetTextBox)
        {
            while (clickedElement != null)
            {
                if (clickedElement == targetTextBox)
                    return true;

                clickedElement = VisualTreeHelper.GetParent(clickedElement);
            }
            return false;
        }
        #endregion

        #region Border
        private Brush _borderColor;
        public Brush _BorderColor
        {
            get => _borderColor;
            set
            {
                _borderColor = value;

                if (_borderColor == null)
                    _borderColor = XGridViewDefaults.BorderColor;

                BR_Grid.BorderBrush = _borderColor;
            }
        }

        private Brush _innerBorderColor;
        public Brush _InnerBorderColor
        {
            get => _innerBorderColor;
            set
            {
                _innerBorderColor = value;

                if (_innerBorderColor == null)
                    _innerBorderColor = XGridViewDefaults.InnerBorderColor;

                BR_Header.BorderBrush = _innerBorderColor;
            }
        }
        #endregion

        #region Grid
        private Brush _gridBackGround;
        public Brush _GridBackground
        {
            get => _gridBackGround;
            set
            {
                _gridBackGround = value;

                if (_gridBackGround == null)
                    _gridBackGround = XGridViewDefaults.GridBackground;

                BR_Grid.Background = _gridBackGround;
            }
        }

        private int? _GridScrollIndex
        {
            get
            {
                if (GR_Columns.ColumnDefinitions.Count == 0)
                    return null;

                return GR_Columns.ColumnDefinitions.Count - 1;
            }
        }

        private Thickness? _gridMargin;
        public Thickness? _GridMargin
        {
            get => _gridMargin;
            set
            {
                _gridMargin = value;

                if (_gridMargin == null)
                    _gridMargin = XGridViewDefaults.GridMargin;

                BR_Grid.Margin = _gridMargin.Value;

                SetGridBorder();
            }
        }
        #endregion

        #region Grid Header
        private double? _headerHeight;
        public double? _HeaderHeight
        {
            get => _headerHeight;
            set
            {
                _headerHeight = value;

                if (_headerHeight == null)
                    _headerHeight = XGridViewDefaults.HeaderHeight;

                if (_headerHeight < 32)
                    _headerHeight = 32;

                GR_Row_Header.Height = new GridLength(_headerHeight.Value, GridUnitType.Pixel);

                SetHeightRow();
                SetGridBorder();
            }
        }

        private Brush _headerBackground;
        public Brush _HeaderBackground
        {
            get => _headerBackground;
            set
            {
                _headerBackground = value;

                if (_headerBackground == null)
                    _headerBackground = XGridViewDefaults.HeaderBackground;

                BR_Header.Background = _headerBackground;
            }
        }

        private Brush _headerTxtForeground;
        public Brush _HeaderTxtForeground
        {
            get => _headerTxtForeground;
            set
            {
                _headerTxtForeground = value;

                if (_headerTxtForeground == null)
                    _headerTxtForeground = XGridViewDefaults.HeaderTxtForeground;

                HandleColumnTextProperties();
            }
        }

        private double? _headerTxtFontSize;
        public double? _HeaderTxtFontSize
        {
            get => _headerTxtFontSize;
            set
            {
                _headerTxtFontSize = value;

                if (_headerTxtFontSize == null || _headerTxtFontSize < 10)
                    _headerTxtFontSize = XGridViewDefaults.HeaderTxtFontSize;

                HandleColumnTextProperties();
            }
        }

        private FontWeight? _headerTxtFontWeight;
        public FontWeight? _HeaderTxtFontWeight
        {
            get => _headerTxtFontWeight;
            set
            {
                _headerTxtFontWeight = value;

                if (_headerTxtFontWeight == null)
                    _headerTxtFontWeight = XGridViewDefaults.HeaderTxtFontWeight;

                HandleColumnTextProperties();
            }
        }

        private FontStyle? _headerTxtFontStyle;
        public FontStyle? _HeaderTxtFontStyle
        {
            get => _headerTxtFontStyle;
            set
            {
                _headerTxtFontStyle = value;

                if (_headerTxtFontStyle == null)
                    _headerTxtFontStyle = XGridViewDefaults.HeaderTxtFontStyle;

                HandleColumnTextProperties();
            }
        }

        private bool? _headerTxtSelectable;
        public bool? _HeaderTxtSelectable
        {
            get => _headerTxtSelectable;
            set
            {
                _headerTxtSelectable = value;

                if (_headerTxtSelectable == null)
                    _headerTxtSelectable = XGridViewDefaults.HeaderTxtSelectable;

                HandleColumnTextProperties();
            }
        }

        private Brush _headerTxtSelectedBrush;
        public Brush _HeaderTxtSelectedBrush
        {
            get => _headerTxtSelectedBrush;
            set
            {
                _headerTxtSelectedBrush = value;

                if (_headerTxtSelectedBrush == null)
                    _headerTxtSelectedBrush = XGridViewDefaults.HeaderTxtSelectedBrush;

                HandleColumnTextProperties();
            }
        }

        private double? _headerTxtSelectionOpacity;
        public double? _HeaderTxtSelectionOpacity
        {
            get => _headerTxtSelectionOpacity;
            set
            {
                _headerTxtSelectionOpacity = value;

                if (_headerTxtSelectionOpacity == null || _headerTxtSelectionOpacity < 0.1)
                    _headerTxtSelectionOpacity = XGridViewDefaults.HeaderTxtSelectionOpacity;

                HandleColumnTextProperties();
            }
        }
        #endregion

        #region Grid Row
        private double? _rowHeight;
        public double? _RowHeight
        {
            get => _rowHeight;
            set
            {
                _rowHeight = value;

                if (_rowHeight == null)
                    _rowHeight = XGridViewDefaults.RowHeight;

                if (_rowHeight < 32)
                    _rowHeight = 32;

                SetHeightRow();
                SetGridBorder();
            }
        }

        private Brush _rowSelectedBackground;
        public Brush _RowSelectedBackground
        {
            get => _rowSelectedBackground;
            set
            {
                _rowSelectedBackground = value;

                if (_rowSelectedBackground == null)
                    _rowSelectedBackground = XGridViewDefaults.RowSelectedBackground;
            }
        }

        private bool? _rowSelectable;
        public bool? _RowSelectable
        {
            get => _rowSelectable;
            set
            {
                _rowSelectable = value;

                if (_rowSelectable == null)
                    _rowSelectable = XGridViewDefaults.RowSelectable;

                HandleRowSelected();
            }
        }

        private bool? _rowMultiSelectable;
        public bool? _RowMultiSelectable
        {
            get => _rowMultiSelectable;
            set
            {
                _rowMultiSelectable = value;

                if (_rowMultiSelectable == null)
                    _rowMultiSelectable = XGridViewDefaults.RowMultiSelectable;

                HandleRowSelected();
            }
        }

        private Brush _rowTxtForeground;
        public Brush _RowTxtForeground
        {
            get => _rowTxtForeground;
            set
            {
                _rowTxtForeground = value;

                if (_rowTxtForeground == null)
                    _rowTxtForeground = XGridViewDefaults.RowTxtForeground;
            }
        }

        private double? _rowTxtFontSize;
        public double? _RowTxtFontSize
        {
            get => _rowTxtFontSize;
            set
            {
                _rowTxtFontSize = value;

                if (_rowTxtFontSize == null || _rowTxtFontSize < 10)
                    _rowTxtFontSize = XGridViewDefaults.RowTxtFontSize;

                HandleRowTextProperties();
            }
        }

        private FontWeight? _rowTxtFontWeight;
        public FontWeight? _RowTxtFontWeight
        {
            get => _rowTxtFontWeight;
            set
            {
                _rowTxtFontWeight = value;

                if (_rowTxtFontWeight == null)
                    _rowTxtFontWeight = XGridViewDefaults.RowTxtFontWeight;

                HandleRowTextProperties();
            }
        }

        private FontStyle? _rowTxtFontStyle;
        public FontStyle? _RowTxtFontStyle
        {
            get => _rowTxtFontStyle;
            set
            {
                _rowTxtFontStyle = value;

                if (_rowTxtFontStyle == null)
                    _rowTxtFontStyle = XGridViewDefaults.RowTxtFontStyle;

                HandleRowTextProperties();
            }
        }

        private bool? _rowTxtSelectable;
        public bool? _RowTxtSelectable
        {
            get => _rowTxtSelectable;
            set
            {
                _rowTxtSelectable = value;

                if (_rowTxtSelectable == null)
                    _rowTxtSelectable = XGridViewDefaults.RowTxtSelectable;

                HandleRowTextProperties();
            }
        }

        private Brush _rowTxtSelectedBrush;
        public Brush _RowTxtSelectedBrush
        {
            get => _rowTxtSelectedBrush;
            set
            {
                _rowTxtSelectedBrush = value;

                if (_rowTxtSelectedBrush == null)
                    _rowTxtSelectedBrush = XGridViewDefaults.RowTxtSelectedBrush;

                HandleRowTextProperties();
            }
        }

        private double? _rowTxtSelectionOpacity;
        public double? _RowTxtSelectionOpacity
        {
            get => _rowTxtSelectionOpacity;
            set
            {
                _rowTxtSelectionOpacity = value;

                if (_rowTxtSelectionOpacity == null || _rowTxtSelectionOpacity < 0.1)
                    _rowTxtSelectionOpacity = XGridViewDefaults.RowTxtSelectionOpacity;

                HandleRowTextProperties();
            }
        }

        private bool? _rowIndicator;
        public bool? _RowIndicator
        {
            get => _rowIndicator;
            set
            {
                _rowIndicator = value;

                if (_rowIndicator == null)
                    _rowIndicator = XGridViewDefaults.RowIndicator;

                if (getAllDataSources != null)
                    HandleRowIndicator();
            }
        }
        #endregion

        #region Grid Row Fields
        private IEnumerable<(string FieldName, string DisplayName)> _displayNameFields = [];

        public IEnumerable<(string FieldName, string DisplayName)> _DisplayNameFields
        {
            internal get => _displayNameFields;
            set
            {
                _displayNameFields = value ?? [];

                if (getAllDataSources != null)
                    SetDisplayNameFields();
            }
        }

        private IEnumerable<(string FieldName, bool Visible)> _visibilityFields = [];

        public IEnumerable<(string FieldName, bool Visible)> _VisibilityFields
        {
            internal get => _visibilityFields;
            set
            {
                _visibilityFields = value ?? [];

                if (getAllDataSources != null)
                    SetVisibilityFields();
            }
        }

        private IEnumerable<(string FieldName, int Order)> _orderFields = [];

        public IEnumerable<(string FieldName, int Order)> _OrderFields
        {
            internal get => _orderFields;
            set
            {
                _orderFields = value ?? [];

                if (getAllDataSources != null)
                    SetOrderFields();
            }
        }

        private IEnumerable<(string FieldName, double Width)> _widthFields = [];

        public IEnumerable<(string FieldName, double Width)> _WidthFields
        {
            internal get => _widthFields;
            set
            {
                _widthFields = value ?? [];

                if (getAllDataSources != null)
                    SetWidthFields();
            }
        }
        #endregion

        #region NoData
        private string _noDataText;
        public string _NoDataText
        {
            get => _noDataText;
            set
            {
                _noDataText = value;

                if (_noDataText.IsNullOrEmpty())
                    _noDataText = XGridViewDefaults.NoDataText;

                Lb_NoData.Content = _noDataText;
            }
        }

        private double? _noDataFontSize;
        public double? _NoDataFontSize
        {
            get => _noDataFontSize;
            set
            {
                _noDataFontSize = value;

                if (_noDataFontSize == null || _noDataFontSize < 10)
                    _noDataFontSize = XGridViewDefaults.NoDataFontSize;

                Lb_NoData.FontSize = _noDataFontSize.Value;
            }
        }

        private Brush _noDataForeground;
        public Brush _NoDataForeground
        {
            get => _noDataForeground;
            set
            {
                _noDataForeground = value;

                if (_noDataForeground == null)
                    _noDataForeground = XGridViewDefaults.NoDataForeground;

                Lb_NoData.Foreground = _noDataForeground;
            }
        }

        private Brush _noDataBackGround;
        public Brush _NoDataBackGround
        {
            get => _noDataBackGround;
            set
            {
                _noDataBackGround = value;

                if (_noDataBackGround == null)
                    _noDataBackGround = XGridViewDefaults.NoDataBackGround;

                GR_NoData.Background = _noDataBackGround;
            }
        }
        #endregion

        private object _dataSource;
        public object _DataSource
        {
            internal get => _dataSource;
            set
            {
                _dataSource = value;
                getAllDataSources = null;

                if (_dataSource != null)
                {
                    RefreshDataSource();
                }
            }
        }

        #region Data Source
        private void RefreshDataSource()
        {
            //Read Data Source
            getAllDataSources = ((IEnumerable)_dataSource).Cast<object>().ToList();
            var dataSourceType = _dataSource.GetType();

            //Read Header
            var itemType = dataSourceType.GetGenericArguments()[0];

            //Clear Columns
            GR_Columns.ColumnDefinitions.Clear();
            GR_Columns.Children.Clear();

            //Columns
            SetXColumns(itemType);

            //Generate Column
            GenerateColumn();

            //Rows
            SetXRows();

            //Generate Row
            GenerateRow();

            //Handle Scroll
            GridScroll();

            //Set Initialize
            IsInitializedDataSource = true;
            DataSourceInitialized?.Invoke();
        }

        public List<T> GetDataSource<T>(Func<T, bool>? predicate = null)
        {
            return (getAllDataSources?.Cast<T>() ?? Enumerable.Empty<T>())
                   .Where(x => predicate == null || predicate(x))
                   .ToList();
        }

        public T _DataSourceSelected<T>()
        {
            var getSelected = xGridData.Rows.FirstOrDefault(x => x.IsSelected);
            return (T)getSelected.Data;
        }

        public List<T> _DataSourceSelectedItems<T>()
        {
            var getSelectedItems = xGridData.Rows.Where(x => x.IsSelected).Select(x => (T)x.Data).ToList();
            return getSelectedItems;
        }

        public void UpdateRow(object Data)
        {
            var getRow = xGridData.Rows.FirstOrDefault(x => x.Data == Data);
            UpdateRowData(getRow);
        }

        public void UpdateRows<T>(List<T> Datas)
        {
            var getRows = xGridData.Rows.Where(x => Datas.Contains((T)x.Data)).ToList();
            foreach (var item in getRows)
            {
                UpdateRowData(item);
            }
        }

        private void UpdateRowData(XGridRow getRow)
        {
            var getBorder = GR_Data.FindChildByTag<Border>(getRow.Key);
            var getGrid = getBorder.FindChildByTag<Grid>(getRow.Key);
            var getTxts = getGrid.FindChilds<TextBox>();
            foreach (var item in getRow.Cells)
            {
                item.Value = item.Column.Property.GetValue(getRow.Data);
                if (item.Column.Visible)
                {
                    var getTxt = getTxts.FirstOrDefault(x => x.Tag?.ToString() == item.Key);
                    getTxt.Text = item.Value.FillStringSafe();
                }
            }
        }

        #endregion

        #region Grid Scroll
        private void SC_Data_ScrollChanged(object sender, ScrollChangedEventArgs e)
        {
            GridScroll();
        }

        private void GridScroll()
        {
            try
            {

                bool isVisible = SC_Data.ComputedVerticalScrollBarVisibility == Visibility.Visible;
                if (isVisible && !IsVerticalScrollBarVisible)
                {
                    if (_GridScrollIndex != null)
                    {
                        IsVerticalScrollBarVisible = true;
                        GR_Columns.ColumnDefinitions[_GridScrollIndex.Value].Width = new GridLength(18, GridUnitType.Pixel);
                    }
                }
                else if (!isVisible && IsVerticalScrollBarVisible)
                {
                    IsVerticalScrollBarVisible = false;

                    if (_GridScrollIndex != null)
                        GR_Columns.ColumnDefinitions[_GridScrollIndex.Value].Width = new GridLength(0, GridUnitType.Pixel);
                }

                // Bottom End
                if (IsInitializedRowData)
                {
                    if (SC_Data.ScrollableHeight > 0)
                    {
                        if (SC_Data.VerticalOffset >= SC_Data.ScrollableHeight - 0.5)
                            BR_Grid.BorderThickness = new Thickness(1, 1, 1, 0);
                        else
                            BR_Grid.BorderThickness = new Thickness(1);
                    }
                    else
                        SetGridBorder();
                }
            }
            catch (Exception)
            {
            }
        }

        private void BR_Grid_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            SetGridBorder();
        }

        private void SetGridBorder()
        {
            if (IsInitializedRowData)
            {
                double MaxHeight = _HeaderHeight.Value + (GR_Data.RowDefinitions.Count * _rowHeight.Value);

                double marginHeight = 0;
                var gridHeight = BR_Grid.ActualHeight;
                if (_gridMargin.HasValue && (_gridMargin.Value.Top > 0 || _gridMargin.Value.Bottom > 0))
                {
                    marginHeight = _gridMargin.Value.Top + _gridMargin.Value.Bottom;
                    gridHeight = gridHeight + marginHeight;
                }

                if (MaxHeight + marginHeight + 4 >= gridHeight)
                    BR_Grid.BorderThickness = new Thickness(1, 1, 1, 0);
                else
                    BR_Grid.BorderThickness = new Thickness(1);
            }
        }
        #endregion

        #region Grid Header
        private void SetXColumns(Type itemType)
        {
            xGridData = new XGridData();

            var properties = itemType
                .GetProperties(BindingFlags.Public | BindingFlags.Instance)
                .ToList();

            var columns = new XGridColumnCollection();

            foreach (var property in properties)
            {
                var getDisplayName = _displayNameFields.FirstOrDefault(x => x.FieldName == property.Name);
                var getVisibilty = _visibilityFields.FirstOrDefault(x => x.FieldName == property.Name);
                var getOrder = _orderFields.FirstOrDefault(x => x.FieldName == property.Name);
                var getWidth = _widthFields.FirstOrDefault(x => x.FieldName == property.Name);

                columns.Add(new XGridColumn
                {
                    FieldName = property.Name,

                    DisplayName = getDisplayName.FieldName == null ? property.Name : getDisplayName.DisplayName,

                    Visible = getVisibilty.FieldName == null ? true : getVisibilty.Visible,

                    Order = getOrder.FieldName == null ? 1 : getOrder.Order,

                    Width = getWidth.FieldName == null ? 0 : getWidth.Width,

                    Property = property,
                });
            }

            xGridData.Columns.AddRange(columns);
        }

        private void GenerateColumn()
        {
            GenerateColumnIndicator();

            var columnIndex = GR_Columns.ColumnDefinitions.Count();
            var sortedColumn = xGridData.Columns.OrderBy(x => x.Order).ToList();
            var visibleColumn = xGridData.VisibleColumns.OrderBy(x => x.Order).ToList();
            foreach (var column in sortedColumn)
            {
                var IsLastColumn = visibleColumn.IndexOf(column) == visibleColumn.Count - 1;

                var columnWidth = new GridLength(0);
                if (column.Visible)
                    columnWidth = column.Width > 0 ? new GridLength(column.Width, GridUnitType.Pixel) : new GridLength(1, GridUnitType.Star);

                GR_Columns.ColumnDefinitions.Add(new ColumnDefinition
                {
                    Width = columnWidth
                });

                var borderColumn = new Border
                {
                    Style = XAppMethods.BorderInnerGridStyle(),
                    BorderThickness = new Thickness(0, 0, IsLastColumn ? 0 : 1, 0),
                    Background = Colors.Transparent.GetBrushColor(),
                    Tag = "BR" + column.FieldName.ToString(),
                };
                Grid.SetColumn(borderColumn, columnIndex);

                var textBox = new TextBox
                {
                    Tag = column.FieldName.ToString(),
                    Text = column.DisplayName.IsNullOrEmpty() ? column.FieldName : column.DisplayName,
                    Foreground = _headerTxtForeground,
                    Background = Colors.Transparent.GetBrushColor(),
                    HorizontalAlignment = HorizontalAlignment.Center,
                    VerticalAlignment = VerticalAlignment.Center,
                    BorderThickness = new Thickness(0),
                    FontSize = _headerTxtFontSize.Value,
                    FontWeight = _headerTxtFontWeight.Value,
                    IsReadOnly = true,
                    Style = _HeaderTxtSelectable.Value ? this.FindResource("TextBoxEventStyle") as Style : null,
                    FocusVisualStyle = null,
                    FontStyle = _headerTxtFontStyle.Value,
                    SelectionBrush = _HeaderTxtSelectable.Value ? _HeaderTxtSelectedBrush : Colors.Transparent.GetBrushColor(),
                    SelectionOpacity = _HeaderTxtSelectionOpacity.Value
                };

                borderColumn.Child = textBox;
                GR_Columns.Children.Add(borderColumn);

                columnIndex++;
            }

            //Scroll

            columnIndex = columnIndex + 1;

            var gridColumn = new ColumnDefinition
            {
                Width = new GridLength(0)
            };

            GR_Columns.ColumnDefinitions.Add(gridColumn);

            var borderScrollColumn = new Border
            {
                Style = XAppMethods.BorderInnerGridStyle(),
                BorderThickness = new Thickness(1, 0, 0, 0)
            };
            Grid.SetColumn(borderScrollColumn, columnIndex);

            GR_Columns.Children.Add(borderScrollColumn);
        }

        private void HandleColumnTextProperties()
        {
            if (!IsInitializedDataSource)
                return;

            var getTextBoxes = GR_Columns.FindChilds<TextBox>();
            foreach (TextBox item in getTextBoxes)
            {
                item.Foreground = _HeaderTxtForeground;
                item.FontSize = _HeaderTxtFontSize.Value;
                item.FontWeight = _HeaderTxtFontWeight.Value;
                item.FontStyle = _HeaderTxtFontStyle.Value;
                item.Style = _HeaderTxtSelectable.Value ? this.FindResource("TextBoxEventStyle") as Style : null;
                item.SelectionBrush = _HeaderTxtSelectable.Value ? _HeaderTxtSelectedBrush : Colors.Transparent.GetBrushColor();
                item.SelectionOpacity = _HeaderTxtSelectionOpacity.Value;
            }
        }
        #endregion

        #region Grid Row
        private void SetXRows()
        {
            foreach (var item in getAllDataSources)
            {
                var row = new XGridRow()
                {
                    Data = item,
                    Key = "Row_" + Guid.NewGuid().ToString()
                };

                if (item is XDataKeyDto)
                    ((XDataKeyDto)item).XKey = row.Key;

                foreach (var column in xGridData.Columns)
                {
                    row.Cells.Add(new XGridCell
                    {
                        Column = column,
                        Value = column.Property.GetValue(item),
                        Key = "Column_" + Guid.NewGuid().ToString()
                    });
                }

                xGridData.Rows.Add(row);
            }
        }

        private void GenerateRow()
        {
            //Clear Rows
            GR_Data.RowDefinitions.Clear();
            GR_Data.Children.Clear();

            if (xGridData.Rows.Count == 0)
                return;

            HandleGridVisibility();

            foreach (var row in xGridData.Rows)
            {
                var rowIndex = xGridData.Rows.IndexOf(row);
                var IsLastRow = rowIndex == xGridData.Rows.Count - 1;

                //Add Row
                GR_Data.RowDefinitions.Add(new RowDefinition());

                //Row Border
                var rowBorder = new Border
                {
                    Style = XAppMethods.BorderInnerGridStyle(),
                    BorderThickness = new Thickness(0, 0, 0, 1),
                    Background = Colors.Transparent.GetBrushColor(),
                    Tag = row.Key
                };
                rowBorder.MouseLeftButtonDown += CellBorder_MouseLeftButtonDown;
                Grid.SetRow(rowBorder, rowIndex);

                //Row Grid
                var rowGrid = new Grid()
                {
                    Background = Colors.Transparent.GetBrushColor(),
                    Tag = row.Key
                };
                rowBorder.Child = rowGrid;

                GenerateRowIndicator(rowGrid);

                var sortedCell = row.Cells.OrderBy(x => x.Column.Order).ToList();
                var visibleCell = row.VisibleCells.OrderBy(x => x.Column.Order).ToList();
                foreach (var cell in sortedCell)
                {
                    var columnWidth = new GridLength(0);
                    if (cell.Column.Visible)
                        columnWidth = cell.Column.Width > 0 ? new GridLength(cell.Column.Width, GridUnitType.Pixel) : new GridLength(1, GridUnitType.Star);

                    var columnDefinition = new ColumnDefinition()
                    {
                        Width = columnWidth
                    };
                    rowGrid.ColumnDefinitions.Add(columnDefinition);

                    var IsLastCell = visibleCell.IndexOf(cell) == visibleCell.Count - 1;

                    var cellBorder = new Border
                    {
                        Style = XAppMethods.BorderInnerGridStyle(),
                        BorderThickness = new Thickness(0, 0, IsLastCell ? 0 : 1, 0),
                        Background = Colors.Transparent.GetBrushColor(),
                        Tag = cell.Key
                    };
                    Grid.SetColumn(cellBorder, rowGrid.ColumnDefinitions.Count - 1);

                    var textBox = new TextBox
                    {
                        Text = cell.Value.FillStringSafe(),
                        Foreground = _rowTxtForeground,
                        Background = Colors.Transparent.GetBrushColor(),
                        HorizontalAlignment = HorizontalAlignment.Center,
                        VerticalAlignment = VerticalAlignment.Center,
                        FontSize = _rowTxtFontSize.Value,
                        FontWeight = _rowTxtFontWeight.Value,
                        FontStyle = _rowTxtFontStyle.Value,
                        IsReadOnly = true,
                        BorderThickness = new Thickness(0),
                        Style = _RowTxtSelectable.Value ? this.FindResource("TextBoxEventStyle") as Style : null,
                        SelectionBrush = _RowTxtSelectable.Value ? _RowTxtSelectedBrush : Colors.Transparent.GetBrushColor(),
                        SelectionOpacity = _rowTxtSelectionOpacity.Value,
                        Padding = new Thickness(4, 0, 4, 0),
                        FocusVisualStyle = null,
                        Tag = cell.Key
                    };

                    cellBorder.Child = textBox;
                    rowGrid.Children.Add(cellBorder);
                }

                //Add Row Border
                GR_Data.Children.Add(rowBorder);
            }

            //Set Initialize
            IsInitializedRowData = true;

            //Set Border Height
            SetHeightRow();
        }

        private void SetHeightRow()
        {
            if (!IsInitializedRowData)
                return;

            //double BorderHeight = 0;
            foreach (var item in GR_Data.RowDefinitions)
            {
                var i = GR_Data.RowDefinitions.IndexOf(item);
                item.Height = new GridLength(_rowHeight.Value, GridUnitType.Pixel);
            }
        }

        private void HandleGridVisibility()
        {
            if (xGridData.Rows.Count == 0)
            {
                GR_Row_Data.Height = new GridLength(0);
                GR_Row_NoData.Height = new GridLength(1, GridUnitType.Star);
                GR_Row_Header.Height = new GridLength(0);
            }
            else
            {
                GR_Data.ClearValue(HeightProperty);
                GR_Row_NoData.Height = new GridLength(0);
                GR_Row_Header.Height = new GridLength(_headerHeight.Value, GridUnitType.Pixel);
            }
        }
        #endregion

        #region Grid Row Indicator
        private void GenerateColumnIndicator()
        {
            GR_Columns.ColumnDefinitions.Add(new ColumnDefinition
            {
                Width = new GridLength(_rowIndicator.Value ? 50 : 0, GridUnitType.Pixel)
            });

            var borderColumn = new Border
            {
                Style = XAppMethods.BorderInnerGridStyle(),
                BorderThickness = new Thickness(0, 0, 1, 0),
                Background = Colors.Transparent.GetBrushColor()
            };
            Grid.SetColumn(borderColumn, 0);

            var textBox = new Label
            {
                Content = "*",
                Foreground = _headerTxtForeground,
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center,
                FontSize = 15,
                Padding = new Thickness(0, 5, 0, 0),
                FocusVisualStyle = null,
                Opacity = 0.5
            };

            borderColumn.Child = textBox;
            GR_Columns.Children.Add(borderColumn);
        }

        private void GenerateRowIndicator(Grid gridRow)
        {
            var columnDefinition = new ColumnDefinition()
            {
                Width = new GridLength(_rowIndicator.Value ? 50 : 0, GridUnitType.Pixel)
            };
            gridRow.ColumnDefinitions.Add(columnDefinition);

            var borderColumn = new Border
            {
                Style = XAppMethods.BorderInnerGridStyle(),
                BorderThickness = new Thickness(0, 0, 1, 0),
                Background = Colors.Transparent.GetBrushColor()
            };
            Grid.SetColumn(borderColumn, 0);

            var label = new Label
            {
                Content = ">",
                Foreground = _headerTxtForeground,
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center,
                FontSize = 10,
                FocusVisualStyle = null,
                Opacity = 0.5
            };

            borderColumn.Child = label;
            gridRow.Children.Add(borderColumn);
        }

        private void HandleRowIndicator()
        {

        }
        #endregion

        #region Grid Row Fields
        private void SetDisplayNameFields()
        {
            var getTxts = GR_Columns.FindChilds<TextBox>();
            foreach (var item in _displayNameFields)
            {
                var getColumn = xGridData.Columns.FirstOrDefault(x => x.FieldName == item.FieldName);
                if (getColumn != null)
                {
                    getColumn.DisplayName = item.DisplayName.FillStringSafe();
                    var getTxt = getTxts.FirstOrDefault(x => x.Tag?.ToString() == item.FieldName);
                    getTxt.Text = getColumn.DisplayName;
                }
            }
        }

        private void SetOrderFields()
        {
            foreach (var item in _orderFields)
            {
                var getColumn = xGridData.Columns.FirstOrDefault(x => x.FieldName == item.FieldName);
                if (getColumn != null)
                    getColumn.Order = item.Order;
            }

            SetPropertyFields();
        }

        private void SetWidthFields()
        {
            foreach (var item in _widthFields)
            {
                var getColumn = xGridData.Columns.FirstOrDefault(x => x.FieldName == item.FieldName);
                if (getColumn != null)
                    getColumn.Width = item.Width;
            }

            SetPropertyFields();
        }

        private void SetVisibilityFields()
        {
            foreach (var item in _visibilityFields)
            {
                var getColumn = xGridData.Columns.FirstOrDefault(x => x.FieldName == item.FieldName);
                if (getColumn != null)
                    getColumn.Visible = item.Visible;
            }

            SetPropertyFields();
        }

        private void SetPropertyFields()
        {
            var IsScroll = GR_Columns.ColumnDefinitions[_GridScrollIndex.Value].Width.Value > 0;

            var getBorders = GR_Columns.FindChilds<Border>(x => !x.Tag.IsNullOrEmpty());

            var sortedColumns = xGridData.Columns.OrderBy(x => x.Order).ToList();
            var VisibleColumns = xGridData.VisibleColumns.OrderBy(x => x.Order).ToList();
            foreach (var item in sortedColumns)
            {
                //Index (+1 For Shower Column)
                var Index = sortedColumns.IndexOf(item) + 1;
                var IsLastColumn = VisibleColumns.IndexOf(item) == VisibleColumns.Count - 1;

                //Column Width
                var colWidth = new GridLength(0);
                if (item.Visible)
                    colWidth = item.Width > 0 ? new GridLength(item.Width, GridUnitType.Pixel) : new GridLength(1, GridUnitType.Star);
                GR_Columns.ColumnDefinitions[Index].Width = colWidth;

                var getBorder = getBorders.FirstOrDefault(x => x.Tag?.ToString() == "BR" + item.FieldName);

                //Set Border
                getBorder.BorderThickness = new Thickness(0, 0, IsLastColumn && !IsScroll ? 0 : 1, 0);

                //Column Index
                Grid.SetColumn(getBorder, Index);
            }

            if (xGridData.Rows.Count > 0)
            {
                var grids = GR_Data.FindChilds<Grid>(x => !x.Tag.IsNullOrEmpty());
                foreach (var item in xGridData.Rows)
                {
                    var sortedCells = item.Cells.OrderBy(cell => cell.Column.Order).ToList();
                    var getGridRow = grids.FirstOrDefault(x => x.Tag?.ToString() == item.Key);
                    var getCellBorders = getGridRow.FindChilds<Border>(x => !x.Tag.IsNullOrEmpty());
                    var VisibleCells = item.VisibleCells.OrderBy(x => x.Column.Order).ToList();
                    foreach (var cell in sortedCells)
                    {
                        //Index (+1 For Shower Column)
                        var Index = sortedCells.IndexOf(cell) + 1;
                        var IsLastColumn = VisibleCells.IndexOf(cell) == VisibleCells.Count - 1;

                        //Cell Width
                        var colWidth = new GridLength(0);
                        if (cell.Column.Visible)
                            colWidth = cell.Column.Width > 0 ? new GridLength(cell.Column.Width, GridUnitType.Pixel) : new GridLength(1, GridUnitType.Star);
                        getGridRow.ColumnDefinitions[Index].Width = colWidth;

                        var getBorder = getCellBorders.FirstOrDefault(x => x.Tag?.ToString() == cell.Key);

                        //Set Border
                        getBorder.BorderThickness = new Thickness(0, 0, IsLastColumn && !IsScroll ? 0 : 1, 0);

                        //Cell Index
                        Grid.SetColumn(getBorder, Index);
                    }
                }
            }
        }
        #endregion

        #region Grid Row Events
        private void CellBorder_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (!_rowSelectable.Value && !_rowMultiSelectable.Value)
                return;

            var border = sender as Border;
            var getKey = border.Tag.ToString();
            var getSlcRow = xGridData.Rows.FirstOrDefault(x => x.Key == getKey);
            if (e.ClickCount == 1)
            {
                RemoveLastSelected();
                if (getSlcRow.IsSelected)
                {
                    getSlcRow.IsSelected = false;
                    RemoveRowBackground(getSlcRow.Key);
                }
                else
                {
                    getSlcRow.IsSelected = true;
                    EnableRowBackground(getSlcRow.Key);
                }
            }
            else if (e.ClickCount == 2)
            {
                RemoveLastSelected();

                EnableRowBackground(getSlcRow.Key);
                getSlcRow.IsSelected = true;

                if (!_rowMultiSelectable.Value)
                    RowDoubleClick?.Invoke(sender, getSlcRow);
            }

            void RemoveLastSelected()
            {
                if (!_rowMultiSelectable.Value)
                {
                    var getLastSlcRow = xGridData.Rows.FirstOrDefault(x => x.IsSelected);
                    if (getLastSlcRow != null && getLastSlcRow.Key != getSlcRow.Key)
                    {
                        RemoveRowBackground(getLastSlcRow.Key);
                        getLastSlcRow.IsSelected = false;
                    }
                }
            }
        }

        private void HandleRowSelected()
        {
            var getSlcRows = xGridData.Rows.Where(x => x.IsSelected).ToList();
            if (getSlcRows.Count() > 0)
            {
                if (getSlcRows.Count() > 1 && !_rowMultiSelectable.Value)
                {
                    var getFlowRows = getSlcRows.Skip(1).ToList();
                    foreach (var item in getFlowRows)
                    {
                        item.IsSelected = false;
                        RemoveRowBackground(item.Key);
                    }
                }

                if (!_rowSelectable.Value)
                {
                    var getFirstRow = getSlcRows.FirstOrDefault();
                    RemoveRowBackground(getFirstRow.Key);
                }
            }
        }

        private void RemoveRowBackground(string Key)
        {
            var getBorder = GR_Data.FindChildByTag<Border>(Key);
            getBorder.Background = Colors.Transparent.GetBrushColor();
        }

        private void EnableRowBackground(string Key)
        {
            var getBorder = GR_Data.FindChildByTag<Border>(Key);
            getBorder.Background = _rowSelectedBackground;
        }
        #endregion

        #region Grid Row Text
        private void TextBox_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (sender is TextBox textBox)
            {
                if (e.ClickCount == 1)
                {
                    textBox.SelectionLength = 0;
                }
                else if (e.ClickCount == 2)
                {
                    textBox.SelectAll();
                    e.Handled = true;
                }
            }
        }

        private void TextBox_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Tab && sender is TextBox textBox)
            {
                textBox.SelectAll();
            }
        }

        private void TextBox_LostKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            if (sender is TextBox textBox)
            {
                textBox.SelectionLength = 0;
            }
        }

        private void HandleRowTextProperties()
        {
            if (!IsInitializedRowData)
                return;

            var getTextBlocks = GR_Data.FindChilds<TextBox>();
            foreach (TextBox item in getTextBlocks)
            {
                item.FontSize = _RowTxtFontSize.Value;
                item.FontWeight = _rowTxtFontWeight.Value;
                item.FontStyle = _rowTxtFontStyle.Value;
                item.Style = _RowTxtSelectable.Value ? this.FindResource("TextBoxEventStyle") as Style : null;
                item.SelectionBrush = _RowTxtSelectable.Value ? _RowTxtSelectedBrush : Colors.Transparent.GetBrushColor();
                item.SelectionOpacity = _rowTxtSelectionOpacity.Value;
            }
        }
        #endregion

        private void ColorPicker_SelectedColorChanged(object sender, RoutedPropertyChangedEventArgs<Color?> e)
        {
            Color? color = e.NewValue;
            if (color.HasValue)
            {
                Color selectedColor = color.Value;
                BR_Row_data.Background = XElementHelper.GetBrushColor(selectedColor);
            }
        }
    }

    public class DoubleToGridLengthConverter : IValueConverter
    {
        public object Convert(
            object value,
            Type targetType,
            object parameter,
            CultureInfo culture)
        {
            return new GridLength(
                value is double height ? height : 32,
                GridUnitType.Pixel);
        }

        public object ConvertBack(
            object value,
            Type targetType,
            object parameter,
            CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}

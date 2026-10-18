using System.Collections;
using System.Data;
using System.Globalization;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
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
        private bool _isVerticalScrollBarVisible = false;

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
            _headerTxtFontSize = XGridViewDefaults.HeaderTxtFontSize;
            _HeaderTxtFontWeight = XGridViewDefaults.HeaderTxtFontWeight;
            _HeaderTxtFontStyle = XGridViewDefaults.HeaderTxtFontStyle;

            //Grid Row
            _RowHeight = XGridViewDefaults.RowHeight;
            _RowTxtForeground = XGridViewDefaults.RowTxtForeground;
            _RowTxtFontSize = XGridViewDefaults.RowTxtFontSize;
            _RowTxtFontWeight = XGridViewDefaults.RowTxtFontWeight;
            _RowTxtFontStyle = XGridViewDefaults.RowTxtFontStyle;
            _rowTxtSelectable = XGridViewDefaults.RowTxtSelectable;
            _RowTxtSelectedBrush = XGridViewDefaults.RowTxtSelectedBrush;
            _rowTxtSelectionOpacity = XGridViewDefaults.RowTxtSelectionOpacity;

            //NoData
            _NoDataForeground = XGridViewDefaults.NoDataForeground;
            _NoDataBackGround = XGridViewDefaults.NoDataBackGround;
            _NoDataText = XGridViewDefaults.NoDataText;
            _NoDataFontSize = XGridViewDefaults.NoDataFontSize;
        }

        #region User Control
        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            SetGridBorder();
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
            var visibleColumns = xGridData.Columns.Where(x => x.Visible).ToList();
            foreach (var item in visibleColumns)
            {
                var columnIndex = visibleColumns.IndexOf(item);
                var IsLastColumn = columnIndex == visibleColumns.Count - 1;
                GenerateColumn(item, IsLastColumn);
            }

            //Rows
            SetXRows();

            //Generate Row
            GenerateRow();

            //Handle Scroll
            GridScroll();
            SetGridBorder();
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
                if (isVisible && !_isVerticalScrollBarVisible)
                {
                    var gR_Column_Scroll = GR_Columns._FindName<ColumnDefinition>(XGridViewDefaults.N_GR_Column_Scroll);
                    if (gR_Column_Scroll != null)
                    {
                        _isVerticalScrollBarVisible = true;
                        gR_Column_Scroll.Width = new GridLength(18, GridUnitType.Pixel);
                    }
                }
                else if (!isVisible && _isVerticalScrollBarVisible)
                {
                    _isVerticalScrollBarVisible = false;

                    var gR_Column_Scroll = GR_Columns._FindName<ColumnDefinition>(XGridViewDefaults.N_GR_Column_Scroll);
                    if (gR_Column_Scroll != null)
                        gR_Column_Scroll.Width = new GridLength(0, GridUnitType.Pixel);
                }

                // Bottom End
                if (IsInitializedDataSource)
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
            if (IsInitializedDataSource)
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

        #region Columns
        private void SetXColumns(Type itemType)
        {
            xGridData = new XGridData();

            var properties = itemType
                .GetProperties(BindingFlags.Public | BindingFlags.Instance)
                .ToList();

            var items = properties
                .Select((property, index) => new
                {
                    Property = property,
                    OriginalIndex = index,
                    Attribute = property.GetCustomAttribute<XDataBindAttribute>()
                })
                .ToList();

            var ordered = items
                .Where(x => x.Attribute?.Order > 0)
                .OrderBy(x => x.Attribute!.Order)
                .ThenBy(x => x.OriginalIndex)
                .ToList();

            var unordered = items
                .Where(x => x.Attribute?.Order <= 0 || x.Attribute?.Order == null)
                .OrderBy(x => x.OriginalIndex)
                .ToList();

            var result = new List<(PropertyInfo Property, XDataBindAttribute Attribute, int Order)>();

            var usedOrders = new HashSet<int>();

            foreach (var item in ordered)
            {
                int order = item.Attribute!.Order;

                while (usedOrders.Contains(order))
                    order++;

                usedOrders.Add(order);

                result.Add((
                    item.Property,
                    item.Attribute,
                    order
                ));
            }

            int nextOrder = 1;

            foreach (var item in unordered)
            {
                while (usedOrders.Contains(nextOrder))
                    nextOrder++;

                usedOrders.Add(nextOrder);

                result.Add((
                    item.Property,
                    item.Attribute,
                    nextOrder
                ));

                nextOrder++;
            }

            var columns = new XGridColumnCollection();

            foreach (var item in result.OrderBy(x => x.Order))
            {
                var attribute = item.Attribute;

                columns.Add(new XGridColumn
                {
                    Name = item.Property.Name,

                    DisplayName = attribute?.DisplayName.IsNullOrEmpty() == false ? attribute.DisplayName : item.Property.Name,

                    Visible = attribute?.Visible == true,

                    Order = item.Order,

                    Width = attribute?.Width ?? 0,

                    Property = item.Property,

                    Attribute = attribute
                });
            }

            xGridData.Columns.AddRange(columns);
        }

        private void GenerateColumn(XGridColumn column, bool isLastVisible)
        {
            var attribute = column.Attribute;

            if (!column.Visible)
                return;

            var columnWidth = column.Width > 0 ? new GridLength(column.Width, GridUnitType.Pixel) : new GridLength(1, GridUnitType.Star);

            var columnIndex = GR_Columns.ColumnDefinitions.Count;

            GR_Columns.ColumnDefinitions.Add(new ColumnDefinition
            {
                Width = columnWidth
            });

            var borderColumn = new Border
            {
                Style = XAppMethods.BorderInnerGridStyle(),
                BorderThickness = new Thickness(0, 0, isLastVisible ? 0 : 1, 0)
            };
            Grid.SetColumn(borderColumn, columnIndex);

            var textBlock = new TextBox
            {
                Text = column.DisplayName,
                Foreground = _headerTxtForeground,
                Background = Colors.Transparent.GetBrushColor(),
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center,
                BorderThickness = new Thickness(0),
                FontSize = _headerTxtFontSize.Value,
                FontWeight = _headerTxtFontWeight.Value,
                IsReadOnly = true,
                Style = this.FindResource("TextBoxEventStyle") as Style,
                FocusVisualStyle = null,
                FontStyle = _headerTxtFontStyle.Value
            };

            borderColumn.Child = textBlock;
            GR_Columns.Children.Add(borderColumn);

            //Generate Column For Scroll
            if (isLastVisible)
            {
                GR_Columns._UnregisterName(XGridViewDefaults.N_GR_Column_Scroll);

                columnIndex = columnIndex + 1;

                var gridColumn = new ColumnDefinition
                {
                    Width = new GridLength(0)
                };
                RegisterName(XGridViewDefaults.N_GR_Column_Scroll, gridColumn);

                GR_Columns.ColumnDefinitions.Add(gridColumn);

                borderColumn = new Border
                {
                    Style = XAppMethods.BorderInnerGridStyle(),
                    BorderThickness = new Thickness(1, 0, 0, 0)
                };
                Grid.SetColumn(borderColumn, columnIndex);

                GR_Columns.Children.Add(borderColumn);
            }
        }
        #endregion

        #region Rows
        private void SetXRows()
        {
            foreach (var item in getAllDataSources)
            {
                var row = new XGridRow();

                foreach (var column in xGridData.Columns)
                {
                    row.Cells.Add(new XGridCell
                    {
                        Column = column,
                        Value = column.Property.GetValue(item)
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
            {
                IsInitializedDataSource = false;
                return;
            }

            HandleGridVisibility();

            var visibleColumns = xGridData.Columns.Where(x => x.Visible).ToList();
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
                    Background = Colors.Transparent.GetBrushColor()
                };
                Grid.SetRow(rowBorder, rowIndex);

                //Row Grid
                var rowGrid = new Grid() { Background = Colors.Transparent.GetBrushColor() };
                rowBorder.Child = rowGrid;

                //Visible Columns
                foreach (var item in visibleColumns)
                {
                    var columnWidth = item.Width > 0 ? new GridLength(item.Width, GridUnitType.Pixel) : new GridLength(1, GridUnitType.Star);
                    var columnDefinition = new ColumnDefinition()
                    {
                        Width = columnWidth
                    };
                    rowGrid.ColumnDefinitions.Add(columnDefinition);
                }

                foreach (var cell in row.VisibleCells)
                {
                    var cellIndex = row.VisibleCells.IndexOf(cell);
                    var IsLastCell = cellIndex == row.VisibleCells.Count - 1;

                    var cellBorder = new Border
                    {
                        Style = XAppMethods.BorderInnerGridStyle(),
                        BorderThickness = new Thickness(0, 0, IsLastCell ? 0 : 1, 0),
                        Background = Colors.Transparent.GetBrushColor()
                    };
                    Grid.SetColumn(cellBorder, cellIndex);

                    var textBlock = new TextBox
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
                    };

                    cellBorder.Child = textBlock;
                    rowGrid.Children.Add(cellBorder);
                }

                //Add Row Border
                GR_Data.Children.Add(rowBorder);
            }

            //Set Border Height
            IsInitializedDataSource = true;
            SetHeightRow();
        }

        private void SetHeightRow()
        {
            if (GR_Data.RowDefinitions.Count == 0 || !IsInitializedDataSource)
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

        #region Row TextBox
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
            if (!IsInitializedDataSource)
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
            //Color? color = e.NewValue;
            //
            //if (color.HasValue)
            //{
            //    Color selectedColor = color.Value;
            //    GR_NoData.Background = XElementHelper.GetBrushColor(selectedColor);
            //}
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

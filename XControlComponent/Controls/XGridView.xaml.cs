using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Globalization;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;
using XControlComponents.Models;
using XControlComponents.Models.XGrid;
using XControlComponents.Tools;
using XControlComponents.Tools.Controls;
using XControlHelper;

namespace XControlComponents.Controls
{
    /// <summary>
    /// Interaction logic for XGridView.xaml
    /// </summary>
    public partial class XGridView : UserControl
    {
        private List<object> getAllDataSources { get; set; }
        public XGridData xGridData { get; set; } = null!;
        private bool SearchMode { get; set; }

        public XGridView()
        {
            InitializeComponent();

            //Border
            _BorderColor = XGridViewDefaults.BorderColor;
            _InnerBorderColor = XGridViewDefaults.InnerBorderColor;

            //Grid
            _HeaderHeight = XGridViewDefaults.HeaderHeight;
            _RowHeight = XGridViewDefaults.RowHeight;

            //NoData
            _NoDataForeground = XGridViewDefaults.NoDataForeground;
            _NoDataBackGround = XGridViewDefaults.NoDataBackGround;
            _NoDataText = XGridViewDefaults.NoDataText;
            _NoDataFontSize = XGridViewDefaults.NoDataFontSize;
        }

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
            }
        }

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

                foreach (var item in GR_Data.RowDefinitions)
                {
                    item.Height = new GridLength(_rowHeight.Value, GridUnitType.Pixel);
                }
            }
        }

        private Brush _backGround;
        public Brush _Background
        {
            get => _backGround;
            set
            {
                _backGround = value;

                if (_backGround == null)
                    _backGround = XGridViewDefaults.Background;

                BR_Grid.Background = _backGround;
            }
        }

        private Brush _headerBackGround;
        public Brush _HeaderBackground
        {
            get => _headerBackGround;
            set
            {
                _headerBackGround = value;

                if (_headerBackGround == null)
                    _headerBackGround = XGridViewDefaults.HeaderBackground;

                BR_Header.Background = _headerBackGround;
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
            //Clear Header
            GR_Columns.ColumnDefinitions.Clear();
            GR_Columns.Children.Clear();

            //Read Data Source
            getAllDataSources = ((IEnumerable)_dataSource).Cast<object>().ToList();
            var dataSourceType = _dataSource.GetType();

            //Read Header
            var itemType = dataSourceType.GetGenericArguments()[0];

            //Columns
            SetXColumns(itemType);

            //Generate Column
            var visibleColumns = xGridData.Columns.Where(x => x.Visible).ToList();
            for (int i = 0; i < visibleColumns.Count; i++)
            {
                GenerateColumn(visibleColumns[i], i == visibleColumns.Count - 1);
            }

            //Rows
            SetXRows();

            //Clear Rows
            ClearRows();

            //Generate Row
            //for (int row = 0; row < xGridData.Rows.Count; row++)
            //{
            //    var IsLastRow = row == xGridData.Rows.Count - 1;
            //
            //    var getRow = xGridData.Rows[row];
            //    var getRowCellCount = getRow.VisibleCells.Count;
            //    for (int cell = 0; cell < getRowCellCount; cell++)
            //    {
            //        var IsLastCell = cell == getRowCellCount - 1;
            //        GenerateRow(getRow.VisibleCells[cell], IsLastRow, IsLastCell);
            //    }
            //}
            //foreach (var row in xGridData.Rows)
            //{
            //    foreach (var cell in row.VisibleCells)
            //    {
            //
            //    }
            //}
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
                Style = XAppMethods.BorderInnerGrid(),
                BorderThickness = new Thickness(0, 0, isLastVisible ? 0 : 1, 0)
            };
            Grid.SetColumn(borderColumn, columnIndex);

            var textBlock = new TextBlock
            {
                Text = column.DisplayName,
                Foreground = XAppMethods.Color_Black_424242(),
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center,
                FontSize = 14,
                FontWeight = FontWeights.Normal,
                FontStyle = FontStyles.Normal
            };

            borderColumn.Child = textBlock;
            GR_Columns.Children.Add(borderColumn);
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

        private void ClearRows()
        {
            //GR_Data.RowDefinitions.Clear();
            //GR_Data.Children.Clear();

            GR_Row_NoData.Height = xGridData.Rows.Count > 0 ? new GridLength(0) : new GridLength(1, GridUnitType.Star);
            GR_Row_Data.Height = xGridData.Rows.Count > 0 ? new GridLength(1, GridUnitType.Star) : new GridLength(0);
        }

        private void GenerateRow(XGridCell cell, bool isLastRow, bool isLastColumn)
        {
            GR_Data.RowDefinitions.Add(new RowDefinition()
            {
                Height = new GridLength(_rowHeight.Value, GridUnitType.Pixel)
            });

            // Border اصلی
            var mainBorder = new Border
            {
                Style = (Style)FindResource("BorderInnerGrid"),
                BorderThickness = new Thickness(0, 0, 0, 1)
            };

            Grid.SetRow(mainBorder, 0);

            // Grid داخلی
            var innerGrid = new Grid();

            innerGrid.ColumnDefinitions.Add(new ColumnDefinition
            {
                Width = new GridLength(100)
            });

            innerGrid.ColumnDefinitions.Add(new ColumnDefinition());
            innerGrid.ColumnDefinitions.Add(new ColumnDefinition());
            innerGrid.ColumnDefinitions.Add(new ColumnDefinition());


            // Column 0
            var border0 = new Border
            {
                Style = (Style)FindResource("BorderInnerGrid"),
                BorderThickness = new Thickness(0, 0, 1, 0)
            };

            var textBlock = new TextBlock
            {
                Text = "ردیف",
                FontSize = 15,
                FontStyle = FontStyles.Normal,
                TextDecorations = null,
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center
            };

            border0.Child = textBlock;

            Grid.SetColumn(border0, 0);
            innerGrid.Children.Add(border0);


            // Column 1
            var border1 = new Border
            {
                Style = (Style)FindResource("BorderInnerGrid"),
                BorderThickness = new Thickness(0, 0, 1, 0)
            };

            var label = new Label
            {
                Content = "ردیف",
                FontSize = 15,
                FontStyle = FontStyles.Normal,
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center
            };

            border1.Child = label;

            Grid.SetColumn(border1, 1);
            innerGrid.Children.Add(border1);


            // Column 2
            var border2 = new Border
            {
                Style = (Style)FindResource("BorderInnerGrid"),
                BorderThickness = new Thickness(0, 0, 1, 0)
            };

            Grid.SetColumn(border2, 2);
            innerGrid.Children.Add(border2);


            // Column 3
            var border3 = new Border
            {
                Style = (Style)FindResource("BorderInnerGrid"),
                BorderThickness = new Thickness(0, 0, 0, 0)
            };

            Grid.SetColumn(border3, 3);
            innerGrid.Children.Add(border3);


            // قرار دادن Grid داخلی داخل Border اصلی
            mainBorder.Child = innerGrid;

            // اضافه کردن به GR_Data
            GR_Data.Children.Add(mainBorder);
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

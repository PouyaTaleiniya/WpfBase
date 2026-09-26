using System.Collections;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using XControlComponents.Models;
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
        private bool SearchMode { get; set; }

        public XGridView()
        {
            InitializeComponent();

            //Border
            _BorderColor = XGridViewDefaults.BorderColor;
            _InnerBorderColor = XGridViewDefaults.InnerBorderColor;

            //Grid
            _HeaderHeight = XGridViewDefaults.HeaderHeight;

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

                //Clear Grid Content
                //GR_Popup.Children.Clear();
                //GR_Popup.RowDefinitions.Clear();

                if (_dataSource != null)
                {
                    //Clear Header
                    GR_Columns.ColumnDefinitions.Clear();
                    GR_Columns.Children.Clear();

                    //Read Data Source
                    getAllDataSources = ((IEnumerable)_dataSource).Cast<object>().ToList();
                    var dataSourceType = _dataSource.GetType();

                    //Read Header
                    var itemType = dataSourceType.GetGenericArguments()[0];

                    var properties = GetOrderedProperties(itemType);

                    //Get Property
                    //var properties = itemType.GetProperties();
                    //var propertyInfos = itemType.GetProperties(BindingFlags.Public | BindingFlags.Instance);

                    //Visible Fields
                    var visiblePrp = properties.Where(p => p.GetCustomAttribute<XDataBindAttribute>()?.Visible == true).ToList();
                    for (int i = 0; i < visiblePrp.Count; i++)
                    {
                        GenerateColumn(visiblePrp[i], i == visiblePrp.Count - 1);
                    }

                    var x = getAllDataSources.FirstOrDefault();
                    //var getFirstRow =

                    getAllDataSources.ForEach(item =>
                    {
                        var getTypes = item.GetType();
                        var getFields = getTypes.GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
                        var getLstFields = getFields.ToList();

                        getLstFields.ForEach(item =>
                        {

                        });
                    });
                }
            }
        }

        private void GenerateColumn(PropertyInfo property, bool isLastVisible)
        {
            var attribute = property.GetCustomAttribute<XDataBindAttribute>();

            if (attribute.Visible)
            {
                var columnWidth = attribute.Width > 0 ? new GridLength(attribute.Width, GridUnitType.Pixel) : new GridLength(1, GridUnitType.Star);

                var columnIndex = GR_Columns.ColumnDefinitions.Count();

                GR_Columns.ColumnDefinitions.Add(new ColumnDefinition
                {
                    Width = columnWidth,
                });

                var borderColumn = new Border
                {
                    Style = (Style)FindResource("BorderInnerGrid"),
                    BorderThickness = new Thickness(0, 0, isLastVisible ? 0 : 1, 0)
                };
                Grid.SetColumn(borderColumn, columnIndex);

                var content = attribute.DisplayName.IsNullOrEmpty() ? "Empty Field" : attribute.DisplayName;
                var textBlock = new TextBlock
                {
                    Text = content,
                    Foreground = XAppMethods.Color_Black_424242(),
                    HorizontalAlignment = HorizontalAlignment.Center,
                    VerticalAlignment = VerticalAlignment.Center,
                    FontSize = 14,
                    FontWeight = FontWeights.Normal,
                    FontStyle = FontStyles.Normal,
                    //TextDecorations = TextDecorations.Underline
                };
                borderColumn.Child = textBlock;

                GR_Columns.Children.Add(borderColumn);
            }
        }

        private List<PropertyInfo> GetOrderedProperties(Type itemType)
        {
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

            var result = new List<(PropertyInfo Property, int Order)>();

            var usedOrders = new HashSet<int>();

            // اولویت با Order های تعیین شده توسط کاربر
            foreach (var item in ordered)
            {
                int order = item.Attribute!.Order;

                // اگر Order قبلاً استفاده شده،
                // اولین Order آزاد بعدی را پیدا کن
                while (usedOrders.Contains(order))
                {
                    order++;
                }

                usedOrders.Add(order);

                result.Add((item.Property, order));
            }

            // Property های بدون Order
            int nextOrder = 1;

            foreach (var item in unordered)
            {
                while (usedOrders.Contains(nextOrder))
                {
                    nextOrder++;
                }

                usedOrders.Add(nextOrder);

                result.Add((item.Property, nextOrder));
                nextOrder++;
            }

            return result
                .OrderBy(x => x.Order)
                .Select(x => x.Property)
                .ToList();
        }

        private void ColorPicker_SelectedColorChanged(object sender, RoutedPropertyChangedEventArgs<Color?> e)
        {
            Color? color = e.NewValue;

            if (color.HasValue)
            {
                Color selectedColor = color.Value;
                GR_NoData.Background = XElementHelper.GetBrushColor(selectedColor);
            }
        }
    }
}

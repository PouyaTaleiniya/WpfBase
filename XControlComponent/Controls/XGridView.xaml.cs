using System.Collections;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
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
            _GridBorderColor = XGridViewDefaults.BorderColor;
            _GridInnerBorderColor = XGridViewDefaults.InnerBorderColor;

            //Grid
            _RowHeaderHeight = XGridViewDefaults.RowHeaderHeight;

            //NoData
            _NoDataForeground = XGridViewDefaults.NoDataForeground;
            _NoDataBackGround = XGridViewDefaults.NoDataBackGround;
            _NoDataText = XGridViewDefaults.NoDataText;
            _NoDataFontSize = XGridViewDefaults.NoDataFontSize;
        }

        #region Border
        private Brush _gridBorderColor;
        public Brush _GridBorderColor
        {
            get => _gridBorderColor;
            set
            {
                _gridBorderColor = value;

                if (_gridBorderColor == null)
                    _gridBorderColor = XGridViewDefaults.BorderColor;

                BR_Grid.BorderBrush = _gridBorderColor;
            }
        }

        private Brush _gridInnerBorderColor;
        public Brush _GridInnerBorderColor
        {
            get => _gridInnerBorderColor;
            set
            {
                _gridInnerBorderColor = value;

                if (_gridInnerBorderColor == null)
                    _gridInnerBorderColor = XGridViewDefaults.InnerBorderColor;

                BR_Columns.BorderBrush = _gridInnerBorderColor;
            }
        }
        #endregion

        #region Grid
        private double? _rowHeaderHeight;
        public double? _RowHeaderHeight
        {
            get => _rowHeaderHeight;
            set
            {
                _rowHeaderHeight = value;

                if (_rowHeaderHeight == null)
                    _rowHeaderHeight = XGridViewDefaults.RowHeaderHeight;

                if (_rowHeaderHeight < 30)
                    _rowHeaderHeight = 30;

                GR_Row_Columns.Height = new GridLength(_rowHeaderHeight.Value, GridUnitType.Pixel);
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
                    getAllDataSources = ((IEnumerable)_dataSource).Cast<object>().ToList();

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

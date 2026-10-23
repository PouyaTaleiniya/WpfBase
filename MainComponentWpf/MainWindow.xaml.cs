using System.Windows;
using System.Windows.Media;
using XControlComponents.Models;
using XControlComponents.Tools;
using XControlHelper;

namespace MainComponentWpf
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : BaseWindow
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void BaseWindow_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            //xComboBoxData1._Items = new List<string>() { "ایتم 1", "ایتم 2", "ایتم 3" };

            var result = new List<DataBindDto>()
            {
                new DataBindDto
                {
                    Id = 1,
                    FirstName = "F1",
                    LastName = "L1",
                    NationalCode = "NC1"
                },
                new DataBindDto
                {
                    Id = 2,
                    FirstName = "F1",
                    LastName = "L1",
                    NationalCode = "NC1"
                },
                 new DataBindDto
                {
                    Id = 3,
                    FirstName = "F3",
                    LastName = "L3",
                    NationalCode = "NC3"
                }
            };
            xGridView._DataSource = result;
        }

        private void ColorPicker_SelectedColorChanged(object sender, RoutedPropertyChangedEventArgs<Color?> e)
        {
            Color? color = e.NewValue;

            if (color.HasValue)
            {
                //colorPicker.SelectedColor = XElementHelper.GetColorByBrush(BR_Grid.BorderBrush);

                Color selectedColor = color.Value;
                //BR_Test.BorderBrush = XElementHelper.GetColor(selectedColor);
                // اینجا مقدار رنگ در لحظه در دسترسه
                byte r = selectedColor.R;
                byte g = selectedColor.G;
                byte b = selectedColor.B;
                byte a = selectedColor.A;

                string hexColor = $"#{r:X2}{g:X2}{b:X2}";
                //Lb_ColorValue.Text = hexColor;
            }
        }

        private void xGridView_RowDoubleClick(object sender, object Data)
        {
            var x = sender as DataBindDto;
            //var y = xGridView._DataSourceSelected as DataBindDto;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var get = xGridView._DataSourceSelected<DataBindDto>();
            if (get != null)
            {
                get!.FirstName = get!.FirstName + "E";
                get!.LastName = get!.LastName + "E";
                get!.NationalCode = get!.NationalCode + "E";
                xGridView.UpdateRow(get);
            }

            //var getDtos = xGridView.GetDataSource<DataBindDto>();
            //foreach (var item in getDtos)
            //{
            //    item!.FirstName = item!.FirstName + "E";
            //    item!.LastName = item!.LastName + "E";
            //    item!.NationalCode = item!.NationalCode + "E";
            //}
            //xGridView.UpdateRows(getDtos);
        }
    }

    public class DataBindDto
    {
        [XDataBind(DisplayName = "ردیف", Order = 1, Width = 100)]
        public int Id { get; set; }

        [XDataBind(DisplayName = "نام", Order = 4)]
        public string? FirstName { get; set; }

        [XDataBind(DisplayName = "نام خانوادگی", Order = 2)]
        public string? LastName { get; set; }

        [XDataBind(DisplayName = "کد ملی", Order = 3)]
        public string? NationalCode { get; set; }

        [XDataBind(Visible = false)]
        public string? GuidKey { get; set; }
    }
}
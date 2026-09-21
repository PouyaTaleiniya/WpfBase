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

            var result = new List<DataBindDto>();
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

    }

    public class DataBindDto
    {
        [XDataBind(DisplayName = "ردیف", Order = 1, Width = 12)]
        public int Id { get; set; }

        [XDataBind(DisplayName = "نام")]
        public string? FirstName { get; set; }

        [XDataBind(DisplayName = "نام خانوادگی")]
        public string? LastName { get; set; }

        [XDataBind(DisplayName = "کد ملی")]
        public string? NationalCode { get; set; }

        [XDataBind(Visible = false)]
        public string? GuidKey { get; set; }
    }
}
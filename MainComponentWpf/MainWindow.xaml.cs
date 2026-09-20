using System.Windows;
using System.Windows.Media;
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
        [DataBind("ردیف")]
        public int Id { get; set; }

        [DataBind("نام")]
        public string? FirstName { get; set; }

        [DataBind("نام خانوادگی")]
        public string? LastName { get; set; }

        [DataBind("کد ملی")]
        public string? NationalCode { get; set; }

        [DataBind(Visible = false)]
        public string? GuidKey { get; set; }
    }

    [AttributeUsage(AttributeTargets.Property)]
    public class DataBindAttribute : Attribute
    {
        public string? DisplayName { get; }
        public int Order { get; set; }
        public bool Visible { get; set; } = true;

        public DataBindAttribute(string displayName = "")
        {
            DisplayName = displayName;
        }
    }
}
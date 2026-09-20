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

    }
}
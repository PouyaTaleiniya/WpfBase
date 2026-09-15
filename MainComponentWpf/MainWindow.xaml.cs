using XControlComponents.Tools;

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
            xComboBoxData1._Items = new List<string>() { "ایتم 1", "ایتم 2", "ایتم 3" };
        }
    }

    public class DataBindDto
    {

    }
}
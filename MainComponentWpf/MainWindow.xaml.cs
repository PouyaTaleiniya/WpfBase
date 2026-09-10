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
            xComboBoxData._Items.Add("Item1");
        }
    }

    public class DataBindDto
    {

    }
}
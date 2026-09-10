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
            xComboBoxData.ite.Add("Item1"); xComboBoxData._DataSourceSelected
        }
    }

    public class DataBindDto
    {

    }
}
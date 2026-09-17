using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

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
        }

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
    }
}

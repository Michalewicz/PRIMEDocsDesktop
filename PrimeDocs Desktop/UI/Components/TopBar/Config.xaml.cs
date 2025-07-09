using PrimeDocs_Desktop.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PrimeDocs_Desktop
{
    /// <summary>
    /// Interação lógica para Config.xam
    /// </summary>
    public partial class Config : UserControl
    {
        public Config()
        {
            InitializeComponent();
        }
        private void btInitialWindowConfig_Click(object sender, RoutedEventArgs e)
        {
            Configurations config = new Configurations();
            config.ShowDialog();
        }
    }
}

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

namespace PrimeDocs_Desktop.UI.Components
{
    /// <summary>
    /// Interação lógica para SearchFilter.xam
    /// </summary>
    public partial class SearchFilter : UserControl
    {
        public SearchFilter()
        {
            InitializeComponent();
            cbSearchMode.SelectedIndex = 0;
        }

        private void cbSearchMode_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cbSearchMode.SelectedIndex == 0) // Por Data
            {
                SearchDate.Visibility = Visibility.Visible;
                SearchRange.Visibility = Visibility.Collapsed;
            }
            else if (cbSearchMode.SelectedIndex == 1) // Por Intervalo de Datas
            {
                SearchDate.Visibility = Visibility.Collapsed;
                SearchRange.Visibility = Visibility.Visible;
            }


        }
    }
}

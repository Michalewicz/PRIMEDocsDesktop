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
using System.Windows.Shapes;

namespace PrimeDocs_Desktop.UI
{
    /// <summary>
    /// Lógica interna para Configurations.xaml
    /// </summary>
    public partial class Configurations : Window
    {
        public Configurations()
        {
            InitializeComponent();

            this.StateChanged += WindowStateChanged;
            tbpInitialWindowTopBar.windowControlTopBar.btMaximize.Visibility = Visibility.Hidden;
            tbpInitialWindowTopBar.windowControlTopBar.btMinimize.Visibility = Visibility.Hidden;
            tbpInitialWindowTopBar.tbTopBarTitle.Text = "Configurações";

        }
        private void WindowStateChanged(object? sender, EventArgs e)
        {
            if (WindowState == WindowState.Maximized)
            {
                MainBorder.CornerRadius = new CornerRadius(0);
                MainBorder.Padding = new Thickness(0, 5, 0, 0);
                tbpInitialWindowTopBar.TopBarBorder.CornerRadius = new CornerRadius(0);
                tbpInitialWindowTopBar.windowControlTopBar.btClose.Style = (Style)FindResource("WindowControlCloseMaximizedButtonStyle");
            }
            else if (WindowState == WindowState.Normal)
            {
                MainBorder.CornerRadius = new CornerRadius(13);
                MainBorder.Padding = new Thickness(0);
                tbpInitialWindowTopBar.TopBarBorder.CornerRadius = new CornerRadius(7);
                tbpInitialWindowTopBar.windowControlTopBar.btClose.Style = (Style)FindResource("WindowControlCloseNormalButtonStyle");
            }
        }
    }
}

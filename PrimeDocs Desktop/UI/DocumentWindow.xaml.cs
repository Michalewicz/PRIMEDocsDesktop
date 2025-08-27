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

namespace PrimeDocs_Desktop
{
    /// <summary>
    /// Lógica interna para DocumentWindow.xaml
    /// </summary>
    public partial class DocumentWindow : Window
    {
        public DocumentWindow(String typeExtension)
        {
            InitializeComponent();
            this.StateChanged += WindowStateChanged;
            tbpInitialWindowTopBar.tbTopBarTitle.Text = "Documento - PrimeDocs";

            UpdateWindowState(this.WindowState);
        }

        private void WindowStateChanged(object? sender, EventArgs e)
        {
            UpdateWindowState(this.WindowState);
        }
        public void UpdateWindowState(WindowState currentState)
        {
            if (currentState == WindowState.Maximized)
            {
                MainBorder.CornerRadius = new CornerRadius(0);
                MainBorder.Padding = new Thickness(0, 5, 0, 0);
                SecondaryBorder.BorderThickness = new Thickness(0, 0, 5, 53);
                tbpInitialWindowTopBar.btInitialWindowHome.Margin = new Thickness(7, 0, 0, 0);
                tbpInitialWindowTopBar.TopBarBorder.CornerRadius = new CornerRadius(0);
                tbpInitialWindowTopBar.windowControlTopBar.btClose.Style = (Style)FindResource("WindowControlCloseMaximizedButtonStyle");
            }
            else // Abrange WindowState.Normal
            {
                MainBorder.CornerRadius = new CornerRadius(13);
                MainBorder.Padding = new Thickness(0);
                SecondaryBorder.BorderThickness = new Thickness(0, 0, 0, 0);
                tbpInitialWindowTopBar.btInitialWindowHome.Margin = new Thickness(0);
                tbpInitialWindowTopBar.TopBarBorder.CornerRadius = new CornerRadius(7);
                tbpInitialWindowTopBar.windowControlTopBar.btClose.Style = (Style)FindResource("WindowControlCloseNormalButtonStyle");
            }
        }
    }
}

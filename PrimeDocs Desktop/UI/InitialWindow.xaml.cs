using PrimeDocs_Desktop.UI;
using PrimeDocs_Desktop.UI.Components;
using System;
using System.Configuration;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PrimeDocs_Desktop
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            newDocumentControl.CollapsedChanged += NewDocumentControl_CollapsedChanged;

            // Tirar arredondamentos quando maximizado
            this.StateChanged += WindowStateChanged;
        }

        private void CollapseChange()
        {
            if (newDocumentControl.documentCollapsed)
                openDocumentControl.Margin = new Thickness(0, -235, 0, 0);
            else
                openDocumentControl.Margin = new Thickness(0, 0, 0, 0);
        }
        private void NewDocumentControl_CollapsedChanged(object sender, EventArgs e)
        {
            CollapseChange();
        }
        private void WindowStateChanged(object sender, EventArgs e)
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
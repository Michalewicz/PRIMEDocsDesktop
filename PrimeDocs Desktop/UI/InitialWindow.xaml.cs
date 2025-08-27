using PrimeDocs_Desktop.UI;
using PrimeDocs_Desktop.UI.Components;
using System;
using System.Windows;

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
            this.StateChanged += WindowStateChanged;

            tbpInitialWindowTopBar.tbTopBarTitle.Text = "Início";
            if (this.WindowState == WindowState.Maximized)
                MainBorder.Margin = new Thickness(0, 5, 0, 0);

            UpdateWindowState(this.WindowState);
            UpdateLayoutMargin();
        }
        private void UpdateLayoutMargin()
        {
            if (newDocumentControl.documentCollapsed)
            {
                openDocumentControl.Margin = new Thickness(0, -235, 0, 0);
            }
            else
            {
                openDocumentControl.Margin = new Thickness(0, 0, 0, 0);
            }
        }
        private void NewDocumentControl_CollapsedChanged(object? sender, EventArgs e)
        {
            UpdateLayoutMargin();
        }
        public void UpdateWindowState(WindowState currentState)
        {
            if (currentState == WindowState.Maximized)
            {
                MainBorder.CornerRadius = new CornerRadius(0);
                MainBorder.Margin = new Thickness(0, 5, 0, 0); 
                tbpInitialWindowTopBar.btInitialWindowHome.Margin = new Thickness(7, 0, 5, 0);
                tbpInitialWindowTopBar.TopBarBorder.CornerRadius = new CornerRadius(0);
                tbpInitialWindowTopBar.windowControlTopBar.btClose.Style = (Style)FindResource("WindowControlCloseMaximizedButtonStyle");
            }
            else 
            {
                MainBorder.CornerRadius = new CornerRadius(13);
                MainBorder.Margin = new Thickness(0); 
                tbpInitialWindowTopBar.btInitialWindowHome.Margin = new Thickness(0, 0, 5, 0);
                tbpInitialWindowTopBar.TopBarBorder.CornerRadius = new CornerRadius(7);
                tbpInitialWindowTopBar.windowControlTopBar.btClose.Style = (Style)FindResource("WindowControlCloseNormalButtonStyle");
            }
        }
        private void WindowStateChanged(object? sender, EventArgs e)
        {
            UpdateWindowState(this.WindowState);
        }
    }
}

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
    /// Interação lógica para WindowControl.xam
    /// </summary>
    public partial class WindowControl : UserControl
    {
        public WindowControl()
        {
            InitializeComponent();
        }

        private Window GetParentWindow()
        {
            return Window.GetWindow(this);
        }

        private void btInitialWindowMaximize_Click(object sender, RoutedEventArgs e)
        {
            var window = GetParentWindow();
            if (window != null)
            {
                if (window.WindowState == WindowState.Maximized)
                    window.WindowState = WindowState.Normal;
                else
                    window.WindowState = WindowState.Maximized;
            }
        }

        private void btInitialWindowClose_Click(object sender, RoutedEventArgs e)
        {
            var window = GetParentWindow();
            window?.Close();
        }

        private void btInitialWindowMinimize_Click(object sender, RoutedEventArgs e)
        {
            var window = GetParentWindow();
            if (window != null)
                window.WindowState = WindowState.Minimized;
        }
    }
}

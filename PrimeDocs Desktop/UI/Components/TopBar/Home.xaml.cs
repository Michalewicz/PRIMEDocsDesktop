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
    /// Interação lógica para Home.xam
    /// </summary>
    public partial class Home : UserControl
    {
        public Home()
        {
            InitializeComponent();
        }

        private void btInitialWindowHome_Click(object sender, RoutedEventArgs e)
        {
            Window actualWindow = Window.GetWindow(this);
            if (actualWindow is DocumentWindow)
            {
                var actual = Application.Current.Windows.OfType<Window>().SingleOrDefault(w => w.IsActive);

                var initialWindow = new MainWindow
                {
                    WindowStartupLocation = WindowStartupLocation.Manual
                };

                if (actual != null && !(actualWindow.WindowState == WindowState.Maximized))
                {
                    initialWindow.Left = actual.Left;
                    initialWindow.Top = actual.Top;
                    initialWindow.Width = actual.Width;
                    initialWindow.Height = actual.Height;

                }
                else
                {

                    initialWindow.WindowState = WindowState.Maximized;
                    initialWindow.UpdateWindowState(WindowState.Maximized);
                }
                initialWindow.Show();
                actualWindow.Close();
            }
        }
    }
}

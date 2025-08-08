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
using System.Windows.Media.TextFormatting;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PrimeDocs_Desktop.UI.Components
{
    /// <summary>
    /// Interação lógica para NewDocument.xam
    /// </summary>
    public partial class NewDocument : UserControl
    {
        public bool documentCollapsed = false;

        public NewDocument()
        {
            InitializeComponent();
        }
    
        private void btInitialWindowDocumentText_Click(object sender, RoutedEventArgs e)
        {
            Window actualWindow = Window.GetWindow(this);
            var actual = Application.Current.Windows.OfType<Window>().SingleOrDefault(w => w.IsActive);

                var documentWindow = new DocumentWindow("txt")
                {
                    WindowStartupLocation = WindowStartupLocation.Manual
                };

            if (actual != null && !(actualWindow.WindowState == WindowState.Maximized))
            {
                documentWindow.Left = actual.Left;
                documentWindow.Top = actual.Top;
                documentWindow.Width = actual.Width;
                documentWindow.Height = actual.Height;
            }
            else
            {
                documentWindow.WindowState = WindowState.Maximized;
            }
            documentWindow.Show();
            actualWindow.Close();
        }

        private void btInitialWindowDocumentA4_Click(object sender, RoutedEventArgs e)
        {
            Window actualWindow = Window.GetWindow(this);
            var actual = Application.Current.Windows.OfType<Window>().SingleOrDefault(w => w.IsActive);

            var documentWindow = new DocumentWindow("docx")
            {
                WindowStartupLocation = WindowStartupLocation.Manual
            };

            if (actual != null && !(actualWindow.WindowState == WindowState.Maximized))
            {
                documentWindow.Left = actual.Left;
                documentWindow.Top = actual.Top;
                documentWindow.Width = actual.Width;
                documentWindow.Height = actual.Height;
            } else
            {
                documentWindow.WindowState = WindowState.Maximized;
            }
                documentWindow.Show();
            actualWindow.Close();

        }

        private void btInitialWindowDocumentMD_Click(object sender, RoutedEventArgs e)
        {
            Window actualWindow = Window.GetWindow(this);
            var actual = Application.Current.Windows.OfType<Window>().SingleOrDefault(w => w.IsActive);

            var documentWindow = new DocumentWindow("md")
            {
                WindowStartupLocation = WindowStartupLocation.Manual
            };

            if (actual != null && !(actualWindow.WindowState == WindowState.Maximized))
            {
                documentWindow.Left = actual.Left;
                documentWindow.Top = actual.Top;
                documentWindow.Width = actual.Width;
                documentWindow.Height = actual.Height;
            }
            else
            {
                documentWindow.WindowState = WindowState.Maximized;
            }
            documentWindow.Show();
            actualWindow.Close();
        }
        private void rotateCollapseButton(Button button)
        {
            double newAngle;
            if (button.RenderTransform is RotateTransform existentRotation)
            {
                double angle = existentRotation.Angle;
                if (angle == 180)
                    newAngle = 0;
                else
                    newAngle = 180;
            }
            else
            {
                newAngle = 180; // Default angle if no rotation exists
            }
            RotateTransform rotation = new RotateTransform(newAngle);
            button.RenderTransform = rotation;
            button.RenderTransformOrigin = new Point(0.5, 0.5);
        }
        public event EventHandler CollapsedChanged;

        private void btInitialWindowDocumentCreationCollapse_Click(object sender, RoutedEventArgs e)
        {
            rotateCollapseButton(btInitialWindowDocumentCreationCollapse);

            if (grInitialWindowDocumentCreationNew.Visibility == Visibility.Visible)
            {
                documentCollapsed = true;
                grInitialWindowDocumentCreationNew.Visibility = Visibility.Collapsed;
            }
            else
            {
                documentCollapsed = false;
                grInitialWindowDocumentCreationNew.Visibility = Visibility.Visible;
            }

            // Dispara evento
            CollapsedChanged?.Invoke(this, EventArgs.Empty);
        }

    }
}

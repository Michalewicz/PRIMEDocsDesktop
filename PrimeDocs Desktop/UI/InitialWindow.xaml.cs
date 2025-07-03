using PrimeDocs_Desktop.UI;
using System.Configuration;
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
        }

        private void btInitialWindowProfile_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btInitialWindowConfig_Click(object sender, RoutedEventArgs e)
        {
            Configurations config = new Configurations();
            config.ShowDialog();
        }

        private void btInitialWindowDocumentText_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btInitialWindowDocumentA4_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btInitialWindowDocumentMD_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btInitialWindowDocumentCreationCollapse_Click(object sender, RoutedEventArgs e)
        {
            rotateCollapseButton(btInitialWindowDocumentCreationCollapse);

            if (grInitialWindowDocumentCreationNew.Visibility == Visibility.Visible)
            {
                grInitialWindowDocumentCreationNew.Visibility = Visibility.Collapsed;
                grInitialWindowOpenDocument.Margin = new Thickness(10, 120, 10, 0);
            }
            else
            {
                grInitialWindowDocumentCreationNew.Visibility = Visibility.Visible;
                grInitialWindowOpenDocument.Margin = new Thickness(10, 360, 10, 0);
            }
        }

        private void btInitialWindowOpenDocumentCollapse_Click(object sender, RoutedEventArgs e)
        {
            rotateCollapseButton(btInitialWindowOpenDocumentCollapse);
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

        
    }
}
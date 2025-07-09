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
    /// Interação lógica para OpenDocument.xam
    /// </summary>
    public partial class OpenDocument : UserControl
    {
        public OpenDocument()
        {
            InitializeComponent();

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
        private void btInitialWindowOpenDocumentCollapse_Click(object sender, RoutedEventArgs e)
        {
            rotateCollapseButton(btInitialWindowOpenDocumentCollapse);
            if (grInitialWindowOpenDocumentNew.Visibility == Visibility.Visible)
            {
                grInitialWindowOpenDocumentNew.Visibility = Visibility.Collapsed;
            }
            else
            {
                grInitialWindowOpenDocumentNew.Visibility = Visibility.Visible;
            }
        }
        private void btInitialWindowOpenDocumentCollapseFilter_Click(object sender, RoutedEventArgs e)
        {
            rotateCollapseButton(btInitialWindowOpenDocumentCollapseFilter);
        }
    }
}
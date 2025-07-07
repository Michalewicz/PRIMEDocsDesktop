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
using PrimeDocs_Desktop.UI;

namespace PrimeDocs_Desktop
{
    /// <summary>
    /// Interação lógica para TopBarPrimeDocs.xam
    /// </summary>
    public partial class TopBarPrimeDocs : UserControl
    {
        private bool isDraggingFromMaximized = false;

        public TopBarPrimeDocs()
        {
            InitializeComponent();
            this.MouseDown += TopBar_MouseDown;
            this.MouseMove += TopBar_MouseMove;
        }

        private void TopBar_MouseDown(object sender, MouseButtonEventArgs e)
        {
            var window = Window.GetWindow(this);
            if (window == null) return;

            if (e.ChangedButton == MouseButton.Left)
            {
                if (e.ClickCount == 2)
                {
                    // Duplo clique: maximiza/restaura
                    if (window.WindowState == WindowState.Maximized)
                        window.WindowState = WindowState.Normal;
                    else
                        window.WindowState = WindowState.Maximized;
                }
                else if (window.WindowState == WindowState.Maximized)
                {
                    // Clique simples: prepara para arrastar se mover
                    isDraggingFromMaximized = true;
                }
                else if (window.WindowState == WindowState.Normal)
                {
                    // Permite arrastar normalmente quando não está maximizada
                    try
                    {
                        window.DragMove();
                    }
                    catch (InvalidOperationException)
                    {
                        // Ignora a exceção, pois pode ocorrer se o usuário soltar o mouse muito rápido
                    }
                }
            }
        }

        private void TopBar_MouseMove(object sender, MouseEventArgs e)
        {
            var window = Window.GetWindow(this);
            if (window == null) return;

            if (isDraggingFromMaximized && e.LeftButton == MouseButtonState.Pressed)
            {
                isDraggingFromMaximized = false;

                // Calcula a posição do mouse relativa à janela maximizada
                var mouseX = e.GetPosition(this).X;
                double percentHorizontal = mouseX / ActualWidth;
                window.WindowState = WindowState.Normal;

                window.Dispatcher.InvokeAsync(() =>
                {
                    double screenWidth = SystemParameters.WorkArea.Width;
                    double screenLeft = SystemParameters.WorkArea.Left;
                    double newLeft = screenLeft + (screenWidth - window.Width) * percentHorizontal;
                    window.Left = newLeft;
                    window.Top = SystemParameters.WorkArea.Top + 10;

                    try
                    {
                        window.DragMove();
                    }
                    catch (InvalidOperationException)
                    {
                        // Ignora a exceção, pois pode ocorrer se o usuário soltar o mouse muito rápido
                    }
                }, System.Windows.Threading.DispatcherPriority.ApplicationIdle);
            }
            else if (e.LeftButton != MouseButtonState.Pressed)
            {
                isDraggingFromMaximized = false;
            }
        }
    }
}

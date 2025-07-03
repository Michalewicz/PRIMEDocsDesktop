using PrimeDocs_Desktop.UI;
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
        private bool isDraggingFromMaximized = false;

        public MainWindow()
        {
            InitializeComponent();
            Loaded += MainWindow_Loaded;
            brInitialWindowTopBar.MouseMove += brInitialWindowTopBar_MouseMove;
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            var hwndSource = (HwndSource)PresentationSource.FromVisual(this);
            if (hwndSource != null)
                hwndSource.AddHook(WindowProc);
        }

        private const int WM_GETMINMAXINFO = 0x0024;

        [StructLayout(LayoutKind.Sequential)]
        private struct POINT
        {
            public int X;
            public int Y;
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct MINMAXINFO
        {
            public POINT ptReserved;
            public POINT ptMaxSize;
            public POINT ptMaxPosition;
            public POINT ptMinTrackSize;
            public POINT ptMaxTrackSize;
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct MONITORINFO
        {
            public int cbSize;
            public RECT rcMonitor;
            public RECT rcWork;
            public int dwFlags;
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct RECT
        {
            public int left;
            public int top;
            public int right;
            public int bottom;
        }

        [DllImport("user32.dll")]
        private static extern IntPtr MonitorFromWindow(IntPtr hwnd, int dwFlags);

        [DllImport("user32.dll", SetLastError = true)]
        private static extern bool GetMonitorInfo(IntPtr hMonitor, ref MONITORINFO lpmi);

        private IntPtr WindowProc(IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam, ref bool handled)
        {
            if (msg == WM_GETMINMAXINFO)
            {
                WmGetMinMaxInfo(hwnd, lParam);
                handled = true;
            }
            return IntPtr.Zero;
        }

        private void WmGetMinMaxInfo(IntPtr hwnd, IntPtr lParam)
        {
            var mmi = Marshal.PtrToStructure<MINMAXINFO>(lParam);

            IntPtr monitor = MonitorFromWindow(hwnd, 2);
            if (monitor != IntPtr.Zero)
            {
                var monitorInfo = new MONITORINFO();
                monitorInfo.cbSize = Marshal.SizeOf(typeof(MONITORINFO));
                if (GetMonitorInfo(monitor, ref monitorInfo))
                {
                    RECT rcWorkArea = monitorInfo.rcWork;
                    RECT rcMonitorArea = monitorInfo.rcMonitor;
                    mmi.ptMaxPosition.X = rcWorkArea.left - rcMonitorArea.left;
                    mmi.ptMaxPosition.Y = rcWorkArea.top - rcMonitorArea.top;
                    mmi.ptMaxSize.X = rcWorkArea.right - rcWorkArea.left;
                    mmi.ptMaxSize.Y = rcWorkArea.bottom - rcWorkArea.top;
                }
            }

            // Adicione este trecho para respeitar o tamanho mínimo do XAML
            mmi.ptMinTrackSize.X = (int)this.MinWidth;
            mmi.ptMinTrackSize.Y = (int)this.MinHeight;

            Marshal.StructureToPtr(mmi, lParam, true);
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

        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (this.ActualWidth < 900)
                tbInitialWindowName.Visibility = Visibility.Collapsed;
            else
                tbInitialWindowName.Visibility = Visibility.Visible;
        }
        private void WindowStateChange(bool doubleClick, MouseButtonEventArgs e)
        {
            if (WindowState == WindowState.Maximized)
            {
                var mouseX = e.GetPosition(this).X;
                double percentHorizontal = mouseX / ActualWidth;
                if (doubleClick)
                    WindowState = WindowState.Normal;

                this.Dispatcher.InvokeAsync(() =>
                {
                    double screenWidth = SystemParameters.WorkArea.Width;
                    double screenLeft = SystemParameters.WorkArea.Left;
                    double newLeft = screenLeft + (screenWidth - this.Width) * percentHorizontal;
                    this.Left = newLeft;
                    this.Top = SystemParameters.WorkArea.Top + 10;

                    if (WindowState == WindowState.Normal)
                    {
                        try
                        {
                            DragMove();
                        }
                        catch (InvalidOperationException)
                        {
                            // Ignora a exceção, pois pode ocorrer se o usuário soltar o mouse muito rápido
                        }
                    }
                }, System.Windows.Threading.DispatcherPriority.ApplicationIdle);
            }
            else
            {
                try
                {
                    if (doubleClick)
                        WindowState = WindowState.Maximized;
                    DragMove();
                }
                catch (InvalidOperationException)
                {
                    // Ignora a exceção, pois pode ocorrer se o usuário soltar o mouse muito rápido
                }
            }
        }
        private void brInitialWindowTopBar_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                if (e.ClickCount == 2)
                {
                    // Duplo clique: maximiza/restaura
                    if (WindowState == WindowState.Maximized)
                        WindowState = WindowState.Normal;
                    else
                        WindowState = WindowState.Maximized;
                }
                else if (WindowState == WindowState.Maximized)
                {
                    // Clique simples: prepara para arrastar se mover
                    isDraggingFromMaximized = true;
                }
                else if (WindowState == WindowState.Normal)
                {
                    // Permite arrastar normalmente quando não está maximizada
                    try
                    {
                        DragMove();
                    }
                    catch (InvalidOperationException)
                    {
                        // Ignora a exceção, pois pode ocorrer se o usuário soltar o mouse muito rápido
                    }
                }
            }
        }

        private void brInitialWindowTopBar_MouseMove(object sender, MouseEventArgs e)
        {
            if (isDraggingFromMaximized && e.LeftButton == MouseButtonState.Pressed)
            {
                isDraggingFromMaximized = false;

                // Calcula a posição do mouse relativa à janela maximizada
                var mouseX = e.GetPosition(this).X;
                double percentHorizontal = mouseX / ActualWidth;
                WindowState = WindowState.Normal;

                this.Dispatcher.InvokeAsync(() =>
                {
                    double screenWidth = SystemParameters.WorkArea.Width;
                    double screenLeft = SystemParameters.WorkArea.Left;
                    double newLeft = screenLeft + (screenWidth - this.Width) * percentHorizontal;
                    this.Left = newLeft;
                    this.Top = SystemParameters.WorkArea.Top + 10;

                    try
                    {
                        DragMove();
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

        private void Minimize_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void MaximizeRestore_Click(object sender, RoutedEventArgs e)
        {
            if (WindowState == WindowState.Maximized)
                WindowState = WindowState.Normal;
            else
                WindowState = WindowState.Maximized;
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        // Função para minimizar
        private void btInitialWindowMinimize_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        // Função para maximizar/restaurar
        private void btInitialWindowMaximize_Click(object sender, RoutedEventArgs e)
        {
            if (WindowState == WindowState.Maximized)
                WindowState = WindowState.Normal;
            else
                WindowState = WindowState.Maximized;

            UpdateMaximize();
        }

        // Atualiza o ícone do botão de maximizar/restaurar
        private void UpdateMaximize()
        {
            var brush = btInitialWindowMaximize.Background as ImageBrush;
            if (brush != null)
            {
                if (WindowState == WindowState.Maximized)
                {
                    brush.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Resources/Icons/InitialWindow/minimize.png"));
                    MainBorder.CornerRadius = new CornerRadius(0);
                    SecondaryBorder.CornerRadius = new CornerRadius(0);
                    brInitialWindowTopBar.CornerRadius = new CornerRadius(0, 0, 13, 13);
                }
                else
                {
                    brush.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Resources/Icons/InitialWindow/maximize.png"));
                    MainBorder.CornerRadius = new CornerRadius(15);
                    SecondaryBorder.CornerRadius = new CornerRadius(15);
                    brInitialWindowTopBar.CornerRadius = new CornerRadius(13);

                }
            }
        }

        // Garante atualização ao mudar o estado da janela por outros meios
        protected override void OnStateChanged(EventArgs e)
        {
            base.OnStateChanged(e);
            UpdateMaximize();
        }

        // Função para fechar
        private void btInitialWindowClose_Click(object sender, RoutedEventArgs e)
        {
            Close();
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
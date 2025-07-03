using System;
using System.Runtime.InteropServices;
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
using System.Windows.Interop;

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
            Loaded += MainWindow_Loaded;
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

            IntPtr monitor = MonitorFromWindow(hwnd, 2 /*MONITOR_DEFAULTTONEAREST*/);
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

            Marshal.StructureToPtr(mmi, lParam, true);
        }

        private void btInitialWindowProfile_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btInitialWindowConfig_Click(object sender, RoutedEventArgs e)
        {

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

        private void btInitialWindowDocumentCreationDropDown_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (this.ActualWidth < 900)
                tbInitialWindowName.Visibility = Visibility.Collapsed;
            else
                tbInitialWindowName.Visibility = Visibility.Visible;
        }

        private void brInitialWindowTopBar_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                if (WindowState == WindowState.Maximized)
                {
                    // Calcula a posição do mouse relativa à janela maximizada
                    var mouseX = e.GetPosition(this).X;
                    double percentHorizontal = mouseX / ActualWidth;
                    WindowState = WindowState.Normal;

                    // Aguarda o layout atualizar
                    this.Dispatcher.InvokeAsync(() =>
                    {
                        // Usando apenas WPF:
                        double screenWidth = SystemParameters.WorkArea.Width;
                        double screenLeft = SystemParameters.WorkArea.Left;
                        double newLeft = screenLeft + (screenWidth - this.Width) * percentHorizontal;
                        this.Left = newLeft;
                        this.Top = SystemParameters.WorkArea.Top + 10; // Pequeno deslocamento para baixo

                        // Inicia o arrasto da janela
                        DragMove();
                    }, System.Windows.Threading.DispatcherPriority.ApplicationIdle);
                }
                else
                {
                    DragMove();
                }
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

            UpdateMaximizeIcon();
        }

        // Atualiza o ícone do botão de maximizar/restaurar
        private void UpdateMaximizeIcon()
        {
            var brush = btInitialWindowMaximize.Background as ImageBrush;
            if (brush != null)
            {
                if (WindowState == WindowState.Maximized)
                    brush.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Resources/Icons/InitialWindow/minimize.png"));
                else
                    brush.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Resources/Icons/InitialWindow/maximize.png"));
            }
        }

        // Garante atualização ao mudar o estado da janela por outros meios
        protected override void OnStateChanged(EventArgs e)
        {
            base.OnStateChanged(e);
            UpdateMaximizeIcon();
        }

        // Função para fechar
        private void btInitialWindowClose_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace PrimeDocs_Desktop.UI.Components
{
    public partial class TopBarPrimeDocsClose : UserControl
    {
        private bool isDraggingFromMaximized = false;
        private Window window;

        public TopBarPrimeDocsClose()
        {
            InitializeComponent();

            this.MouseDown += TopBar_MouseDown;
            this.MouseMove += TopBar_MouseMove;

            this.Loaded += TopBarPrimeDocs_Loaded;
        }

        private void TopBarPrimeDocs_Loaded(object sender, RoutedEventArgs e)
        {
            window = Window.GetWindow(this);
            if (window == null) return;

            // Sempre que o estado da janela mudar, atualiza o ícone
            window.StateChanged += (s, args) => AtualizarIconeMaximize();

            // Atualiza o ícone com o estado inicial
            AtualizarIconeMaximize();
        }

        private void TopBar_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (window == null) return;

            if (e.ChangedButton == MouseButton.Left)
            {
                if (e.ClickCount == 2 && !(window is Configurations))
                {
                    AlternarEstadoJanela();
                }
                else if (window.WindowState == WindowState.Maximized)
                {
                    isDraggingFromMaximized = true;
                }
                else
                {
                    TentarDragMove();
                }
            }
        }

        private void TopBar_MouseMove(object sender, MouseEventArgs e)
        {
            if (window == null) return;

            if (isDraggingFromMaximized && e.LeftButton == MouseButtonState.Pressed)
            {
                isDraggingFromMaximized = false;

                double mouseX = e.GetPosition(this).X;
                double percentHorizontal = mouseX / ActualWidth;

                // Restaura a janela e força o layout a ser atualizado imediatamente
                window.WindowState = WindowState.Normal;
                window.UpdateLayout();

                // Calcula a nova posição da janela com base na posição do mouse
                double screenWidth = SystemParameters.WorkArea.Width;
                double screenLeft = SystemParameters.WorkArea.Left;
                double newLeft = screenLeft + (screenWidth - window.Width) * percentHorizontal;

                window.Left = newLeft;
                window.Top = SystemParameters.WorkArea.Top; // Removido o +10 fixo

                AtualizarIconeMaximize();
                TentarDragMove();
            }
            else if (e.LeftButton != MouseButtonState.Pressed)
            {
                isDraggingFromMaximized = false;
            }
        }

        private void AlternarEstadoJanela()
        {
            if (window.WindowState == WindowState.Maximized)
                window.WindowState = WindowState.Normal;
            else
               window.WindowState = WindowState.Maximized;

            AtualizarIconeMaximize();
        }

        private void AtualizarIconeMaximize()
        {
            if (windowControlTopBar?.imMaximize == null) return;

            string nomeImagem = window.WindowState == WindowState.Maximized
                ? "minimize.png" // restaurar
                : "maximize.png"; // maximizar

            string caminhoImagem = $"pack://application:,,,/Resources/Icons/InitialWindow/{nomeImagem}";

            try
            {
                windowControlTopBar.imMaximize.Source = new BitmapImage(new Uri(caminhoImagem, UriKind.Absolute));
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao carregar imagem: {ex.Message}");
            }
        }

        private void TentarDragMove()
        {
            try
            {
                window?.DragMove();
            }
            catch (InvalidOperationException)
            {
            }
        }
    }
}

using PrimeDocs_Desktop.UI;
using PrimeDocs_Desktop.UI.Components;
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
        public MainWindow()
        {
            InitializeComponent();
            newDocumentControl.CollapsedChanged += NewDocumentControl_CollapsedChanged;
        }
        private void CollapseChange()
        {
            if (newDocumentControl.documentCollapsed)
                openDocumentControl.Margin = new Thickness(0, -235, 0, 0);
            else
                openDocumentControl.Margin = new Thickness(0, 0, 0, 0);
        }

        private void NewDocumentControl_CollapsedChanged(object sender, EventArgs e)
        {
            CollapseChange();
        }

        
    }
}
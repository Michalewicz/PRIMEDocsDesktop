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
    /// Interação lógica para TextBoxPrimeDocs.xam
    /// </summary>
    public partial class Placeholder : UserControl
    {
        public Placeholder()
        {
            InitializeComponent();
        }

        public static readonly DependencyProperty PlaceholderTextProperty =
                   DependencyProperty.Register("PlaceholderText", typeof(string), typeof(Placeholder), new PropertyMetadata("Placeholder"));

        public string PlaceholderText
        {
            get => (string)GetValue(PlaceholderTextProperty);
            set => SetValue(PlaceholderTextProperty, value);
        }
    

        private void tbxPlaceholder_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(tbxPlaceholder.Text))
            {
                tbPlaceholder.Visibility = Visibility.Visible;
            }
            else
            {
                tbPlaceholder.Visibility = Visibility.Collapsed;
            }
        }
    }
}

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
using System.Windows.Shapes;

namespace Turbo.azWithLinq.Views
{
    /// <summary>
    /// Interaction logic for CarsDataWindow.xaml
    /// </summary>
    public partial class CarsDataWindow : Window
    {
        public bool IsData { get; set; } = true;

        public CarsDataWindow()
        {
            InitializeComponent();
            
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (IsData == false)
            {
                MessageBox.Show("Bağışlayın, göndərdiyiniz sorğu üzrə heç bir nəticə tapılmamışdır.Digər meyarlar üzrə axtarışa cəhd edin.");
                Close();
            }
        }
    }
}

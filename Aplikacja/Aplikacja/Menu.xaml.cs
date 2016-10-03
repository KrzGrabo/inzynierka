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

namespace Aplikacja
{
    /// <summary>
    /// Interaction logic for Menu.xaml
    /// </summary>
    public partial class Menu : Window
    {
        public Menu()
        {
            InitializeComponent();
        }

        private void pomocButton_Click(object sender, RoutedEventArgs e)
        {
            Pomoc pomoc = new Pomoc();
            pomoc.Show();
        }

        private void wylogujButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            Intro intro = new Intro();
            intro.Show();
        }

        private void zakonczButton_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void oprogramieButton_Click(object sender, RoutedEventArgs e)
        {
            Informacje informacje = new Informacje();
            informacje.Show();
        }

        private void oautorachButton_Click(object sender, RoutedEventArgs e)
        {
            Autorzy autorzy = new Autorzy();
            autorzy.Show();
        }
    }
}

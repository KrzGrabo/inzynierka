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
            Intro intro = new Intro();
            intro.Show();
            this.Close();
        }

        private void zakonczButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult wynik = MessageBox.Show("Czy na pewno chcesz zakończyć działanie programu?", "Appka", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (wynik == MessageBoxResult.Yes)
            {
                Application.Current.Shutdown();
            }
        }

        private void oprogramieButton_Click(object sender, RoutedEventArgs e)
        {
            Informacje informacje = new Informacje();
            informacje.Show();
        }

        private void oautorachButton_Click(object sender, RoutedEventArgs e)
        {
            Autorzy okno = new Autorzy();
            okno.Show();
        }

        private void kalkulatoryButton_Click(object sender, RoutedEventArgs e)
        {
            Kalkulatory okno = new Kalkulatory();
            okno.Show();
        }

        private void dietaButton_Click(object sender, RoutedEventArgs e)
        {
            Dieta okno = new Dieta();
            okno.Show();
        }

        private void suplementacjaButton_Click(object sender, RoutedEventArgs e)
        {
            Suplementacja okno = new Suplementacja();
            okno.Show();
        }

        private void profilButton_Click(object sender, RoutedEventArgs e)
        {
            Profil okno = new Profil();
            okno.Show();
        }

        private void treningButton_Click(object sender, RoutedEventArgs e)
        {
            Trening okno = new Trening();
            okno.Show();
        }

        private void statystykiButton_Click(object sender, RoutedEventArgs e)
        {
            Statystyki okno = new Statystyki();
            okno.Show();
        }

        private void produktySpozywczeButton_Click(object sender, RoutedEventArgs e)
        {
            TabelaProduktow okno = new TabelaProduktow();
            okno.Show();
        }

        private void dziennikButton_Click(object sender, RoutedEventArgs e)
        {
            Dziennik okno = new Dziennik();
            okno.Show();
        }


   
    }
}

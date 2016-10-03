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
    /// Interaction logic for Intro.xaml
    /// </summary>
    public partial class Intro : Window
    {
        public Intro()
        {
            InitializeComponent();
        }

        private void rejestracjaButton_Click(object sender, RoutedEventArgs e)
        {
            Rejestracja rej = new Rejestracja();
            rej.Show();
        }

        private void logowanieButton_Click(object sender, RoutedEventArgs e)
        {
            string walidacja = "";
            string login = loginTextbox.Text.Trim();
            string haslo = hasloTextbox.Text.Trim();

            if (login == "")
            {
                walidacja = walidacja + " \nNie wpisałeś loginu";
            }

            if (haslo == "")
            {
                walidacja = walidacja + " \nNie wpisałeś hasła";
            }

            if (walidacja == "")
            {
                Menu menu = new Menu();
                menu.Show();
                this.Close();

            }
            else
            {
                walidacja = "Wystąpiły błędy przy logowaniu:" + walidacja;
                MessageBox.Show(walidacja, "Uwaga", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }

        }
    }
}

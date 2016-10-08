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
            AplikacjaEntities baza = new AplikacjaEntities();
            Uzytkownicy u = new Uzytkownicy();
            string walidacja = "";
            u.Login = loginTextbox.Text.Trim();
            u.Haslo = hasloTextbox.Password.ToString();
            //MessageBox.Show(u.Haslo, "Uwaga", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            if (u.Login == "")
            {
                walidacja = walidacja + " \nNie wpisałeś loginu";
            }

            if (u.Haslo == "")
            {
                walidacja = walidacja + " \nNie wpisałeś hasła";
            }

            if (walidacja == "")
            {
                var uzytkownik = baza.Uzytkownicy.Where(m => m.Login.Equals(u.Login)).FirstOrDefault();
                if (uzytkownik != null && uzytkownik.Haslo == u.Haslo)
                {
                    Menu menu = new Menu();
                    menu.Show();
                    this.Close();
                }
                else
                {
                    walidacja = "Nie ma takiego użytkownika";
                    MessageBox.Show(walidacja, "Uwaga", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                }
            }
            else
            {
                walidacja = "Wystąpiły błędy przy logowaniu:" + walidacja;
                MessageBox.Show(walidacja, "Uwaga", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }

        }
    }
}
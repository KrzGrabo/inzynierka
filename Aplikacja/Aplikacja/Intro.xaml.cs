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
using Microsoft.AspNet.Identity;

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
            BazaDanychEntities db = new BazaDanychEntities();
            Uzytkownicy uzytkownik = new Uzytkownicy();
            PasswordHasher hasher = new PasswordHasher();

            string walidacja = "";
            uzytkownik.Login = loginTextbox.Text.Trim();
            uzytkownik.Haslo = hasloTextbox.Password.ToString();
            //MessageBox.Show(uzytkownik.Haslo, "Uwaga", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            if (uzytkownik.Login == "")
            {
                walidacja = walidacja + " \nNie wpisałeś loginu";
            }

            if (uzytkownik.Haslo == "")
            {
                walidacja = walidacja + " \nNie wpisałeś hasła";
            }

            if (walidacja == "")
            {
                var szukanyUzytkwonik = db.Uzytkownicy.Where(m => m.Login.Equals(uzytkownik.Login)).FirstOrDefault();
                if (szukanyUzytkwonik != null && hasher.VerifyHashedPassword(szukanyUzytkwonik.Haslo, uzytkownik.Haslo) == PasswordVerificationResult.Success)
                {
                    Menu menu = new Menu();
                    menu.Show();
                    this.Close();
                }
                else
                {
                    walidacja = "Nie ma takiego użytkownika";
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
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
    /// Interaction logic for Rejestracja.xaml
    /// </summary>
    public partial class Rejestracja : Window
    {
        bool plec = false;

        public Rejestracja()
        {
            InitializeComponent();
        }

        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {
            plec = true;
        }

        private void powrotButton_Click(object sender, RoutedEventArgs e)
        {
            Intro intro = new Intro();
            intro.Show();
        }

        private void rejestracjaButton_Click(object sender, RoutedEventArgs e)
        {
            string walidacja = "";
            AplikacjaEntities baza = new AplikacjaEntities();
            Uzytkownicy u = new Uzytkownicy();
            u.Login = loginTextbox.Text.Trim();
            string haslo = hasloTextbox.Password.ToString();
            string haslo2 = haslo2Textbox.Password.ToString();


            if (plec == false)
            {
                walidacja = walidacja + " \nNie wybrałeś płci";
            }

            if (u.Login == "")
            {
                walidacja = walidacja + " \nNie wpisałeś loginu";
            }

            if (haslo == "")
            {
                walidacja = walidacja + " \nNie wpisałeś hasła";
            }

            if (haslo != haslo2)
            {
                walidacja = walidacja + " \nWpisane hasła nie są identyczne";
            }
            else
            {
                u.Haslo = haslo;
            }

            if (walidacja == "")
            {
                u.ID = 0;
                baza.Uzytkownicy.Add(u);
                baza.SaveChanges();
                MessageBox.Show("Twoje konto zostało utworzone poprawnie!", "App", MessageBoxButton.OK, MessageBoxImage.Information);
                this.Close();
            }
            else
            {
                walidacja = "Wystąpiły błędy przy wpisywaniu danych:" + walidacja;
                MessageBox.Show(walidacja, "Uwaga", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
        }
    }
}
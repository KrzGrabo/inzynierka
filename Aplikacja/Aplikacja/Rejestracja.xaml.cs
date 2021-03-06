﻿using System;
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
    /// Interaction logic for Rejestracja.xaml
    /// </summary>
    public partial class Rejestracja : Window
    {
        BazaDanychEntities db = new BazaDanychEntities();
        Uzytkownicy uzytkownik = new Uzytkownicy();

        public Rejestracja()
        {
            InitializeComponent();
            Bindowanie();
        }

        public void Bindowanie()
        {
            infoLabel.Text = "Zarejestruj się aby uzyskać dostęp do aplikacji. To proste- wystarczy, że podasz login i hasło.";
        }
        private void powrotButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void rejestracjaButton_Click(object sender, RoutedEventArgs e)
        {
            string walidacja = "";
            PasswordHasher hasher = new PasswordHasher();
            
            uzytkownik.Login = loginTextbox.Text.Trim();
            string haslo = hasloTextbox.Password.ToString();
            string haslo2 = haslo2Textbox.Password.ToString();

            if (uzytkownik.Login == "")
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
                uzytkownik.Haslo = hasher.HashPassword(haslo).ToString(); ;
            }
            var szukanyUzytkwonik = db.Uzytkownicy.Where(m => m.Login.Equals(uzytkownik.Login)).FirstOrDefault();
            if (szukanyUzytkwonik != null)
            {
                walidacja = "\nUżytkownik o podanym loginie już istnieje!";
            }
            if (walidacja == "")
            {
                db.Uzytkownicy.Add(uzytkownik);
                db.SaveChanges();
                MessageBox.Show("Twoje konto zostało utworzone poprawnie!", "App", MessageBoxButton.OK, MessageBoxImage.Information);
                this.Close();
            }
            else
            {
                walidacja = "Wystąpiły błędy przy wpisywaniu danych: " + walidacja;
                MessageBox.Show(walidacja, "Uwaga", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
        }
    }
}
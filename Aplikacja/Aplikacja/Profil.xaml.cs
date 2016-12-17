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
    /// Interaction logic for Profil.xaml
    /// </summary>
    public partial class Profil : Window
    {
        BazaDanychEntities db = new BazaDanychEntities();
        int id = Sesja.ZwrocId();
        Dane przypisaneDane = new Dane();
        Uzytkownicy uzytkownik = new Uzytkownicy();
        Historia_Danych historia = new Historia_Danych();
        string wiadomosc = "";
        
        public Profil()
        {
            InitializeComponent();

            uzytkownik = db.Uzytkownicy.Where(m => m.ID.Equals(id)).FirstOrDefault();
            loginTextbox.Text = uzytkownik.Login;

            if (uzytkownik != null && uzytkownik.ID_Profilu != null)
            {
                wczytajDane();
            }
        }

        private void okButton_Click(object sender, RoutedEventArgs e)
        {
            zapiszDane();

            if (walidacja())
            {
                if (uzytkownik.ID_Profilu != null)
                {
                    zapiszHistorie();
                }

                if (uzytkownik != null && uzytkownik.ID_Profilu == null)
                {
                    db.Dane.Add(przypisaneDane);
                    uzytkownik.ID_Profilu = przypisaneDane.ID;
                }

                db.SaveChanges();
                this.Close();
            }
            else
            {
                wiadomosc = "Nie uzupełniłeś poprawnie wszystkich pól: " + wiadomosc;
                MessageBox.Show(wiadomosc, "Uwaga", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
        }

        private void anulujButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void zapiszHistorie() {
            historia.Data = DateTime.Now;
            String.Format("{0:MM/dd/yyyy}", historia.Data);
            historia.ID_Profilu = przypisaneDane.ID;
            historia.Obwod_Bioder = przypisaneDane.Obwod_Bioder;
            historia.Obwod_Pasa = przypisaneDane.Obwod_Pasa;
            historia.Waga = przypisaneDane.Waga;
            historia.Wzrost = przypisaneDane.Wzrost;
            historia.Wiek = przypisaneDane.Wiek;
            historia.Zapotrzebowanie = przypisaneDane.Zapotrzebowanie;
            db.Historia_Danych.Add(historia);
            db.SaveChanges();
        }

        private void zapiszPlec()
        {
            if (plecCombo.SelectedIndex == 0)
            {
                przypisaneDane.Plec = "M";
            }
            else if (plecCombo.SelectedIndex == 1)
            {
                przypisaneDane.Plec = "K";
            }
        }

        private bool walidacja()
        {
            bool result = true;
            if (plecCombo.SelectedIndex == -1)
            {
                wiadomosc +=" \nMusisz wybrać płeć";
                result = false;
            }
            return result;
        }

        private void wczytajDane()
        {
            int id_profilu = uzytkownik.ID_Profilu.GetValueOrDefault();
            przypisaneDane = db.Dane.Where(m => m.ID.Equals(id_profilu)).FirstOrDefault();
            imieTextbox.Text = przypisaneDane.Imie;
            wiekTextbox.Text = przypisaneDane.Wiek.ToString();
            wagaTextbox.Text = przypisaneDane.Waga.ToString();
            wzrostTextbox.Text = przypisaneDane.Wzrost.ToString();
            obwodPasaTextbox.Text = przypisaneDane.Obwod_Pasa.ToString();
            obwodBioderTextbox.Text = przypisaneDane.Obwod_Bioder.ToString();

            if (przypisaneDane.Plec == "M")
            {
                plecCombo.SelectedIndex = 0;
            }
            else if (przypisaneDane.Plec == "K")
            {
                plecCombo.SelectedIndex = 1;
            }
        }

        private void zapiszDane()
        {
            przypisaneDane.Imie = imieTextbox.Text.Trim();
            przypisaneDane.Wiek = int.Parse(wiekTextbox.Text.Trim());
            przypisaneDane.Waga = double.Parse(wagaTextbox.Text.Trim());
            przypisaneDane.Wzrost = double.Parse(wzrostTextbox.Text.Trim());
            przypisaneDane.Obwod_Pasa = double.Parse(obwodPasaTextbox.Text.Trim());
            przypisaneDane.Obwod_Bioder = double.Parse(obwodBioderTextbox.Text.Trim());

            if (plecCombo.SelectedIndex == 0)
            {
                przypisaneDane.Plec = "M";
            }
            else if (plecCombo.SelectedIndex == 1)
            {
                przypisaneDane.Plec = "K";
            }
        }
    }
}

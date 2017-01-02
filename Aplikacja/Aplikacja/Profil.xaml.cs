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

            ///OGARNIJ CZY SOBIE TO DOBRZE ZAPISUJESZ BO POPRAWIELEM WALIDACJE
            zapiszDane();

                string walid = "";
                double wzrostTest = -1, wagaTest = -1, pasTest = -1, biodraTest = -1;
                int wiekTest = -1;

                try
                {
                    wzrostTest = double.Parse(wzrostTextbox.Text.Trim());
                }
                catch (Exception)
                {
                    walid = walid + " \nWpisałeś błędną wartość w pole wzrost";
                }

                try
                {
                    pasTest = double.Parse(obwodPasaTextbox.Text.Trim());
                }
                catch (Exception)
                {
                    walid = walid + " \nWpisałeś błędną wartość w pole obwód pasa";
                }

                try
                {
                    biodraTest = double.Parse(obwodBioderTextbox.Text.Trim());
                }
                catch (Exception)
                {
                    walid = walid + " \nWpisałeś błędną wartość w pole obwód bioder";
                }

                try
                {
                    wagaTest = double.Parse(wagaTextbox.Text.Trim());
                }
                catch (Exception)
                {
                    walid = walid + " \nWpisałeś błędną wartość w pole waga";
                }

            
                try
                {
                    wiekTest = int.Parse(wzrostTextbox.Text.Trim());
                }
                catch (Exception)
                {
                    walid = walid + " \nWpisałeś błędną wartość w pole wiek";
                }
                if (wiekTest > 100 || wiekTest < 10) walid  = walid  + "  \nWartość podana w polu 'wiek' jest z poza zakresu. Dostępny zakres to (10,100)";
                if (biodraTest > 300 || biodraTest < 50) walid  = walid  + "  \nWartość podana w polu 'biodra' jest z poza zakresu. Dostępny zakres to (50,300)";
                if (wagaTest > 300 || wagaTest < 30) walid  = walid  + "  \nWartość podana w polu 'waga' jest z poza zakresu. Dostępny zakres to (30,300)";
                if (wzrostTest > 250 || wzrostTest < 100) walid = walid  + "  \nWartość podana w polu 'wzrost' jest z poza zakresu. Dostępny zakres to (100,250)";
                if (pasTest > 300 || pasTest < 50) walid = walid + "  \nWartość podana w polu 'pas' jest z poza zakresu. Dostępny zakres to (50,300)";

                if (walid == "")
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
                    walid = "Wystąpiły błędy przy wpisywaniu danych:" + walid;
                    MessageBox.Show(walid, "Uwaga", MessageBoxButton.OK, MessageBoxImage.Exclamation);
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



        private void WalidacjaTextboxow(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.OemComma || e.Key == Key.Delete || e.Key == Key.D0 || e.Key == Key.Back || e.Key == Key.D1 || e.Key == Key.D2 || e.Key == Key.D3 || e.Key == Key.D4 || e.Key == Key.D5 || e.Key == Key.D6 || e.Key == Key.D7 || e.Key == Key.D8 || e.Key == Key.D9 || e.Key == Key.NumPad1 || e.Key == Key.NumPad0 || e.Key == Key.NumPad2 || e.Key == Key.NumPad3 || e.Key == Key.NumPad4 || e.Key == Key.NumPad5 || e.Key == Key.NumPad6 || e.Key == Key.NumPad7 || e.Key == Key.NumPad8 || e.Key == Key.NumPad9)
                e.Handled = false;
            else
                e.Handled = true;
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

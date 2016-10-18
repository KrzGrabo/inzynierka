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

            if (uzytkownik != null && uzytkownik.Id_Profilu != null)
            {
                int id_profilu =  uzytkownik.Id_Profilu.GetValueOrDefault();
                przypisaneDane = db.Dane.Where(m => m.ID.Equals(id_profilu)).FirstOrDefault();
                imieTextbox.Text = przypisaneDane.Imie;
                wiekTextbox.Text = przypisaneDane.Wiek.ToString();
                wagaTextbox.Text = przypisaneDane.Waga.ToString();
                wzrostTextbox.Text = przypisaneDane.Wzrost.ToString();
                obwodPasaTextbox.Text = przypisaneDane.Obwod_Pasa.ToString();
                obwodBioderTextbox.Text = przypisaneDane.Obwod_Bioder.ToString();
            }
        }

        private void okButton_Click(object sender, RoutedEventArgs e)
        {
            if (uzytkownik.Id_Profilu != null)
            {
                zapiszHistorie();
            }
            przypisaneDane.Imie = imieTextbox.Text.Trim();
            przypisaneDane.Wiek = int.Parse(wiekTextbox.Text.Trim());
            przypisaneDane.Waga = double.Parse(wagaTextbox.Text.Trim());
            przypisaneDane.Wzrost = double.Parse(wzrostTextbox.Text.Trim());
            przypisaneDane.Obwod_Pasa = double.Parse(obwodPasaTextbox.Text.Trim());
            przypisaneDane.Obwod_Bioder = double.Parse(obwodBioderTextbox.Text.Trim());

            if(uzytkownik != null && uzytkownik.Id_Profilu == null)
            {
                db.Dane.Add(przypisaneDane);
                uzytkownik.Id_Profilu = przypisaneDane.ID;
            }

            db.SaveChanges();
            this.Close();
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
            historia.Wiek = historia.Wiek;
            db.Historia_Danych.Add(historia);
            db.SaveChanges();
        }
    }
}

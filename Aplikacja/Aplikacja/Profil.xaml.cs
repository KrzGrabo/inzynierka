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
                rocznikTextbox.Text = przypisaneDane.Rocznik;
                wagaTextbox.Text = przypisaneDane.Waga;
            }
        }

        private void okButton_Click(object sender, RoutedEventArgs e)
        {
            przypisaneDane.Imie = imieTextbox.Text.Trim();
            przypisaneDane.Rocznik = rocznikTextbox.Text.Trim();
            przypisaneDane.Waga = wagaTextbox.Text.Trim();

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
    }
}

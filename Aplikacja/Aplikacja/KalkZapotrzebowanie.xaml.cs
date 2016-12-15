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
    /// Interaction logic for KalkZapotrzebowanie.xaml
    /// </summary>
    public partial class KalkZapotrzebowanie : Window
    {
        BazaDanychEntities db = new BazaDanychEntities();
        int id = Sesja.ZwrocId();
        Dane przypisaneDane = new Dane();
        Uzytkownicy uzytkownik = new Uzytkownicy();

        public KalkZapotrzebowanie()
        {
            InitializeComponent();
            Bindowanie();
        }

        double zapotrzebowanko=-1;

        private void Bindowanie()
        {
            opisLabel.Text = "Kalkulator zapotrzebowania wyliczy twoje zapotrzebowanie energetyczne. Uzupełnij wszystkie pola aby uzyskać wynik.";
            wynikLabel.Content = "0";
        }
                   
        private void daneButton_Click(object sender, RoutedEventArgs e)
        {
            uzytkownik = db.Uzytkownicy.Where(m => m.ID.Equals(id)).FirstOrDefault();
            przypisaneDane = uzytkownik.Dane;
            double aktWaga = przypisaneDane.Waga.GetValueOrDefault(),
                 wzrost = przypisaneDane.Wzrost.GetValueOrDefault(),
                 wiek = przypisaneDane.Wiek.GetValueOrDefault();

            ustawPlec();
            wagaTextbox.Text = aktWaga.ToString();
            wzrostTextbox.Text = wzrost.ToString();
            wiekTextbox.Text = wiek.ToString();
        }

        private void obliczButton_Click(object sender, RoutedEventArgs e)
        {
            string walidacja = "";
            bool plec = true;
            double wzrost = 0, waga = 0, wiek=0;
            double aktywnosc=0, przemiana=0;
            if (plecCombo.SelectedIndex == -1) walidacja = walidacja + " \nMusisz wybrać płeć";
            if (aktywnoscCombo.SelectedIndex == -1) walidacja = walidacja + " \nMusisz wybrać aktywność fizyczną";
            if (przemianaCombo.SelectedIndex == -1) walidacja = walidacja + " \nMusisz wybrać przemianę materii";
            double wynik = 0;
            try
            {
                waga = double.Parse(wagaTextbox.Text.Trim());
            }
            catch (Exception)
            {
                walidacja = walidacja + " \nWpisałeś błędną wartość w pole waga";
            }
            try
            {
                wzrost = double.Parse(wzrostTextbox.Text.Trim());
            }
            catch (Exception)
            {
                walidacja = walidacja + " \nWpisałeś błędną wartość w pole wzrost";
            }
               try
            {
                wiek = double.Parse(wiekTextbox.Text.Trim());
            }
            catch (Exception)
            {
                walidacja = walidacja + " \nWpisałeś błędną wartość w pole wiek";
            }
            if(walidacja=="")
            {
                if (przemianaCombo.SelectedIndex == 0) przemiana = 800;
                if (przemianaCombo.SelectedIndex == 1) przemiana = 450;
                if (przemianaCombo.SelectedIndex == 2) przemiana = 300;
                if (aktywnoscCombo.SelectedIndex == 0) aktywnosc = 100;
                if (aktywnoscCombo.SelectedIndex == 1) aktywnosc = 300;
                if (aktywnoscCombo.SelectedIndex == 2) aktywnosc = 500;
                if (aktywnoscCombo.SelectedIndex == 3) aktywnosc = 1000;

                double bmr=0;
                if (plec == true)
                {
                    bmr = 10 * waga + 6.25 * wzrost - 4.92 * wiek + 5;
                    wynik = bmr + aktywnosc + przemiana;
                    wynik = 0.8 * wynik;
                }
                else
                {
                    bmr = 10 * waga + 6.25 * wzrost - 4.92 * wiek - 161;
                    wynik = bmr + aktywnosc + przemiana;
                }
                wynikLabel.Content = wynik.ToString()+" kcal.";
                zapotrzebowanko = wynik;
                przypisaneDane.Zapotrzebowanie = zapotrzebowanko;
                db.SaveChanges();

            }
            else
            {
                walidacja = "Wystąpiły błędy przy wpisywaniu danych:" + walidacja;
                MessageBox.Show(walidacja, "Uwaga", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
        }

        private void dietaButton_Click(object sender, RoutedEventArgs e)
        {
         // tutaj powinno być zapisanie do bazy
        }

        private void ustawPlec()
        {
            if (przypisaneDane.Plec == "M")
            {
                plecCombo.SelectedIndex = 0;
            }
            else if (przypisaneDane.Plec == "K")
            {
                plecCombo.SelectedIndex = 1;
            }
        }
    }
}

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


///DO Dokonczenia algorytmy+uzupelnienie wyswietlania wynikow

namespace Aplikacja
{
    /// <summary>
    /// Interaction logic for KalkWaga.xaml
    /// </summary>
    public partial class KalkWaga : Window
    {
        BazaDanychEntities db = new BazaDanychEntities();
        int id = Sesja.ZwrocId();
        Dane przypisaneDane = new Dane();
        Uzytkownicy uzytkownik = new Uzytkownicy();

        public KalkWaga()
        {
            InitializeComponent();
            Bindowanie();
            uzytkownik = db.Uzytkownicy.Where(m => m.ID.Equals(id)).FirstOrDefault();
            przypisaneDane = uzytkownik.Dane.FirstOrDefault();
        }

        private void obliczButton_Click(object sender, RoutedEventArgs e)
        {
            string walidacja = "", plec;
            double wzrost = 0, waga = 0, pas = 0, biodra = 0;
            if (plecCombo.SelectedIndex == 0)
                plec = "M";
            else
                plec = "K";
            if (plecCombo.SelectedIndex == -1)
            {
                walidacja = walidacja + " \nMusisz wybrać płeć";
            }
            if (wagaTextbox.Text.Trim() != "")
            {
                try
                {
                    waga = double.Parse(wagaTextbox.Text.Trim());
                }
                catch (Exception)
                {
                    walidacja = walidacja + " \nWpisałeś błędną wartość w pole waga";
                }
            }
            if (wzrostTextbox.Text.Trim() != "")
            {
                try
                {
                    wzrost = double.Parse(wzrostTextbox.Text.Trim());
                }
                catch (Exception)
                {
                    walidacja = walidacja + " \nWpisałeś błędną wartość w pole wzrost";
                }
            }
            if (pasTextbox.Text.Trim() != "")
            {
                try
                {
                    pas = double.Parse(pasTextbox.Text.Trim());
                }
                catch (Exception)
                {
                    walidacja = walidacja + " \nWpisałeś błędną wartość w pole obwód pasa";
                }
            }
            if (biodraTextbox.Text.Trim() != "")
            {
                try
                {
                    biodra = double.Parse(biodraTextbox.Text.Trim());
                }
                catch (Exception)
                {
                    walidacja = walidacja + " \nWpisałeś błędną wartość w pole obwód bioder";
                }
            }
            if (biodra > 300 || biodra < 50) walidacja = walidacja + "  \nWartość podana w polu 'biodra' jest z poza zakresu. Dostępny zakres to (50,300)";
            if (waga > 300 || waga < 30) walidacja = walidacja + "  \nWartość podana w polu 'waga' jest z poza zakresu. Dostępny zakres to (30,300)";
            if (wzrost > 250 || wzrost < 100) walidacja = walidacja + "  \nWartość podana w polu 'wzrost' jest z poza zakresu. Dostępny zakres to (100,250)";
            if (pas > 300 || pas < 50) walidacja = walidacja + "  \nWartość podana w polu 'pas' jest z poza zakresu. Dostępny zakres to (50,300)";
  

            if (walidacja == "")
            {

                bool wagaBool = false, wzrostBool = false, pasBool = false, biodraBool = false;
                if (biodraTextbox.Text.Trim() != "") biodraBool = true;
                if (pasTextbox.Text.Trim() != "") pasBool = true;
                if (wagaTextbox.Text.Trim() != "") wagaBool = true;
                if (wzrostTextbox.Text.Trim() != "") wzrostBool = true;

                if (wagaBool == true && wzrostBool == true)
                {
                    bmiLabel.Content = "Twoje BMI to: " + String.Format("{0:N2}",BmiFun(waga, wzrost));
                }
                else bmiLabel.Content = "Aby wylicz swoje BMI, musisz uzupełnić pola: płeć, waga, wzrost";

                if (wzrostBool == true)
                {
                    wagaLabel.Content = "Twoja idealna waga to: " + IdealnaWagaFun(plec, wzrost);
                }
                else wagaLabel.Content = "Aby wylicz swoją idealną wagę, musisz uzupełnić pola: płeć, wzrost";

                if (pasBool == true && biodraBool == true)
                {
                    whrLabel.Content = "Twoje WHR to: " + WhrFun(biodra, pas);
                }
                else whrLabel.Content = "Aby wylicz swoje WHR, musisz uzupełnić pola: płeć, obwód pasa, obwód bioder";

                if (wagaBool == true && pasBool == true)
                {
                    tluszczLabel.Content = "Twój poziom tłuszczu to: " + String.Format("{0:N2}", TluszczFun(plec, waga, pas)) + "%";
                }
                else tluszczLabel.Content = "Aby wylicz swój poziom tłuszczu, musisz uzupełnić pola: płeć, waga, obwód pasa";
                ToolTipService.SetIsEnabled(bmiLabel, true);
                ToolTipService.SetIsEnabled(tluszczLabel, true);
                ToolTipService.SetIsEnabled(wagaLabel, true);
                ToolTipService.SetIsEnabled(whrLabel, true);

            }
            else
            {
                walidacja = "Wystąpiły błędy przy wpisywaniu danych:" + walidacja;
                MessageBox.Show(walidacja, "Uwaga", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
        }

        private void Bindowanie()
        {
            opisTextblock.Text = "Kalkulator wagi, korzystając z listy wejściowych danych, oblicza kilka wskaźników opisujących ciało. Ze względu na brak możliwości szczegółowego zbadania ciała użytkownika(tylko poprzez wprowadzone do interfejsu wartości liczbowe) wyniki mogą nieznacznie odbiegać od rzeczywistych wartości. Poszczególne wskaźniki są opisane w szczegółowy sposób niżej- w sekcji 'wyniki'. Uzupełnij wszystkie pola aby uzyskać wyniki.";
        
        }

        private void mojedaneButton_Click(object sender, RoutedEventArgs e)
        {
           if (przypisaneDane != null)
           {
               string plec = przypisaneDane.Plec;
               double aktWaga = przypisaneDane.Waga.GetValueOrDefault(),
                   wzrost = przypisaneDane.Wzrost.GetValueOrDefault(),
                   aktPas = przypisaneDane.Obwod_Pasa.GetValueOrDefault(),
                   aktBiodra = przypisaneDane.Obwod_Bioder.GetValueOrDefault();

               if (plec == "M") plecCombo.SelectedIndex = 0;
               else plecCombo.SelectedIndex = 1;
               wagaTextbox.Text = aktWaga.ToString();
               wzrostTextbox.Text = wzrost.ToString();
               pasTextbox.Text = aktPas.ToString();
               biodraTextbox.Text = aktBiodra.ToString();
           }
           else
           {
               string msg = "Brak danych profilowych. Przejdź do modułu Twoje Dane i uzupełnił swoje dane.";
               MessageBox.Show(msg, "Uwaga", MessageBoxButton.OK, MessageBoxImage.Exclamation);
           }
        }

        private double BmiFun(double waga, double wzrost)
        {
            double wynik = 0;
            wynik = waga / Math.Pow(wzrost / 100, 2); ///wzorek na bmi
            return wynik;
        }

        private double IdealnaWagaFun(string plec, double wzrost)
        {
            double wynik = 0;
            if(plec == "M")
                wynik = (wzrost - 100) * 0.9; ///wzorek na idealna wage, wg roznych tam wspolczynnikow
            else
                wynik = (wzrost - 100) * 0.85;
            return wynik;
        }

        private double WhrFun(double biodra, double pas)
        {
            double wynik = 0;
            wynik = pas / biodra; ///wzorek na whr
            return wynik;
        }

        private double TluszczFun(string plec, double waga, double pas)
        {
            double wynik = 0;
            double pasPom = (4.15 * pas) / 2.54;
            double masaPom = 0.082 * waga * 2.2;
            if (plec == "M")
                wynik = (pasPom - masaPom - 98.42) / (waga * 2.2) * 100; ///wzorek na tłuszcz
            else
                wynik = (pasPom - masaPom - 76.76) / (waga * 2.2) * 100;
            return wynik;
        }

        private void WalidacjaTextboxow(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.OemComma || e.Key == Key.Delete || e.Key == Key.D0 || e.Key == Key.Back || e.Key == Key.D1 || e.Key == Key.D2 || e.Key == Key.D3 || e.Key == Key.D4 || e.Key == Key.D5 || e.Key == Key.D6 || e.Key == Key.D7 || e.Key == Key.D8 || e.Key == Key.D9 || e.Key == Key.NumPad1 || e.Key == Key.NumPad0 || e.Key == Key.NumPad2 || e.Key == Key.NumPad3 || e.Key == Key.NumPad4 || e.Key == Key.NumPad5 || e.Key == Key.NumPad6 || e.Key == Key.NumPad7 || e.Key == Key.NumPad8 || e.Key == Key.NumPad9)
                e.Handled = false;
            else
                e.Handled = true;
        }

        
    }
}

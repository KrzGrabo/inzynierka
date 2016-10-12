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
    /// Interaction logic for KalkWaga.xaml
    /// </summary>
    public partial class KalkWaga : Window
    {
        public KalkWaga()
        {
            InitializeComponent();
            Bindowanie();
        }

        private void obliczButton_Click(object sender, RoutedEventArgs e)
        {
            string walidacja = "";
            bool plec = true;
            double wzrost = 0, waga = 0, pas = 0, biodra = 0;
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
            if (walidacja == "")
            {

                bool wagaBool = false, wzrostBool = false, pasBool = false, biodraBool = false;
                if (biodraTextbox.Text.Trim() != "") biodraBool = true;
                if (pasTextbox.Text.Trim() != "") pasBool = true;
                if (wagaTextbox.Text.Trim() != "") wagaBool = true;
                if (wzrostTextbox.Text.Trim() != "") wzrostBool = true;

                if (wagaBool == true && wzrostBool == true)
                {
                    bmiLabel.Content = "Twoje BMI to: " + BmiFun(plec, waga, wzrost);
                }
                else bmiLabel.Content = "Aby wylicz swoje BMI, musisz uzupełnić pola: płeć, waga, wzrost";

                if (wzrostBool == true)
                {
                    wagaLabel.Content = "Twoja idealna waga to: " + IdealnaWagaFun(plec, wzrost);
                }
                else wagaLabel.Content = "Aby wylicz swoją idealną wagę, musisz uzupełnić pola: płeć, wzrost";

                if (pasBool == true && biodraBool == true)
                {
                    whrLabel.Content = "Twoje WHR to: " + WhrFun(plec, biodra, pas);
                }
                else whrLabel.Content = "Aby wylicz swoje WHR, musisz uzupełnić pola: płeć, obwód pasa, obwód bioder";

                if (wagaBool == true && pasBool == true)
                {
                    tluszczLabel.Content = "Twój poziom tłuszczu to: " + TluszczFun(plec, waga, pas);
                }
                else tluszczLabel.Content = "Aby wylicz swój poziom tłuszczu, musisz uzupełnić pola: płeć, waga, obwód pasa";

            }
            else
            {
                walidacja = "Wystąpiły błędy przy wpisywaniu danych:" + walidacja;
                MessageBox.Show(walidacja, "Uwaga", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
        }

        private void Bindowanie()
        {
            bmiLabel.Content = "Uzupełnij pola i kliknij oblicz, aby uzyskać wyniki";
        }

        private void mojedaneButton_Click(object sender, RoutedEventArgs e)
        {
           bool plec=false;
           double aktWaga=1, wzrost=1, aktPas=1, aktBiodra=1; //Do tych zmienny przypisane wartosci z bazy danych z profilu

           if (plec == true) plecCombo.SelectedIndex = 0;
           else plecCombo.SelectedIndex = 1;
           wagaTextbox.Text = aktWaga.ToString();
           wzrostTextbox.Text = wzrost.ToString();
           pasTextbox.Text = aktPas.ToString();
           biodraTextbox.Text = aktBiodra.ToString();
        }

        private double BmiFun(bool plec, double waga, double wzrost)
        {
            double wynik = 0;
            wynik = waga * wzrost; ///wzorek na bmi
            return wynik;
        }

        private double IdealnaWagaFun(bool plec, double wzrost)
        {
            double wynik = 0;
            wynik = 2 * wzrost; ///wzorek na idealna wage, wg roznych tam wspolczynnikow
            return wynik;
        }

        private double WhrFun(bool plec, double biodra, double pas)
        {
            double wynik = 0;
            wynik = 2 * pas* biodra; ///wzorek na whr
            return wynik;
        }

        private double TluszczFun(bool plec, double waga, double pas)
        {
            double wynik = 0;
            wynik = waga*pas; ///wzorek na tłuszcz
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

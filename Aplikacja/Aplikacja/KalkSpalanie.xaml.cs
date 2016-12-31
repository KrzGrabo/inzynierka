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


    ////ZROBIONY WYGLAD DORZUCIC ALGORYTM LICZENIA
    /// <summary>
    /// Interaction logic for KalkSpalanie.xaml
    /// </summary>
    public partial class KalkSpalanie : Window
    {
        BazaDanychEntities db = new BazaDanychEntities();

        public KalkSpalanie()
        {
            InitializeComponent();
            Opis();
        }


        private void Opis()
        {
            opisLabel.Text = "'Kalkulator spalania kalorii' pozwala na obliczenie ilości spalonych kalorii podczas różnych aktywności. Wyniki mogą w małym stopniu odbiegać od wartości rzeczywistych. Aby otrzymać wynik uzupełnij swoje dane i wybierz jedną z dostępnych aktywności.";
        }


        private void wagaTextbox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.OemComma || e.Key == Key.Delete || e.Key == Key.D0 || e.Key == Key.Back || e.Key == Key.D1 || e.Key == Key.D2 || e.Key == Key.D3 || e.Key == Key.D4 || e.Key == Key.D5 || e.Key == Key.D6 || e.Key == Key.D7 || e.Key == Key.D8 || e.Key == Key.D9 || e.Key == Key.NumPad1 || e.Key == Key.NumPad0 || e.Key == Key.NumPad2 || e.Key == Key.NumPad3 || e.Key == Key.NumPad4 || e.Key == Key.NumPad5 || e.Key == Key.NumPad6 || e.Key == Key.NumPad7 || e.Key == Key.NumPad8 || e.Key == Key.NumPad9)
                e.Handled = false;
            else
                e.Handled = true;
        }

        private void czasTextbox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.OemComma || e.Key == Key.Delete || e.Key == Key.D0 || e.Key == Key.Back || e.Key == Key.D1 || e.Key == Key.D2 || e.Key == Key.D3 || e.Key == Key.D4 || e.Key == Key.D5 || e.Key == Key.D6 || e.Key == Key.D7 || e.Key == Key.D8 || e.Key == Key.D9 || e.Key == Key.NumPad1 || e.Key == Key.NumPad0 || e.Key == Key.NumPad2 || e.Key == Key.NumPad3 || e.Key == Key.NumPad4 || e.Key == Key.NumPad5 || e.Key == Key.NumPad6 || e.Key == Key.NumPad7 || e.Key == Key.NumPad8 || e.Key == Key.NumPad9)
                e.Handled = false;
            else
                e.Handled = true;
        }

        private void obliczButton_Click(object sender, RoutedEventArgs e)
        {
            string walidacja = "";
            double waga=0;
            int indeks=aktywnosciCombo.SelectedIndex;
            double czas=0;

            try
            {
                waga = double.Parse(wagaTextbox.Text.Trim());
            }
            catch (Exception)
            {
                walidacja = walidacja + " \nWpisałeś błędną wartość w pole waga";
            }
            if (waga > 300 || waga < 30) walidacja = walidacja + " \nWartość podana w polu 'waga' jest z poza zakresu. Dostępny zakres to (30,300)";
            try
            {
                czas = double.Parse(czasTextbox.Text.Trim());
            }
            catch (Exception)
            {
                walidacja = walidacja + " \nWpisałeś błędną wartość w pole czas";
            }
            if (czas > 10000 || waga < 1) walidacja = walidacja + " \nWartość podana w polu 'czas' jest z poza zakresu. Dostępny zakres to (1,10'000)";
            if(aktywnosciCombo.SelectedIndex==-1)
            {
                walidacja = walidacja + " \nNie wybrałeś żadnej aktywności";
            }
            if (walidacja == "")
            {
                wynikLabel.Content = Spalanie(waga,czas).ToString()+" kcal";
            }
            else
            {
                walidacja = "Wystąpiły błędy przy wpisywaniu danych:" + walidacja;
                MessageBox.Show(walidacja, "Uwaga", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
        }

        private double Spalanie(double waga, double czas)
        {
            double wynik = 0;
            Spalanie aktywnosc = (Spalanie)aktywnosciCombo.SelectedItem;
            wynik = 0.0175 * aktywnosc.MET.GetValueOrDefault() * waga * czas;
            return wynik;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            System.Windows.Data.CollectionViewSource spalanieViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("spalanieViewSource")));
            // Load data by setting the CollectionViewSource.Source property:
            // spalanieViewSource.Source = [generic data source]
            spalanieViewSource.Source = db.Spalanie.ToList();
        }

    }
}

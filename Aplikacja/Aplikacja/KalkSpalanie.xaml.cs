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
        public KalkSpalanie()
        {
            InitializeComponent();
            Bindowanie();
        }
        private string[] aktywnosci = { "akt1", "akt2", "akt3", "akt4" };
        private double[] wspolczynniki = { 1, 2, 3, 4 };

        private void Bindowanie()
        {
            opisLabel.Text = "'Kalkulator spalania kalorii' pozwala na obliczenie ilości spalonych kalorii podczas różnych aktywności. Wyniki mogą w małym stopniu odbiegać od wartości rzeczywistych. Aby otrzymać wynik uzupełnij swoje dane i wybierz jedną z dostępnych aktywności.";
            aktywnosciCombo.ItemsSource = aktywnosci.ToList();
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
            double aktywnosc = 0;
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
                czas = double.Parse(czasTextbox.Text.Trim());
            }
            catch (Exception)
            {
                walidacja = walidacja + " \nWpisałeś błędną wartość w pole czas";
            }
            if(aktywnosciCombo.SelectedIndex==-1)
            {
                walidacja = walidacja + " \nNie wybrałeś żadnej aktywności";
            }
            if (walidacja == "")
            {
                aktywnosc = wspolczynniki[indeks]; 
                wynikLabel.Content = Spalanie(aktywnosc,waga,czas).ToString();
            }
            else
            {
                walidacja = "Wystąpiły błędy przy wpisywaniu danych:" + walidacja;
                MessageBox.Show(walidacja, "Uwaga", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
        }

        private double Spalanie(double aktywnosc, double waga, double czas)
        {
            double wynik = 0;
            wynik = aktywnosc * waga * czas;
            return wynik;
        }

    }
}

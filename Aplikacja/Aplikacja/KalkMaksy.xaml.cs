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
    /// Interaction logic for KalkMaksy.xaml
    /// </summary>
    public partial class KalkMaksy : Window
    {
        private double[] wspolczynnik = {1, 1.0275, 1.06, 1.09, 1.125, 1.1625, 1.2, 1.2425, 1.285, 1.3325};
        private int[] powtorzenia = {1,2,3,4,5,6,7,8,9,10};
        public KalkMaksy()
        {
            InitializeComponent();
            Bindowanie();
    
        }
    
        private void Bindowanie()
        {
            powtorzeniaCombo.ItemsSource = powtorzenia.ToList();
            opisLabel.Content = "opis lalalalalaalalalaal";
            label_wynik.Content = "Uzupełnij pola i kliknij oblicz aby uzyskać wynik";
        }

        private void obliczButton_Click(object sender, RoutedEventArgs e)
        {
            string walidacja = "";
            double ciezar=0;
            int indeks=powtorzeniaCombo.SelectedIndex;
            double powtorzen;
            double wynik = 0;
            try
            {
                ciezar = double.Parse(ciezarTextbox.Text.Trim());
            }
            catch (Exception)
            {
                walidacja = walidacja + " \nWpisałeś błędną wartość w pole ciężar";
            }
            if(powtorzeniaCombo.SelectedIndex==-1)
            {
                walidacja = walidacja + " \nNie wybrałeś ilości powtórzeń";
            }
            if (walidacja == "")
            {
               powtorzen = wspolczynnik[indeks];
               //string tekst = "wybrane wartosci";
               //tekst = tekst + powtorzen.ToString() + indeks.ToString()+ ciezar.ToString();
               //MessageBox.Show(tekst, "Uwaga", MessageBoxButton.OK, MessageBoxImage.Exclamation);
               wynik = powtorzen * ciezar;
               label_wynik.Content = wynik.ToString();
            }
            else
            {
                walidacja = "Wystąpiły błędy przy wpisywaniu danych:" + walidacja;
                MessageBox.Show(walidacja, "Uwaga", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
        }

        private void ciezarTextbox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.OemComma || e.Key == Key.Delete || e.Key == Key.D0 || e.Key == Key.Back || e.Key == Key.D1 || e.Key == Key.D2 || e.Key == Key.D3 || e.Key == Key.D4 || e.Key == Key.D5 || e.Key == Key.D6 || e.Key == Key.D7 || e.Key == Key.D8 || e.Key == Key.D9 || e.Key == Key.NumPad1 || e.Key == Key.NumPad0 || e.Key == Key.NumPad2 || e.Key == Key.NumPad3 || e.Key == Key.NumPad4 || e.Key == Key.NumPad5 || e.Key == Key.NumPad6 || e.Key == Key.NumPad7 || e.Key == Key.NumPad8 || e.Key == Key.NumPad9)
                e.Handled = false;
            else
                e.Handled = true;
        }
    }
}

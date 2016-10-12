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

        public KalkZapotrzebowanie()
        {
            InitializeComponent();
            Bindowanie();
        }

        private void Bindowanie()
        {
            opisLabel.Content = "opis lalalalalala";
            wynikLabel.Content = "aby uzyskać wynik uzupełnij pola i kliknij oblicz";
        }
            

        //public double Zapotrzebowanie(double waga, double wzrost, int wiek, int plec, int aktywnosc)
        //{
        //    double bmr, epoc, tea, neat = 500, tef;
        //    int dni;
        //    dni = this.aktywnosc_dni[aktywnosc];
        //    bmr = (9.99 * waga) + (6.25 * wzrost) - (4.92 * wiek) + this.plec_wsp[plec]; //podstawowe spalanie
        //    epoc = dni * 0.07 * bmr;  //pomocnicza do tea
        //    tea = ((9 * 60 * dni) + epoc) / 7 + this.aktywnosc_wsp[aktywnosc]; //ze wgledu na aktywnosc
        //    tef = 0.08 * (bmr + epoc + neat); //efekt termiczny pozywienia
        //    return Math.Round(bmr+epoc+tea+tef, 2);
        //}

       
        private void daneButton_Click(object sender, RoutedEventArgs e)
        {
            bool plec = false;
            double aktWaga = 1, wzrost = 1; //Do tych zmienny przypisane wartosci z bazy danych z profilu

            if (plec == true) plecCombo.SelectedIndex = 0;
            else plecCombo.SelectedIndex = 1;
            wagaTextbox.Text = aktWaga.ToString();
            wzrostTextbox.Text = wzrost.ToString();
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
                wynikLabel.Content = "Twoja kalorycznosc: " + wynik.ToString();
            }
            else
            {
                walidacja = "Wystąpiły błędy przy wpisywaniu danych:" + walidacja;
                MessageBox.Show(walidacja, "Uwaga", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
        }
    }
}

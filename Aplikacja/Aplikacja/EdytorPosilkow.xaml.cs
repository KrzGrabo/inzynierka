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
    /// Interaction logic for EdytorDnia.xaml
    /// </summary>
    public partial class EdytorPosilkow : Window
    {

        BazaDanychEntities db = new BazaDanychEntities();
        int id = Sesja.ZwrocId();
        Uzytkownicy uzytkownik = new Uzytkownicy();
        Diety dieta = new Diety();
        DateTime wybranaData = new DateTime();
        Spozycie spozycie = new Spozycie();
        int ilosc_wybranych = 0;
        bool walidacja = true, sprawdzacz = false;

        double bialkoReczne = 0, wegleReczne = 0, tluszczeReczne = 0, kalorieReczne = 0;
        double bialkoReal = 0, wegleReal = 0, tluszczeReal = 0, kalorieReal = 0;
        double bialkoAuto = 0, wegleAuto = 0, tluszczeAuto = 0, kalorieAuto = 0;
        bool wybor = true;

        public EdytorPosilkow()
        {
            InitializeComponent();
            Bindowanie();
            uzytkownik = db.Uzytkownicy.Where(m => m.ID.Equals(id)).FirstOrDefault();
        }

        public void Bindowanie()
        {
            opisLabel.Text = "Tutaj możesz zapisać swoje rzeczywiste spożycie dla danego dnia, wybierz czy chcesz wprowadzić te wartości ręcznie, czy skorzystać z tabeli porduktów spożywczych (z narzędziem automatycznego zliczania kalorii).";
        }

        public void przekazDane(DateTime data)
        {
            wybranaData = data;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

            System.Windows.Data.CollectionViewSource posilekViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("posilekViewSource")));
            // Load data by setting the CollectionViewSource.Source property:
            // posilekViewSource.Source = [generic data source]
            posilekViewSource.Source = db.Posilki.ToList();
            uzytkownik = db.Uzytkownicy.Where(m => m.ID.Equals(id)).FirstOrDefault();
            znajdzDiete();
            sprawdzacz = true;
        }

        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            double kalorieTemp = 0, bialkoTemp = 0, tluszczeTemp = 0, wegleTemp = 0;
            foreach (Posilki posilek in potrawyBox.SelectedItems)
            {
                kalorieTemp = kalorieTemp + (double)posilek.Kalorycznosc;
                bialkoTemp = bialkoTemp + (double)posilek.Bialko;
                wegleTemp = wegleTemp + (double)posilek.Weglowodany;
                tluszczeTemp = tluszczeTemp + (double)posilek.Tluszcz;

            }
            bialkoAuto = bialkoTemp;
            kalorieAuto = kalorieTemp;
            wegleAuto = wegleTemp;
            tluszczeAuto = tluszczeTemp;
            bialkoAutoTextBox.Text = bialkoTemp.ToString();
            tluszczAutoTextBox.Text = tluszczeTemp.ToString();
            weglowodanyAutoTextBox.Text = wegleTemp.ToString();
            kalorieAutoTextBox.Text = kalorieTemp.ToString();


        }

        private void zapiszButton_Click(object sender, RoutedEventArgs e)
        {
            if (db.Spozycie.Where(m => m.Data == wybranaData).FirstOrDefault() != null)
            {
               spozycie = db.Spozycie.Where(m => m.Data == wybranaData).FirstOrDefault();
            }

            spozycie.Data = wybranaData;
            spozycie.Bialko = bialkoReal;
            spozycie.ID_Diety = dieta.Id;
            spozycie.Kalorie = kalorieReal;
            spozycie.Tluszcz = tluszczeReal;
            spozycie.Weglowodany = wegleReal;

            if (db.Spozycie.Where(m => m.Data == wybranaData).FirstOrDefault() == null)
            {
                db.Spozycie.Add(spozycie);
            }
            db.SaveChanges();
            string msg = "Spożycie na dany dzień zostało zapisane poprawnie.";
            MessageBox.Show(msg, "Informacja", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void znajdzDiete()
        {
            var diety = uzytkownik.Diety.ToList();
            foreach (Diety szukana in diety)
            {
                if (szukana.Data_Rozpoczecia <= wybranaData && szukana.Data_Zakonczenia >= wybranaData)
                {
                    dieta = szukana;
                }
            }

        }

        private void Window_Closed(object sender, EventArgs e)
        {
            PlanDnia plan = new PlanDnia();
            plan.przekazDane(wybranaData);
            plan.Show();
        }

        private void walidacjaTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.OemComma || e.Key == Key.Delete || e.Key == Key.D0 || e.Key == Key.Back || e.Key == Key.D1 || e.Key == Key.D2 || e.Key == Key.D3 || e.Key == Key.D4 || e.Key == Key.D5 || e.Key == Key.D6 || e.Key == Key.D7 || e.Key == Key.D8 || e.Key == Key.D9 || e.Key == Key.NumPad1 || e.Key == Key.NumPad0 || e.Key == Key.NumPad2 || e.Key == Key.NumPad3 || e.Key == Key.NumPad4 || e.Key == Key.NumPad5 || e.Key == Key.NumPad6 || e.Key == Key.NumPad7 || e.Key == Key.NumPad8 || e.Key == Key.NumPad9)
                e.Handled = false;
            else
                e.Handled = true;
        }

        private void przeliczKalorieButton_Click(object sender, RoutedEventArgs e)
        {
            string walidacja = "";


            try
            {
                bialkoReczne = double.Parse(bialkoReczneTextBox.Text.Trim());
            }
            catch (Exception)
            {
                walidacja = walidacja + " \nWpisałeś błędną wartość białka";
            }

            try
            {
                tluszczeReczne = double.Parse(tluszczReczneTextBox.Text.Trim());
            }
            catch (Exception)
            {
                walidacja = walidacja + " \nWpisałeś błędną wartość w tłuszczów";
            }
            try
            {
                wegleReczne = double.Parse(weglowodanyRecznePodTextBox.Text.Trim());
            }
            catch (Exception)
            {
                walidacja = walidacja + " \nWpisałeś błędną wartość węglowodanów";
            }

            if (walidacja == "")
            {
                kalorieReczne = tluszczeReczne * 9 + wegleReczne * 4 + bialkoReczne * 4;
                kalorieRecznePodTextBox.Text = kalorieReczne.ToString();
            }
            else
            {
                walidacja = "Wystąpiły błędy przy wpisywaniu danych:" + walidacja;
                MessageBox.Show(walidacja, "Uwaga", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }

        }

        private void zakonczButton_Click(object sender, RoutedEventArgs e)
        {
            if (wybor == true)
            {
                bialkoPodLabel.Content = bialkoReczne;
                tluszczPodLabel.Content = tluszczeReczne;
                weglowodanyPodLabel.Content = wegleReczne;
                kaloriePodLabel.Content = kalorieReczne;
                kalorieReal = kalorieReczne;
                bialkoReal = bialkoReczne;
                tluszczeReal = tluszczeReczne;
                wegleReal = wegleReczne;
            }
            else
            {
                bialkoPodLabel.Content = bialkoAutoTextBox.Text;
                tluszczPodLabel.Content = tluszczAutoTextBox.Text;
                weglowodanyPodLabel.Content = weglowodanyAutoTextBox.Text;
                kaloriePodLabel.Content = kalorieAutoTextBox.Text;
                kalorieReal = kalorieAuto;
                bialkoReal = bialkoAuto;
                tluszczeReal = tluszczeAuto;
                wegleReal = wegleAuto;
            }

            double kalorieTemp = (double)dieta.Kalorycznosc;
            double bialkoTemp = (double)dieta.Bialko;
            double weglowodanyTemp = (double)dieta.Weglowodany;
            double tluszczTemp = (double)dieta.Tluszcz;

            double kalorieRoznica, bialkoRoznica, wegleRoznica, tluszczRoznica;
            kalorieRoznica = kalorieTemp - kalorieReal;
            bialkoRoznica = bialkoTemp - bialkoReal;
            wegleRoznica = weglowodanyTemp - wegleReal;
            tluszczRoznica = tluszczTemp - tluszczeReal;

            if (bialkoRoznica < 0)
            {
                bialkoRoznicaPodLabel.Foreground = Brushes.Crimson;
                bialkoRoznicaPodLabel.Content = "Przekroczyłeś limit białka o [g]: " + bialkoRoznica.ToString();
            }
            else
            {
                bialkoRoznicaPodLabel.Foreground = Brushes.ForestGreen;
                bialkoRoznicaPodLabel.Content = "Brakuje ci do limitu białka [g]: " + bialkoRoznica.ToString();

            }

            if (kalorieRoznica < 0)
            {
                kalorieRoznicaPodLabel.Foreground = Brushes.Crimson;
                kalorieRoznicaPodLabel.Content = "Przekroczyłeś limit kalorii o [kcal]: " + kalorieRoznica.ToString();
            }
            else
            {
                kalorieRoznicaPodLabel.Foreground = Brushes.ForestGreen;
                kalorieRoznicaPodLabel.Content = "Brakuje ci do limitu kalorii [kcal]: " + kalorieRoznica.ToString();
            }

            if (wegleRoznica < 0)
            {
                weglowodanyRoznicaPodLabel.Foreground = Brushes.Crimson;
                weglowodanyRoznicaPodLabel.Content = "Przekroczyłeś limit węglowodanów o [g]: " + wegleRoznica.ToString();
            }
            else
            {
                weglowodanyRoznicaPodLabel.Foreground = Brushes.ForestGreen;
                weglowodanyRoznicaPodLabel.Content = "Brakuje ci do limitu węglowodanów [g]: " + wegleRoznica.ToString();
            }

            if (tluszczRoznica < 0)
            {
                tluszczRoznicaPodLabel.Foreground = Brushes.Crimson;
                tluszczRoznicaPodLabel.Content = "Przekroczyłeś limit tłuszczu o [g]: " + tluszczRoznica.ToString();
            }
            else
            {
                tluszczRoznicaPodLabel.Foreground = Brushes.ForestGreen;
                tluszczRoznicaPodLabel.Content = "Brakuje ci do limitu tłuszczu [g]: " + tluszczRoznica.ToString();
            }
        }

        private void wyborCombo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (sprawdzacz == true)
            {
                if (wyborCombo.SelectedIndex == 0)
                {
                    wybor = true;
                    potrawyBox.IsEnabled = false;
                    bialkoReczneTextBox.IsEnabled = true;
                    tluszczReczneTextBox.IsEnabled = true;
                    weglowodanyRecznePodTextBox.IsEnabled = true;
                    przeliczKalorieButton.IsEnabled = true;
                }
                else
                {
                    wybor = false;
                    potrawyBox.IsEnabled = true;
                    bialkoReczneTextBox.IsEnabled = false;
                    tluszczReczneTextBox.IsEnabled = false;
                    weglowodanyRecznePodTextBox.IsEnabled = false;
                    przeliczKalorieButton.IsEnabled = false;
                }
            }
        }

    }
}
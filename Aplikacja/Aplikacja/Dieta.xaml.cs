using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
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
    /// Interaction logic for Dieta.xaml
    /// </summary>
    public partial class Dieta : Window
    {
        public static double zapotrzebowanie=0;
        BazaDanychEntities db = new BazaDanychEntities();
        int id = Sesja.ZwrocId();
        Dane przypisaneDane = new Dane();
        Uzytkownicy uzytkownik = new Uzytkownicy();

        public Dieta()
        {
            InitializeComponent();
            Bindowanie();
        }

        double wagaPom=0, kaloriePom=0, bialkoPom, weglowodanyPom, tluszczePom, podzialPom;
        //bialko,wegle,tluszcze to wyliczona ilosc danego skladnika w diecie, podzialPom to procentowy udzial wegli do tluszczy zakres<5,95>
        double kalorieBialko, kalorieTluszcz, kalorieWeglowodany;
        //ile kalori dostarczamy z poczegolnych makroskladnikow
       
 
        private void Bindowanie()
        {
            podzialLabel.Content = "50:50";
            if(zapotrzebowanie!=0)
            {
               // wynikLabel.Content = zapotrzebowanie.ToString();
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            KalkZapotrzebowanie menu = new KalkZapotrzebowanie();

            menu.Show();

        }

        public void Algorytm()
        {
            double pozostaleKalorie, ileBialka ;


            if (sportCombo.SelectedIndex == 0) ileBialka = 1.3;
            else
            {
                if (sportCombo.SelectedIndex == 1) ileBialka = 1.6;
                else
                {
                    if (sportCombo.SelectedIndex == 2) ileBialka = 2;
                    else ileBialka = 2;
                }
            }

           if (celCombo.SelectedIndex == 0)
           {
               kaloriePom = kaloriePom - 300;
               ileBialka = ileBialka + 0.5;
           }
           if (celCombo.SelectedIndex == 2) kaloriePom = kaloriePom+500;

           bialkoPom = ileBialka * wagaPom;
           kalorieBialko =4 * bialkoPom;
           pozostaleKalorie = kaloriePom - kalorieBialko;
           kalorieWeglowodany = pozostaleKalorie * podzialPom / 100;
           kalorieTluszcz = pozostaleKalorie - kalorieWeglowodany;
           weglowodanyPom = kalorieWeglowodany / 4;
           tluszczePom = kalorieTluszcz / 9;
        }

        private void WalidacjaTextboxow(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.OemComma || e.Key == Key.Delete || e.Key == Key.D0 || e.Key == Key.Back || e.Key == Key.D1 || e.Key == Key.D2 || e.Key == Key.D3 || e.Key == Key.D4 || e.Key == Key.D5 || e.Key == Key.D6 || e.Key == Key.D7 || e.Key == Key.D8 || e.Key == Key.D9 || e.Key == Key.NumPad1 || e.Key == Key.NumPad0 || e.Key == Key.NumPad2 || e.Key == Key.NumPad3 || e.Key == Key.NumPad4 || e.Key == Key.NumPad5 || e.Key == Key.NumPad6 || e.Key == Key.NumPad7 || e.Key == Key.NumPad8 || e.Key == Key.NumPad9)
                e.Handled = false;
            else
                e.Handled = true;
        }

        private void dalej1Button_Click(object sender, RoutedEventArgs e)
        {
            
 
            string walidacja = "";

            
            try
            {
                wagaPom = double.Parse(wagaTextbox.Text.Trim());
            }
            catch (Exception)
            {
                walidacja = walidacja + " \nWpisałeś błędną wartość w pole waga";
            }
            
            try
            {
                kaloriePom = double.Parse(zapotrzebowanieTextbox.Text.Trim());
            }
            catch (Exception)
            {
                walidacja = walidacja + " \nWpisałeś błędną wartość w pole waga";
            }


            if(walidacja=="")
            {
                krok2Tab.IsEnabled = true;
                krok1Tab.IsEnabled = false;
                oknoTabcontrol.SelectedIndex = 1;           
            }
            else 
            {
                walidacja = "Wystąpiły błędy przy wpisywaniu danych:" + walidacja;
                MessageBox.Show(walidacja, "Uwaga", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
        }

        private void dalej2Button_Click(object sender, RoutedEventArgs e)
        {
            string walidacja = "";

            if (podzialLabel.Content == "50:50") walidacja = walidacja + " \nNie ustawiłeś suwaka proporcji";

            if (celCombo.SelectedIndex == -1) walidacja = walidacja + " \nNie ustawiłeś celu diety";
        
            if (sportCombo.SelectedIndex == -1) walidacja = walidacja + " \nNie wybrałeś kategorii sportu"; 

            if (walidacja == "")
            {
                krok3Tab.IsEnabled = true;
                krok2Tab.IsEnabled = false;
                oknoTabcontrol.SelectedIndex = 2;
            }
            else
            {
                walidacja = "Wystąpiły błędy przy wpisywaniu danych:" + walidacja;
                MessageBox.Show(walidacja, "Uwaga", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
        }

        private void dalej3Button_Click(object sender, RoutedEventArgs e)
        {
            string walidacja = "";

            if (startDatapicker.SelectedDate == null) walidacja = walidacja + " \nNie wybrałeś daty początkowej";
            if (koniecDatapicker.SelectedDate == null) walidacja = walidacja + " \nNie wybrałeś daty końcowej"; 
            if (posilkiCombo.SelectedIndex == -1) walidacja = walidacja + " \nNie wybrałeś ilości posiłków"; 
            //TRZEBA DODAC SPRAWDZENIE CZY KONCOWA JEST POZNIEJ NIZ POCZATKOWA
            if (walidacja == "")
            {
                podsumowanieTab.IsEnabled = true;
                krok3Tab.IsEnabled = false;
                oknoTabcontrol.SelectedIndex = 3;
            }
            else
            {
                walidacja = "Wystąpiły błędy przy wpisywaniu danych:" + walidacja;
                MessageBox.Show(walidacja, "Uwaga", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
        }

        private void powrot2Button_Click(object sender, RoutedEventArgs e)
        {
            krok1Tab.IsEnabled = true;
            krok2Tab.IsEnabled = false;
            oknoTabcontrol.SelectedIndex = 0;
        }

        private void powrot3Button_Click(object sender, RoutedEventArgs e)
        {
            krok2Tab.IsEnabled = true;
            krok3Tab.IsEnabled = false;
            oknoTabcontrol.SelectedIndex = 1;
        }

        

        private void podzialSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            var slider = sender as Slider;
            double value = slider.Value;
            podzialPom = value;
            string tlusz = "", wegle = "";
            tlusz = value.ToString();
            wegle = (100 - value).ToString();
            string tekstowa = tlusz + ":" + wegle;
            if (podzialLabel != null)
            {
                podzialLabel.Content = tekstowa;
            }

        }



        private void mojedaneButton_Click(object sender, RoutedEventArgs e)
        {
            uzytkownik = db.Uzytkownicy.Where(m => m.ID.Equals(id)).FirstOrDefault();
            przypisaneDane = uzytkownik.Dane;
            double aktWaga = przypisaneDane.Waga.GetValueOrDefault(),
                 wzrost = przypisaneDane.Wzrost.GetValueOrDefault(),
                 wiek = przypisaneDane.Wiek.GetValueOrDefault();
            zapotrzebowanie = przypisaneDane.Zapotrzebowanie.GetValueOrDefault();

            ustawPlec();
            wagaTextbox.Text = aktWaga.ToString();
            zapotrzebowanieTextbox.Text = zapotrzebowanie.ToString();
        }

        private void porownanieButton_Click(object sender, RoutedEventArgs e)
        {
            Porownanie okno = new Porownanie();
            okno.Show();
        }

        private void startDatapicker_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            // ... Get DatePicker reference.
            var picker = sender as DatePicker;

            // ... Get nullable DateTime from SelectedDate.
            DateTime? date = picker.SelectedDate;
            if (date == null)
            {
                // ... A null object.
                this.Title = "No date";
            }
            else
            {
                // ... No need to display the time.
                this.Title = date.Value.ToShortDateString();
            }
        }

        private void koniecDatapicker_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            // ... Get DatePicker reference.
            var picker = sender as DatePicker;

            // ... Get nullable DateTime from SelectedDate.
            DateTime? date = picker.SelectedDate;
            if (date == null)
            {
                // ... A null object.
                this.Title = "No date";
            }
            else
            {
                // ... No need to display the time.
                this.Title = date.Value.ToShortDateString();
            }
        }


        private async void obliczButton_Click(object sender, RoutedEventArgs e)
        {
            Algorytm();
            var progress = new Progress<int>(value => pbStatus.Value = value);
            await Task.Run(() =>
            {
                for (int i = 0; i <= 100; i++)
                {
                    ((IProgress<int>)progress).Report(i);
                    Thread.Sleep(10);
                }
            });
            opis4Label.Content = "Dieta zostala wyliczona";
            string tekst = "Twoje zapotrzebowanie to: " + zapotrzebowanie.ToString() + " Kaloryczność twojej diety to: " + kaloriePom.ToString() + " Ilość białka w twojej diecie: " + bialkoPom.ToString() + " (to " + kalorieBialko.ToString() + " kalorii)" + " Ilość tluszczu w twojej diecie: " + tluszczePom.ToString() + " (to " + kalorieTluszcz.ToString() + " kalorii)" + " Ilość weglowodanow w twojej diecie: " + weglowodanyPom.ToString() + " (to " + kalorieWeglowodany.ToString() + " kalorii)";
            MessageBox.Show(tekst);
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

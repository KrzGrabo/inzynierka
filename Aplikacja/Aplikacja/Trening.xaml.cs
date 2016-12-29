﻿using System;
using System.Collections;
using System.Collections.Generic;
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
    /// Interaction logic for Trening.xaml
    /// </summary>
    public partial class Trening : Window
    {

        public static double zapotrzebowanie = 0;
        BazaDanychEntities db = new BazaDanychEntities();
        int id = Sesja.ZwrocId();
        Dane przypisaneDane = new Dane();
        Uzytkownicy uzytkownik = new Uzytkownicy();
        ArrayList wybor = new ArrayList();
        String dataPocz;
        String dataKon;
        private string[] typyTrening = { "odpoczynek", "siłowy", "wytrzymałościowy", "szybkościowy", "techniczny", "gibkościowy", "interwałowy", "kondycyjny", "zwinności", "ogólnorozwojowy" };
        double poniedzialekCzas, wtorekCzas, srodaCzas, czwartekCzas, piatekCzas, sobotaCzas, niedzielaCzas, sumaCzas, sredniCzas;
        int poniedzialekTyp, wtorekTyp, srodaTyp, czwartekTyp, piatekTyp, sobotaTyp, niedzielaTyp;
        int iloscTrenTydz=0;
        double wagaPom, kaloriePom;
        public Trening()
        {
            InitializeComponent();
            Bindowanie();
        }

        public void Bindowanie()
        {
            poniedzialekCombo.ItemsSource = typyTrening;
            wtorekCombo.ItemsSource = typyTrening;
            srodaCombo.ItemsSource = typyTrening;
            czwartekCombo.ItemsSource = typyTrening;
            piatekCombo.ItemsSource = typyTrening;
            sobotaCombo.ItemsSource = typyTrening;
            niedzielaCombo.ItemsSource = typyTrening;

            opis1Label.Content = "Kreator treningu- krok I";
            opis1Textblock.Text = "Kreator treningu dopasuje, dostosowany pod twoje cele i preferencje cykl treningowy. Następnie będziesz miał możliwość dodania go do bazy w wybranym przedziale czasowym. Postępuj zgodnie z poleceniami w kolejnych krokach, w pierwszym uzupełnij podstawowe dane, na których będzie bazować kreator.";
            opis2aLabel.Content = "Kreator treningu- krok II";
            opis3aLabel.Content = "Kreator treningu- krok III";
            opis2Label.Text = "W drugim kroku uzupełnij swój plan tygodnia. Do każdego dnia dobierz odpowiadający typ treningu(jeśli jest to dzień nietreningowy, pozostaw opcję odpoczynek). Dodatkowo dobierz przewidywany czas treningu.";
            opis3Label.Text = "Trzeci krok skupia się na technicznych aspektach cyklu- wybierz na jaki okres planujesz cykl.";

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
                walidacja = walidacja + " \nWpisałeś błędną wartość w pole kalorie";
            }
            if (stazCombo.SelectedIndex == -1) walidacja = walidacja + "\nNie wybrałeś stażu treningowego";
            if (plecCombo.SelectedIndex == -1) walidacja = walidacja + "\nNie wybrałeś swojej płci";
            if (wagaPom > 300 || wagaPom < 30) walidacja = walidacja + " \nWpisałeś wartość z poza zakresu w pole waga, dostępne wartości(30,300)";
            if (kaloriePom > 10000 || kaloriePom < 500) walidacja = walidacja + " \nWpisałeś wartość z poza zakresu w pole zapotrzebowanie, dostępne wartości(500,10'000)";
            if (walidacja == "")
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

        private void mojedaneButton_Click(object sender, RoutedEventArgs e)
        {
            uzytkownik = db.Uzytkownicy.Where(m => m.ID.Equals(id)).FirstOrDefault();
            przypisaneDane = uzytkownik.Dane;
            double aktWaga = przypisaneDane.Waga.GetValueOrDefault(),
                 wiek = przypisaneDane.Wiek.GetValueOrDefault();
            zapotrzebowanie = przypisaneDane.Zapotrzebowanie.GetValueOrDefault();

            ustawPlec();
            wagaTextbox.Text = aktWaga.ToString();
            zapotrzebowanieTextbox.Text = zapotrzebowanie.ToString();
            // wiekTextbox.Text = wiek.ToString();
        }

        private void WalidacjaTextboxow(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.OemComma || e.Key == Key.Delete || e.Key == Key.D0 || e.Key == Key.Back || e.Key == Key.D1 || e.Key == Key.D2 || e.Key == Key.D3 || e.Key == Key.D4 || e.Key == Key.D5 || e.Key == Key.D6 || e.Key == Key.D7 || e.Key == Key.D8 || e.Key == Key.D9 || e.Key == Key.NumPad1 || e.Key == Key.NumPad0 || e.Key == Key.NumPad2 || e.Key == Key.NumPad3 || e.Key == Key.NumPad4 || e.Key == Key.NumPad5 || e.Key == Key.NumPad6 || e.Key == Key.NumPad7 || e.Key == Key.NumPad8 || e.Key == Key.NumPad9)
                e.Handled = false;
            else
                e.Handled = true;
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

        private void powrot2Button_Click(object sender, RoutedEventArgs e)
        {
            krok1Tab.IsEnabled = true;
            krok2Tab.IsEnabled = false;
            oknoTabcontrol.SelectedIndex = 0;
        }

        private void dalej2Button_Click(object sender, RoutedEventArgs e)
        {
            string walidacja = "";


            if (poniedzialekCombo.SelectedIndex > 0)
            {
                try
                {
                    poniedzialekCzas = double.Parse(poniedzialekCzasTextbox.Text.Trim());
                }
                catch (Exception)
                {
                    walidacja = walidacja + " \nWpisałeś błędną wartość w polu przewidywany czas treningu w poniedziałek";
                }
                if (poniedzialekCzas < 10 || poniedzialekCzas > 500) walidacja = walidacja + " \nWpisałeś wartość z poza zakresu w pole czas treningu w poniedziałek, dostępne wartości(10,500)";
            }
         
            if (wtorekCombo.SelectedIndex > 0)
            {
                try
                {
                    wtorekCzas = double.Parse(wtorekCzasTextbox.Text.Trim());
                }
                catch (Exception)
                {
                    walidacja = walidacja + " \nWpisałeś błędną wartość w polu przewidywany czas treningu w wtorek";
                }
                if (wtorekCzas < 10 || wtorekCzas > 500) walidacja = walidacja + " \nWpisałeś wartość z poza zakresu w pole czas treningu w wtorek, dostępne wartości(10,500)";
            }
        
            if (srodaCombo.SelectedIndex > 0)
            {
                try
                {
                    srodaCzas = double.Parse(srodaCzasTextbox.Text.Trim());
                }
                catch (Exception)
                {
                    walidacja = walidacja + " \nWpisałeś błędną wartość w polu przewidywany czas treningu w środę";
                }
                if (srodaCzas < 10 || srodaCzas > 500) walidacja = walidacja + " \nWpisałeś wartość z poza zakresu w pole czas treningu w środę, dostępne wartości(10,500)";
            }
        

            if (czwartekCombo.SelectedIndex > 0)
            {
                try
                {
                    czwartekCzas = double.Parse(czwartekCzasTextbox.Text.Trim());
                }
                catch (Exception)
                {
                    walidacja = walidacja + " \nWpisałeś błędną wartość w polu przewidywany czas treningu w czwartek";
                }
                if (czwartekCzas < 10 || czwartekCzas > 500) walidacja = walidacja + " \nWpisałeś wartość z poza zakresu w pole czas treningu w czwartek, dostępne wartości(10,500)";
            }
            
            if (piatekCombo.SelectedIndex > 0)
            {
                try
                {
                    piatekCzas = double.Parse(piatekCzasTextbox.Text.Trim());
                }
                catch (Exception)
                {
                    walidacja = walidacja + " \nWpisałeś błędną wartość w polu przewidywany czas treningu w piątek";
                }
                if (piatekCzas < 10 || piatekCzas > 500) walidacja = walidacja + " \nWpisałeś wartość z poza zakresu w pole czas treningu w piątek, dostępne wartości(10,500)";
            }
            
            if (sobotaCombo.SelectedIndex > 0)
            {
                try
                {
                    sobotaCzas = double.Parse(sobotaCzasTextbox.Text.Trim());
                }
                catch (Exception)
                {
                    walidacja = walidacja + " \nWpisałeś błędną wartość w polu przewidywany czas treningu w sobotę";
                }
                if (sobotaCzas < 10 || sobotaCzas > 500) walidacja = walidacja + " \nWpisałeś wartość z poza zakresu w pole czas treningu w sobotę dostępne wartości(10,500)";
            }
            
            if (niedzielaCombo.SelectedIndex > 0)
            {
                try
                {
                    niedzielaCzas = double.Parse(niedzielaCzasTextbox.Text.Trim());
                }
                catch (Exception)
                {
                    walidacja = walidacja + " \nWpisałeś błędną wartość w polu przewidywany czas treningu w niedzielę";
                }
                if (niedzielaCzas < 10 || niedzielaCzas > 500) walidacja = walidacja + " \nWpisałeś wartość z poza zakresu w pole czas treningu w niedzielę, dostępne wartości(10,500)";
            }
            
            if (walidacja == "")
            {

                poniedzialekTyp = poniedzialekCombo.SelectedIndex;
                wtorekTyp = wtorekCombo.SelectedIndex;
                srodaTyp = srodaCombo.SelectedIndex;
                czwartekTyp = czwartekCombo.SelectedIndex;
                piatekTyp = piatekCombo.SelectedIndex;
                sobotaTyp = sobotaCombo.SelectedIndex;
                niedzielaTyp = niedzielaCombo.SelectedIndex;
                
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
           
            //TRZEBA DODAC SPRAWDZENIE CZY KONCOWA JEST POZNIEJ NIZ POCZATKOWA
            if (walidacja == "")
            {
                podsumowanieTab.IsEnabled = true;
                krok3Tab.IsEnabled = false;
                oknoTabcontrol.SelectedIndex = 3;
                
                this.Height = 800;
                this.Width = 800;
            }
            else
            {
                walidacja = "Wystąpiły błędy przy wpisywaniu danych:" + walidacja;
                MessageBox.Show(walidacja, "Uwaga", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }

        }

        private void powrot3Button_Click(object sender, RoutedEventArgs e)
        {
            krok2Tab.IsEnabled = true;
            krok3Tab.IsEnabled = false;
            oknoTabcontrol.SelectedIndex = 1;
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
            }
            else
            {
                // ... No need to display the time
                dataPocz = date.Value.ToShortDateString();
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
            }
            else
            {
                // ... No need to display the time.
                dataKon = date.Value.ToShortDateString();
            }
        }


        private async void obliczButton_Click(object sender, RoutedEventArgs e)
        {
            var progress = new Progress<int>(value => pbStatus.Value = value);
            await Task.Run(() =>
            {
                for (int i = 0; i <= 100; i++)
                {
                    ((IProgress<int>)progress).Report(i);
                    Thread.Sleep(10);
                }
            });
            opis4Label.Content = "Cykl treningowy został utworzony.";
            treningPoniedzialekLabel.Content = typyTrening[poniedzialekCombo.SelectedIndex];
            treningWtorekLabel.Content = typyTrening[wtorekCombo.SelectedIndex];
            treningSrodaLabel.Content = typyTrening[srodaCombo.SelectedIndex];
            treningCzwartekLabel.Content = typyTrening[czwartekCombo.SelectedIndex];
            treningPiatekLabel.Content = typyTrening[piatekCombo.SelectedIndex];
            treningSobotaLabel.Content = typyTrening[sobotaCombo.SelectedIndex];
            treningNiedzielaLabel.Content = typyTrening[niedzielaCombo.SelectedIndex];

            if (poniedzialekCombo.SelectedIndex > 0)
            {
                czasPoniedzialekLabel.Content = poniedzialekCzas + " min";
                iloscTrenTydz = iloscTrenTydz + 1;
            }
            if (wtorekCombo.SelectedIndex > 0)
            {
                iloscTrenTydz = iloscTrenTydz + 1;
                czasWtorekLabel.Content = wtorekCzas + " min";
            }
            if (srodaCombo.SelectedIndex > 0)
            {
                iloscTrenTydz = iloscTrenTydz + 1;
                czasSrodaLabel.Content = srodaCzas + " min";
            }
            if (czwartekCombo.SelectedIndex > 0)
            {
                iloscTrenTydz = iloscTrenTydz + 1;
                czasCzwartekLabel.Content = czwartekCzas + " min";
            }
            if (piatekCombo.SelectedIndex > 0)
            {
                iloscTrenTydz = iloscTrenTydz + 1;
                czasPiatekLabel.Content = piatekCzas + " min";
            }
            if (sobotaCombo.SelectedIndex > 0)
            {
                iloscTrenTydz = iloscTrenTydz + 1;
                czasSobotaLabel.Content = sobotaCzas + " min";
            }
            if (niedzielaCombo.SelectedIndex > 0)
            {
                iloscTrenTydz = iloscTrenTydz + 1;
                czasNiedzielaLabel.Content = niedzielaCzas + " min";
            }
            
            sumaCzas = poniedzialekCzas + wtorekCzas + srodaCzas + czwartekCzas + piatekCzas + sobotaCzas + niedzielaCzas;
            sumczasPodLabel.Content = sumaCzas.ToString();
            sredniczasPodLabel.Content = (sumaCzas / 7).ToString();
            iloscPodLabel.Content=iloscTrenTydz.ToString();
            
            if(iloscTrenTydz>5)
            {
                czestotliwoscPodLabel.Content = "Bardzo wysoka";
                czestotliwoscPodLabel.Foreground = Brushes.Crimson;
            }
            else
            {
                if (iloscTrenTydz > 3) 
                {
                    czestotliwoscPodLabel.Content = "Wysoka";
                    czestotliwoscPodLabel.Foreground = Brushes.Yellow;
                }
                else
                {
                    if (iloscTrenTydz == 3) 
                    {
                        czestotliwoscPodLabel.Content = "Średnia";
                        czestotliwoscPodLabel.Foreground = Brushes.SpringGreen;
                    }
                    else
                    {
                        if (iloscTrenTydz<3 && iloscTrenTydz>0)
                        {
                            czestotliwoscPodLabel.Content = "Niska";
                            czestotliwoscPodLabel.Foreground = Brushes.SlateBlue;
                        }
                        else czestotliwoscPodLabel.Content = "Brak treningów";
                    }
                }
            }

            if (sumaCzas > 700)
            {
                dlugoscPodLabel.Content = "Bardzo wysoka";
                dlugoscPodLabel.Foreground = Brushes.Crimson;
            }
            else
            {
                if (sumaCzas > 550)
                {
                    dlugoscPodLabel.Content = "Wysoka";
                    dlugoscPodLabel.Foreground = Brushes.Yellow;
                }
                else
                {
                    if (sumaCzas  > 240)
                    {
                        dlugoscPodLabel.Content = "Średnia";
                        dlugoscPodLabel.Foreground = Brushes.SpringGreen;
                    }
                    else
                    {
                        if (sumaCzas > 150)
                        {
                            dlugoscPodLabel.Content = "Niska";
                            dlugoscPodLabel.Foreground = Brushes.SlateBlue;
                        }
                        else
                        {
                            dlugoscPodLabel.Content = "Brak treningów";
                        }
                    }
                }
            }

        }

        private void zmianaTrening_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (poniedzialekCombo.SelectedIndex == 0) 
            {
                poniedzialekCzasTextbox.Text = "0";
                poniedzialekCzasTextbox.IsEnabled = false; 
            }
            else poniedzialekCzasTextbox.IsEnabled = true;
        }

        private void wtorekCombo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (wtorekCombo.SelectedIndex == 0)
            {
                wtorekCzasTextbox.Text = "0";
                wtorekCzasTextbox.IsEnabled = false;
            }
            else wtorekCzasTextbox.IsEnabled = true;
        }

        private void srodaCombo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (srodaCombo.SelectedIndex == 0)
            {
                srodaCzasTextbox.Text = "0";
                srodaCzasTextbox.IsEnabled = false;
            }
            else srodaCzasTextbox.IsEnabled = true;
        }

        private void czwartekCombo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (czwartekCombo.SelectedIndex == 0)
            {
                czwartekCzasTextbox.Text = "0";
                czwartekCzasTextbox.IsEnabled = false;
            }
            else czwartekCzasTextbox.IsEnabled = true;
        }

        private void piatekCombo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (piatekCombo.SelectedIndex == 0)
            {
                piatekCzasTextbox.Text = "0";
                piatekCzasTextbox.IsEnabled = false;
            }
            else piatekCzasTextbox.IsEnabled = true;
        }

        private void sobotaCombo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (sobotaCombo.SelectedIndex == 0)
            {
                sobotaCzasTextbox.Text = "0";
                sobotaCzasTextbox.IsEnabled = false;
            }
            else sobotaCzasTextbox.IsEnabled = true;
        }

        private void niedzielaCombo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (niedzielaCombo.SelectedIndex == 0)
            {
                niedzielaCzasTextbox.Text = "0";
                niedzielaCzasTextbox.IsEnabled = false;
            }
            else niedzielaCzasTextbox.IsEnabled = true;
        }
    }
}
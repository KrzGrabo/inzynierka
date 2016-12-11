using System;
using System.Collections;
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
    /// Interaction logic for Trening.xaml
    /// </summary>
    public partial class Trening : Window
    {

        BazaDanychEntities db = new BazaDanychEntities();
        int id = Sesja.ZwrocId();
        Dane przypisaneDane = new Dane();
        Uzytkownicy uzytkownik = new Uzytkownicy();
        ArrayList wybor = new ArrayList();
        String dataPocz;
        String dataKon;

        public Trening()
        {
            InitializeComponent();
        }

        private void dalej1Button_Click(object sender, RoutedEventArgs e)
        {
            krok2Tab.IsEnabled = true;
            krok1Tab.IsEnabled = false;
            oknoTabcontrol.SelectedIndex = 1;  
        }

        private void mojedaneButton_Click(object sender, RoutedEventArgs e)
        {
            uzytkownik = db.Uzytkownicy.Where(m => m.ID.Equals(id)).FirstOrDefault();
            przypisaneDane = uzytkownik.Dane;
            double aktWaga = przypisaneDane.Waga.GetValueOrDefault(),
                 wiek = przypisaneDane.Wiek.GetValueOrDefault();

            ustawPlec();
            wagaTextbox.Text = aktWaga.ToString();
            wiekTextbox.Text = wiek.ToString();
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
            var list = sportList.SelectedItems;
            string sport;
            foreach(var x in list)
            {
                sport = x.ToString().Replace("System.Windows.Controls.ListBoxItem: ", "");
                wybor.Add(sport);
            }

            krok3Tab.IsEnabled = true;
            krok2Tab.IsEnabled = false;
            oknoTabcontrol.SelectedIndex = 2;
        }

        private void dalej3Button_Click(object sender, RoutedEventArgs e)
        {
            podsumowanie();
            podsumowanieTab.IsEnabled = true;
            krok3Tab.IsEnabled = false;
            oknoTabcontrol.SelectedIndex = 3;
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

        private void podsumowanie()
        {
            celLabel.Content = celCombo.SelectedItem.ToString().Replace("System.Windows.Controls.ComboBoxItem: ", "");
            string aktywnosci = "";
            foreach(string x in wybor)
            {
                aktywnosci += x + ",";
            }
            aktywnosci = aktywnosci.Remove(aktywnosci.Length - 1);
            aktywnosciLabel.Content = aktywnosci;
            dataPoczLabel.Content = dataPocz;
            dataZakLabel.Content = dataKon;
            godzinyLabel.Content = godzinyTextbox.Text;

        }

        private void obliczButton_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}

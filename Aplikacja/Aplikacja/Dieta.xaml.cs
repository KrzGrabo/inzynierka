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
    /// Interaction logic for Dieta.xaml
    /// </summary>
    public partial class Dieta : Window
    {
        public static double zapotrzebowanie=0;
        public Dieta()
        {
            InitializeComponent();
            Bindowanie();
        }
       
        private void Bindowanie()
        {
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

        private void WalidacjaTextboxow(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.OemComma || e.Key == Key.Delete || e.Key == Key.D0 || e.Key == Key.Back || e.Key == Key.D1 || e.Key == Key.D2 || e.Key == Key.D3 || e.Key == Key.D4 || e.Key == Key.D5 || e.Key == Key.D6 || e.Key == Key.D7 || e.Key == Key.D8 || e.Key == Key.D9 || e.Key == Key.NumPad1 || e.Key == Key.NumPad0 || e.Key == Key.NumPad2 || e.Key == Key.NumPad3 || e.Key == Key.NumPad4 || e.Key == Key.NumPad5 || e.Key == Key.NumPad6 || e.Key == Key.NumPad7 || e.Key == Key.NumPad8 || e.Key == Key.NumPad9)
                e.Handled = false;
            else
                e.Handled = true;
        }

        private void dalej1Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void dalej2Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void powrot2Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void powrot3Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void dalej3Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void podzialSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            var slider = sender as Slider;
            double value = slider.Value;
            
            //this.Title = "Value: " + value.ToString("0.0") + "/" + slider.Maximum;
        }

        private void mojedaneButton_Click(object sender, RoutedEventArgs e)
        {

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


    }
}

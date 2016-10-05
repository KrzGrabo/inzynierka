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
        public double waga = 0; //kg
        public double wzrost = 0; //cm
        public int wiek = 25;
        public int plec = 0;
        public string[] plec_nazwa = { "mezczyzna", "kobieta" };
        public int[] plec_wsp = { 5, -161 };
        public int aktywnosc = 0;
        public string[] aktywnosc_nazwa = { "lekka", "srednia", "wysoka" };
        public int[] aktywnosc_wsp = { 5, 35, 180 };
        public int[] aktywnosc_dni = { 1, 3, 5 };

        public KalkZapotrzebowanie()
        {
            InitializeComponent();
            for (int i = 0; i < this.plec_nazwa.Length; i++)
                this.combo_plec.Items.Add(this.plec_nazwa[i]);
            for (int i = 0; i < this.aktywnosc_nazwa.Length; i++)
                this.combo_aktywnosc.Items.Add(this.aktywnosc_nazwa[i]);
        }

        public double Zapotrzebowanie(double waga, double wzrost, int wiek, int plec, int aktywnosc)
        {
            double bmr, epoc, tea, neat = 500, tef;
            int dni;
            dni = this.aktywnosc_dni[aktywnosc];
            bmr = (9.99 * waga) + (6.25 * wzrost) - (4.92 * wiek) + this.plec_wsp[plec]; //podstawowe spalanie
            epoc = dni * 0.07 * bmr;  //pomocnicza do tea
            tea = ((9 * 60 * dni) + epoc) / 7 + this.aktywnosc_wsp[aktywnosc]; //ze wgledu na aktywnosc
            tef = 0.08 * (bmr + epoc + neat); //efekt termiczny pozywienia
            return Math.Round(bmr+epoc+tea+tef, 2);
        }

        private void label_waga_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (Double.TryParse(this.label_waga.Text, out this.waga))
            {
                PokazWynik();
            }
        }

        private void label_wzrost_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (Double.TryParse(this.label_wzrost.Text, out this.wzrost))
            {
                PokazWynik();
            }
        }

        private void combo_plec_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            this.plec = this.combo_plec.SelectedIndex;
            PokazWynik();
        }

        private void combo_aktywnosc_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            this.aktywnosc = this.combo_aktywnosc.SelectedIndex;
            PokazWynik();
        }
        public void PokazWynik()
        {
            this.label_wynik.Content = Zapotrzebowanie(this.waga, this.wzrost, this.wiek, this.plec, this.aktywnosc) + " kcal";
        }
    }
}

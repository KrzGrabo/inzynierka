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
        public double waga = 0;
        public int ile_powtorzen = 0; //jako index wspolczynnika
        public KalkMaksy()
        {
            InitializeComponent();
            for (int i = 1; i <= 10; i++ )
                this.combo_ile.Items.Add(i);
        }
        public double MaxWycisk(double waga, int ile_index)
        {
            return Math.Round(this.wspolczynnik[ile_index] * waga, 2);
        }
        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (Double.TryParse(this.text_waga.Text, out this.waga))
            {
                if (this.waga < 0) 
                    this.waga *= -1;
                PokazWynik();
            }
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            this.ile_powtorzen = this.combo_ile.SelectedIndex;
            PokazWynik();
        }
        public void PokazWynik()
        {
            this.label_wynik.Content = MaxWycisk(this.waga, this.ile_powtorzen) + " kg";
        }
    }
}

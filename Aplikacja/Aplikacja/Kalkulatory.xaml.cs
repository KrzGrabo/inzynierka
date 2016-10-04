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
    /// Interaction logic for Kalkulatory.xaml
    /// </summary>
    public partial class Kalkulatory : Window
    {
        public Kalkulatory()
        {
            InitializeComponent();
        }

        private void wagaButton_Click(object sender, RoutedEventArgs e)
        {
            KalkWaga okno = new KalkWaga();
            okno.Show();
        }

        private void zapotrzebowanieButton_Click(object sender, RoutedEventArgs e)
        {
            KalkZapotrzebowanie okno = new KalkZapotrzebowanie();
            okno.Show();
        }

        private void spalanieButton_Click(object sender, RoutedEventArgs e)
        {
            KalkSpalanie okno = new KalkSpalanie();
            okno.Show();
        }

        private void maksyButton_Click(object sender, RoutedEventArgs e)
        {
            KalkMaksy okno = new KalkMaksy();
            okno.Show();
        }
    }
}

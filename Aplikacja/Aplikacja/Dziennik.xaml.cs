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
    /// Interaction logic for Dziennik.xaml
    /// </summary>
    public partial class Dziennik : Window
    {
        public Dziennik()
        {
            InitializeComponent();
        }

        private void Cal_SelectedDatesChanged(object sender, SelectionChangedEventArgs e)
        {
            EdytorDnia edytor = new EdytorDnia();
            edytor.ustawDate(Kalendarz.SelectedDate.GetValueOrDefault());
            edytor.Show();
        }



    }
}

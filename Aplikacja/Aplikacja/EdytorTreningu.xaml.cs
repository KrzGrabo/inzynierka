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
    /// Interaction logic for EdytorTreningu.xaml
    /// </summary>
    public partial class EdytorTreningu : Window
    {

        BazaDanychEntities db = new BazaDanychEntities();
        int id = Sesja.ZwrocId();
        Uzytkownicy uzytkownik = new Uzytkownicy();
        DateTime wybranaData = new DateTime();
        private string[] typyTrening = { "odpoczynek", "siłowy", "wytrzymałościowy", "szybkościowy", "techniczny", "gibkościowy", "interwałowy", "kondycyjny", "zwinności", "ogólnorozwojowy" };

        public EdytorTreningu()
        {
            InitializeComponent();
            Bindowanie();
        }

        public void przekazDane(DateTime data)
        {
            wybranaData = data;
        }

        private void Bindowanie()
        {
            cwiczenieCombo.ItemsSource = typyTrening;
        }

        private void zapiszButton_Click(object sender, RoutedEventArgs e)
        {
            DzienTreningowy trening = db.DzienTreningowy.Where(m => m.Data == wybranaData).FirstOrDefault();
            trening.Czas = int.Parse(czasTextbox.Text.ToString());
            trening.Cwiczenie = cwiczenieCombo.SelectedItem.ToString();
            db.SaveChanges();
            string msg = "Trening zostały poprawnie zapisany.";
            MessageBox.Show(msg, "Informacja", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            PlanDnia plan = new PlanDnia();
            plan.przekazDane(wybranaData);
            plan.Show();
        }
    }
}

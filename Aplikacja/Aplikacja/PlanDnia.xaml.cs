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
    /// Interaction logic for PlanDnia.xaml
    /// </summary>
    public partial class PlanDnia : Window
    {

        BazaDanychEntities db = new BazaDanychEntities();
        int id = Sesja.ZwrocId();
        Uzytkownicy uzytkownik = new Uzytkownicy();
        DateTime wybranaData = new DateTime();
        List<Posilek> posilki = new List<Posilek>();


        public PlanDnia()
        {
            InitializeComponent();
            uzytkownik = db.Uzytkownicy.Where(m => m.ID.Equals(id)).FirstOrDefault();
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
            pobierzPosilki();
            posilekViewSource.Source = posilki;
        }

        private void pobierzPosilki()
        {
            DateTime data = wybranaData;
            foreach (Spis_Posilkow rekord in db.Spis_Posilkow.Where(m => m.Data == wybranaData))
            {
                posilki.Add(db.Posilek.Where(m => m.Id == rekord.ID_Posilku).FirstOrDefault());
            }
        }

        private void edytujPosilkiButton_Click(object sender, RoutedEventArgs e)
        {
                EdytorDnia edytor = new EdytorDnia();
                edytor.przekazDane(wybranaData);
                this.Close();
                edytor.Show();
        }

    }
}

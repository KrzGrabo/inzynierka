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
        Diety dieta = new Diety();
        Treningi trening = new Treningi();
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
            dieta = null;
            trening = null;
            znajdzDiete();
            znajdzTrening();

            System.Windows.Data.CollectionViewSource posilekViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("posilekViewSource")));
            // Load data by setting the CollectionViewSource.Source property:
            // posilekViewSource.Source = [generic data source]
            if (dieta != null)
            {
                brakDiety.Visibility = Visibility.Hidden;
                pobierzPosilki();
                posilekViewSource.Source = posilki;
            }
            else
            {
                posilkiList.Visibility = Visibility.Hidden;
                edytujPosilkiButton.IsEnabled = false;

            }
            if(trening != null)
            {
                brakTreningu.Visibility = Visibility.Hidden;
                pobierzTrening();
            }
            else
            {
                czasLabel.Visibility = Visibility.Hidden;
                czasLabel1.Visibility = Visibility.Hidden;
                cwiczenieLabel.Visibility = Visibility.Hidden;
                cwiczenieLabel1.Visibility = Visibility.Hidden;
                edytujTreningButton.IsEnabled = false;
            }

        }

        private void pobierzPosilki()
        {
            foreach (Spis_Posilkow rekord in db.Spis_Posilkow.Where(m => m.Data == wybranaData))
            {
                posilki.Add(db.Posilek.Where(m => m.Id == rekord.ID_Posilku).FirstOrDefault());
            }
        }

        private void pobierzTrening()
        {
            DzienTreningowy trening = db.DzienTreningowy.Where(m => m.Data == wybranaData).FirstOrDefault();
            cwiczenieLabel.Content = trening.Cwiczenie.ToString();
            czasLabel.Content = trening.Czas.ToString();
        }

        private void edytujPosilkiButton_Click(object sender, RoutedEventArgs e)
        {
                EdytorPosilkow edytorPos = new EdytorPosilkow();
                edytorPos.przekazDane(wybranaData);
                this.Close();
                edytorPos.Show();
        }

        private void edytujTreningButton_Click(object sender, RoutedEventArgs e)
        {
            EdytorTreningu edytorTren = new EdytorTreningu();
            edytorTren.przekazDane(wybranaData);
            this.Close();
            edytorTren.Show();
        }

        private void znajdzDiete()
        {
            var diety = uzytkownik.Diety.ToList();
            foreach (Diety szukana in diety)
            {
                if (szukana.Data_Rozpoczecia <= wybranaData && szukana.Data_Zakonczenia >= wybranaData)
                {
                    dieta = szukana;
                }
            }
        }

        private void znajdzTrening()
        {
            var treningi = uzytkownik.Treningi.ToList();
            foreach (Treningi szukany in treningi)
            {
                if (szukany.Data_Rozpoczecia <= wybranaData && szukany.Data_Zakonczenia >= wybranaData)
                {
                    trening = szukany;
                }
            }
        }

    }
}

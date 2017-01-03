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

        public void Bindowanie()
        {
            string datowa=wybranaData.ToLongDateString();
            opisLabel.Text = "Twój plan na " + datowa;
            //DateTime now = DateTime.Now;
           // DateTime poczDieta = dieta.Data_Rozpoczecia;         dzień cyklu dietetycznego/treningowego      <----- tu generalnie chcialem wstawic do stringa "[numer dnia cyklu]/[ilość dni w całym cyklu]"
            //DateTime konDieta = dieta.Data_Zakonczenia;
        //    double dlugoscCyklu = (dieta.Data_Zakonczenia - dieta.Data_Rozpoczecia).TotalDays;
            if (dieta.Id != 0)
            {
                bialkoPodLabel.Content = dieta.Bialko.ToString();
                kaloriePodLabel.Content = dieta.Kalorycznosc.ToString();
                tluszczPodLabel.Content = dieta.Tluszcz.ToString();
                weglowodanyPodLabel.Content = dieta.Weglowodany.ToString();
                posilkiPodLabel.Content = dieta.Ilosc_Posilkow.ToString();
            }
///walidacja czy jest trening
           /// treningPodLabel.Content=trening.typ podpiecie nazwy treningu
            /// czasTrenPodLabel.Content=trening.czas podpiecie czasu treningu
        
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
            Bindowanie();
            
            System.Windows.Data.CollectionViewSource posilekViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("posilekViewSource")));
            // Load data by setting the CollectionViewSource.Source property:
            // posilekViewSource.Source = [generic data source]
            if (dieta != null)
            {
           //     brakDiety.Visibility = Visibility.Hidden;
                pobierzPosilki();
                posilekViewSource.Source = posilki;
            }
            else
            {
           //     posilkiList.Visibility = Visibility.Hidden;
                edytujPosilkiButton.IsEnabled = false;

            }
            if(trening != null)
            {
                
                pobierzTrening();
            }
            else
            {
              
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
            //EdytorTreningu edytorTren = new EdytorTreningu();
            //edytorTren.przekazDane(wybranaData);
            //this.Close();
            //edytorTren.Show();
            MessageBox.Show(wybranaData.ToLongDateString());
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

        private void edytujProfilButton_Click(object sender, RoutedEventArgs e)
        {
            ProfilHistoryczny.data = wybranaData.ToShortDateString();
            ProfilHistoryczny okno = new ProfilHistoryczny();
            okno.Show();
        }

    }
}

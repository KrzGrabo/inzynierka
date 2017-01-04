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


        public PlanDnia()
        {
            InitializeComponent();
            uzytkownik = db.Uzytkownicy.Where(m => m.ID.Equals(id)).FirstOrDefault();
            
        }

        public void Bindowanie()
        {
            string datowa=wybranaData.ToLongDateString();
            opisLabel.Text = "Twój plan na " + datowa;
            double dlugoscCyklu;

            if (dieta != null)
            {
                DateTime poczDieta = dieta.Data_Rozpoczecia.GetValueOrDefault();         //dzień cyklu dietetycznego/treningowego      <----- tu generalnie chcialem wstawic do stringa "[numer dnia cyklu]/[ilość dni w całym cyklu]"
                dlugoscCyklu = (wybranaData - poczDieta).TotalDays + 1;

                bialkoPodLabel.Content = dieta.Bialko.ToString();
                kaloriePodLabel.Content = dieta.Kalorycznosc.ToString();
                tluszczPodLabel.Content = dieta.Tluszcz.ToString();
                weglowodanyPodLabel.Content = dieta.Weglowodany.ToString();
                posilkiPodLabel.Content = dieta.Ilosc_Posilkow.ToString();
                dzienDietyLabel.Content = dlugoscCyklu.ToString();
            }

            if (trening != null)
            {
                DzienTreningowy dzien = db.DzienTreningowy.Where(m => m.Data == wybranaData).FirstOrDefault();
                DateTime poczTren = trening.Data_Rozpoczecia.GetValueOrDefault();
                dlugoscCyklu = (wybranaData - poczTren).TotalDays + 1;

                treningPodLabel.Content = dzien.Cwiczenie;
                czasTrenPodLabel.Content = dzien.Czas;
                dzienTreninguPodLabel.Content = dlugoscCyklu.ToString();
            }
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

            /*System.Windows.Data.CollectionViewSource posilekViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("posilekViewSource")));
            // Load data by setting the CollectionViewSource.Source property:
            // posilekViewSource.Source = [generic data source]
            if (dieta != null)
            {
                Bindowanie();
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
              
            }*/

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

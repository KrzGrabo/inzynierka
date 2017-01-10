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

                bialkoPodLabel.Content = String.Format("{0:N2}", dieta.Bialko);
                kaloriePodLabel.Content = String.Format("{0:N2}", dieta.Kalorycznosc);
                tluszczPodLabel.Content = String.Format("{0:N2}", dieta.Tluszcz);
                weglowodanyPodLabel.Content = String.Format("{0:N2}", dieta.Weglowodany);
                posilkiPodLabel.Content = dieta.Ilosc_Posilkow.ToString();
                dzienDietyLabel.Content = dlugoscCyklu.ToString();

                Spozycie spozycie = dieta.Spozycie.Where(m => m.Data == wybranaData).FirstOrDefault();

                if (spozycie != null)
                {
                    ///TU ZBINDOWAĆ RZECZYWISTE SPORZYCIE
                    bialkoRzPodLabel.Content = String.Format("{0:N2}", spozycie.Bialko);
                    kaloriePodRzLabel.Content = String.Format("{0:N2}", spozycie.Kalorie);
                    tluszczPodRzLabel.Content = String.Format("{0:N2}", spozycie.Tluszcz);
                    weglowodanyRzPodLabel.Content = String.Format("{0:N2}", spozycie.Weglowodany);
                    posilkiPodRzLabel.Content = dieta.Ilosc_Posilkow.ToString();
                    dzienzRzDietyLabel.Content = dlugoscCyklu.ToString();
                }
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
        }

        private void edytujPosilkiButton_Click(object sender, RoutedEventArgs e)
        {
                EdytorPosilkow edytorPos = new EdytorPosilkow();
                edytorPos.przekazDane(wybranaData);
                this.Close();
                edytorPos.Show();
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

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
    /// Interaction logic for Statystyki.xaml
    /// </summary>
    public partial class Statystyki : Window
    {

        BazaDanychEntities db = new BazaDanychEntities();
        int id = Sesja.ZwrocId();
        Uzytkownicy uzytkownik = new Uzytkownicy();
        Diety dieta = new Diety();
        Treningi trening = new Treningi();
        Dane dane = new Dane();


        public Statystyki()
        {
            InitializeComponent();
            uzytkownik = db.Uzytkownicy.Where(m => m.ID.Equals(id)).FirstOrDefault();
            dane = uzytkownik.Dane.FirstOrDefault();
            Bindowanie();
        }
        public void Bindowanie()
        {
            dieta = null;
            trening = null;
            znajdzTrening();
            znajdzDiete();

            if (dane != null)
            {
                ustawProfil();
            }
            if (trening != null)
            {
                ustawTrening();
            }
            if (dieta != null)
            {
                ustawDiete();
            }

        }

        private void ustawProfil()
        {
            List<obwodPasaStat> obwod1 = new List<obwodPasaStat>();
            List<obwodBioderStat> obwod2 = new List<obwodBioderStat>();
            List<wagaStat> waga = new List<wagaStat>();
            Historia_Danych historia = new Historia_Danych();
            List<Historia_Danych> calaHist = dane.Historia_Danych.ToList();

            for (int i = 0; i < dane.Historia_Danych.Count; i++)
            {
                historia = calaHist.ElementAt(i);
                string dataHist = historia.Data.GetValueOrDefault().ToShortDateString();
                double wagaHist = historia.Waga.GetValueOrDefault();
                double obwodBiod = historia.Obwod_Bioder.GetValueOrDefault();
                double obwodPasa = historia.Obwod_Pasa.GetValueOrDefault();
                waga.Add(new wagaStat() { data = dataHist, wartosc = wagaHist });
                obwod1.Add(new obwodPasaStat() { data = dataHist, wartosc = obwodPasa });
                obwod2.Add(new obwodBioderStat() { data = dataHist, wartosc = obwodBiod });
            }

            wagaChart.DataContext = waga;
            pasChart.DataContext = obwod1;
            biodraChart.DataContext = obwod2;
        }

        private void ustawTrening()
        {
            List<DzienTreningowy> dzienTren = trening.DzienTreningowy.ToList();
            List<treningiStat> trenStat = new List<treningiStat>();
            string[] typyTrening = { "siłowy", "wytrzymałościowy", "szybkościowy", "techniczny", "gibkościowy", "interwałowy", "kondycyjny", "zwinności", "ogólnorozwojowy" };
            int[] czasy = new int[typyTrening.Length];

            foreach(DzienTreningowy dzien in dzienTren)
            {
                for(int i = 0; i<typyTrening.Length; i++)
                {
                    if(dzien.Cwiczenie == typyTrening[i])
                    {
                        czasy[i] += dzien.Czas.GetValueOrDefault();
                    }

                }
            }

            for(int i = 0; i<typyTrening.Length; i++)
            {
                trenStat.Add(new treningiStat() { typ = typyTrening[i], czas = czasy[i] });
            }

            treningChart.DataContext = trenStat;
        }


        private void znajdzTrening()
        {
            DateTime now = DateTime.Now;
            TimeSpan time = new TimeSpan(0, 0, 0);
            now = now.Date + time;
            DateTime data = now;
            var treningi = uzytkownik.Treningi.ToList();

            foreach (Treningi szukany in treningi)
            {
                if (szukany.Data_Rozpoczecia <= data && szukany.Data_Zakonczenia >= data)
                {
                    trening = szukany;
                }
            }
        }

        private void ustawDiete()
        {
            List<dietaStat> dietaStat = new List<dietaStat>();
            List<dietaStat2> dietaStat2 = new List<dietaStat2>();
            double bialko, tluszcz, wegle;
            tluszcz = (double)dieta.Tluszcz.GetValueOrDefault();
            wegle = (double)dieta.Weglowodany.GetValueOrDefault();
            bialko = (double)dieta.Bialko.GetValueOrDefault();

            double tluszczWp = tluszcz*9;
            double bialkoWp = bialko*4;
            double wegleWp = wegle*4;
            
           // dietaStat.Add(new dietaStat() { nazwa = "Kalorie", wartosc = (int)dieta.Kalorycznosc.GetValueOrDefault() });
            dietaStat.Add(new dietaStat() { nazwa = "Tłuszcz", wartosc = (int)tluszcz });
            dietaStat.Add(new dietaStat() { nazwa = "Węglowodany", wartosc = (int)wegle });
            dietaStat.Add(new dietaStat() { nazwa = "Białko", wartosc = (int)bialko });

            dietaStat2.Add(new dietaStat2() { nazwa = "Tłuszcz", wartosc = (int)tluszczWp });
            dietaStat2.Add(new dietaStat2() { nazwa = "Węglowodany", wartosc = (int)wegleWp });
            dietaStat2.Add(new dietaStat2() { nazwa = "Białko", wartosc = (int)bialkoWp });

            int kcal = (int)dieta.Kalorycznosc.GetValueOrDefault();
            kalorieLabel.Content = "Dzienna suma kalorii podczas cyklu: " + kcal.ToString();
            //waga użytkownika

            dietaChart.DataContext = dietaStat2;
            dieta2Chart.DataContext = dietaStat;

        }

        private void znajdzDiete()
        {
            DateTime now = DateTime.Now;
            TimeSpan time = new TimeSpan(0, 0, 0);
            now = now.Date + time;
            DateTime data = now;
            var diety = uzytkownik.Diety.ToList();
            foreach (Diety szukana in diety)
            {
                if (szukana.Data_Rozpoczecia <= data && szukana.Data_Zakonczenia >= data)
                {
                    dieta = szukana;
                }
            }
        }

    }
    public class obwodPasaStat
    {
        public string data { get; set; }
        public double wartosc { get; set; }
    }

    public class obwodBioderStat
    {
        public string data { get; set; }
        public double wartosc { get; set; }
    }

    public class wagaStat
    {
        public string data { get; set; }
        public double wartosc { get; set; }
    }

    public class treningiStat
    {
        public string typ { get; set; }
        public int czas { get; set; }
    }

    public class dietaStat
    {
        public string nazwa { get; set; }
        public int wartosc { get; set; }
    }
    public class dietaStat2
    {
        public string nazwa { get; set; }
        public int wartosc { get; set; }
    }
}

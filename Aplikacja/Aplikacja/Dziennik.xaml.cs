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
        BazaDanychEntities db = new BazaDanychEntities();
        int id = Sesja.ZwrocId();
        Uzytkownicy uzytkownik = new Uzytkownicy();
        Diety dieta = new Diety();
        Treningi trening = new Treningi();
        DateTime data = new DateTime();

        public Dziennik()
        {
            InitializeComponent();
            uzytkownik = db.Uzytkownicy.Where(m => m.ID.Equals(id)).FirstOrDefault();
        }

        private void Cal_SelectedDatesChanged(object sender, RoutedEventArgs e)
        {
            dieta = null;
            trening = null;
            znajdzDiete();
            znajdzTrening();
            if (dieta == null && trening == null)
            {
                string msg = "Brak diety lub treningu na dany dzień, przejdź do odpowiednich modułów, aby ustalić plan na diete lub trening.";
                MessageBox.Show(msg, "Uwaga", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
            else
            {
                PlanDnia plan = new PlanDnia();
                plan.przekazDane(data);
                plan.Show();
            }
        }

        private void znajdzDiete()
        {
            var diety = uzytkownik.Diety.ToList();
            data = Kalendarz.SelectedDate.GetValueOrDefault();
            foreach (Diety szukana in diety)
            {
                if (szukana.Data_Rozpoczecia <= data && szukana.Data_Zakonczenia >= data)
                {
                    dieta = szukana;
                }
            }
        }

        private void znajdzTrening()
        {
            var treningi = uzytkownik.Treningi.ToList();
            data = Kalendarz.SelectedDate.GetValueOrDefault();
            foreach (Treningi szukany in treningi)
            {
                if (szukany.Data_Rozpoczecia <= data && szukany.Data_Zakonczenia >= data)
                {
                    trening = szukany;
                }
            }
        }



    }
}

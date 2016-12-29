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
        DateTime data = new DateTime();

        public Dziennik()
        {
            InitializeComponent();
        }

        private void Cal_SelectedDatesChanged(object sender, RoutedEventArgs e)
        {
            dieta = null;
            znajdzDiete();
            if (dieta != null)
            {
                PlanDnia plan = new PlanDnia();
                plan.przekazDane(data);
                plan.Show();
            }
            else
            {
                string msg = "Brak diety na dany dzień, przejdź do modułu diety i ustal odpowiednią diete na dany okres.";
                MessageBox.Show(msg, "Uwaga", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
        }

        private void znajdzDiete()
        {
            uzytkownik = db.Uzytkownicy.Where(m => m.ID.Equals(id)).FirstOrDefault();
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



    }
}

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
        public Statystyki()
        {
            InitializeComponent();
            Bindowanie();
        }
        public void Bindowanie()
        {
            List<obwodPasaStat> dana = new List<obwodPasaStat>();
            dana.Add(new obwodPasaStat() { data = "nazwa", wartosc = 2 });
            dana.Add(new obwodPasaStat() { data = "n21azwa", wartosc = 3 });
            dana.Add(new obwodPasaStat() { data = "na21232zwa", wartosc = 5 });
            dana.Add(new obwodPasaStat() { data = "naz3232wa", wartosc = 1 });
            
            
            
            dietaChart.DataContext = dana;
            pasChart.DataContext = dana;
            wagaChart.DataContext = dana;
            biodraChart.DataContext = dana;
            treningChart.DataContext = dana;
        }


        ////NAJPIERW TRZEBA DATY(chyba bo nie wiem jak on to posortuje) DO STRINGOW PRZEKONWERTOWAC DOPIERO POTEM WRZUCAC DO STRINGOW W KLASACH, CHOCIAŻ NIE WIEM 

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
        public double czas { get; set; }
    }

    public class dietaStat
    {
        public string nazwa { get; set; }
        public int numerek { get; set; }
    }
}

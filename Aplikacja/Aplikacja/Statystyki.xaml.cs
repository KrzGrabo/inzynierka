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
            List<Danee> dana = new List<Danee>();
            dana.Add(new Danee() { nazwa = "nazwa", numerek = 2 });
            dana.Add(new Danee() { nazwa = "n21azwa", numerek = 3 });
            dana.Add(new Danee() { nazwa = "na21232zwa", numerek = 5 });
            dana.Add(new Danee() { nazwa = "naz3232wa", numerek = 1 });
            columnChart.DataContext = dana;
            pieChart.DataContext = dana;
        }

    }
    public class Danee
    {
        public string nazwa { get; set; }
        public int numerek { get; set; }
    }

}

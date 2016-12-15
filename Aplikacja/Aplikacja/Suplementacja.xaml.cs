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
    /// Interaction logic for Suplementacja.xaml
    /// </summary>
    public partial class Suplementacja : Window
    {
        public Suplementacja()
        {
            InitializeComponent();
            Bindowanie();
        }

        public void Bindowanie()
        {
            opis1Label.Text = "Dzięki temu modułowi dopasujesz suplementację, która pomoże ci w osięgnięciu określonych celów. Wprowadź wymagane dane w celu otrzymania spersonalizowanego wyniku. W celu uzyskania kompleksowych informacji na temat wybranych produktów, użyj przycisku znajdującego się na prawo od listy propozycji.";
        }

        public class Suple
        {
            public string Tytul { get; set; }
        }


        private void okButton_Click(object sender, RoutedEventArgs e)
        {
            List<Suple> items = new List<Suple>();
            items.Add(new Suple() { Tytul = "Complete this WPF tutorial" });
            items.Add(new Suple() { Tytul = "Learn C#" });
            items.Add(new Suple() { Tytul = "Wash the car" });

            SupleList.ItemsSource = items;
        }

        private void infoButton_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}

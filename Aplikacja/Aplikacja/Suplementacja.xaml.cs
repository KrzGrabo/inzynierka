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
            opis1Label.Text = "Dzięki temu modułowi dopasujesz suplementację, która pomoże ci w osięgnięciu określonych celów. Wprowadź wymagane dane aby otrzymać spersonalizowany wyniku. W celu uzyskania kompleksowych informacji na temat wybranych produktów, wciśnij przycisk znajdujący się na prawo od listy propozycji.";
        }

        public class Suple
        {
            public string Tytul { get; set; }
        }


        private void okButton_Click(object sender, RoutedEventArgs e)
        {
            string walidacja = "";

            if (celCombo.SelectedIndex == -1)
            {
                walidacja = walidacja + " \nMusisz wybrać cel";
            }

            if (poziomMiesniCombo.SelectedIndex == -1)
            {
                walidacja = walidacja + " \nMusisz wybrać stopień umięśnienia";
            }

            if (poziomTluszczuCombo.SelectedIndex == -1)
            {
                walidacja = walidacja + " \nMusisz wybrać stopień zatłuszczenia";
            }

            if (stazCombo.SelectedIndex == -1)
            {
                walidacja = walidacja + " \nMusisz wybrać staż treningowy";
            }

            if (walidacja == "")
            {
                List<Suple> items = new List<Suple>();
                if(celCombo.SelectedIndex==0)
                {
                    items.Add(new Suple() { Tytul = "Omega-3" });
                    items.Add(new Suple() { Tytul = "Witamina D" });
                    items.Add(new Suple() { Tytul = "ZMA" });
                    items.Add(new Suple() { Tytul = "Glutamina" });
                }

                if (celCombo.SelectedIndex == 1)
                {
                    items.Add(new Suple() { Tytul = "Kreatyna" });
                    items.Add(new Suple() { Tytul = "Tribulus terrestris" });
                    items.Add(new Suple() { Tytul = "HMB" });
                }

                if (celCombo.SelectedIndex == 2)
                {
                    items.Add(new Suple() { Tytul = "L-karnityna" });
                    items.Add(new Suple() { Tytul = "CLA" });
                    items.Add(new Suple() { Tytul = "Ekstrakt z zielonej herbaty" });

                }

                if (celCombo.SelectedIndex == 3)
                {
                    items.Add(new Suple() { Tytul = "Beta-alanina" });
                    items.Add(new Suple() { Tytul = "Arginina" });
                    items.Add(new Suple() { Tytul = "BCAA" });
               
                }

                if (celCombo.SelectedIndex == 4)
                {
                    items.Add(new Suple() { Tytul = "Arginina" });
                    items.Add(new Suple() { Tytul = "Kreatyna" });
                    items.Add(new Suple() { Tytul = "Tribulus terrestris" });
                }

                if (celCombo.SelectedIndex == 5)
                {
                    items.Add(new Suple() { Tytul = "Arginina" });
                    items.Add(new Suple() { Tytul = "Kofeina" });
                    items.Add(new Suple() { Tytul = "Tauryna" });
                    items.Add(new Suple() { Tytul = "Żeń-szeń" });
                }

                SupleList.ItemsSource = items;
            }
            else
            {
                walidacja = "Wystąpiły błędy przy wpisywaniu danych:" + walidacja;
                MessageBox.Show(walidacja, "Uwaga", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
         }

        private void infoButton_Click(object sender, RoutedEventArgs e)
        {
            SuplementacjaInfo okno = new SuplementacjaInfo();
            okno.Show();
        }
    }
}

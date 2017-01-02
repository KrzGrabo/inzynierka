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
    /// Interaction logic for Autorzy.xaml
    /// </summary>
    public partial class Autorzy : Window
    {
        public Autorzy()
        {
            InitializeComponent();
            Bindowanie();
        }

        private void Bindowanie()
        {
            opis.Text = "Michał Kuś i Wiktor Czarnecki są studentami ostatniego semestru studiów inżynierskich na kierunku Informatyka, wchodzącego w skład wydziału 'Elektronika, Telekomunikacja i Informatyka' na Politechnice Gdańskiej. ";

        }
    }
}

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
    /// Interaction logic for Informacje.xaml
    /// </summary>
    public partial class Informacje : Window
    {
        public Informacje()
        {
            InitializeComponent();
            Bindowanie();
        }

        private void Bindowanie(){
            opis.Text = "Celem projektu jest przygotowanie aplikacji desktopowej, będącej narzędziem dla sportowców, pozwalającym im na indywidualny dobór i planowanie aktywności takich jak trening, dieta i regeneracja, a także zbieranie zmieniających się parametrów/danych użytkownika oraz jego wyników, dzięki czemu możliwe będzie monitorowanie rozwoju, a także wizualizacja statystyk. Tak uzyskane dane będą służyć doborowi jeszcze lepszego reżimu treningowego i około-treningowego. Aplikacja ma za zadanie pokryć jak najwięcej potrzebnych klientowi funkcji (między innymi: kalkulator kalorii, baza produktów spożywczych, modele diet, reżimy treningowe plus szczegółowy opis ich poszczególnych elementów, lista zalecanych suplementów, funkcja dziennika treningowego, moduł statystyk) dzięki czemu stałaby się dla użytkownika kompleksowym instrumentem do zarządzania aktywnościami związanymi ze sportem w jego życiu. Ikony/Obrazki z menu i przycisków wykorzystane z zasobów: http://www.flaticon.com/packs/healthly-lifestyle na licencji 'Flaticon Basic License ': Designed by Freepik and distributed by Flaticon, Logo eti wykorzystane w sekcji informacyjnej zostało pobrane z oficjalnej strony wydziału 'Elektroniki, Telekomunikacji i Informatyki' z Politechniki Gdańskiej";

        }
    }
}

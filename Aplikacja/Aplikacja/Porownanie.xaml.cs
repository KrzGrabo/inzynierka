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
    /// Interaction logic for Porownanie.xaml
    /// </summary>
    public partial class Porownanie : Window
    {
        public Porownanie()
        {
            InitializeComponent();
            Bindowanie();
        }

        public void Bindowanie()
        {
            opisLabel.Text = "Dotychczas tłuszcze uważano za makroelement nieporządany w diecie, jednak ostatnimi możemy dostrzec odmienny trend, który mówi, że oparcie diety na produktach 'tłuszczowych' nie tylko nie jest niezdrowe ale oferuje, więcej korzyści niż diety zgodne ze starymi zaleceniami mówiące o węglowodanach jako filarze każdej diety.\n Przedstawiamy po krótce zalty i wady każdej z opcji:\n\n Dieta oparta na tłuszczu\n Zalety: powolne uwalnianie energii, udowodnione w badaniach szybsze tempo spalania tłuszczu, wyższa wrażliwość na insulinę, mniej inwazyjne dla nerek i trzustki, brak uczucia głodu podczas redukcji \n Minusy: może powodować spadki odczuwanej energii/wigoru, czasem organizm źle reaguje na tą dietę- mogą wystąpić problemy z wątrobą, pęcherzem żółciowym, czy ze stronu układu pokarmowego(zatwardzenia, gazy, itp.)\n\n Dieta oparta na węglowodanach\n Plusy: węglowodany łatwo dają duży zastrzyk energii, wspomaga proces odbudowy i regeneracji ciała, wspomaga transport składników w krwi, mniej inwazyjne dla wątroby, napędza metabolizm\n Minusy: wzmaga retencję wody w organizmie, łatwiej się zatłuścić, wzmaga insulinooporność, możliwe problemy ze strony układu pokarmowego(np. rozwolnienia, biegunki ze względu na dużą ilość błonnika) \n \nWybór pomiędzy tymi opcjami jest sprawą indywidualną i powinna być oparta na własnych preferencjach i celach. Należy testować obie metody(oraz mix'ów np. 50-50) i dochodzić do rozwiązań przy których czuje się najlepiej i dzięki którym organizm pracuje na najwyższych obrotach, przez co dochodzi się do swoich celów.";
            
        }

        
    }
}

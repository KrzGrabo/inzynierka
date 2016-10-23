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
    /// Interaction logic for TabelaProduktow.xaml
    /// </summary>
    public partial class TabelaProduktow : Window
    {


        BazaDanychEntities dane = new BazaDanychEntities();
        public TabelaProduktow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            System.Windows.Data.CollectionViewSource kategorieViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("kategorieViewSource")));
            // Load data by setting the CollectionViewSource.Source property:
            // kategorieViewSource.Source = [generic data source]
            List<String> kategorie = dane.Kategorie.Select(k => k.Nazwa).ToList();
            kategorieViewSource.Source = dane.Kategorie.ToList();

            System.Windows.Data.CollectionViewSource produktyViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("produktyViewSource")));
            // Load data by setting the CollectionViewSource.Source property:
            // produktyViewSource.Source = [generic data source]
            var danaKategoria = dane.Kategorie.Where(m => m.ID.Equals(1)).FirstOrDefault();
            produktyViewSource.Source = danaKategoria.Produkty.ToList();
        }

        private void kategoriaCombobox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            System.Windows.Data.CollectionViewSource produktyViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("produktyViewSource")));
            var danaKategoria = dane.Kategorie.Where(m => m.ID.Equals(kategoriaCombobox.SelectedIndex + 1)).FirstOrDefault();
            produktyViewSource.Source = danaKategoria.Produkty.ToList();
        }

    }
}

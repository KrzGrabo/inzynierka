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
    /// Interaction logic for SuplementacjaInfo.xaml
    /// </summary>
    public partial class SuplementacjaInfo : Window
    {
        BazaDanychEntities dane = new BazaDanychEntities();

        public SuplementacjaInfo()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

            System.Windows.Data.CollectionViewSource suplementyViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("suplementyViewSource")));
            // Load data by setting the CollectionViewSource.Source property:
            // suplementyViewSource.Source = [generic data source]
            suplementyViewSource.Source = dane.Suplementy.ToList();
        }

    }
}

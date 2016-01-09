using MahApps.Metro.Controls;
using NLog;
using swk5.ufo.dal;
using swk5.ufo.server;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using ufo.commander.ViewModels;

namespace ufo.commander
{
    /// <summary>
    /// Interaction logic for InsertArtistWindow.xaml
    /// </summary>
    public partial class InsertArtistWindow
    {
        private static Logger Logger = LogManager.GetCurrentClassLogger();
        private ICommanderBL commander = BLFactory.GetCommander();


        public InsertArtistWindow(UFOCollectionVM ufoCollectionVM)
        {
            InitializeComponent();
            DataContext = ufoCollectionVM;
            Owner = Application.Current.Windows.OfType<MetroWindow>().FirstOrDefault();

        }

        private void MetroWindow_Loaded(object sender, RoutedEventArgs e)
        {
            // can be done in XAML aswell
            cmbCategory.ItemsSource = commander.GetCategories().OrderBy(c => c.CategoryName);
            cmbCountry.ItemsSource = commander.GetCountries().OrderBy(c => c.Name);
        }

        private void btnCancelSubmitArtist_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}

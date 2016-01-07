using MahApps.Metro.Controls.Dialogs;
using NLog;
using swk5.ufo.dal;
using swk5.ufo.server;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using ufo.commander.ViewModels;

namespace ufo.commander.Views
{
    /// <summary>
    /// Interaction logic for ArtistsOverview.xaml
    /// </summary>
    public partial class ArtistsOverview : UserControl
    {
        private static Logger Logger = LogManager.GetCurrentClassLogger();
        private ICommanderBL commander = BLFactory.GetCommander();
        private IDialogCoordinator dialogCoordinator = DialogCoordinator.Instance;

        public ArtistsOverview()
        {
            InitializeComponent();
            EditArtistGrid.Visibility = Visibility.Hidden;
        }

        private void btnCancelEditArtist_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            EditArtistGrid.Visibility = Visibility.Hidden;
        }

        private void UserControl_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            cmbCategory.ItemsSource = commander.GetCategories().OrderBy(c => c.CategoryName);
            cmbCountry.ItemsSource = commander.GetCountries().OrderBy(c => c.Name);
        }

        private void ArtistGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            EditArtistGrid.Visibility = Visibility.Visible;
        }
    }
}

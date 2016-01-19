using MahApps.Metro.Controls;
using swk5.ufo.server;
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
using ufo.commander.ViewModels;

namespace ufo.commander
{
    /// <summary>
    /// Interaction logic for InsertVenueWindow.xaml
    /// </summary>
    public partial class InsertVenueWindow
    {
        private UFOCollectionVM ufoCollVM;

        public InsertVenueWindow(UFOCollectionVM uFOCollectionVM)
        {
            InitializeComponent();
            DataContext = uFOCollectionVM;
            ufoCollVM = uFOCollectionVM;
        }

        private void btnCancelSubmitVenue_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void MetroWindow_Closed(object sender, EventArgs e)
        {
            ufoCollVM.NewVenue.ShortName = null;
            ufoCollVM.NewVenue.Description = null;
            ufoCollVM.NewVenue.Latitude = 0;
            ufoCollVM.NewVenue.Longitude = 0;
        }
    }
}

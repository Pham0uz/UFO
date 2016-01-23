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

        private void btnSubmitVenue_Click(object sender, RoutedEventArgs e)
        {
            // check if artist already exists and if any required fields are empty
            ufoCollVM.LoadVenues();
            if (ufoCollVM.Venues.Where(x => x.ShortName == txtShortName.Text).Count() > 0)
            {
                txtError.Text = $"Venue with name: {txtShortName.Text} already exists!";
                return;
            }
            else if (string.IsNullOrEmpty(txtShortName.Text) && string.IsNullOrEmpty(txtDescription.Text)
                  && string.IsNullOrEmpty(txtLatitude.Text) && string.IsNullOrEmpty(txtLongitude.Text))
            {
                txtError.Text = "Fields with * musn't be empty!";
                return;
            }
            else if (string.IsNullOrEmpty(txtShortName.Text))
            {
                txtError.Text = "Please enter a Shortname";
                return;
            }
            else if (string.IsNullOrEmpty(txtDescription.Text))
            {
                txtError.Text = "Please enter a Description";
                return;
            }
            else if (string.IsNullOrEmpty(txtLatitude.Text))
            {
                txtError.Text = "Please enter a Latitude";
                return;
            }
            else if (string.IsNullOrEmpty(txtLongitude.Text))
            {
                txtError.Text = "Please enter a Longitude";
            }
            txtError.Text = "";

            // Execute command in tag
            var button = sender as Button;
            if (button != null)
            {
                var command = button.Tag as ICommand;
                if (command != null)
                    command.Execute(button.CommandParameter);
            }
        }
    }
}


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
        private UFOCollectionVM ufoCollVM;

        public InsertArtistWindow(UFOCollectionVM ufoCollectionVM)
        {
            InitializeComponent();
            DataContext = ufoCollectionVM;
            ufoCollVM = ufoCollectionVM;
            Owner = Application.Current.Windows.OfType<MetroWindow>().FirstOrDefault();
        }

        private void MetroWindow_Loaded(object sender, RoutedEventArgs e)
        {
            // can be done in XAML aswell and at the loading of the collections
            cmbCategory.ItemsSource = ufoCollVM.Categories.OrderBy(c => c.Name);
            cmbCountry.ItemsSource = ufoCollVM.Countries.OrderBy(c => c.Name);

            ufoCollVM.NewArtist.Artist.Name = null;
            ufoCollVM.NewArtist.Artist.PictureURL = null;
            ufoCollVM.NewArtist.Artist.PromoVideoURL = null;
            ufoCollVM.NewArtist.Artist.CategoryName = null;
            ufoCollVM.NewArtist.Artist.CountryCode = null;
        }

        private void btnCancelSubmitArtist_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void btnSubmitArtist_Click(object sender, RoutedEventArgs e)
        {
            // check if artist already exists and if any required fields are empty
            ufoCollVM.LoadArtists();
            if (ufoCollVM.Artists.Where(x => x.Name == txtName.Text).Count() > 0)
            {
                txtError.Text = $"Artist with name: {txtName.Text} already exists!";
                return;
            }
            else if (string.IsNullOrEmpty(txtName.Text) && cmbCountry.SelectedIndex < 0 && cmbCategory.SelectedIndex < 0)
            {
                txtError.Text = "Fields with * musn't be empty!";
                return;
            }
            else if (string.IsNullOrEmpty(txtName.Text))
            {
                txtError.Text = "Please enter a Name";
                return;
            }
            else if (cmbCategory.SelectedIndex < 0)
            {
                txtError.Text = "Please select a Category";
                return;
            }
            else if (cmbCountry.SelectedIndex < 0)
            {
                txtError.Text = "Please select a Country";
                return;
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

using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using NLog;
using swk5.ufo.dal;
using swk5.ufo.server;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
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
            // TODO check if anything changed, if then alert user!
        }

        private async void btnSaveArtist_Click(object sender, RoutedEventArgs e)
        {
            var mySettings = new MetroDialogSettings()
            {
                AffirmativeButtonText = "Save",
                NegativeButtonText = "Cancel",
                AnimateShow = true,
                AnimateHide = false
            };

            Logger.Info("after mysettings");

            var mainwindow = Application.Current.Windows.OfType<MetroWindow>().FirstOrDefault();
            if (mainwindow != null)
            {
                var result = await mainwindow.ShowMessageAsync("Save changes?",
                "Sure you want to save changes?",
                MessageDialogStyle.AffirmativeAndNegative, mySettings);

                Logger.Info("after var result");

                if (result == MessageDialogResult.Affirmative)
                {
                    Logger.Info("in result == message..");
                    // Execute command in tag
                    var button = sender as Button;
                    if (button != null)
                    {
                        var command = button.Tag as ICommand;
                        if (command != null)
                            command.Execute(button.CommandParameter);
                    }
                }
                EditArtistGrid.Visibility = Visibility.Hidden;
            }
        }
    }
}

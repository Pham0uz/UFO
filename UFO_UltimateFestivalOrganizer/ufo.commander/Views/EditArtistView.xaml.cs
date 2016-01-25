using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using NLog;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ufo.commander.Views
{
    /// <summary>
    /// Interaction logic for EditArtistView.xaml
    /// </summary>
    public partial class EditArtistView : UserControl
    {
        private ICommanderBL commander = BLFactory.GetCommander();
        private MetroWindow mainWindow;

        private async void ShowMessage(object sender, string affirmativeButtonText, string negativeButtonText, string title, string message, bool animateShow = true, bool animateHide = false)
        {
            var mySettings = new MetroDialogSettings()
            {
                AffirmativeButtonText = affirmativeButtonText,
                NegativeButtonText = negativeButtonText,
                AnimateShow = animateShow,
                AnimateHide = animateHide
            };

            if (mainWindow != null)
            {
                var result = await mainWindow.ShowMessageAsync(title,
                message,
                MessageDialogStyle.AffirmativeAndNegative, mySettings);

                if (result == MessageDialogResult.Affirmative)
                {
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

        internal void ToggleFlyout(int index)
        {
            if (mainWindow != null)
            {
                var flyout = mainWindow.Flyouts.Items[index] as Flyout;
                if (flyout == null)
                {
                    return;
                }
                flyout.IsOpen = !flyout.IsOpen;
            }
        }

        public EditArtistView()
        {
            InitializeComponent();
            mainWindow = Application.Current.Windows.OfType<MetroWindow>().FirstOrDefault(x => x.Title=="Ultimate Festival Organizer");
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            cmbCategory.ItemsSource = commander.GetCategories().OrderBy(c => c.CategoryName);
            cmbCountry.ItemsSource = commander.GetCountries().OrderBy(c => c.Name);
        }

        private void btnSaveArtist_Click(object sender, RoutedEventArgs e)
        {
            ShowMessage(sender, "Save", "Cancel", "Save changes?", "Sure you want to save changes?");
        }

        private void btnDeleteArtist_Click(object sender, RoutedEventArgs e)
        {
            ShowMessage(sender, "Delete", "Cancel", "Delete selected artist?", "Sure you want to delete selected artist?");
        }

        private void btnCancelEditArtist_Click(object sender, RoutedEventArgs e)
        {
            ShowMessage(sender, "Continue", "Cancel", "Revert changes?", "Sure you want to revert all changes?");
        }

    }
}

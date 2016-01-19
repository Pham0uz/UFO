using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
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
    /// Interaction logic for EditVenueView.xaml
    /// </summary>
    public partial class EditVenueView : UserControl
    {
        private async void ShowMessage(object sender, string affirmativeButtonText, string negativeButtonText, string title, string message, bool animateShow = true, bool animateHide = false)
        {
            var mySettings = new MetroDialogSettings()
            {
                AffirmativeButtonText = affirmativeButtonText,
                NegativeButtonText = negativeButtonText,
                AnimateShow = animateShow,
                AnimateHide = animateHide
            };

            var mainWindow = Application.Current.Windows.OfType<MetroWindow>().FirstOrDefault(x => x.Title == "Ultimate Festival Organizer");
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

        public EditVenueView()
        {
            InitializeComponent();
        }

        private void btnSaveVenue_Click(object sender, RoutedEventArgs e)
        {
            ShowMessage(sender, "Save", "Cancel", "Save changes?", "Sure you want to save changes?");
        }

        private void btnDeleteVenue_Click(object sender, RoutedEventArgs e)
        {
            ShowMessage(sender, "Delete", "Cancel", "Delete selected venue?", "Sure you want to delete selected venue?");
        }

        private void btnCancelEditVenue_Click(object sender, RoutedEventArgs e)
        {
            ShowMessage(sender, "Continue", "Cancel", "Revert changes?", "Sure you want to revert all changes?");
        }
    }
}

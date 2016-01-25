using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using swk5.ufo.dal;
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
using ufo.commander.ViewModels;

namespace ufo.commander.Views
{
    /// <summary>
    /// Interaction logic for EditPerformanceView.xaml
    /// </summary>
    public partial class EditPerformanceView : UserControl
    {
        private ICommanderBL commander = BLFactory.GetCommander();
        private MetroWindow mainWindow;
        private UFOCollectionVM ufoVM;

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

        public EditPerformanceView()
        {
            InitializeComponent();
            mainWindow = Application.Current.Windows.OfType<MetroWindow>().FirstOrDefault(x => x.Title == "Ultimate Festival Organizer");
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            ufoVM = (UFOCollectionVM)mainWindow.DataContext;

            // quick and dirty workaround
            CalendarDateRange cdr = new CalendarDateRange(DateTime.MinValue, new DateTime(2015, 07, 23).AddDays(-1));/*DateTime.Today.AddDays(-1);*/
            datePicker.BlackoutDates.Add(cdr);

            cmbPerformanceTime.ItemsSource = ufoVM.PerformancesTimes;
            cmbPerformanceVenue.ItemsSource = ufoVM.Venues.OrderBy(x => x.Description);
            cmbPerformanceArtist.ItemsSource = ufoVM.Artists.OrderBy(x => x.Name);
        }

        private void btnSavePerformance_Click(object sender, RoutedEventArgs e)
        {
            var performance = ufoVM.SelectedPerformance;
            var selectedDate = performance.Date;
            var selectedTime = performance.Time;
            var selectedVenue = performance.Venue;
            var selectedArtist = performance.Artist;

            if (string.IsNullOrEmpty(performance.Artist))
            {
                txtError.Text = "Please select an artist.";
                return;
            }

            ufoVM.LoadPerformancesOfDay(selectedDate);
            // if there's already a performance, which is not from selectedArtist
            if (ufoVM.PerformancesOfDay.Where(x => x.Date == selectedDate
                                                && x.Time == selectedTime
                                                && x.Venue == selectedVenue
                                                && x.Artist != selectedArtist).Count() > 0)
            {
                txtError.Text = txtError.Text = $"Performance on {selectedDate:yyyy/MM/dd} - {selectedTime}:00 at {ufoVM.Venues.FirstOrDefault(v => v.ShortName == selectedVenue).Description} already exists!";
                return;
            }
            // if it's the artist's performance return
            else if (ufoVM.PerformancesOfDay.Where(x => x.Date == selectedDate
                                                && x.Time == selectedTime
                                                && x.Venue == selectedVenue
                                                && x.Artist == selectedArtist).Count() > 0)
            {
                // do nothing
                txtError.Text = txtError.Text = "No changes were made!";
                return;
            }
            else // check if performance is possible
            {
                if (!commander.PerformanceIsPossible(ufoVM.SelectedPerformance.Performance))
                {
                    txtError.Text = "Artist needs 1 hour break and can't have 2 simultaneously!";
                    return;
                }
            }

            txtError.Text = "";

            ShowMessage(sender, "Save", "Cancel", "Save changes?", "Sure you want to save changes?");
        }

        private void btnDeletePerformance_Click(object sender, RoutedEventArgs e)
        {
            txtError.Text = "";
            ShowMessage(sender, "Delete", "Cancel", "Delete selected artist?", "Sure you want to delete selected performance?");
        }

        private void btnCancelEditPerformance_Click(object sender, RoutedEventArgs e)
        {
            txtError.Text = "";
            ShowMessage(sender, "Continue", "Cancel", "Revert changes?", "Sure you want to revert all changes?");
        }
    }
}

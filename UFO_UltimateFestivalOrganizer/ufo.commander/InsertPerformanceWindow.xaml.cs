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
    /// Interaction logic for InsertPerformanceWindow.xaml
    /// </summary>
    public partial class InsertPerformanceWindow
    {
        private UFOCollectionVM ufoCollVM;
        private ICommanderBL commander = BLFactory.GetCommander();

        public InsertPerformanceWindow(UFOCollectionVM uFOCollectionVM)
        {
            InitializeComponent();
            DataContext = uFOCollectionVM;
            ufoCollVM = uFOCollectionVM;
        }

        private void btnCancelSubmitPerformance_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void MetroWindow_Closed(object sender, EventArgs e)
        {
            cmbTime.SelectedIndex = 0;
            cmbVenue.SelectedIndex = -1;
            cmbArtist.SelectedIndex = -1;
        }

        private void btnSubmitPerformance_Click(object sender, RoutedEventArgs e)
        {
            if (cmbArtist.SelectedIndex == -1 && cmbTime.SelectedIndex == -1 && cmbVenue.SelectedIndex == -1)
            {
                txtError.Text = "Fields with * musn't be empty!";
                return;
            } else if (cmbTime.SelectedIndex == -1) {
                txtError.Text = "Please select a time.";
                return;
            } else if (cmbVenue.SelectedIndex == -1)
            {
                txtError.Text = "Please select a venue.";
                return;

            } else if (cmbArtist.SelectedIndex == -1)
            {
                txtError.Text = "Please select an artist.";
                return;
            }

            // check if artist already exists and if any required fields are empty
            ufoCollVM.LoadPerformancesOfDay(ufoCollVM.NewPerformance.Date);
            if (ufoCollVM.PerformancesOfDay.Where(x => x.Date == ufoCollVM.NewPerformance.Date
                                                    && x.Time == ufoCollVM.NewPerformance.Time
                                                    && x.Venue == ufoCollVM.NewPerformance.Venue).Count() > 0)
            {
                txtError.Text = $"Performance on date, time and venue: {ufoCollVM.NewPerformance.Date:yyyy/MM/dd}, {ufoCollVM.NewPerformance.Time}Uhr at {ufoCollVM.NewPerformance.Venue} already exists!";
                return;
            }
            else
            {
                if (ufoCollVM.PerformancesOfDay.Where(x => x.Artist == ufoCollVM.NewPerformance.Artist).Count() > 0)
                {
                    if (!commander.PerformanceIsPossible(ufoCollVM.NewPerformance.Performance))
                    {
                        txtError.Text = "Artist needs at least 1 hour break between performances!";
                        return;
                    }
                }
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

        private void MetroWindow_Loaded(object sender, RoutedEventArgs e)
        {
            datePicker.DisplayDateStart = DateTime.Today.Date;
            CalendarDateRange cdr = new CalendarDateRange(DateTime.MinValue, DateTime.Today.AddDays(-1));
            datePicker.BlackoutDates.Add(cdr);

            cmbTime.ItemsSource = ufoCollVM.PerformancesTimes;
            cmbVenue.ItemsSource = ufoCollVM.Venues.OrderBy(x => x.Description);
            cmbArtist.ItemsSource = ufoCollVM.Artists;
        }
    }
}


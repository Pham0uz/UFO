using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using swk5.ufo.dal;
using swk5.ufo.server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
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
    /// Interaction logic for PerformanceOverview.xaml
    /// </summary>
    public partial class PerformanceOverview : UserControl
    {
        private MetroWindow mainWindow;
        private UFOCollectionVM ufoVM;
        private ProgressDialogController _controller;
        // workaround
        private bool initialized = false;
        private bool firstTime = true;
        private bool preventChange = false;

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
            preventChange = false;
        }

        public PerformanceOverview()
        {
            mainWindow = Application.Current.Windows.OfType<MetroWindow>().FirstOrDefault(x => x.Title == "Ultimate Festival Organizer");
            ufoVM = (UFOCollectionVM)mainWindow.DataContext;
            DataContext = ufoVM;
            InitializeComponent();
            initialized = true;
        }

        private async void PerformanceDay_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (!initialized)
                return;
            else {
                var selectedItem = (DateTime)PerformanceDay.SelectedItem;
                IsEnabled = false;
                _controller = await mainWindow.ShowProgressAsync("Performances", "Loading...");
                _controller.SetIndeterminate();

                await Task.Factory.StartNew(() =>
                {
                    ufoVM.LoadPerformancesOfDay(selectedItem);
                });

                await _controller.CloseAsync();

                IsEnabled = true;
                _controller.SetProgress(1);
            }
        }

        private void PerformancesGrid_CurrentCellChanged(object sender, EventArgs e)
        {
            DataGrid dg = (DataGrid)sender;

            // workaround because editcell is disabled, so currentcellchanged is triggered twice
            if (firstTime)
            {
                firstTime = false;
                int idx;

                if (dg.CurrentCell.Column != null)
                    idx = dg.CurrentCell.Column.DisplayIndex;
                else
                    idx = dg.SelectedCells[0].Column.DisplayIndex;

                var tdypgm = (UFOCollectionVM.TodaysProgramVM)dg.CurrentCell.Item;

                ufoVM.SelectedPerformance = tdypgm.Performances[idx - 2];
                if (!string.IsNullOrEmpty(ufoVM.SelectedPerformance.Artist) && !preventChange)
                {
                    ufoVM.toDeletePerformance = ufoVM.SelectedPerformance;
                    preventChange = true;
                }

                ToggleFlyout(2);
                //Debug
                //if (mainWindow != null)
                //    mainWindow.ShowMessageAsync("Debug", $"SelectionChanged, { tdypgm.Performances[idx - 2].Artist}");
            }
            else
                firstTime = true;
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            var buttonCmdParam = button.CommandParameter;
            var command = button.Tag as ICommand;
            IsEnabled = false;
            _controller = await mainWindow.ShowProgressAsync("EMail", "Sending...");
            _controller.SetIndeterminate();

            await Task.Factory.StartNew(() =>
            {
                // Execute command in tag
                if (button != null)
                {
                    if (command != null)
                        command.Execute(buttonCmdParam);
                }
                return true;
            });

            await _controller.CloseAsync();

            IsEnabled = true;
            await mainWindow.ShowMessageAsync("EMail", "EMails were sent successfully!");
            _controller.SetProgress(1);
        }
    }
}

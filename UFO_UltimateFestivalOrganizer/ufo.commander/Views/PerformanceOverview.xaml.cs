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

        public PerformanceOverview()
        {
            InitializeComponent();
            ufoVM = new UFOCollectionVM();
            DataContext = ufoVM;
            mainWindow = Application.Current.Windows.OfType<MetroWindow>().FirstOrDefault(x => x.Title == "Ultimate Festival Organizer");
        }

        private async void PerformanceDay_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
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

        private void PerformancesGrid_CurrentCellChanged(object sender, EventArgs e)
        {
            DataGrid dg = (DataGrid)sender;
            UFOCollectionVM.TodaysProgramVM tdypgm = (UFOCollectionVM.TodaysProgramVM)dg.Items.CurrentItem;
            int i = dg.Items.CurrentPosition;

            if (mainWindow != null)
                //mainWindow.ShowMessageAsync("Debug", $"SelectionChanged, {ufoVM.SelectedPerformance.Artist}");
                //mainWindow.ShowMessageAsync("Debug", $"SelectionChanged, {dg.CurrentCell.Item.ToString()}");
                //mainWindow.ShowMessageAsync("Debug", $"SelectionChanged, {dg.CurrentColumn.DisplayIndex}");
                mainWindow.ShowMessageAsync("Debug", $"SelectionChanged, { tdypgm.Performances[5].Artist}");
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

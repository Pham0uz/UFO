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
    /// Interaction logic for PerformanceOverview.xaml
    /// </summary>
    public partial class PerformanceOverview : UserControl
    {
        private MetroWindow mainWindow;
        private UFOCollectionVM ufoVM;

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

        private void PerformanceDay_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ufoVM.LoadPerformancesOfDay((DateTime)PerformanceDay.SelectedItem);
        }

        private void PerformancesGrid_CurrentCellChanged(object sender, EventArgs e)
        {
            DataGrid dg = (DataGrid)sender;
            if (mainWindow != null)
                //mainWindow.ShowMessageAsync("Debug", $"SelectionChanged, {ufoVM.SelectedPerformance.Artist}");
                mainWindow.ShowMessageAsync("Debug", $"SelectionChanged, {dg.CurrentCell.Item.ToString()}");
        }
    }
}

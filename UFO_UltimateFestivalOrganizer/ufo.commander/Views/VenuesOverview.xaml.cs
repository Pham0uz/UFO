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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ufo.commander.Views
{
    /// <summary>
    /// Interaction logic for VenuesOverview.xaml
    /// </summary>
    public partial class VenuesOverview : UserControl
    {
        private ICommanderBL commander = BLFactory.GetCommander();
        private MetroWindow mainWindow;

        public VenuesOverview()
        {
            InitializeComponent();
            mainWindow = Application.Current.Windows.OfType<MetroWindow>().FirstOrDefault(x => x.Title == "Ultimate Festival Organizer");
        }

        private void VenueGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (mainWindow != null)
                ToggleFlyout(1);
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
    }
}

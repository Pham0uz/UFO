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
        private MetroWindow mainWindow;

        public ArtistsOverview()
        {
            InitializeComponent();
            // get MainWindow
            mainWindow = Application.Current.Windows.OfType<MetroWindow>().FirstOrDefault(x => x.Title=="UltimateFestivalOrganizer");
        }

        private void ArtistGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ToggleFlyout(0);
            // TODO check if anything changed, if then alert user!
        }

        //private ICommand openFirstFlyoutCommand;
        //public ICommand OpenFirstFlyoutCommand
        //{
        //    get
        //    {

        //        if (mainWindow != null)
        //        {
        //            return this.openFirstFlyoutCommand ?? (this.openFirstFlyoutCommand = new SimpleCommand
        //            {
        //                CanExecuteDelegate = x => mainWindow.Flyouts.Items.Count > 0,
        //                ExecuteDelegate = x => ToggleFlyout(0)
        //            }); 
        //        }
        //        return null;
        //    }
        //}

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

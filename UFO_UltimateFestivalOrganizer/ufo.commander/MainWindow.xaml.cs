using System.Windows;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using swk5.ufo.server;
using ufo.commander.ViewModels;
using NLog;

namespace ufo.commander
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        private static Logger Logger = LogManager.GetCurrentClassLogger();
        ICommanderBL commander;
        private bool shutdown;

        public MainWindow()
        {
            Logger.Info("Application successfully started!");
            Application.Current.MainWindow = this;
            commander = BLFactory.GetCommander();
            DataContext = new UFOCollectionVM();
            InitializeComponent();
        }

        private async void UFOMain_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = !shutdown;
            if (shutdown) return;

            var mySettings = new MetroDialogSettings()
            {
                AffirmativeButtonText = "Quit",
                NegativeButtonText = "Cancel",
                AnimateShow = true,
                AnimateHide = false
            };

            var result = await this.ShowMessageAsync("Quit application?",
                "Sure you want to quit application?",
                MessageDialogStyle.AffirmativeAndNegative, mySettings);

            shutdown = result == MessageDialogResult.Affirmative;

            Logger.Info("Application says BYE!");
            if (shutdown)
                Application.Current.Shutdown();
        }
    }
}

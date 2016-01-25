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
        ICommanderBL commander = BLFactory.GetCommander();
        private bool shutdown;

        public MainWindow()
        {
            Application.Current.MainWindow = this;
            DataContext = new UFOCollectionVM(this);
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

            if (shutdown)
                Application.Current.Shutdown();
        }
    }
}

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

        public static MainWindow Instance { get; private set; }


        public MainWindow()
        {
            Logger.Info("Application successfully started!");
            InitializeComponent();
            Application.Current.MainWindow = this;
            commander = BLFactory.GetCommander();
            DataContext = new UFOCollectionVM();
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

        //private async void ShowLoginDialogPasswordPreview(object sender, RoutedEventArgs e)
        //{
        //    LoginDialogData result = await this.ShowLoginAsync("Authentication", "Enter your credentials", new LoginDialogSettings { ColorScheme = this.MetroDialogOptions.ColorScheme, InitialUsername = "MahApps", EnablePasswordPreview = true });
        //    if (result == null)
        //    {
        //        //User pressed cancel
        //    }
        //    else
        //    {
        //        MessageDialogResult messageResult = await this.ShowMessageAsync("Authentication Information", String.Format($"Username: {result.Username}\nPassword: {result.Password}"));
        //    }
        //}
    }


}

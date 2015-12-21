using NLog;
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
    /// Interaction logic for ArtistsOverview.xaml
    /// </summary>
    public partial class ArtistsOverview : UserControl
    {
        private static Logger Logger = LogManager.GetCurrentClassLogger();
        UFOCollectionVM session;
        public ArtistsOverview()
        {
            InitializeComponent();
            DataContext = session;
            Logger.Info("ArtistsOverview started!");
        }

        private void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void doBinding()
        {
            Logger.Info("Binding successful.");
        }
    }
}

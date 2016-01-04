using NLog;
using System.Windows.Controls;
using ufo.commander.ViewModels;

namespace ufo.commander.Views
{
    /// <summary>
    /// Interaction logic for ArtistsOverview.xaml
    /// </summary>
    public partial class ArtistsOverview : UserControl
    {
        private static Logger Logger = LogManager.GetCurrentClassLogger();

        public ArtistsOverview()
        {
            InitializeComponent();
            DataContext = new UFOCollectionVM();
            Logger.Info("ArtistsOverview started!");
        }
    }
}

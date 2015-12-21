using MahApps.Metro.Controls;
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
using System.Windows.Shapes;
using ufo.commander.ViewModels;

namespace ufo.commander
{
    /// <summary>
    /// Interaction logic for InsertArtistWindow.xaml
    /// </summary>
    public partial class InsertArtistWindow
    {
        private static Logger Logger = LogManager.GetCurrentClassLogger();
        UFOCollectionVM session;

        public InsertArtistWindow()
        {
            InitializeComponent();
            DataContext = session;
        }
    }
}

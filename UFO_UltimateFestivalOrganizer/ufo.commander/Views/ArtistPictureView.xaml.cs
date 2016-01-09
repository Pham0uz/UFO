using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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
    /// Interaction logic for ArtistPictureView.xaml
    /// </summary>
    public partial class ArtistPictureView : UserControl
    {
        public ArtistPictureView()
        {
            InitializeComponent();
        }

        private void ArtistPicture_ImageFailed(object sender, ExceptionRoutedEventArgs e)
        {
            ((Image)sender).Source = new BitmapImage(new Uri("../Resource/images/userplacehold.png", UriKind.Relative));
        }
    }
}

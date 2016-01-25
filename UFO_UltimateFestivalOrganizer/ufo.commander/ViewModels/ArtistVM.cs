using MahApps.Metro.Controls.Dialogs;
using NLog;
using swk5.ufo.dal;
using swk5.ufo.server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ufo.commander.ViewModels
{
    public class ArtistVM : ViewModelBase
    {
        private Artist artist;

        public ArtistVM(Artist artist)
        {
            this.artist = artist;
        }

        #region Properties

        public string Name
        {
            get { return artist.Name; }
            set
            {
                artist.Name = value;
                RaisePropertyChangedEvent(nameof(Name));
                // better version = RaisePropertyChangedEven(nameof(Name));
            }
        }

        public string PictureURL
        {
            get { return artist.PictureURL; }
            set
            {
                artist.PictureURL = value;
                RaisePropertyChangedEvent(nameof(PictureURL));
            }
        }

        public string PromoVideoURL
        {
            get { return artist.PromoVideoURL; }
            set
            {
                artist.PromoVideoURL = value;
                RaisePropertyChangedEvent(nameof(PromoVideoURL));
            }
        }

        public string CategoryName
        {
            get { return artist.CategoryName; }
            set
            {
                artist.CategoryName = value;
                RaisePropertyChangedEvent(nameof(CategoryName));
            }
        }

        public string CountryCode
        {
            get { return artist.CountryCode; }
            set
            {
                artist.CountryCode = value;
                RaisePropertyChangedEvent(nameof(CountryCode));
            }
        }

        public Artist Artist { get { return artist; } }

        #endregion
    }
}

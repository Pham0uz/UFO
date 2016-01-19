using swk5.ufo.dal;
using swk5.ufo.server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ufo.commander.ViewModels
{
    public class VenueVM : ViewModelBase
    {
        private ICommanderBL commander = BLFactory.GetCommander();
        private Venue venue;

        public VenueVM(Venue venue)
        {
            this.venue = venue;
        }

        #region Properties

        public string ShortName
        {
            get { return venue.ShortName; }
            set
            {
                venue.ShortName = value;
                RaisePropertyChangedEvent("ShortName");
            }
        }

        public string Description
        {
            get { return venue.Description; }
            set
            {
                venue.Description = value;
                RaisePropertyChangedEvent("Description");
            }
        }

        public double Latitude
        {
            get { return venue.Latitude; }
            set
            {
                venue.Latitude = value;
                RaisePropertyChangedEvent(nameof(Latitude));
            }
        }

        public double Longitude
        {
            get { return venue.Longitude; }
            set
            {
                venue.Longitude = value;
                RaisePropertyChangedEvent(nameof(Longitude));
            }
        }

        public Venue Venue { get { return venue; } }

        #endregion
    }
}

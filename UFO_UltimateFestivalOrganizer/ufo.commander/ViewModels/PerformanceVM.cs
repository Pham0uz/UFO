using swk5.ufo.dal;
using swk5.ufo.server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ufo.commander.ViewModels
{
    public class PerformanceVM : ViewModelBase
    {
        private Performance performance;

        public PerformanceVM(Performance performance)
        {
            this.performance = performance;
        }

        #region Properties

        public DateTime Date
        {
            get { return performance.Date; }
            set
            {
                performance.Date = value;
                RaisePropertyChangedEvent("Date");
            }
        }

        public int Time
        {
            get { return performance.Time; }
            set
            {
                performance.Time = value;
                RaisePropertyChangedEvent("Time");
            }
        }

        public string Artist
        {
            get { return performance.Artist; }
            set
            {
                performance.Artist = value;
                RaisePropertyChangedEvent("Artist");
            }
        }

        public string Venue
        {
            get { return performance.Venue; }
            set
            {
                performance.Venue = value;
                RaisePropertyChangedEvent("Venue");
            }
        }

        public Performance Performance { get { return performance; } }

        #endregion
    }
}

using NLog;
using swk5.ufo.dal;
using swk5.ufo.server;

namespace ufo.commander.ViewModels
{
    public class CountryVM : ViewModelBase
    {
        Logger Logger = LogManager.GetCurrentClassLogger();
        private ICommanderBL commander = BLFactory.GetCommander();
        private Country country;

        public CountryVM(Country country)
        {
            this.country = country;
        }

        #region Properties

        public string Code
        {
            get { return country.Code; }
            set
            {
                country.Code = value;
                RaisePropertyChangedEvent(nameof(Code));
            }
        }

        public string Name
        {
            get { return country.Name; }
            set
            {
                country.Name = value;
                RaisePropertyChangedEvent(nameof(Name));
            }
        }

        public Country Country { get { return country; } }

        #endregion
    }
}
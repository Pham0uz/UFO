using swk5.ufo.server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using swk5.ufo.dal;
using System.Windows.Input;
using ufo.commander;
using MahApps.Metro.Controls;
using System.Windows;
using ufo.commander.Views;
using NLog;

namespace ufo.commander.ViewModels
{
    public class UFOCollectionVM : ViewModelBase
    {
        private static Logger Logger = LogManager.GetCurrentClassLogger();

        private ICommanderBL commander = BLFactory.GetCommander();

        private UFOCollectionVM currentSession;

        private ArtistVM currentArtist;
        private ArtistVM newArtist;

        public ArtistVM CurrentArtist
        {
            get { return currentArtist; }
            set
            {
                if ((currentArtist != null) && (value != null))
                {
                    currentArtist = value;
                    RaisePropertyChangedEvent("CurrentArtist");
                }
            }
        }

        //public ArtistVM NewArtist
        //{
        //    get
        //    {
        //        if (newArtist == null)
        //        {
        //            Artist artist = new Artist();
        //            newArtist = new ArtistVM(artist);
        //        }
        //    }
        //    set
        //    {
        //        if ((currentArtist != null) && (value != null))
        //        {
        //            currentArtist = value;
        //            RaisePropertyChangedEvent("CurrentArtist");
        //        }
        //    }
        //}

        private VenueVM currentVenue;
        private VenueVM newVenue;

        private PerformanceVM currentPerformance;
        public PerformanceVM newPerformance;

        private InsertArtistWindow createArtistWindow;

        #region TransactionCommands

        // Artist commands
        private ICommand openCreateArtistCommand;
        private ICommand cancelCreateArtistCommand;
        private ICommand openEditArtistCommand;
        private ICommand createArtistCommand;
        private ICommand updateArtistCommand;
        private ICommand deleteArtistCommand;

        // Venue commands
        private ICommand openCreateVenueCommand;
        private ICommand openEditVenueCommand;
        private ICommand createVenueCommand;
        private ICommand updateVenueCommand;
        private ICommand deleteVenueCommand;

        // Performance commands
        private ICommand openCreatePerformanceCommand;
        private ICommand openEditPerformanceCommand;
        private ICommand createPerformanceCommand;
        private ICommand updatePerformanceCommand;
        private ICommand deletePerformanceCommand;

        #endregion

        #region Artist Commands / Methods

        public ICommand OpenCreateArtistCommand
        {

            get
            {
                Logger.Info("OpenCreateArtistCommand");
                return openCreateArtistCommand ?? (openCreateArtistCommand = new RelayCommand(param => OpenCreateArtist()));
                //return this.openCreateArtistCommand ?? (this.openCreateArtistCommand = new SimpleCommand
                //{
                //    CanExecuteDelegate = x => MainWindow.Instance.Flyouts.Items.Count > 0,
                //    ExecuteDelegate = x => MainWindow.Instance.ToggleFlyout(0)
                //});
            }
        }

        private void OpenCreateArtist()
        {
            createArtistWindow = new InsertArtistWindow();
            createArtistWindow.ShowDialog();
        }

        public ICommand CreateArtistCommand
        {
            get
            {
                if (createArtistCommand == null)
                    createArtistCommand = new RelayCommand(param => CreateArtist((ArtistVM)param));
                return createArtistCommand;
            }
        }

        private void CreateArtist(ArtistVM artist)
        {
            commander.InsertArtist(artist.Artist);
            LoadArtists();
            newArtist = null;
        }

        //private ICommand CancelInsertArtistCommand
        //{
        //    get
        //    {
        //        return cancelCreateArtistCommand ?? (cancelCreateArtistCommand = new SimpleCommand
        //        {
        //            CanExecuteDelegate = x => MainWindow.Instance.Flyouts.Items.Count > 0,
        //            ExecuteDelegate = x => MainWindow.Instance.ToggleFlyout(0)
        //        });
        //    }
        //}

        public ICommand UpdateArtistCommand
        {
            get
            {
                if (updateArtistCommand == null)
                    updateArtistCommand = new RelayCommand(param => UpdateArtist((ArtistVM)param));
                return updateArtistCommand;
            }
        }

        private void UpdateArtist(ArtistVM artist)
        {
            commander.UpdateArtist(artist.Artist);
            LoadArtists();
            newArtist = null;
        }

        public ICommand DeleteArtistCommand
        {
            get
            {
                if (deleteArtistCommand == null)
                    deleteArtistCommand = new RelayCommand(param => DeleteArtist((ArtistVM)param));
                return deleteArtistCommand;
            }
        }

        public void DeleteArtist(ArtistVM artist)
        {
            Artists.Remove(artist);
            commander.DeleteArtist(artist.Artist);
        }

        #endregion

        #region Collections

        public ObservableCollection<ArtistVM> Artists { get; set; }
        public ObservableCollection<VenueVM> Venues { get; set; }

        public ObservableCollection<PerformanceVM> Performances { get; set; }
        public ObservableCollection<PerformanceVM> Performances1 { get; set; }
        public ObservableCollection<PerformanceVM> Performances2 { get; set; }
        public ObservableCollection<PerformanceVM> Performances3 { get; set; }

        #endregion

        public UFOCollectionVM()
        {
            Logger.Info("Loading UFOCollectionVM");
            Artists = new ObservableCollection<ArtistVM>();
            Venues = new ObservableCollection<VenueVM>();
            Performances = new ObservableCollection<PerformanceVM>();
            Performances1 = new ObservableCollection<PerformanceVM>();
            Performances2 = new ObservableCollection<PerformanceVM>();
            Performances3 = new ObservableCollection<PerformanceVM>();
            LoadArtists();
            LoadVenues();
            LoadPerformances();
        }

        #region Loading methods
        private void LoadArtists()
        {
            Artists.Clear();
            ICollection<Artist> artistList = commander.GetArtists();
            foreach (Artist artist in artistList)
            {
                Artists.Add(new ArtistVM(artist));
            }
        }

        private void LoadVenues()
        {
            Venues.Clear();
            ICollection<Venue> venueList = commander.GetVenues();
            foreach (Venue venue in venueList)
            {
                Venues.Add(new VenueVM(venue));
            }
        }

        private void LoadPerformances()
        {
            Performances.Clear();
            Performances1.Clear();
            Performances2.Clear();
            Performances3.Clear();
            ICollection<Performance> peformanceList = commander.GetPerformances();
            ICollection<Performance> performanceList1 = commander.GetPerformancesByDate(new DateTime(2015, 07, 23));
            ICollection<Performance> performanceList2 = commander.GetPerformancesByDate(new DateTime(2015, 07, 24));
            ICollection<Performance> performanceList3 = commander.GetPerformancesByDate(new DateTime(2015, 07, 25));
            foreach (Performance performance in peformanceList)
            {
                Performances.Add(new PerformanceVM(performance));
            }
            foreach (Performance performance in performanceList1)
            {
                Performances1.Add(new PerformanceVM(performance));
            }
            foreach (Performance performance in performanceList2)
            {
                Performances2.Add(new PerformanceVM(performance));
            }
            foreach (Performance performance in performanceList3)
            {
                Performances2.Add(new PerformanceVM(performance));
            }
        }

        #endregion

        public UFOCollectionVM CurrentSession
        {
            get { return currentSession; }
            set
            {
                if (currentSession != value)
                {
                    currentSession = value;
                    currentSession.LoadArtists();
                    currentSession.LoadVenues();
                    currentSession.LoadPerformances();
                    RaisePropertyChangedEvent("CurrentSession");
                    Logger.Info("CurrentSession loaded.");
                }
            }
        }
    }

}

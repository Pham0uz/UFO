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
using MahApps.Metro.Controls.Dialogs;
using System.Collections;

namespace ufo.commander.ViewModels
{
    public class UFOCollectionVM : ViewModelBase
    {
        #region Helper Classes
        private static Logger Logger = LogManager.GetCurrentClassLogger();

        private ICommanderBL commander = BLFactory.GetCommander();

        #endregion

        #region Private Members
        private UFOCollectionVM currentSession;
        private ArtistVM newArtist;
        private ArtistVM selectedArtist;


        #endregion

        #region Collections

        public ObservableCollection<ArtistVM> Artists { get; set; }
        public ObservableCollection<CategoryVM> Categories { get; set; }
        public ObservableCollection<CountryVM> Countries { get; set; }
        public ObservableCollection<VenueVM> Venues { get; set; }
        public ObservableCollection<PerformanceVM> Performances { get; set; }
        public ObservableCollection<PerformanceVM> Performances1 { get; set; }
        public ObservableCollection<PerformanceVM> Performances2 { get; set; }
        public ObservableCollection<PerformanceVM> Performances3 { get; set; }

        #endregion

        #region Constructor
        public UFOCollectionVM()
        {
            Logger.Info("Loading UFOCollectionVM");
            InitViewModelCollections();
            LoadCollections();
        }

        #endregion

        #region Helper Methods
        private void InitViewModelCollections()
        {
            Artists = new ObservableCollection<ArtistVM>();
            Categories = new ObservableCollection<CategoryVM>();
            Countries = new ObservableCollection<CountryVM>();
            Venues = new ObservableCollection<VenueVM>();
            Performances = new ObservableCollection<PerformanceVM>();
            Performances1 = new ObservableCollection<PerformanceVM>();
            Performances2 = new ObservableCollection<PerformanceVM>();
            Performances3 = new ObservableCollection<PerformanceVM>();
        }

        #endregion

        #region Loading methods

        private void LoadCollections()
        {
            LoadArtists();
            LoadCategories();
            LoadCountries();
            LoadVenues();
            LoadPerformances();
        }

        private void LoadArtists()
        {
            Artists.Clear();
            ICollection<Artist> artistList = commander.GetArtists();
            foreach (Artist artist in artistList)
            {
                Artists.Add(new ArtistVM(artist));
            }
        }

        private void LoadCategories()
        {
            Categories.Clear();
            ICollection<Category> categoriesList = commander.GetCategories();
            foreach (Category category in categoriesList)
            {
                Categories.Add(new CategoryVM(category));
            }
        }

        private void LoadCountries()
        {
            Countries.Clear();
            ICollection<Country> countriesList = commander.GetCountries();
            foreach (Country country in countriesList)
            {
                Countries.Add(new CountryVM(country));
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

        #region Properties
        public UFOCollectionVM CurrentSession
        {
            get { return currentSession; }
            set
            {
                if (currentSession != value)
                {
                    currentSession = value;
                    currentSession.LoadArtists();
                    currentSession.LoadCategories();
                    currentSession.LoadCountries();
                    currentSession.LoadVenues();
                    currentSession.LoadPerformances();
                    RaisePropertyChangedEvent(nameof(CurrentSession));
                    Logger.Info("CurrentSession loaded.");
                }
            }
        }

        public ArtistVM NewArtist
        {
            get { return newArtist; }
            set
            {
                if (value != null)
                {
                    newArtist = value;
                    RaisePropertyChangedEvent(nameof(NewArtist));
                }
            }
        }

        public ArtistVM SelectedArtist
        {
            get { return selectedArtist; }
            set
            {
                if ((selectedArtist != null) && (value != null))
                {
                    selectedArtist = value;
                    RaisePropertyChangedEvent(nameof(SelectedArtist));
                }
            }
        }

        #endregion

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

                //var mainWindow = (MetroWindow)Application.Current.MainWindow;
                //return openCreateArtistCommand ?? (openCreateArtistCommand = new SimpleCommand
                //{
                //    CanExecuteDelegate = x => mainWindow.Flyouts.Items.Count > 0,
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
                Logger.Info("createArtistCommand");
                if (createArtistCommand == null)
                    createArtistCommand = new RelayCommand((param) =>
                    {
                        ArtistVM a = (ArtistVM)param;
                        if (a != null)
                        {
                            Logger.Info(a.Name + a.PictureURL + a.PromoVideoURL);
                            Logger.Info(a.CategoryName + a.CountryCode);
                        }
                        CreateArtist((ArtistVM)param);
                    });
                return createArtistCommand;
            }
        }

        private void CreateArtist(ArtistVM artist)
        {
            Logger.Info("createArtist");
            commander.InsertArtist(artist.Artist);
            LoadArtists();
            newArtist = null;
            createArtistWindow.Close();
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
    }
}
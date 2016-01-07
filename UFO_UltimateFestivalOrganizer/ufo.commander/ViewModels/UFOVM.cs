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
using System.Windows.Threading;

namespace ufo.commander.ViewModels
{
    public class UFOCollectionVM : ViewModelBase
    {
        #region Helper Classes
        private static Logger Logger = LogManager.GetCurrentClassLogger();

        private ICommanderBL commander = BLFactory.GetCommander();

        #endregion

        #region Private Members
        // save reference to MainWindow
        MetroWindow metroWindow = (Application.Current.MainWindow as MetroWindow);
        IDialogCoordinator dialogCoordinator;
        //private UFOCollectionVM currentSession;

        private ArtistVM newArtist;
        private ArtistVM selectedArtist;
        private bool isArtistSelected;

        private InsertArtistWindow createArtistWindow;

        private VenueVM currentVenue;
        private VenueVM newVenue;

        private PerformanceVM currentPerformance;
        public PerformanceVM newPerformance;

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
        public UFOCollectionVM(DialogCoordinator dialogCoordinator)
        {
            InitViewModelCollections();
            LoadCollections();
            this.dialogCoordinator = dialogCoordinator;
            isArtistSelected = false;
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
        //public UFOCollectionVM CurrentSession
        //{
        //    get { return currentSession; }
        //    set
        //    {
        //        if (currentSession != value)
        //        {
        //            currentSession = value;
        //            currentSession.LoadArtists();
        //            currentSession.LoadCategories();
        //            currentSession.LoadCountries();
        //            currentSession.LoadVenues();
        //            currentSession.LoadPerformances();
        //            RaisePropertyChangedEvent(nameof(CurrentSession));
        //            Logger.Info("CurrentSession loaded.");
        //        }
        //    }
        //}

        public ArtistVM NewArtist
        {
            get
            {
                if (newArtist == null)
                {
                    Artist artist = new Artist();
                    newArtist = new ArtistVM(artist);
                }
                return newArtist;
            }
            set
            {
                if ((newArtist != value) && (value != null))
                {
                    newArtist = value;
                    RaisePropertyChangedEvent(nameof(NewArtist));
                }
            }
        }

        public ArtistVM SelectedArtist
        {
            get
            {
                return selectedArtist;
            }
            // because I couldn't use the Dialog from MahApps I had to use the standard MessageBox
            set
            {
                // store current value so that we can
                // rollback
                var origValue = selectedArtist;

                //Logger.Info($"orig: {origValue.Name}| selected: {selectedArtist.Name}");
                Logger.Info("SET selectedArtist");

                if (selectedArtist == value)
                    return;

                // Note that we actually change the value for now.
                // This is necessary because WPF seems to query the
                // value after the change. The combo box likes to know
                // that the value did change
                selectedArtist = value;

                if (isArtistSelected)
                {

                    if (MessageBox.Show(
                            $"Save changes of selected artist: {origValue.Name}?",
                            "Continue", MessageBoxButton.YesNo
                        ) != MessageBoxResult.Yes)
                    {
                        // ROLLBACK the change, but after the
                        // UI has finished it's current context operation.
                        // for successful unit-tests check if Application.Current is not null
                        if (Application.Current != null)
                        {
                            Logger.Info("Application.Current exists");
                            Application.Current.Dispatcher.BeginInvoke(
                                new Action(() =>
                                {
                                    // Do this against the underlying value so
                                    // that we don't invoke the cancellation question again.
                                    Logger.Info("rollback selectedArtist");
                                    Logger.Info($"orig: {origValue.Name}| selected: {selectedArtist.Name}");
                                    selectedArtist = origValue;
                                    RaisePropertyChangedEvent(nameof(SelectedArtist));
                                }),
                                DispatcherPriority.ContextIdle,
                                null
                                );
                            // exit early
                            return;
                        }
                    }


                }
                else
                    // after first run
                    isArtistSelected = true;

                // Normal path. Selection applied.
                // Raise PropertyChange on the field.
                RaisePropertyChangedEvent(nameof(SelectedArtist));
            }
        }

        #endregion

        #region TransactionCommands

        // Artist commands
        private ICommand openCreateArtistCommand;
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

        #region Insert Artist

        public ICommand OpenCreateArtistCommand
        {
            get
            {
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
            createArtistWindow = new InsertArtistWindow(this);
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
            // to update view
            LoadArtists();
            newArtist = null;
            createArtistWindow.Close();
        }

        #endregion

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
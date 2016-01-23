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
using System.Net.Mail;

namespace ufo.commander.ViewModels
{
    public class UFOCollectionVM : ViewModelBase
    {
        #region TodaysProgram

        public class TodaysProgramVM : ViewModelBase
        {
            #region private members

            private VenueVM venue;
            private ObservableCollection<PerformanceVM> performances;

            #region Properties
            public VenueVM Venue
            {
                get { return venue; }
            }

            public ObservableCollection<PerformanceVM> Performances
            {
                get { return performances; }
            }

            #endregion

            //private string venueShortName;
            //private string venueDescription;

            //private string artist1;
            //private string artist2;
            //private string artist3;
            //private string artist4;
            //private string artist5;
            //private string artist6;
            //private string artist7;
            //private string artist8;
            //private string artist9;
            //private string artist10;

            #endregion

            #region ctor
            public TodaysProgramVM(VenueVM v)
            {
                venue = v;
                performances = new ObservableCollection<PerformanceVM>();
            }

            //public TodaysProgramVM()
            //{
            //    artist1 = "";
            //    artist2 = "";
            //    artist3 = "";
            //    artist4 = "";
            //    artist5 = "";
            //    artist6 = "";
            //    artist7 = "";
            //    artist8 = "";
            //    artist9 = "";
            //    artist10 = "";
            //}

            #endregion

        }

        #endregion

        #region Helper Classes
        private static Logger Logger = LogManager.GetCurrentClassLogger();

        private ICommanderBL commander = BLFactory.GetCommander();

        #endregion

        #region Private/Public Members
        // get MainWindow
        private MetroWindow mainWindow;

        private ArtistVM newArtist;
        private ArtistVM selectedArtist;
        private InsertArtistWindow createArtistWindow;

        private VenueVM newVenue;
        private VenueVM selectedVenue;
        private InsertVenueWindow createVenueWindow;

        private DateTime selectedDate;
        private int[] performanceTimes = { 14, 15, 16, 17, 18, 19, 20, 21, 22, 23 };

        private PerformanceVM newPerformance;
        public PerformanceVM selectedPerformance;

        #endregion

        #region Collections

        public ObservableCollection<ArtistVM> Artists { get; set; }
        public ObservableCollection<CategoryVM> Categories { get; set; }
        public ObservableCollection<CountryVM> Countries { get; set; }
        public ObservableCollection<VenueVM> Venues { get; set; }
        public ObservableCollection<DateTime> PerformanceDates { get; set; }
        public ObservableCollection<PerformanceVM> Performances { get; set; }
        public SortedDictionary<DateTime, ObservableCollection<PerformanceVM>> PerformancesMap { get; set; }
        public ObservableCollection<PerformanceVM> PerformancesOfDay { get; set; }
        public ObservableCollection<TodaysProgramVM> TodaysProgram { get; set; }

        #endregion

        #region Constructor
        public UFOCollectionVM()
        {
            InitViewModelCollections();
            LoadCollections();
            mainWindow = Application.Current.Windows.OfType<MetroWindow>().FirstOrDefault(x => x.Title == "Ultimate Festival Organizer");
        }

        #endregion

        #region Helper Methods
        private void InitViewModelCollections()
        {
            Artists = new ObservableCollection<ArtistVM>();
            Categories = new ObservableCollection<CategoryVM>();
            Countries = new ObservableCollection<CountryVM>();
            Venues = new ObservableCollection<VenueVM>();
            PerformanceDates = new ObservableCollection<DateTime>();
            Performances = new ObservableCollection<PerformanceVM>();
            PerformancesOfDay = new ObservableCollection<PerformanceVM>();
            PerformancesMap = new SortedDictionary<DateTime, ObservableCollection<PerformanceVM>>();
            TodaysProgram = new ObservableCollection<TodaysProgramVM>();
        }

        internal void ToggleFlyout(int index)
        {
            if (mainWindow != null)
            {
                var flyout = mainWindow.Flyouts.Items[index] as Flyout;
                if (flyout == null)
                {
                    return;
                }
                flyout.IsOpen = !flyout.IsOpen;
            }
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
            LoadPerformanceDates();
            LoadPerformancesMap();
            LoadPerformancesOfDay(PerformanceDates.FirstOrDefault());
        }

        public void LoadArtists()
        {
            Artists.Clear();
            ICollection<Artist> artistList = commander.GetArtists();
            foreach (Artist artist in artistList)
            {
                Artists.Add(new ArtistVM(artist));
            }
        }

        public void LoadCategories()
        {
            Categories.Clear();
            ICollection<Category> categoriesList = commander.GetCategories();
            foreach (Category category in categoriesList)
            {
                Categories.Add(new CategoryVM(category));
            }
        }

        public void LoadCountries()
        {
            Countries.Clear();
            ICollection<Country> countriesList = commander.GetCountries();
            foreach (Country country in countriesList)
            {
                Countries.Add(new CountryVM(country));
            }
        }

        public void LoadVenues()
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
            ICollection<Performance> peformanceList = commander.GetPerformances();
            foreach (Performance performance in peformanceList)
            {
                Performances.Add(new PerformanceVM(performance));
            }
        }

        private void LoadPerformanceDates()
        {
            PerformanceDates.Clear();
            // get all possible dates
            var query = from performance in Performances
                        select new { date = performance.Date };
            ICollection<DateTime> dateList = Performances.Select(p => p.Date).Distinct().ToList();
            foreach (DateTime date in dateList)
            {
                PerformanceDates.Add(date);
            }

        }

        public void LoadPerformancesOfDay(DateTime selectedDate)
        {
            PerformancesOfDay.Clear();
            ICollection<Performance> performanceList = commander.GetPerformancesByDate(selectedDate);
            foreach (Performance performance in performanceList)
            {
                PerformancesOfDay.Add(new PerformanceVM(performance));
            }

            // create TodaysProgram + fill it with performances of today an empty strings
            TodaysProgram.Clear();
            bool performanceAdded = false;
            foreach (VenueVM v in Venues)
            {
                TodaysProgramVM tmp = new TodaysProgramVM(v);
                foreach (int time in PerformancesTimes)
                {
                    foreach (var p in PerformancesOfDay)
                    {
                        // if performance with current time and venue already exist, add it to todaysprogram
                        // else add a new performance with date, time, venue and empty artist
                        if (p.Venue == v.Venue.ShortName && p.Time == time)
                        {
                            tmp.Performances.Add(p);
                            performanceAdded = true;
                        }
                    }
                    if (!performanceAdded)
                        tmp.Performances.Add(new PerformanceVM(new Performance(selectedDate, time, "", v.Venue.ShortName)));
                    else
                        performanceAdded = false;

                }
                TodaysProgram.Add(tmp);
            }
        }

        public void LoadPerformancesMap()
        {
            PerformancesMap.Clear();
            ICollection<Performance> performanceList = commander.GetPerformances();
            foreach (Performance performance in performanceList)
            {
                if (!PerformancesMap.ContainsKey(performance.Date))
                {
                    var newColl = new ObservableCollection<PerformanceVM>();
                    newColl.Add(new PerformanceVM(performance));
                    PerformancesMap.Add(performance.Date, newColl);
                }
                else
                    PerformancesMap[performance.Date].Add(new PerformanceVM(performance));
            }
        }

        #endregion

        #region Properties
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
            set
            {
                if (selectedArtist != value)
                {
                    selectedArtist = value;
                    RaisePropertyChangedEvent(nameof(SelectedArtist));
                }
            }
        }

        public VenueVM NewVenue
        {
            get
            {
                if (newVenue == null)
                {
                    Venue venue = new Venue();
                    newVenue = new VenueVM(venue);
                }
                return newVenue;
            }
            set
            {
                if ((newVenue != value) && (value != null))
                {
                    newVenue = value;
                    RaisePropertyChangedEvent(nameof(NewVenue));
                }
            }
        }

        public VenueVM SelectedVenue
        {
            get
            {
                return selectedVenue;
            }
            set
            {
                if (selectedVenue != value)
                {
                    selectedVenue = value;
                    RaisePropertyChangedEvent(nameof(SelectedVenue));
                }
            }
        }

        public DateTime SelectedDate
        {
            get
            {
                return selectedDate;
            }
            set
            {
                if (selectedDate != value)
                {
                    selectedDate = value;
                    RaisePropertyChangedEvent(nameof(SelectedDate));
                }
            }
        }

        public PerformanceVM NewPerformance
        {
            get
            {
                if (newPerformance == null)
                {
                    Performance p = new Performance();
                    newPerformance = new PerformanceVM(p);
                }
                return newPerformance;
            }
            set
            {
                if ((newPerformance != value) && (value != null))
                {
                    newPerformance = value;
                    RaisePropertyChangedEvent(nameof(NewPerformance));
                }
            }
        }

        public PerformanceVM SelectedPerformance
        {
            get
            {
                return selectedPerformance;
            }
            set
            {
                if (selectedPerformance != value)
                {
                    selectedPerformance = value;
                    RaisePropertyChangedEvent(nameof(SelectedPerformance));
                }
            }
        }

        public int[] PerformancesTimes
        {
            get
            {
                return performanceTimes;
            }
        }

        #endregion

        #region TransactionCommands

        // Artist commands
        private ICommand openCreateArtistCommand;
        private ICommand createArtistCommand;
        private ICommand updateArtistCommand;
        private ICommand deleteArtistCommand;
        private ICommand cancelEditArtistCommand;
        private ICommand closeEditFlyoutCommand;

        // Venue commands
        private ICommand openCreateVenueCommand;
        private ICommand createVenueCommand;
        private ICommand updateVenueCommand;
        private ICommand deleteVenueCommand;
        private ICommand cancelEditVenueCommand;
        private ICommand closeEditFlyoutCommand2;

        // Performance commands
        private ICommand selectedDateChangeCommand;
        //private ICommand openCreatePerformanceCommand;
        //private ICommand openEditPerformanceCommand;
        //private ICommand createPerformanceCommand;
        //private ICommand updatePerformanceCommand;
        //private ICommand deletePerformanceCommand;

        private ICommand sendEmailWithProgramCommand;
        private ICommand closeApplicationCommand;

        #endregion

        #region Artist Commands / Methods

        #region Insert Artist

        public ICommand OpenCreateArtistCommand
        {
            get
            {
                return openCreateArtistCommand ?? (openCreateArtistCommand = new RelayCommand(param => OpenCreateArtist()));
            }
        }

        private void OpenCreateArtist()
        {
            createArtistWindow = new InsertArtistWindow(this);
            createArtistWindow.Owner = mainWindow;
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

        #region Update Artist
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

        #endregion

        #region Delete Artist
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
            LoadPerformances();
            // delete future performances
            foreach(var p in Performances)
            {
                // if same artist, future performances (Date & Time)
                if (p.Artist == artist.Name && p.Date < DateTime.Now && p.Time > DateTime.Now.TimeOfDay.TotalHours)
                    commander.DeletePerformance(p.Performance);
            }
            commander.DeleteArtist(artist.Artist);
            LoadArtists();
        }

        #endregion

        public ICommand CancelEditArtistCommand
        {
            get
            {
                if (cancelEditArtistCommand == null)
                    cancelEditArtistCommand = new RelayCommand(param => LoadArtists());
                return cancelEditArtistCommand;
            }
        }

        // bad work-around
        public ICommand CloseEditFlyoutCommand
        {
            get
            {
                if (closeEditFlyoutCommand == null)
                    closeEditFlyoutCommand = new RelayCommand(param =>
                    {
                        ToggleFlyout(0);
                        LoadArtists();
                    });
                return closeEditFlyoutCommand;
            }
        }

        #endregion

        #region Venue Commands / Methods

        #region Insert Venue

        public ICommand OpenCreatevenueCommand
        {
            get
            {
                return openCreateVenueCommand ?? (openCreateVenueCommand = new RelayCommand(param => OpenCreateVenue()));
            }
        }

        private void OpenCreateVenue()
        {
            createVenueWindow = new InsertVenueWindow(this);
            createVenueWindow.Owner = mainWindow;
            createVenueWindow.ShowDialog();
        }

        public ICommand CreateVenueCommand
        {
            get
            {
                if (createVenueCommand == null)
                    createVenueCommand = new RelayCommand(param => CreateVenue((VenueVM)param));
                return createVenueCommand;
            }
        }

        private void CreateVenue(VenueVM venue)
        {
            commander.InsertVenue(venue.Venue);
            // to update view
            LoadVenues();
            newVenue = null;
            createVenueWindow.Close();
        }

        #endregion

        #region Update Venue

        public ICommand UpdateVenueCommand
        {
            get
            {
                if (updateVenueCommand == null)
                    updateVenueCommand = new RelayCommand(param => UpdateVenue((VenueVM)param));
                return updateVenueCommand;
            }
        }

        private void UpdateVenue(VenueVM venue)
        {
            commander.UpdateVenue(venue.Venue);
            LoadVenues();
        }

        #endregion

        #region Delete Venue

        public ICommand DeleteVenueCommand
        {
            get
            {
                if (deleteVenueCommand == null)
                    deleteVenueCommand = new RelayCommand(param => DeleteVenue((VenueVM)param));
                return deleteVenueCommand;
            }
        }

        public void DeleteVenue(VenueVM venue)
        {
            commander.DeleteVenue(venue.Venue);
            LoadVenues();
        }

        #endregion

        public ICommand CancelEditVenueCommand
        {
            get
            {
                if (cancelEditVenueCommand == null)
                    cancelEditVenueCommand = new RelayCommand(param => LoadVenues());
                return cancelEditVenueCommand;
            }
        }

        // bad work-around
        public ICommand CloseEditFlyoutCommand2
        {
            get
            {
                if (closeEditFlyoutCommand2 == null)
                    closeEditFlyoutCommand2 = new RelayCommand(param =>
                    {
                        ToggleFlyout(1);
                        LoadVenues();
                    });
                return closeEditFlyoutCommand2;
            }
        }

        #endregion

        public ICommand SelectedDateChangedCommand
        {
            get
            {
                return selectedDateChangeCommand ?? (selectedDateChangeCommand = new RelayCommand(param => LoadPerformancesOfDay((DateTime)param)));
            }
        }

        public ICommand CloseApplicationCommand
        {
            get
            {
                if (closeApplicationCommand == null)
                    closeApplicationCommand = new RelayCommand(param => CloseApplication());
                return closeApplicationCommand;
            }
        }

        public ICommand SendEmailWithProgramCommand
        {
            get
            {
                if (sendEmailWithProgramCommand == null)
                    sendEmailWithProgramCommand = new RelayCommand(param =>
                    {
                        try
                        {
                            System.Net.NetworkCredential auth = new System.Net.NetworkCredential("donoutreply@gmail.com", "noreply#123");
                            SmtpClient client = new SmtpClient("smtp.gmail.com", 587);

                            client.EnableSsl = true;
                            client.UseDefaultCredentials = false;
                            client.Credentials = auth;

                            MailMessage mail = new MailMessage();
                            mail.IsBodyHtml = false;

                            //Put the email address
                            mail.From = new MailAddress("donoutreply@gmail.com");
                            mail.Subject = "Ultimate Festival Organizer - Your incoming performances";

                            StringBuilder sbBody = new StringBuilder();

                            //sbBody.AppendLine($"Hi {artist.Name},");
                            sbBody.AppendLine("Hi phamouz,");

                            sbBody.AppendLine("here are your incoming performances:");
                            sbBody.AppendLine();

                            // get all artists

                            // get all performances, iterate through each of them, check if performance.date is < today (current date)
                            // if they are append them to body with Date, Venue, Time

                            //foreach(var p in ufoVM.Performances)
                            //{
                            //    if (p.Artist == a.Name && p.Date < DateTime.Now)
                            //        sbBody.AppendLine($"# {p.Date}: {p.Venue} - {p.Time}");
                            //}

                            // get list of email adresses of artists,
                            // iterate through it an send each of them a personal EMail, but I didnt implement artists with EMail
                            mail.To.Add("neoclon007@yahoo.de");


                            sbBody.AppendLine("# 2016-26-01: Hauptplatz 17:00");

                            sbBody.AppendLine();
                            sbBody.AppendLine("Goodluck and have fun!");
                            sbBody.AppendLine("Regards, UFO-Team!");
                            sbBody.AppendLine();
                            sbBody.AppendLine(DateTime.Now.ToString());

                            mail.Body = sbBody.ToString();

                            ////Your log file path
                            //System.Net.Mail.Attachment attachment = new System.Net.Mail.Attachment(@"yourpath");

                            //mail.Attachments.Add(attachment);

                            client.Send(mail);
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.ToString());
                        }
                    });
                return sendEmailWithProgramCommand;
            }
        }

        private async void CloseApplication()
        {
            var mySettings = new MetroDialogSettings()
            {
                AffirmativeButtonText = "Quit",
                NegativeButtonText = "Cancel",
                AnimateShow = true,
                AnimateHide = false
            };

            var result = await mainWindow.ShowMessageAsync("Quit application?",
                "Sure you want to quit application?",
                MessageDialogStyle.AffirmativeAndNegative, mySettings);

            if (result == MessageDialogResult.Affirmative)
                Application.Current.Shutdown();
        }
    }
}
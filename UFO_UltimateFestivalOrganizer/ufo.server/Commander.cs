using System;
using System.Collections.Generic;
using swk5.ufo.dal;

namespace swk5.ufo.server
{
    public class Commander : ICommanderBL
    {
        private IDatabase database;
        private IArtistDao artistDao;
        private ICategoryDao categoryDao;
        private ICountryDao countryDao;
        private IPerformanceDao performanceDao;
        private IUserDao userDao;
        private IVenueDao venueDao;

        public Commander(IDatabase database)
        {
            this.database = database;
            this.artistDao = DALFactory.CreateArtistDao(database);
            this.categoryDao = DALFactory.CreateCategoryDao(database);
            this.countryDao = DALFactory.CreateCountryDao(database);
            this.performanceDao = DALFactory.CreatePerformanceDao(database);
            this.userDao = DALFactory.CreateUserDao(database);
            this.venueDao = DALFactory.CreateVenueDao(database);
        }

        #region Get Tables

        public ICollection<Artist> GetArtists()
        {
            return artistDao.GetAll();
        }

        public ICollection<Category> GetCategories()
        {
            return categoryDao.GetAll();
        }

        public ICollection<Country> GetCountries()
        {
            return countryDao.GetAll();
        }

        public ICollection<Performance> GetPerformances()
        {
            return performanceDao.GetAll();
        }

        public ICollection<Performance> GetPerformancesByDate(DateTime date)
        {
            return performanceDao.GetAllByDate(date);
        }

        public ICollection<Performance> GetPerformancesByDate_Time(DateTime date, int time)
        {
            return performanceDao.GetAllByDate_Time(date, time);
        }

        public ICollection<User> GetUsers()
        {
            return userDao.GetAll();
        }

        public ICollection<Venue> GetVenues()
        {
            return venueDao.GetAll();
        }

        #endregion

        #region other Get-Methods

        public Artist GetArtistByName(string name)
        {
            return artistDao.GetByName(name);
        }

        public Category GetCategoryByName(string name)
        {
            return categoryDao.GetByName(name);
        }

        public Country GetCountryByCode(string code)
        {
            return countryDao.GetByCode(code);
        }

        public Country GetCountryByName(string name)
        {
            return countryDao.GetByName(name);
        }

        public Performance GetPerformanceByDate_Time_Venue(DateTime date, int time, string venue)
        {
            return performanceDao.GetByDate_Time_Venue(date, time, venue);
        }

        public User GetUserByEMail(string email)
        {
            return userDao.GetByEMail(email);
        }

        public Venue GetVenueByShortName(string shortname)
        {
            return venueDao.GetByName(shortname);
        }

        public Venue GetVenueByDescription(string descr)
        {
            return venueDao.GetByDescription(descr);
        }

        #endregion

        public bool AuthenticateUser(string email, string password)
        {
            IList<User> userList = userDao.GetAll();
            foreach (User u in userList)
            {
                if ((email == u.EMail) && (password == u.PasswordHash))
                    return true;
            }
            return false;
        }

        private bool PerformanceIsPossible(Performance performance)
        {
            foreach (Performance p in GetPerformancesByDate(performance.Date))
            {
                // at least 1 hour break
                if (p.Time == performance.Time - 1 || p.Time == performance.Time + 1)
                    if (p.Artist == performance.Artist)
                        return false;

                // can't have 2 performances simultaneously
                if (p.Time == performance.Time && p.Artist == performance.Artist)
                    return false;
            }
            return true;
        }

        #region Transactions

        public bool DeleteArtist(Artist artist)
        {
            return artistDao.Delete(artist.Name);
        }

        public bool DeletePerformance(Performance performance)
        {
            return performanceDao.Delete(performance.Date,performance.Time, performance.Venue);
        }

        public bool DeleteVenue(Venue venue)
        {
            return venueDao.Delete(venue.ShortName);
        }

        public bool InsertArtist(Artist artist)
        {
            return artistDao.Insert(artist);
        }

        public bool InsertPerformance(Performance performance)
        {
            if (PerformanceIsPossible(performance))
                return performanceDao.Insert(performance);
            return false;
        }

        public bool InsertVenue(Venue venue)
        {
            return venueDao.Insert(venue);
        }

        public bool UpdateArtist(Artist artist)
        {
            return artistDao.Update(artist);
        }

        public bool UpdatePerformance(Performance performance)
        {
            if (PerformanceIsPossible(performance))
                return performanceDao.Update(performance);
            return false;
        }

        public bool UpdateVenue(Venue venue)
        {
            return venueDao.Update(venue);
        }

        #endregion
    }
}
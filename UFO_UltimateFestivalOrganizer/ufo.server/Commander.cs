using System;
using System.Collections.Generic;
using swk5.ufo.dal;
using System.Security.Cryptography;
using System.Text;

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
        private static User authenticatedUser = new User();

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

        public string generateRealPwd(string email, string pwd)
        {
            using (SHA1Managed sha1 = new SHA1Managed())
            {
                string input = email + "|" + pwd;
                var hash = sha1.ComputeHash(Encoding.UTF8.GetBytes(input));
                var sb = new StringBuilder(hash.Length * 2);

                foreach (byte b in hash)
                {
                    sb.Append(b.ToString("x2"));
                }

                return sb.ToString();
            }
        }

        public bool AuthenticateUser(string email, string password)
        {
            string realpwd = generateRealPwd(email, password);
            IList<User> userList = userDao.GetAll();
            foreach (User u in userList)
            {
                if ((email == u.EMail) && (realpwd == u.PasswordHash))
                    return true;
            }
            return false;
        }

        public bool PerformanceIsPossible(Performance performance)
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
            return performanceDao.Delete(performance.Date, performance.Time, performance.Venue);
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

        public bool IsAuthenticated(User user)
        {
            return authenticatedUser != null;
        }

        public ICollection<Performance> FilterPerformances(Artist artist, Venue venue, Category category, int from, int to)
        {
            ICollection<Performance> performances = performanceDao.GetAll();
            ICollection<Artist> artists = artistDao.GetAll();
            HashSet<Performance> pList1 = null;
            HashSet<Performance> pList2 = null;
            HashSet<Performance> pList3 = null;
            HashSet<Performance> pList4 = null;

            // get performances containing artist
            if (artist != null)
            {
                foreach (Performance p in performances)
                {
                    if (p.Artist == artist.Name)
                        pList1.Add(p);
                }
            }

            // get performances at venue
            if (venue != null)
            {
                foreach (Performance p in performances)
                {
                    if (p.Venue == venue.ShortName)
                        pList2.Add(p);
                }
            }

            // get performances containing artists of category
            if (category != null)
            {
                foreach (Artist a in artists)
                {
                    if (a.CategoryName == category.CategoryName)
                    {
                        foreach (Performance p in performances)
                        {
                            if (p.Artist == a.Name)
                                pList3.Add(p);
                        }
                    }
                }
            }

            // get performances from starting with 1400, to approx. 2400
            if ((from >= 14 && from <= 23) && (to <= 24 && to >= 15))
            {
                foreach (Performance p in performances)
                {
                    if (p.Time >= from && p.Time <= to)
                        pList4.Add(p);
                }
            }
            else if (from >= 14 && from <= 23) // only from
            {
                {
                    foreach (Performance p in performances)
                    {
                        if (p.Time >= from)
                            pList4.Add(p);
                    }
                }
            }
            else if (to <= 24 && to >= 15) // only to
            {
                foreach (Performance p in performances)
                {
                    if (p.Time <= to)
                        pList4.Add(p);
                }
            }

            // create result out of partial results
            HashSet<Performance> result = new HashSet<Performance>();
            if (pList1 != null)
                foreach (Performance p in pList1)
                    result.Add(p);
            if (pList2 != null)
                foreach (Performance p in pList2)
                    result.Add(p);
            if (pList3 != null)
                foreach (Performance p in pList3)
                    result.Add(p);
            if (pList4 != null)
                foreach (Performance p in pList4)
                    result.Add(p);

            return new List<Performance>(result);
        }

        public User GetAuthenticatedUser()
        {
            return authenticatedUser;
        }

        public void Login(User user)
        {
            authenticatedUser = user;
        }

        public void Logout(User user)
        {
            authenticatedUser = null;
        }
    }
}
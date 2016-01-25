using swk5.ufo.dal;
using swk5.ufo.server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;

namespace ufo.webservice
{
    /// <summary>
    /// ufo.webservice, which will provide the business logic needed for ufo.web
    /// </summary>
    [WebService(Namespace = "http://ufo.fh-hagenberg.at")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]

    //[System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]

    public class UfoWebService : System.Web.Services.WebService
    {
        private ICommanderBL commander = BLFactory.GetCommander();

        #region Non-Getter

        [WebMethod]
        public bool AuthenticateUser(string email, string password)
        {
            return commander.AuthenticateUser(email, password);
        }

        [WebMethod]
        public bool PerformanceIsPossible(Performance performance)
        {
            return commander.PerformanceIsPossible(performance);
        }

        #endregion

        #region Get Tables

        [WebMethod]
        public List<User> GetUsers()
        {
            return commander.GetUsers().ToList();
        }

        [WebMethod]
        public List<Artist> GetArtists()
        {
            return commander.GetArtists().ToList();
        }

        [WebMethod]
        public List<Category> GetCategories()
        {
            return commander.GetCategories().ToList();
        }

        [WebMethod]
        public List<Country> GetCountries()
        {
            return commander.GetCountries().ToList();
        }

        [WebMethod]
        public List<Performance> GetPerformances()
        {
            return commander.GetPerformances().ToList();
        }

        [WebMethod]
        public List<Performance> GetPerformancesByDate(DateTime date)
        {
            return commander.GetPerformancesByDate(date).ToList();
        }

        [WebMethod]
        public List<Performance> GetPerformancesByDate_Time(DateTime date, int time)
        {
            return commander.GetPerformancesByDate_Time(date, time).ToList();
        }

        [WebMethod]
        public List<Venue> GetVenues()
        {
            return commander.GetVenues().ToList();
        }

        #endregion

        #region Other Get-Methods

        [WebMethod]
        public Artist GetArtistByName(string name)
        {
            return commander.GetArtistByName(name);
        }

        [WebMethod]
        public Category GetCategoryByName(string name)
        {
            return commander.GetCategoryByName(name);
        }

        [WebMethod]
        public Country GetCountryByCode(string code)
        {
            return commander.GetCountryByCode(code);
        }

        [WebMethod]
        public Country GetCountryByName(string name)
        {
            return commander.GetCountryByName(name);
        }

        [WebMethod]
        public Performance GetPerformanceByDate_Time_Venue(DateTime date, int time, string venue)
        {
            return commander.GetPerformanceByDate_Time_Venue(date, time, venue);
        }

        [WebMethod]
        public User GetUserByEMail(string email)
        {
            return commander.GetUserByEMail(email);
        }

        [WebMethod]
        public Venue GetVenueByShortName(string shortname)
        {
            return commander.GetVenueByShortName(shortname);
        }

        [WebMethod]
        public Venue GetVenueByDescription(string descr)
        {
            return commander.GetVenueByDescription(descr);
        }

        #endregion

        #region Transactions

        [WebMethod]
        public bool DeleteArtist(Artist artist)
        {
            return commander.DeleteArtist(artist);
        }

        [WebMethod]
        public bool DeletePerformance(Performance performance)
        {
            return commander.DeletePerformance(performance);
        }

        [WebMethod]
        public bool DeleteVenue(Venue venue)
        {
            return commander.DeleteVenue(venue);
        }

        [WebMethod]
        public bool InsertArtist(Artist artist)
        {
            return commander.InsertArtist(artist);
        }

        [WebMethod]
        public bool InsertPerformance(Performance performance)
        {
            return commander.InsertPerformance(performance);
        }

        [WebMethod]
        public bool InsertVenue(Venue venue)
        {
            return commander.InsertVenue(venue);
        }

        [WebMethod]
        public bool UpdateArtist(Artist artist)
        {
            return commander.UpdateArtist(artist);
        }

        [WebMethod]
        public bool UpdatePerformance(Performance performance)
        {
            return commander.UpdatePerformance(performance);
        }

        [WebMethod]
        public bool UpdateVenue(Venue venue)
        {
            return commander.UpdateVenue(venue);
        }

        #endregion

    }
}

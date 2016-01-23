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
    /// ufo.webservice, which will provide functions to interact with the functionality needed for ufo.web (JAVA)
    /// </summary>
    [WebService(Namespace = "http://ufo.fh-hagenberg.at")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]

    //[System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]

    public class UfoWebService : System.Web.Services.WebService
    {
        private ICommanderBL commander = BLFactory.GetCommander();

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





    }
}

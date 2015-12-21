using swk5.ufo.dal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace swk5.ufo.server
{
    public interface ICommanderBL
    {
        bool AuthenticateUser(string email, string password);

        #region Get Tables
        ICollection<Artist> GetArtists();
        ICollection<Category> GetCategories();
        ICollection<Country> GetCountries();
        ICollection<Performance> GetPerformances();
        ICollection<Performance> GetPerformancesByDate(DateTime date);
        ICollection<User> GetUsers();
        ICollection<Venue> GetVenues();
        #endregion

        #region Other Get-Methods

        Artist GetArtistByName(string name);
        Category GetCategoryByName(string name);
        Country GetCountryByCode(string code);
        Country GetCountryByName(string name);
        Performance GetPerformanceByDate_Time_Venue(DateTime date, int time, string venue);
        User GetUserByEMail(string email);
        Venue GetVenueByName(string name);
        Venue GetVenueByDescription(string descr);
        
        #endregion

        #region Transactions
        bool InsertArtist(Artist artist);
        bool UpdateArtist(Artist artist);
        bool DeleteArtist(Artist artist);
        bool InsertPerformance(Performance performance);
        bool UpdatePerformance(Performance performance);
        bool DeletePerformance(Performance performance);
        bool InsertVenue(Venue venue);
        bool UpdateVenue(Venue venue);
        bool DeleteVenue(Venue venue);
        #endregion
    }
}
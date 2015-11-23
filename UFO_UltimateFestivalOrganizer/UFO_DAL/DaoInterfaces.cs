using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// of course i could have used a generic interface for all Daos (would have to cast them all)
// but I chose not to, because of simplicity, altough its almost "copy paste"

namespace swk5.UFO.DAL
{
    public interface IUserDao
    {
        User GetByName(string name);
        IList<User> GetAll();
        bool Update(User user); // returns true if updated, false if not --> used instead of exception
        bool Insert(User user);
        bool Delete(string username);
    } // IUserDao

    public interface IArtistDao
    {
        Artist GetByName(string name);
        IList<Artist> GetAll();
        bool Update(Artist artist); // returns true if updated, false if not --> used instead of exception
        bool Insert(Artist artist);
        bool Delete(string artistname);
    } // IArtistDao

    public interface ICountryDao
    {
        // Get Country by Code or Name
        Country GetByCode(string code);
        Country GetByName(string name);
        IList<Country> GetAll();
        bool Update(Country country); // returns true if updated, false if not --> used instead of exception
        bool Insert(Country country);
        bool Delete(string countrycode);
    } // ICountryDao

    public interface ICategoryDao
    {
        Category GetByName(string name);
        IList<Category> GetAll();
        bool Update(Category category); // returns true if updated, false if not --> used instead of exception
        bool Insert(Category category);
        bool Delete(string categoryname);
    } // ICategoryDao
    public interface IVenueDao
    {
        // Get Venue by ShortName or Description
        Venue GetByName(string name);
        Venue GetByDescription(string descr);
        IList<Venue> GetAll();
        bool Update(Venue venue); // returns true if updated, false if not --> used instead of exception
        bool Insert(Venue venue);
        bool Delete(string shortname);
    } // IVenueDao

    public interface IPerformanceDao
    {
        // Get Performance by Artist and the Date + Time of it
        Performance GetByArtistNameAndDateTime(string name, DateTime datetime);
        IList<Performance> GetAll();
        bool Update(Performance performance); // returns true if updated, false if not --> used instead of exception
        bool Insert(Performance performance);
        bool Delete(string performance);
    } // IPerformanceDao
}

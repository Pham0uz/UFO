using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// of course i could have used a generic interface for all Daos (would have to cast them all)
// but I chose not to, because of simplicity, altough its almost "copy paste"

namespace swk5.ufo.dal
{
    public interface IUserDao
    {
        User GetByEMail(string email);
        IList<User> GetAll();

        // readonly domain class, managed of admin of db
        //bool Update(User user); // returns true if updated, false if not --> used instead of exception
        //bool Insert(User user);
        //bool Delete(string username);
    } // IUserDao

    public interface IArtistDao
    {
        Artist GetByName(string name);
        IList<Artist> GetAll();
        bool Update(Artist a); // returns true if updated, false if not --> used instead of exception
        bool Insert(Artist a);
        bool Delete(string artistname);
    } // IArtistDao

    public interface ICountryDao
    {
        // Get Country by Code or Name
        Country GetByCode(string code);
        Country GetByName(string name);
        IList<Country> GetAll();

        // not needed, managed by admin of db
        //bool Update(Country country); // returns true if updated, false if not --> used instead of exception
        //bool Insert(Country country);
        //bool Delete(string countrycode);
    } // ICountryDao

    public interface ICategoryDao
    {
        Category GetByName(string name);
        IList<Category> GetAll();

        // not needed, managed by admin of db
        //bool Update(Category category); // returns true if updated, false if not --> used instead of exception
        //bool Insert(Category category);
        //bool Delete(string categoryname);
    } // ICategoryDao
    public interface IVenueDao
    {
        // Get Venue by ShortName or Description
        Venue GetByName(string name);
        Venue GetByDescription(string descr);
        IList<Venue> GetAll();
        bool Update(Venue v); // returns true if updated, false if not --> used instead of exception
        bool Insert(Venue v);
        bool Delete(string shortname);
    } // IVenueDao

    public interface IPerformanceDao
    {
        // Get Performance by Artist and the Date + Time of it
        Performance GetByDateNTimeAndVenue(DateTime datetime, string venue);
        IList<Performance> GetAll();
        bool Update(Performance p); // returns true if updated, false if not --> used instead of exception
        bool Insert(Performance p);
        bool Delete(DateTime datetime, string venue);
    } // IPerformanceDao
}

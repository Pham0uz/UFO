using swk5.ufo.dal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace swk5.coco.server
{
    public interface ICommanderBL
    {
        bool AuthenticateUser(string email, string password);

        #region Get Tables
        ICollection<Artist> GetArtist();
        ICollection<Category> GetCategories();
        ICollection<User> GetCountries();
        ICollection<Performance> GetPerformances();
        ICollection<User> GetUsers();
        ICollection<Venue> GetVenues();
        #endregion

        #region Other Get-Methods
        User GetUserByEMail(string email);
        Artist GetArtistByName(string name);
        #endregion

        #region Transactions
        bool InsertArtist(Artist artist);
        #endregion
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace swk5.UFO.DAL
{
    /// <summary>
    /// Domainclass for User
    /// who has administrative rights
    /// (insert, edit, etc.)
    /// </summary>
    [Serializable]
    public class User
    {
        public string UserName { get; set; }

        public string PasswordHash { get; set; }

        public string EMail { get; set; }

        public User()
        {
        }

        /// <summary>
        /// Constructor of User
        /// </summary>
        /// unique primary key
        /// <param name="username"></param>
        /// <param name="email"></param>
        /// username and passowrd hashed with salt
        /// <param name="passwordhash"></param>
        public User(string username, string passwordhash, string email)
        {
            this.UserName = username;
            this.PasswordHash = passwordhash;
            this.EMail = email;
        }



        public override string ToString()
        {
            return $"User: {UserName,20} | Password: {PasswordHash,30} | E-Mail: {EMail,20}";
        }
    } // User

    /// <summary>
    /// Domainclass for Artist
    /// who are part of performances
    /// </summary>
    [Serializable]
    public class Artist
    { 
        public string Name { get; set; }

        public string PictureURL { get; set; }

        public string PromoVideoURL { get; set; }

        public Category Category { get; set; }

        public Country Country { get; set; }

        /// <summary>
        /// Constructor of Artist
        /// </summary>
        /// auto incremented int id
        /// <param name="id"></param>
        /// name of artist/group
        /// <param name="name"></param>
        /// picture url to load picture from
        /// <param name="picurl"></param>
        /// promo video url to load video from
        /// <param name="vidurl"></param>
        /// category of the artist
        /// <param name="cat"></param>
        /// country of the artist
        /// <param name="country"></param>
        public Artist(string name, string picurl, string vidurl, Category cat, Country country)
        {
            this.Name = name;
            this.PictureURL = picurl;
            this.PromoVideoURL = vidurl;
            this.Category = cat;
            this.Country = country;
        }

    } // Artist

    /// <summary>
    /// Domainclass for Country
    /// includes ISO Country-Code
    /// and Name
    /// </summary>
    [Serializable]
    public class Country
    {
        public string Code { get; set; }

        public string Name { get; set; }

        /// <summary>
        /// Constructor of Country
        /// </summary>
        /// ISO Country Code
        /// <param name="code"></param>
        /// <param name="name"></param>
        public Country(string code, string name)
        {
            this.Code = code;
            this.Name = name;
        }

    } // Country

    /// <summary>
    /// Domainclass for Category
    /// created class for reuse
    /// </summary>
    public class Category
    {
        public string CategoryName { get; set; }

        public Category(string categoryname)
        {
            this.CategoryName = categoryname;
        }
    } // Category

    /// <summary>
    /// Domainclass for Venue
    /// Venues are locations
    /// where the performances take place
    /// </summary>
    [Serializable]
    public class Venue
    {
        public string ShortName { get; set; }

        public string Description { get; set; }

        public double Latitude { get; set; }

        public double Longitude { get; set; }

        /// <summary>
        /// Constructor for Venue
        /// </summary>
        /// <param name="shortname"></param>
        /// "long" name
        /// <param name="descr"></param>
        /// latitude of venue
        /// <param name="latitd"></param>
        /// longitude of venue
        /// <param name="longitd"></param>
        public Venue(string shortname, string descr, double latitd, double longitd)
        {
            this.ShortName = shortname;
            this.Description = descr;
            this.Latitude = latitd;
            this.Longitude = longitd;
        }
    } // Venue

    /// <summary>
    /// Domainclass for Performance
    /// each of them have 1 artist who performs it
    /// and 1 venue where it takes place
    /// </summary>
    [Serializable]
    public class Performance
    {
        public DateTime DateNTime { get; set; }

        public Artist Artist { get; set; }

        public Venue Venue { get; set;} 

        public Performance(DateTime datentime, Artist artist, Venue venue)
        {
            this.DateNTime = datentime;
            this.Artist = artist;
            this.Venue = venue;
        }

    } // Performance
}

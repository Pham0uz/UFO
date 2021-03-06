﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace swk5.ufo.dal
{
    /// <summary>
    /// Domainclass for User
    /// who has administrative rights
    /// (insert, edit, etc.)
    /// </summary>
    [Serializable]
    public class User
    {
        public string EMail { get; set; }

        public string PasswordHash { get; set; }

        /// <summary>
        /// Constructor of User
        /// </summary>
        /// unique primary key
        /// <param name="email"></param>
        /// username and passowrd hashed with salt
        /// <param name="passwordhash"></param>
        public User(string email, string passwordhash)
        {
            this.PasswordHash = passwordhash;
            this.EMail = email;
        }

        public User() { }

        public override string ToString()
        {
            return $"User: E-Mail: {EMail,20} | Password: {PasswordHash,30} | ";
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

        public string CategoryName { get; set; }

        public string CountryCode { get; set; }

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
        /// categoryname of the artist
        /// <param name="cat"></param>
        /// country code of the artist
        /// <param name="country"></param>
        public Artist(string name, string picurl, string vidurl, string cat, string countrycd)
        {
            this.Name = name;
            this.PictureURL = picurl;
            this.PromoVideoURL = vidurl;
            this.CategoryName = cat;
            this.CountryCode = countrycd;
        }

        public Artist() { }

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

        public Country() { }

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

        public Category() { }
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

        public Venue () { }
    } // Venue

    /// <summary>
    /// Domainclass for Performance
    /// each of them have 1 artist who performs it
    /// and 1 venue where it takes place
    /// </summary>
    [Serializable]
    public class Performance
    {
        public DateTime Date { get; set; }

        public int Time { get; set; }

        public string Artist { get; set; }

        public string Venue { get; set; }

        public Performance(DateTime date, int time, string artist, string venue)
        {
            this.Date = date;
            this.Time = time;
            this.Artist = artist;
            this.Venue = venue;
        }

        public Performance() { }

    } // Performance
}

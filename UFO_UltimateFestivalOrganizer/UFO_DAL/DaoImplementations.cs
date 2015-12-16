using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace swk5.ufo.dal
{
    class UserDao : IUserDao
    {
        const string SQL_GET_ALL = "select * from User";
        const string SQL_GET_BY_NAME = "select * from User where Username=@Username";

        private IDatabase database;

        public UserDao(IDatabase database)
        {
            this.database = database;
        }

        private DbCommand CreateGetAllCmd()
        {
            return database.CreateCommand(SQL_GET_ALL);
        }

        private DbCommand CreateGetByNameCmd(string name)
        {
            var cmd = database.CreateCommand(SQL_GET_BY_NAME);
            database.DefineParameter(cmd, "@Username", DbType.String, name);
            return cmd;
        }


        public IList<User> GetAll()
        {
            var userList = new List<User>();
            DbCommand cmd = CreateGetAllCmd();
            using (IDataReader reader = database.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
                    userList.Add(new User((string)reader["Username"],
                                          (string)reader["PasswordHash"],
                                          (string)reader["Email"]));
                }
            } // autodisposable interface | no close of reader needed
            return userList;
        }

        public User GetByName(string name)
        {
            DbCommand cmd = CreateGetByNameCmd(name);
            using (IDataReader reader = database.ExecuteReader(cmd))
            {
                if (!reader.Read()) // return null if doesnt find anything
                    return null;
                return new User((string)reader["Username"],
                                (string)reader["PasswordHash"],
                                (string)reader["Email"]);
            }
        }

        public bool Update(User user)
        {
            // not needed atm
            throw new NotImplementedException();
        }

        public bool Insert(User user)
        {
            throw new NotImplementedException();
        }

        public bool Delete(string username)
        {
            throw new NotImplementedException();
        }
    }

    class ArtistDao : IArtistDao
    {
        const string SQL_GET_ALL = "select * from Artist";
        const string SQL_GET_BY_NAME = "select * from Artist where Name=@Name";
        const string SQL_GET_COUNTRY_BY_CODE = "select * from Country where Code=@Code";
        const string SQL_GET_CATEGORY_BY_NAME = "select * from Category where CategoryName=@CategoryName";

        private IDatabase database;

        public ArtistDao(IDatabase database)
        {
            this.database = database;
        }

        private DbCommand CreateGetAllCmd()
        {
            return database.CreateCommand(SQL_GET_ALL);
        }

        private DbCommand CreateGetByNameCmd(string name)
        {
            var cmd = database.CreateCommand(SQL_GET_BY_NAME);
            database.DefineParameter(cmd, "@Name", DbType.String, name);
            return cmd;
        }

        private DbCommand CreateGetCountryByCodeCmd(string code)
        {
            var cmd = database.CreateCommand(SQL_GET_COUNTRY_BY_CODE);
            database.DefineParameter(cmd, "@Code", DbType.String, code);
            return cmd;
        }

        private DbCommand CreateCategoryGetByNameCmd(string name)
        {
            var cmd = database.CreateCommand(SQL_GET_CATEGORY_BY_NAME);
            database.DefineParameter(cmd, "@CategoryName", DbType.String, name);
            return cmd;
        }

        public Country GetCountryByCode(string code)
        {
            DbCommand cmd = CreateGetCountryByCodeCmd(code);
            using (IDataReader reader = database.ExecuteReader(cmd))
            {
                if (!reader.Read())
                    return null;
                return new Country((string)reader["Code"],
                                   (string)reader["Name"]);
            }
        }

        public Category GetCategoryByName(string name)
        {
            DbCommand cmd = CreateCategoryGetByNameCmd(name);
            using (IDataReader reader = database.ExecuteReader(cmd))
            {
                if (!reader.Read())
                    return null;
                return new Category((string)reader["CategoryName"]);
            }
        }

        public IList<Artist> GetAll()
        {
            var artistList = new List<Artist>();
            DbCommand cmd = CreateGetAllCmd();

            using (IDataReader reader = database.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
                    // get category
                    Category category = GetCategoryByName((string)reader["Category"]);

                    // get country
                    Country country = GetCountryByCode((string)reader["Country"]);

                    // add to artistList
                    artistList.Add(new Artist((string)reader["Name"],
                                              (string)reader["PictureURL"],
                                              (string)reader["PromoVideoURL"],
                                              category,
                                              country
                    ));
                }
            }
            return artistList;
        }

        public Artist GetByName(string name)
        {
            DbCommand cmd = CreateGetByNameCmd(name);
            using (var reader = database.ExecuteReader(cmd))
            {
                if (!reader.Read())
                    return null;

                Category category = null;
                Country country = null;

                if (!Convert.IsDBNull((string)reader["Category"]))
                {
                    category = GetCategoryByName((string)reader["Category"]);
                }

                if (!Convert.IsDBNull((string)reader["Country"]))
                {
                    country = GetCountryByCode((string)reader["Country"]);
                }

                return new Artist((string)reader["Name"],
                                  (string)reader["PictureURL"],
                                  (string)reader["PromoVideoURL"],
                                  category,
                                  country);
            }
        }

        public bool Update(Artist artist)
        {
            // not needed atm
            throw new NotImplementedException();
        }

        public bool Insert(Artist artist)
        {
            throw new NotImplementedException();
        }

        public bool Delete(string artistname)
        {
            throw new NotImplementedException();
        }
    }

    class CountryDao : ICountryDao
    {
        const string SQL_GET_ALL = "select * from Country";
        const string SQL_GET_BY_CODE = "select * from Country where Code=@Code";
        const string SQL_GET_BY_NAME = "select * from Country where Name=@Name";

        private IDatabase database;

        public CountryDao(IDatabase database)
        {
            this.database = database;
        }

        private DbCommand CreateGetAllCmd()
        {
            return database.CreateCommand(SQL_GET_ALL);
        }

        private DbCommand CreateGetByCode(string code)
        {
            var cmd = database.CreateCommand(SQL_GET_BY_CODE);
            database.DefineParameter(cmd, "@Code", DbType.String, code);
            return cmd;
        }

        private DbCommand CreateGetByName(string name)
        {
            var cmd = database.CreateCommand(SQL_GET_BY_NAME);
            database.DefineParameter(cmd, "@Name", DbType.String, name);
            return cmd;
        }

        public IList<Country> GetAll()
        {
            var countryList = new List<Country>();
            DbCommand cmd = CreateGetAllCmd();
            using (IDataReader reader = database.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
                    countryList.Add(new Country((string)reader["Code"],
                                                (string)reader["Name"]));
                }
            }
            return countryList;
        }


        public Country GetByCode(string code)
        {
            DbCommand cmd = CreateGetByCode(code);
            using (IDataReader reader = database.ExecuteReader(cmd))
            {
                if (!reader.Read())
                    return null;
                return new Country((string)reader["Code"],
                                   (string)reader["Name"]);
            }
        }

        public Country GetByName(string name)
        {
            DbCommand cmd = CreateGetByName(name);
            using (IDataReader reader = database.ExecuteReader(cmd))
            {
                if (!reader.Read())
                    return null;
                return new Country((string)reader["Code"],
                                   (string)reader["Name"]);
            }
        }

        public bool Update(Country country)
        {
            // not needed atm
            throw new NotImplementedException();
        }

        public bool Insert(Country country)
        {
            throw new NotImplementedException();
        }

        public bool Delete(string countrycode)
        {
            throw new NotImplementedException();
        }
    }

    class CategoryDao : ICategoryDao
    {
        const string SQL_GET_ALL = "select * from Category";
        const string SQL_GET_BY_NAME = "select * from Category where CategoryName=@Name";

        private IDatabase database;

        public CategoryDao(IDatabase database)
        {
            this.database = database;
        }

        private DbCommand CreateGetAllCmd()
        {
            return database.CreateCommand(SQL_GET_ALL);
        }

        private DbCommand CreateGetByNameCmd(string name)
        {
            var cmd = database.CreateCommand(SQL_GET_BY_NAME);
            database.DefineParameter(cmd, "@Name", DbType.String, name);
            return cmd;
        }

        public IList<Category> GetAll()
        {
            var categoryList = new List<Category>();
            var cmd = CreateGetAllCmd();
            using (var reader = database.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
                    categoryList.Add(new Category((string)reader["CategoryName"]));
                }
            }
            return categoryList;
        }

        public Category GetByName(string name)
        {
            var cmd = CreateGetByNameCmd(name);
            using (var reader = database.ExecuteReader(cmd))
            {
                if (!reader.Read())
                    return null;
                return new Category((string)reader["CategoryName"]);
            }
        }

        public bool Update(Category category)
        {
            // not needed atm
            throw new NotImplementedException();
        }

        public bool Insert(Category category)
        {
            throw new NotImplementedException();
        }

        public bool Delete(string categoryname)
        {
            throw new NotImplementedException();
        }
    }

    class VenueDao : IVenueDao
    {
        const string SQL_GET_ALL = "select * from Venue";
        const string SQL_GET_BY_NAME = "select * from Venue where ShortName =@Name";
        const string SQL_GET_BY_DESCR = "select * from Venue where Description =@Description";


        private IDatabase database;

        public VenueDao(IDatabase database)
        {
            this.database = database;
        }

        private DbCommand CreateGetAllCmd()
        {
            return database.CreateCommand(SQL_GET_ALL);
        }

        private DbCommand CreateGetByNameCmd(string name)
        {
            var cmd = database.CreateCommand(SQL_GET_BY_NAME);
            database.DefineParameter(cmd, "@Name", DbType.String, name);
            return cmd;
        }

        private DbCommand CreateGetByDescrCmd(string descr)
        {
            var cmd = database.CreateCommand(SQL_GET_BY_DESCR);
            database.DefineParameter(cmd, "@Description", DbType.String, descr);
            return cmd;
        }

        public IList<Venue> GetAll()
        {
            var venueList = new List<Venue>();
            var cmd = CreateGetAllCmd();
            using (var reader = database.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
                    venueList.Add(new Venue((string)reader["ShortName"],
                                            (string)reader["Description"],
                                            (double)reader["Latitude"],
                                            (double)reader["Longitude"]));
                }
            }
            return venueList;
        }

        public Venue GetByName(string name)
        {
            var cmd = CreateGetByNameCmd(name);
            using (var reader = database.ExecuteReader(cmd))
            {
                if (!reader.Read())
                    return null;
                return new Venue((string)reader["ShortName"],
                                 (string)reader["Description"],
                                 (double)reader["Latitude"],
                                 (double)reader["Longitude"]);
            }
        }

        public Venue GetByDescription(string descr)
        {
            var cmd = CreateGetByDescrCmd(descr);
            using (var reader = database.ExecuteReader(cmd))
            {
                if (!reader.Read())
                    return null;
                return new Venue((string)reader["ShortName"],
                                 (string)reader["Description"],
                                 (double)reader["Latitude"],
                                 (double)reader["Longitude"]);
            }
        }

        public bool Update(Venue venue)
        {
            // not needed atm
            throw new NotImplementedException();
        }

        public bool Insert(Venue venue)
        {
            throw new NotImplementedException();
        }

        public bool Delete(string shortname)
        {
            throw new NotImplementedException();
        }
    }

    class PerformanceDao : IPerformanceDao
    {
        const string SQL_GET_ALL = "select * from Performance";
        const string SQL_GET_BY_ARTISTNAME_DATENTIME = "select * from Performance where Artist=@ArtistName "
                                                     + "AND DateNTime=@DateTime";
        const string SQL_ARTIST_GET_BY_NAME = "select * from Artist where Name=@ArtistName";
        const string SQL_VENUE_GET_BY_SHORTNAME = "select * from Venue where ShortName=@ShortName";
        const string SQL_GET_COUNTRY_BY_CODE = "select * from Country where Code=@Code";
        const string SQL_GET_CATEGORY_BY_NAME = "select * from Category where CategoryName=@CategoryName";

        private IDatabase database;

        public PerformanceDao(IDatabase database)
        {
            this.database = database;
        }

        private DbCommand CreateGetAllCmd()
        {
            return database.CreateCommand(SQL_GET_ALL);
        }

        private DbCommand CreateGetByArtistNameAndDateTime(string name, DateTime datetime)
        {
            var cmd = database.CreateCommand(SQL_GET_BY_ARTISTNAME_DATENTIME);
            database.DefineParameter(cmd, "@ArtistName", DbType.String, name);
            database.DefineParameter(cmd, "@DateTime", DbType.DateTime, datetime);
            return cmd;
        }

        private DbCommand CreateArtistGetByNameCmd(string name)
        {
            var cmd = database.CreateCommand(SQL_ARTIST_GET_BY_NAME);
            database.DefineParameter(cmd, "@ArtistName", DbType.String, name);
            return cmd;
        }

        private DbCommand CreateGetCountryByCodeCmd(string code)
        {
            var cmd = database.CreateCommand(SQL_GET_COUNTRY_BY_CODE);
            database.DefineParameter(cmd, "@Code", DbType.String, code);
            return cmd;
        }

        private DbCommand CreateCategoryGetByNameCmd(string name)
        {
            var cmd = database.CreateCommand(SQL_GET_CATEGORY_BY_NAME);
            database.DefineParameter(cmd, "@CategoryName", DbType.String, name);
            return cmd;
        }

        private DbCommand CreateVenueGetByNAmeCmd(string name)
        {
            var cmd = database.CreateCommand(SQL_VENUE_GET_BY_SHORTNAME);
            database.DefineParameter(cmd, "@ShortName", DbType.String, name);
            return cmd;
        }

        public Country GetCountryByCode(string code)
        {
            DbCommand cmd = CreateGetCountryByCodeCmd(code);
            using (IDataReader reader = database.ExecuteReader(cmd))
            {
                if (!reader.Read())
                    return null;
                return new Country((string)reader["Code"],
                                   (string)reader["Name"]);
            }
        }

        public Category GetCategoryByName(string name)
        {
            DbCommand cmd = CreateCategoryGetByNameCmd(name);
            using (IDataReader reader = database.ExecuteReader(cmd))
            {
                if (!reader.Read())
                    return null;
                return new Category((string)reader["CategoryName"]);
            }
        }

        public Artist GetArtistByName(string name)
        {
            DbCommand cmd = CreateArtistGetByNameCmd(name);
            using (var reader = database.ExecuteReader(cmd))
            {
                if (!reader.Read())
                    return null;

                Category category = null;
                Country country = null;

                if (!Convert.IsDBNull((string)reader["Category"]))
                {
                    category = GetCategoryByName((string)reader["Category"]);
                }

                if (!Convert.IsDBNull((string)reader["Country"]))
                {
                    country = GetCountryByCode((string)reader["Country"]);
                }

                return new Artist((string)reader["Name"],
                                  (string)reader["PictureURL"],
                                  (string)reader["PromoVideoURL"],
                                  category,
                                  country);
            }
        }

        public Venue GetVenueByShortName(string name)
        {
            var cmd = CreateVenueGetByNAmeCmd(name);
            using (var reader = database.ExecuteReader(cmd))
            {
                if (!reader.Read())
                    return null;
                return new Venue((string)reader["ShortName"],
                                 (string)reader["Description"],
                                 (double)reader["Latitude"],
                                 (double)reader["Longitude"]);
            }
        }

        public IList<Performance> GetAll()
        {
            var performanceList = new List<Performance>();
            var cmd = CreateGetAllCmd();
            using (var reader = database.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
                    Artist artist = null;
                    Venue venue = null;

                    if (!Convert.IsDBNull((string)reader["Artist"]))
                    {
                        artist = GetArtistByName((string)reader["Artist"]);
                    }

                    if (!Convert.IsDBNull((string)reader["Venue"]))
                    {
                        venue = GetVenueByShortName((string)reader["Venue"]);
                    }

                    performanceList.Add(new Performance((DateTime)reader["DateNTime"],
                                                        artist,
                                                        venue));
                }
            }
            return performanceList;
        }

        // rework: get artist and venue extra
        public Performance GetByArtistNameAndDateTime(string name, DateTime datetime)
        {
            var cmd = CreateGetByArtistNameAndDateTime(name, datetime);
            using (var reader = database.ExecuteReader(cmd))
            {
                if (!reader.Read())
                    return null;

                Artist artist = null;
                Venue venue = null;

                if (!Convert.IsDBNull((string)reader["Artist"]))
                {
                    artist = GetArtistByName((string)reader["Artist"]);
                }

                if (!Convert.IsDBNull((string)reader["Venue"]))
                {
                    venue = GetVenueByShortName((string)reader["Venue"]);
                }

                return new Performance((DateTime)reader["DateNTime"],
                                        artist,
                                        venue);
            }
        }

        public bool Update(Performance performance)
        {
            // not needed atm
            throw new NotImplementedException();
        }

        public bool Insert(Performance performance)
        {
            throw new NotImplementedException();
        }

        public bool Delete(string performance)
        {
            throw new NotImplementedException();
        }
    }

}

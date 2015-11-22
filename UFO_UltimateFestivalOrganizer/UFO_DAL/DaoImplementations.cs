using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace swk5.UFO.DAL
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
    }

    class ArtistDao : IArtistDao
    {
        const string SQL_GET_ALL = "select * from Artist";
        const string SQL_GET_BY_ID = "select * from Artist where Id=@Id";
        const string SQL_GET_BY_NAME = "select * from Artist where Name=@Name";

        private IDatabase database;

        public ArtistDao(IDatabase database)
        {
            this.database = database;
        }

        private DbCommand CreateGetAllCmd()
        {
            return database.CreateCommand(SQL_GET_ALL);
        }

        private DbCommand CreateGetByIdCmd(int id)
        {
            var cmd = database.CreateCommand(SQL_GET_BY_ID);
            database.DefineParameter(cmd, "@Id", DbType.Int32, id);
            return cmd;
        }
        private DbCommand CreateGetByNameCmd(string name)
        {
            var cmd = database.CreateCommand(SQL_GET_BY_NAME);
            database.DefineParameter(cmd, "@Name", DbType.String, name);
            return cmd;
        }

        public IList<Artist> GetAll()
        {
            var artistList = new List<Artist>();
            DbCommand cmd = CreateGetAllCmd();
            using (IDataReader reader = database.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
                    artistList.Add(new Artist((int)reader["Id"],
                                              (string)reader["Name"],
                                              (string)reader["PictureURL"],
                                              (string)reader["PromoVideoURL"],
                                              (Category)reader["Category"],
                                              (Country)reader["Country"]));
                }
            }
            return artistList;
        }

        public Artist GetById(int id)
        {
            throw new NotImplementedException();
        }

        public Artist GetByName(string name)
        {
            throw new NotImplementedException();
        }

        public bool Update(Artist artist)
        {
            // not needed atm
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


    }

    class PerformanceDao : IPerformanceDao
    {
        const string SQL_GET_ALL = "select * from Performance";
        const string SQL_GET_BY_ARTISTNAME_DATENTIME = "select * from Performance where Artist=@ArtistId"
                                                     + "AND DateNTime=@DateTime";

        private IDatabase database;

        public PerformanceDao(IDatabase database)
        {
            this.database = database;
        }

        private DbCommand CreateGetAllCmd()
        {
            return database.CreateCommand(SQL_GET_ALL);
        }

        private DbCommand CreateGetByArtistNameAndDateTime(int id, DateTime datetime)
        {
            var cmd = database.CreateCommand(SQL_GET_BY_ARTISTNAME_DATENTIME);
            database.DefineParameter(cmd, "@ArtistId", DbType.Int32, id);
            database.DefineParameter(cmd, "@DateTime", DbType.DateTime, datetime);
            return cmd;
        }

        public IList<Performance> GetAll()
        {
            var performanceList = new List<Performance>();
            var cmd = CreateGetAllCmd();
            using (var reader = database.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
                    performanceList.Add(new Performance((DateTime)reader["DateNTime"],
                                                        (Artist)reader["Artist"],
                                                        (Venue)reader["Venue"]));
                }
            }
            return performanceList;
        }

        // rework: get artist and venue extra
        public Performance GetByArtistNameAndDateTime(int id, DateTime datetime)
        {
            var cmd = CreateGetByArtistNameAndDateTime(id, datetime);
            using(var reader = database.ExecuteReader(cmd))
            {
                if (!reader.Read())
                    return null;
                return new Performance((DateTime)reader["DateTime"],
                                       (Artist)reader["Artist"],
                                       (Venue)reader["Venue"]);
            }
        }

        public bool Update(Performance performance)
        {
            // not needed atm
            throw new NotImplementedException();
        }
    }

}

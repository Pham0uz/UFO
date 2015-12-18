using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;

namespace swk5.ufo.dal
{
    class ArtistDao : IArtistDao
    {
        const string SQL_GET_ALL = "select * from Artist";
        const string SQL_GET_BY_NAME = "select * from Artist where Name=@Name";

        const string SQL_INSERT =
            "insert into Artist (Name, PictureURL, PromoVideoURL, Category, Country) VALUES (@Name, @PicUrl, @PromoVidUrl, @Cat, @Country)";
        const string SQL_UPDATE =
            "update Artist SET PictureURL=@PicUrl, PromoVideoURL=@PromoVidUrl, Category=@Cat, Country=@Country WHERE Name=@Name";
        const string SQL_DELETE = "delete from Artist WHERE Name=@Name";

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

        private DbCommand CreateInsertCommand(string name, string picurl, string promovidurl, string category, string country)
        {
            var cmd = database.CreateCommand(SQL_INSERT);
            database.DefineParameter(cmd, "@Name", DbType.String, name);
            database.DefineParameter(cmd, "@PicUrl", DbType.String, picurl);
            database.DefineParameter(cmd, "@PromoVidUrl", DbType.String, promovidurl);
            database.DefineParameter(cmd, "@Cat", DbType.String, category);
            database.DefineParameter(cmd, "@Country", DbType.String, country);
            return cmd;
        }

        private DbCommand CreateUpdateCommand(string name, string picurl, string promovidurl, string category, string country)
        {
            var cmd = database.CreateCommand(SQL_UPDATE);
            database.DefineParameter(cmd, "@Name", DbType.String, name);
            database.DefineParameter(cmd, "@PicUrl", DbType.String, picurl);
            database.DefineParameter(cmd, "@PromoVidUrl", DbType.String, promovidurl);
            database.DefineParameter(cmd, "@Cat", DbType.String, category);
            database.DefineParameter(cmd, "@Country", DbType.String, country);
            return cmd;
        }

        private DbCommand CreateDeleteCommand(string name)
        {
            var cmd = database.CreateCommand(SQL_DELETE);
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
                    // add to artistList
                    artistList.Add(new Artist((string)reader["Name"],
                                              (string)reader["PictureURL"],
                                              (string)reader["PromoVideoURL"],
                                              (string)reader["Category"],
                                              (string)reader["Country"]
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

                return new Artist((string)reader["Name"],
                                  (string)reader["PictureURL"],
                                  (string)reader["PromoVideoURL"],
                                  (string)reader["Category"],
                                  (string)reader["Country"]);
            }
        }

        public bool Update(Artist a)
        {
            return database.ExecuteNonQuery(CreateUpdateCommand(a.Name, a.PictureURL, a.PromoVideoURL, a.CategoryName, a.CountryCode)) == 1;
        }

        public bool Insert(Artist a)
        {
            return database.ExecuteNonQuery(CreateInsertCommand(a.Name, a.PictureURL, a.PromoVideoURL, a.CategoryName, a.CountryCode)) == 1;
        }

        public bool Delete(string artistname)
        {
            return database.ExecuteNonQuery(CreateDeleteCommand(artistname)) == 1;
        }
    }
}

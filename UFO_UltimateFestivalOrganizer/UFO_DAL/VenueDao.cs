using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;

namespace swk5.ufo.dal
{
    class VenueDao : IVenueDao
    {
        const string SQL_GET_ALL = "select * from Venue";
        const string SQL_GET_BY_NAME = "select * from Venue where ShortName=@Name";
        const string SQL_GET_BY_DESCR = "select * from Venue where Description =@Description";

        const string SQL_INSERT =
            "insert into Venue (ShortName, Description, Latitude, Longitude) VALUES (@ShortName, @Descr, @Lat, @Long)";
        const string SQL_UPDATE =
            "update Venue SET Description=@Descr, Latitude=@Lat, Longitude=@Long WHERE ShortName=@ShortName";
        const string SQL_DELETE = "delete from Venue WHERE ShortName=@ShortName";


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

        private DbCommand CreateInsertCommand(string shortname, string descr, double latitude, double longitude)
        {
            var cmd = database.CreateCommand(SQL_INSERT);
            database.DefineParameter(cmd, "@ShortName", DbType.String, shortname);
            database.DefineParameter(cmd, "@Descr", DbType.String, descr);
            database.DefineParameter(cmd, "@Lat", DbType.Double, latitude);
            database.DefineParameter(cmd, "@Long", DbType.Double, longitude);
            return cmd;
        }

        private DbCommand CreateUpdateCommand(string shortname, string descr, double latitude, double longitude)
        {
            var cmd = database.CreateCommand(SQL_UPDATE);
            database.DefineParameter(cmd, "@ShortName", DbType.String, shortname);
            database.DefineParameter(cmd, "@Descr", DbType.String, descr);
            database.DefineParameter(cmd, "@Lat", DbType.Double, latitude);
            database.DefineParameter(cmd, "@Long", DbType.Double, longitude);
            return cmd;
        }

        private DbCommand CreateDeleteCommand(string shortname)
        {
            var cmd = database.CreateCommand(SQL_DELETE);
            database.DefineParameter(cmd, "@ShortName", DbType.String, shortname);
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

        public bool Update(Venue v)
        {
            return database.ExecuteNonQuery(CreateUpdateCommand(v.ShortName, v.Description, v.Latitude, v.Longitude)) == 1;
        }

        public bool Insert(Venue v)
        {
            return database.ExecuteNonQuery(CreateInsertCommand(v.ShortName, v.Description, v.Latitude, v.Longitude)) == 1;
        }

        public bool Delete(string shortname)
        {
            return database.ExecuteNonQuery(CreateDeleteCommand(shortname)) == 1;
        }
    }
}

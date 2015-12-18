using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;

namespace swk5.ufo.dal
{
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
    }
}

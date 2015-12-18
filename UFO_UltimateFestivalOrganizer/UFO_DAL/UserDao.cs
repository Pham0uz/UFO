using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;

namespace swk5.ufo.dal
{
    class UserDao : IUserDao
    {
        const string SQL_GET_ALL = "select * from User";
        const string SQL_GET_BY_EMAIL = "select * from User where EMail=@EMail";

        private IDatabase database;

        public UserDao(IDatabase database)
        {
            this.database = database;
        }

        private DbCommand CreateGetAllCmd()
        {
            return database.CreateCommand(SQL_GET_ALL);
        }

        private DbCommand CreateGetByEMailCmd(string email)
        {
            var cmd = database.CreateCommand(SQL_GET_BY_EMAIL);
            database.DefineParameter(cmd, "@EMail", DbType.String, email);
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
                    userList.Add(new User((string)reader["Email"],
                                          (string)reader["PasswordHash"]));
                }
            } // autodisposable interface | no close of reader needed
            return userList;
        }

        public User GetByEMail(string email)
        {
            DbCommand cmd = CreateGetByEMailCmd(email);
            using (IDataReader reader = database.ExecuteReader(cmd))
            {
                if (!reader.Read()) // return null if doesnt find anything
                    return null;
                return new User((string)reader["Email"],
                                (string)reader["PasswordHash"]);
            }
        }
    }
}

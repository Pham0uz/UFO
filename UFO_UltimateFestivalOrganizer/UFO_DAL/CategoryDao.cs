using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;

namespace swk5.ufo.dal
{
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
    }
}

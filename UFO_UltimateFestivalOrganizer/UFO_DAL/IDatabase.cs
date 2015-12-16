using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace swk5.ufo.dal
{
    public interface IDatabase
    {
        // methods for building up commands
        DbCommand CreateCommand(string sql);

        int DeclareParameter(DbCommand command, string name, DbType type);

        int DefineParameter(DbCommand command, string name, DbType type, object value);

        void SetParameter(DbCommand command, string name, object value);

        // methods for executing commands
        IDataReader ExecuteReader(DbCommand command);

        int ExecuteNonQuery(DbCommand command);
    }
}

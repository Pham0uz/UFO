using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Transactions;

namespace swk5.UFO.DAL
{
    public class Database : IDatabase
    {
        private String connectionString;
        private DbProviderFactory providerFactory;

        public Database(String connectionString, String providerName)
        {
            this.connectionString = connectionString;
            this.providerFactory = DbProviderFactories.GetFactory(providerName);
        } //Database

        [ThreadStatic]
        private DbConnection sharedConnection;

        private DbConnection CreateOpenConnection()
        {
            //SqlConnection conn = new SqlConnection(connectionString);
            DbConnection conn = providerFactory.CreateConnection();
            conn.ConnectionString = connectionString;
            conn.Open();
            return conn;
        } //CreateOpenConnection

        private DbConnection GetOpenConnection()
        {
            Transaction currTx = Transaction.Current;
            if (currTx == null)
                return CreateOpenConnection();
            else if (sharedConnection == null)
            {
                sharedConnection = CreateOpenConnection();
                currTx.TransactionCompleted += (s, e) =>
                {
                    sharedConnection.Close();
                    sharedConnection = null;
                };
            }
            return sharedConnection;
        } //GetOpenConnection

        void ReleaseConnection(DbConnection conn)
        {
            if (conn == null)
                return;
            if (Transaction.Current == null) //if there is no shared connection no problems, else do nothing
                conn.Close();
        } //ReleaseConnection

        bool IsSharedConnection()
        {
            return Transaction.Current != null;
        } //IsSharedConnection


        public DbCommand CreateCommand(string sql)
        {
            DbCommand cmd = providerFactory.CreateCommand();
            cmd.CommandText = sql;
            return cmd;
        } //CreateCommand

        public int DeclareParameter(DbCommand command, string name, DbType type)
        {
            if (command.Parameters.Contains(name))
                throw new ArgumentException($"Parameter {name} declared twice.");
            DbParameter param = command.CreateParameter();
            param.ParameterName = name;
            param.DbType = type;
            return command.Parameters.Add(param); // returns index of Parameter
        } //DeclareParameter

        public int DefineParameter(DbCommand command, string name, DbType type, object value)
        {
            int paramIdx = DeclareParameter(command, name, type);
            command.Parameters[paramIdx].Value = value;
            return paramIdx;
        } //DefineParameter

        public void SetParameter(DbCommand command, string name, object value)
        {
            if (!command.Parameters.Contains(name))
                throw new ArgumentException($"Parameter {name} has to be declared first.");
            command.Parameters[name].Value = value;
        } //SetParameter

        public int ExecuteNonQuery(DbCommand command)
        {
            DbConnection connection = null;

            try
            {
                connection = GetOpenConnection();
                command.Connection = connection;
                return command.ExecuteNonQuery();   // return rows affected
            }
            finally
            {
                ReleaseConnection(connection);
            }
        } //ExecuteNonQuery

        public IDataReader ExecuteReader(DbCommand command)
        {
            DbConnection connection = null;

            try
            {
                connection = GetOpenConnection();
                command.Connection = connection;

                CommandBehavior behavior = IsSharedConnection() ?    // if it's a shared connection --> Default, else CloseConnection
                    CommandBehavior.Default :
                    CommandBehavior.CloseConnection;

                return command.ExecuteReader(behavior);   // return rows affected
            }
            catch
            {
                ReleaseConnection(connection);
                throw;
            }
        } //ExecuteReader


    } //Database
}
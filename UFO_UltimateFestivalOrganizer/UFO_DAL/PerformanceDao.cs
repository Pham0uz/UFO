using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace swk5.ufo.dal
{
    class PerformanceDao : IPerformanceDao
    {
        const string SQL_GET_ALL = "select * from Performance";
        const string SQL_GET_ALL_BY_DATE = "select * from Performance WHERE Date=@Date";
        const string SQL_GET_ALL_BY_DATE_TIME = "select * from Performance WHERE Date=@Date AND Time=@Time";
        const string SQL_GET_BY_DATE_TIME_VENUE =
            "select * from Performance where Date=@Date AND Time=@Time AND Venue=@Venue";

        const string SQL_INSERT = "insert into Performance (Date, Time, Artist, Venue) VALUES (@Date, @Time, @Artist, @Venue)";
        const string SQL_UPDATE = "update Performance SET Artist=@Artist WHERE Date=@Date AND Time=@Time AND Venue=@Venue";
        const string SQL_DELETE = "delete from Performance WHERE Date=@Date AND Time=@Time AND Venue=@Venue";

        private IDatabase database;

        public PerformanceDao(IDatabase database)
        {
            this.database = database;
        }

        private DbCommand CreateGetAllCmd()
        {
            return database.CreateCommand(SQL_GET_ALL);
        }
        private DbCommand CreateGetAllByDateCmd(DateTime date)
        {
            var cmd = database.CreateCommand(SQL_GET_ALL_BY_DATE);
            database.DefineParameter(cmd, "@Date", DbType.DateTime, date);
            return cmd;
        }

        private DbCommand CreateGetAllByDate_TimeCmd(DateTime date, int time)
        {
            var cmd = database.CreateCommand(SQL_GET_ALL_BY_DATE_TIME);
            database.DefineParameter(cmd, "@Date", DbType.DateTime, date);
            database.DefineParameter(cmd, "@Time", DbType.Int32, time);
            return cmd;
        }

        private DbCommand CreateGetByDate_Time_VenueCmd(DateTime date, int time, string venue)
        {
            var cmd = database.CreateCommand(SQL_GET_BY_DATE_TIME_VENUE);
            database.DefineParameter(cmd, "@Date", DbType.DateTime, date);
            database.DefineParameter(cmd, "@Time", DbType.Int32, time);
            database.DefineParameter(cmd, "@Venue", DbType.String, venue);
            return cmd;
        }

        private DbCommand CreateInsertCommand(DateTime date, int time, string artist, string venue)
        {
            var cmd = database.CreateCommand(SQL_INSERT);
            database.DefineParameter(cmd, "@Date", DbType.DateTime, date);
            database.DefineParameter(cmd, "@Time", DbType.Int32, time);
            database.DefineParameter(cmd, "@Artist", DbType.String, artist);
            database.DefineParameter(cmd, "@Venue", DbType.String, venue);
            return cmd;
        }

        private DbCommand CreateUpdateCommand(DateTime date, int time, string artist, string venue)
        {
            var cmd = database.CreateCommand(SQL_UPDATE);
            database.DefineParameter(cmd, "@Date", DbType.DateTime, date);
            database.DefineParameter(cmd, "@Time", DbType.Int32, time);
            database.DefineParameter(cmd, "@Artist", DbType.String, artist);
            database.DefineParameter(cmd, "@Venue", DbType.String, venue);
            return cmd;
        }

        private DbCommand CreateDeleteCommand(DateTime date, int time, string venue)
        {
            var cmd = database.CreateCommand(SQL_DELETE);
            database.DefineParameter(cmd, "@Date", DbType.DateTime, date);
            database.DefineParameter(cmd, "@Time", DbType.Int32, time);
            database.DefineParameter(cmd, "@Venue", DbType.String, venue);
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
                    performanceList.Add(new Performance((DateTime)reader["Date"],
                                                        (int)reader["Time"],
                                                        (string)reader["Artist"],
                                                        (string)reader["Venue"]));
                }
            }
            return performanceList;
        }

        public IList<Performance> GetAllByDate(DateTime date)
        {
            var performanceList = new List<Performance>();
            var cmd = CreateGetAllByDateCmd(date);
            using (var reader = database.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
                    performanceList.Add(new Performance((DateTime)reader["Date"],
                                                        (int)reader["Time"],
                                                        (string)reader["Artist"],
                                                        (string)reader["Venue"]));
                }
            }
            return performanceList;
        }

        public IList<Performance> GetAllByDate_Time(DateTime date, int time)
        {
            var performanceList = new List<Performance>();
            var cmd = CreateGetAllByDate_TimeCmd(date, time);
            using (var reader = database.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
                    performanceList.Add(new Performance((DateTime)reader["Date"],
                                                        (int)reader["Time"],
                                                        (string)reader["Artist"],
                                                        (string)reader["Venue"]));
                }
            }
            return performanceList;
        }

        public Performance GetByDate_Time_Venue(DateTime date, int time, string venue)
        {
            DbCommand cmd = CreateGetByDate_Time_VenueCmd(date, time, venue);
            using (var reader = database.ExecuteReader(cmd))
            {
                if (!reader.Read())
                    return null;

                return new Performance((DateTime)reader["Date"],
                                       (int)reader["Time"],
                                       (string)reader["Artist"],
                                       (string)reader["Venue"]);
            }
        }

        public bool Update(Performance p)
        {
            return database.ExecuteNonQuery(CreateUpdateCommand(p.Date, p.Time, p.Artist, p.Venue)) == 1;
        }

        public bool Insert(Performance p)
        {
            return database.ExecuteNonQuery(CreateInsertCommand(p.Date, p.Time, p.Artist, p.Venue)) == 1;
        }

        public bool Delete(DateTime date, int time, string venue)
        {
            return database.ExecuteNonQuery(CreateDeleteCommand(date, time, venue)) == 1;
        }
    }

}

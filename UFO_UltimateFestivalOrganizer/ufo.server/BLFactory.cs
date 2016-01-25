using swk5.ufo.dal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace swk5.ufo.server
{
    public class BLFactory
    {
        private static ICommanderBL commander;
        private static IDatabase database;

        static BLFactory()
        {
            database = DALFactory.CreateDatabase();
        }

        public static ICommanderBL GetCommander()
        {
            if (commander == null)
                commander = new Commander(database);
            return commander;
        }
    }
}

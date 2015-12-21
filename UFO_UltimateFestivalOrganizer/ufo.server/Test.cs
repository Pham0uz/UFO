using swk5.ufo.dal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ufo.server
{
    class Test
    {
        static void PrintTitle(string title)
        {
            ConsoleColor oldCol = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"----- {title} -----");
            Console.ForegroundColor = oldCol;
        }

        static void Main(string[] args)
        {
            PrintTitle("START");
            IDatabase database = DALFactory.CreateDatabase();
            Console.WriteLine($"using database {database.GetType()}");

            IArtistDao artistDao = DALFactory.CreateArtistDao(database);
            PrintTitle("GetByName");
            Artist artist = artistDao.GetByName("figurentheater (isipisi)");
            if (artist != null)
                Console.WriteLine(artist.ToString());

            ICountryDao countryDao = DALFactory.CreateCountryDao(database);
            PrintTitle("GetAll");
            foreach(Country country in countryDao.GetAll())
                Console.WriteLine($"{country.Code, 5}: {country.Name}");

            PrintTitle("END");
        }
    }
}

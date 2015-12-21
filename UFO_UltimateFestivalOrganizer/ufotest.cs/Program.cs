using NLog;
using swk5.ufo.dal;
using System;
using System.Diagnostics;

namespace DAL_ConsoleClient
{
    class Program
    {
        private static Logger Log = LogManager.GetCurrentClassLogger();
        static void Main(string[] args)
        {
            Stopwatch sw = Stopwatch.StartNew();

            IDatabase db = DALFactory.CreateDatabase();
            IUserDao uDao = DALFactory.CreateUserDao(db);
            ICountryDao countryDao = DALFactory.CreateCountryDao(db);
            ICategoryDao catDao = DALFactory.CreateCategoryDao(db);
            IArtistDao aDao = DALFactory.CreateArtistDao(db);
            IPerformanceDao pDao = DALFactory.CreatePerformanceDao(db);

            // ********** USERDAO **********
            //Console.WriteLine("========== UserDao.GetAll() ==========");
            //foreach (User User in uDao.GetAll())
            //    Console.WriteLine($"[{User.EMail,20}]: {User.PasswordHash,30}\n");

            // ********** COUNTRYDAO **********
            //Console.WriteLine("========== CountryDao.GetAll() ==========");
            //foreach (Country Country in countryDao.GetAll())
            //    Console.WriteLine($"[{Country.Code,2}] ({Country.Name,30})\n");

            //Console.WriteLine("========== CountryDao.GetByName() ==========");
            //Country Country1 = countryDao.GetByCode("AT");
            //if (Country1 != null)
            //    Console.WriteLine($"[{Country1.Code,2}] ({Country1.Name,30})\n");
            //else
            //    Console.WriteLine("Country AT not found!");
            //Console.WriteLine();

            // ********** CATEGORYDAO **********
            //Console.WriteLine("========== CategoryDao.GetAll() ==========");
            //foreach (Category Category in catDao.GetAll())
            //    Console.WriteLine($"[{Category.CategoryName,30})\n");

            //Console.WriteLine("========== CategoryDao.GetByName() ==========");
            //Category Category1 = catDao.GetByName("Tanz & Musik");
            //if (Category1 != null)
            //    Console.WriteLine($"[{Category1.CategoryName,30})\n");
            //else
            //    Console.WriteLine("Category Tanz & Musik not found!");
            //Console.WriteLine();


            // ********** ARTISTDAO **********
            //Console.WriteLine("========== ArtistDao.Insert() ==========");
            //Artist newArtist = new Artist("figurentheater(isipisi)", "http://placekitten.com.s3.amazonaws.com/homepage-samples/200/286.jpg", "http://placekitten.com.s3.amazonaws.com/homepage-samples/408/287.jpg", "Straßentheater", "AT");

            //if (aDao.Insert(newArtist))
            //    Console.WriteLine($"Artist: {newArtist.Name} inserted successfully.");
            //else
            //    Console.WriteLine($"ERROR: Could not insert {newArtist.Name}.");

            //Console.WriteLine("========== ArtistDao.GetByName() ==========");
            //Artist a1 = aDao.GetByName("figurentheater(isipisi)");
            //if (a1 != null)
            //    Console.WriteLine($"[{a1.Name,20}] ({a1.CategoryName,20}, {a1.CountryCode,3}), \n");
            //else
            //    Console.WriteLine($"Artist {a1.Name} not found!");
            //Console.WriteLine();


            //Console.WriteLine("========== PerformanceDao.GetAll() ==========");
            //foreach(Performance p in pDao.GetAll())
            //    Console.WriteLine($"{p.Artist.Name}, {p.DateNTime.Date} {p.DateNTime.TimeOfDay}, {p.Venue.Description}");
            //Console.WriteLine("========== PerformancDao.GetByArtistNameAndDateTime ==========");
            //Performance p1 = pDao.GetByArtistNameAndDateTime("figurentheater (isipisi)", new DateTime(2015, 7, 23, 19, 0, 0));
            //if (p1 != null)
            //    Console.WriteLine($"{p1.Artist.Name}, {p1.DateNTime.Date} {p1.DateNTime.TimeOfDay}, {p1.Venue.Description}");
            //else
            //    Console.WriteLine($"Performance with {p1.Artist.Name}, at {p1.DateNTime.Date} {p1.DateNTime.TimeOfDay} at Venue: not found {p1.Venue.Description}.");

            // Diagnostics
            sw.Stop();
            Console.WriteLine($"DONE --- Time Elapsed:{sw.Elapsed}");
            // Diagnostics END
        }
    }
}

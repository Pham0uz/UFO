using System;
using swk5.UFO.DAL;

namespace DAL_ConsoleClient
{
    class Program
    {
        static void Main(string[] args)
        {
            IDatabase db = DALFactory.CreateDatabase();
            IUserDao uDao = DALFactory.CreateUserDao(db);
            ICountryDao countryDao = DALFactory.CreateCountryDao(db);
            ICategoryDao catDao = DALFactory.CreateCategoryDao(db);
            IArtistDao aDao = DALFactory.CreateArtistDao(db);
            IPerformanceDao pDao = DALFactory.CreatePerformanceDao(db);

            //Console.WriteLine("========== UserDao.GetAll() ==========");
            //foreach (User User in uDao.GetAll())
            //    Console.WriteLine($"[{User.UserName,15}] ({User.PasswordHash,15}: {User.EMail,20})\n");

            //Console.WriteLine("========== UserDao.GetByName() ==========");
            //User User1 = uDao.GetByName("Ada");
            //if (User1 != null)
            //    Console.WriteLine($"[{User1.UserName,15}] ({User1.PasswordHash,15}: {User1.EMail,20})\n");
            //else
            //    Console.WriteLine("User Ada not found!");
            //Console.WriteLine();

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

            //Console.WriteLine("========== ArtistDao.GetByName() ==========");
            //Artist a1 = aDao.GetByName("Derek Derek");
            //if (a1 != null)
            //    Console.WriteLine($"[{a1.Name,20}] ({a1.Category.CategoryName,20}, {a1.Country.Code,3}), \n");
            //else
            //    Console.WriteLine("Artist Derek not found!");
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
        }
    }
}

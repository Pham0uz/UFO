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

            //Console.WriteLine("========== CountryDao.GetAll() ==========");
            //foreach (User User in uDao.GetAll())
            //    Console.WriteLine($"[{User.UserName,15}] ({User.PasswordHash,15}: {User.EMail,20})\n");

            //Console.WriteLine("========== CountryDao.GetByName() ==========");
            //User User1 = uDao.GetByName("Ada");
            //if (User1 != null)
            //    Console.WriteLine($"[{User1.UserName,15}] ({User1.PasswordHash,15}: {User1.EMail,20})\n");
            //else
            //    Console.WriteLine("User Ada not found!");
            //Console.WriteLine();

            Console.WriteLine("========== CountryDao.GetAll() ==========");
            foreach (Country Country in countryDao.GetAll())
                Console.WriteLine($"[{Country.Code,2}] ({Country.Name,30})\n");

            Console.WriteLine("========== CountryDao.GetByName() ==========");
            Country Country1 = countryDao.GetByCode("AT");
            if (Country1 != null)
                Console.WriteLine($"[{Country1.Code,2}] ({Country1.Name,30})\n");
            else
                Console.WriteLine("Country AT not found!");
            Console.WriteLine();


        }
    }
}

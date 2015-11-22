using System;
using System.Configuration;
using System.Reflection;

namespace swk5.UFO.DAL
{
    public class DALFactory
    {
        private static string assemblyName;
        private static Assembly dalAssembly;


        private static Type GetType(string typeName)
        {
            Type type = dalAssembly.GetType(typeName);
            if (type == null)
                throw new ArgumentException($"Type {type.FullName} not found found.");
            return type;
        }


        static DALFactory()
        {
            assemblyName = ConfigurationManager.AppSettings["DALAssembly"];
            if (assemblyName == null)
                throw new ArgumentException($"Parameter \"DALAssembly\" not set in configuration.");
            dalAssembly = Assembly.Load(assemblyName);
        }

        private static bool ConstructorExists(Type type, params Type[] constructorParamTypes)
        {
            return type.GetConstructor(constructorParamTypes) != null;
        }

        private static void EnsureConstructorExists(Type type, params Type[] constructorParamTypes)
        {
            if (!ConstructorExists(type, constructorParamTypes))
                throw new ArgumentException($"No suitable cnnstructor for {type.FullName} found.");
        }

        public static IDatabase CreateDatabase()
        {
            var connSettings = ConfigurationManager.ConnectionStrings["DefaultConnectionString"];
            if (connSettings == null)
                throw new ArgumentException($"Parameter \"DefaultConnectionString\" not set in configuration.");

            string connectionString = connSettings.ConnectionString;
            string providerName = connSettings.ProviderName;
            return CreateDatabase(connectionString, providerName);
        }

        public static IDatabase CreateDatabase(string connectionString)
        {
            Type dbType = GetType(assemblyName + ".Database");
            EnsureConstructorExists(dbType, typeof(string));
            return Activator.CreateInstance(dbType,
              new object[] { connectionString }) as IDatabase;
        }

        public static IDatabase CreateDatabase(string connectionString,
                                               string providerName)
        {
            Type dbType = GetType(assemblyName + ".Database");

            if (ConstructorExists(dbType, typeof(string), typeof(string)))
                return Activator.CreateInstance(dbType,
                        new object[] { connectionString, providerName }) as IDatabase;
            else
                return CreateDatabase(connectionString);
        }

        public static IUserDao CreateUserDao(IDatabase database)
        {
            Type userType = GetType(assemblyName + ".UserDao");
            EnsureConstructorExists(userType, typeof(IDatabase));
            return Activator.CreateInstance(userType, new object[] { database })
                     as IUserDao;
        }

        public static IArtistDao CreateArtistDao(IDatabase database)
        {
            Type artistType = GetType(assemblyName + ".ArtistDao");
            EnsureConstructorExists(artistType, typeof(IDatabase));
            return Activator.CreateInstance(artistType, new object[] { database })
                     as IArtistDao;
        }

        public static ICountryDao CreateCountryDao(IDatabase database)
        {
            Type countryType = GetType(assemblyName + ".CountryDao");
            EnsureConstructorExists(countryType, typeof(IDatabase));
            return Activator.CreateInstance(countryType, new object[] { database })
                     as ICountryDao;
        }
        public static ICategoryDao CreateCategoryDao(IDatabase database)
        {
            Type categoryType = GetType(assemblyName + ".CategoryDao");
            EnsureConstructorExists(categoryType, typeof(IDatabase));
            return Activator.CreateInstance(categoryType, new object[] { database })
                     as ICategoryDao;
        }

        public static IVenueDao CreateVenueDao(IDatabase database)
        {
            Type venueType = GetType(assemblyName + ".VenueDao");
            EnsureConstructorExists(venueType, typeof(IDatabase));
            return Activator.CreateInstance(venueType, new object[] { database })
                     as IVenueDao;
        }
        public static IPerformanceDao CreatePerformanceDao(IDatabase database)
        {
            Type performanceType = GetType(assemblyName + ".PerformanceDao");
            EnsureConstructorExists(performanceType, typeof(IDatabase));
            return Activator.CreateInstance(performanceType, new object[] { database })
                     as IPerformanceDao;
        }
    }
}

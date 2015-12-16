using Microsoft.VisualStudio.TestTools.UnitTesting;
using swk5.ufo.dal;
using System.Collections.Generic;

namespace UFO_DAOTests
{
    [TestClass]
    public class VenueDaoTests
    {
        private IDatabase database = DALFactory.CreateDatabase();
        private IVenueDao vDao;

        [TestInitialize]
        public void MyTestInitialize()
        {
            vDao = DALFactory.CreateVenueDao(database);
        }

        [TestMethod]
        public void TestGetByName()
        {
            // arrange
            string expectedValue = "Altes Rathaus";

            // act
            Venue loadedVenue = vDao.GetByName("H4");

            // assert
            Assert.AreEqual(expectedValue, loadedVenue.Description);
        }

        [TestMethod]
        public void TestGetByDescription()
        {
            // arrange
            string expectedValue = "H4";

            // act
            Venue loadedVenue = vDao.GetByDescription("Altes Rathaus");

            // assert
            Assert.AreEqual(expectedValue, loadedVenue.ShortName);
        }

        [TestMethod]
        public void TestGetAll()
        {
            // arrange
            int expectedValue = 41;

            //act
            IList<Venue> VenueList = vDao.GetAll();

            // assert
            int actualValue = VenueList.Count;

            Assert.AreEqual(expectedValue, actualValue);
        }
    }
}

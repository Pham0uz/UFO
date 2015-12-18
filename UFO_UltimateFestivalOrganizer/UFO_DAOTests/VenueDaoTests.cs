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
            int expectedValue = 42;

            //act
            IList<Venue> VenueList = vDao.GetAll();

            // assert
            int actualValue = VenueList.Count;

            Assert.AreEqual(expectedValue, actualValue);
        }

        [TestMethod]
        public void TestInsert()
        {
            // arrange
            bool expectedValue = true;
            Venue newVenue = new Venue("Z9", "Zentral", 0.0, 0.0);
            bool beforeInsert = vDao.GetAll().Count == 42;

            // act
            vDao.Insert(newVenue);
            bool afterInsert = vDao.GetAll().Count == 43;

            // assert
            bool actualValue = (beforeInsert && afterInsert);
            Assert.AreEqual(expectedValue, actualValue, "Venue has not been inserted successfully.");
        }

        [TestMethod]
        public void TestUpdate()
        {
            // arrange
            Venue v = vDao.GetByName("Z9");
            string expectedDescription = "Zentral 17";

            // act
            v.Description = expectedDescription;
            vDao.Update(v);

            // assert
            v = vDao.GetByName("Z9");
            string actualDescription = v.Description;
            Assert.AreEqual(expectedDescription, actualDescription);
        }

        [TestMethod]
        public void TestDelete()
        {
            // arrange
            bool expectedValue = true;
            bool beforeDelete = vDao.GetAll().Count == 43;

            // act
            vDao.Delete("Z9");
            bool afterDelete = vDao.GetAll().Count == 42;

            // assert
            bool actualValue = (beforeDelete && afterDelete);
            Assert.AreEqual(expectedValue, actualValue);
        }
    }
}

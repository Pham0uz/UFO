using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using swk5.UFO.DAL;
using System.Collections.Generic;

namespace UFO_DAOTests
{
    [TestClass]
    public class PerformanceDaoTests
    {
        private IDatabase database = DALFactory.CreateDatabase();
        private IPerformanceDao pDao;

        [TestInitialize]
        public void MyTestInitialize()
        {
            pDao = DALFactory.CreatePerformanceDao(database);
        }

        [TestMethod]
        public void TestGetByArtistNameAndDateTime()
        {
            // arrange
            string expectedValue = "Klosterstraße 7";

            // act
            Performance loadedPerformance = pDao.GetByArtistNameAndDateTime("Duo Kate and Pasi", new DateTime(2015,7,23,17,0,0));

            // assert
            Assert.AreEqual(expectedValue, loadedPerformance.Venue.Description);
        }

        [TestMethod]
        public void TestGetByArtistNameAndDateTime2()
        {
            // arrange
            string expectedValue = "Akrobatik&Tanz";

            // act
            Performance loadedPerformance = pDao.GetByArtistNameAndDateTime("Duo Kate and Pasi", new DateTime(2015, 7, 23, 17, 0, 0));

            // assert
            Assert.AreEqual(expectedValue, loadedPerformance.Artist.Category.CategoryName);
        }

        [TestMethod]
        public void TestGetAll()
        {
            // arrange
            int expectedValue = 8;

            //act
            IList<Performance> PerformanceList = pDao.GetAll();

            // assert
            int actualValue = PerformanceList.Count;

            Assert.AreEqual(expectedValue, actualValue);
        }
    }
}

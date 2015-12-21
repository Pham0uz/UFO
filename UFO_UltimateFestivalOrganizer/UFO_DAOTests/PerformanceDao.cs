using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using swk5.ufo.dal;

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
        public void TestGetByDate_Time_Venue()
        {
            // arrange
            string expectedValue = "T1";

            // act
            Performance loadedPerformance = pDao.GetByDate_Time_Venue(new DateTime(2015, 7, 23), 19, "T1");

            // assert
            Assert.AreEqual(expectedValue, loadedPerformance.Venue);
        }

        [TestMethod]
        public void TestGetByArtistNameAndDateTime2()
        {
            // arrange
            string expectedValue = "figurentheater (isipisi)";

            // act
            Performance loadedPerformance = pDao.GetByDate_Time_Venue(new DateTime(2015, 7, 23), 19, "T1");

            // assert
            Assert.AreEqual(expectedValue, loadedPerformance.Artist);
        }

        [TestMethod]
        public void TestGetAll()
        {
            // arrange
            int expectedValue = 1;

            //act
            IList<Performance> PerformanceList = pDao.GetAll();

            // assert
            int actualValue = PerformanceList.Count;

            Assert.AreEqual(expectedValue, actualValue);
        }

        public void TestGetAllByDate()
        {
            // arrange
            int expectedValue = 1;

            //act
            IList<Performance> PerformanceList = pDao.GetAllByDate(new DateTime(2015, 7, 23));

            // assert
            int actualValue = PerformanceList.Count;

            Assert.AreEqual(expectedValue, actualValue);
        }

        public void TestGetAllByDateTime()
        {
            // arrange
            int expectedValue = 1;

            //act
            IList<Performance> PerformanceList = pDao.GetAllByDate_Time(new DateTime(2015, 7, 23), 19);

            // assert
            int actualValue = PerformanceList.Count;

            Assert.AreEqual(expectedValue, actualValue);
        }

        [TestMethod]
        public void TestInsert()
        {
            // arrange
            bool expectedValue = true;
           Performance newPerformance = new Performance(new DateTime(2015, 7, 23), 19, "MadMatt", "T2");
            bool beforeInsert = pDao.GetAll().Count == 1;

            // act
            pDao.Insert(newPerformance);
            bool afterInsert = pDao.GetAll().Count == 2;

            // assert
            bool actualValue = (beforeInsert && afterInsert);
            Assert.AreEqual(expectedValue, actualValue, "Performance has not been inserted successfully.");
        }

        [TestMethod]
        public void TestUpdate()
        {
            // arrange
            Performance p = pDao.GetByDate_Time_Venue(new DateTime(2015, 7, 23), 19, "T2");
            string expectedArtist = "MadMatt2";

            // act
            p.Artist = "MadMatt2";
            pDao.Update(p);

            // assert
            p = pDao.GetByDate_Time_Venue(new DateTime(2015, 7, 23), 19, "T2");
            string actualArtist = p.Artist;
            Assert.AreEqual(expectedArtist, actualArtist);
        }

        [TestMethod]
        public void TestDelete()
        {
            // arrange
            bool expectedValue = true;
            bool beforeDelete = pDao.GetAll().Count == 2;

            // act
            pDao.Delete(new DateTime(2015, 7, 23), 19, "T2");
            bool afterDelete = pDao.GetAll().Count == 1;

            // assert
            bool actualValue = (beforeDelete && afterDelete);
            Assert.AreEqual(expectedValue, actualValue);
        }
    }
}

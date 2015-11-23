using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using swk5.UFO.DAL;
using System.Collections.Generic;

namespace UFO_DAOTests
{
    [TestClass]
    public class ArtistDaoTests
    {
        private IDatabase database = DALFactory.CreateDatabase();
        private IArtistDao aDao;

        [TestInitialize]
        public void MyTestInitialize()
        {
            aDao = DALFactory.CreateArtistDao(database);
        }

        [TestMethod]
        public void TestGetByName()
        {
            // arrange
            string expectedValue = "Akrobatik&Tanz";

            // act
            Artist loadedArtist = aDao.GetByName("Anne & Mitja");

            // assert
            Assert.AreEqual(expectedValue, loadedArtist.Category.CategoryName);
        }

        [TestMethod]
        public void TestGetByName2()
        {
            // arrange
            string expectedValue = "DE";

            // act
            Artist loadedArtist = aDao.GetByName("Anne & Mitja");

            // assert
            Assert.AreEqual(expectedValue, loadedArtist.Country.Code);
        }

        [TestMethod]
        public void TestGetAll()
        {
            // arrange
            int expectedValue = 74;

            //act
            IList<Artist> ArtistList = aDao.GetAll();

            // assert
            int actualValue = ArtistList.Count;

            Assert.AreEqual(expectedValue, actualValue);
        }
    }
}

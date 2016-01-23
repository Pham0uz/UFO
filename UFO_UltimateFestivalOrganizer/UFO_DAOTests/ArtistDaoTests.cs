using Microsoft.VisualStudio.TestTools.UnitTesting;
using swk5.ufo.dal;
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
            string expectedValue = "Straßentheater";

            // act
            Artist loadedArtist = aDao.GetByName("figurentheater (isipisi)");

            // assert
            Assert.AreEqual(expectedValue, loadedArtist.CategoryName);
        }

        [TestMethod]
        public void TestGetByName2()
        {
            // arrange
            string expectedValue = "AT";

            // act
            Artist loadedArtist = aDao.GetByName("figurentheater (isipisi)");

            // assert
            Assert.AreEqual(expectedValue, loadedArtist.CountryCode);
        }

        [TestMethod]
        public void TestGetAll()
        {
            // arrange
            int expectedValue = 91;

            //act
            IList<Artist> ArtistList = aDao.GetAll();

            // assert
            int actualValue = ArtistList.Count;

            Assert.AreEqual(expectedValue, actualValue);
        }

        [TestMethod]
        public void TestInsert()
        {
            // arrange
            bool expectedValue = true;
            Artist newArtist = new Artist("Anne & Mitja", "test.jpg", "test2.jpg", "Akrobatik&Tanz", "DE");
            bool beforeInsert = aDao.GetAll().Count == 91;

            // act
            aDao.Insert(newArtist);
            bool afterInsert = aDao.GetAll().Count == 92;

            // assert
            bool actualValue = (beforeInsert && afterInsert);
            Assert.AreEqual(expectedValue, actualValue, "Artist has not been inserted successfully.");
        }

        [TestMethod]
        public void TestUpdate()
        {
            // arrange
            Artist a = aDao.GetByName("Anne & Mitja");
            string expectedCountryCode = "DE";

            // act
            a.CountryCode = "DE";
            aDao.Update(a);

            // assert
            a = aDao.GetByName("Anne & Mitja");
            string actualCountryCode = a.CountryCode;
            Assert.AreEqual(expectedCountryCode, actualCountryCode);
        }

        [TestMethod]
        public void TestDelete()
        {
            // arrange
            bool expectedValue = true;
            bool beforeDelete = aDao.GetAll().Count == 92;

            // act
            aDao.Delete("Anne & Mitja");
            bool afterDelete = aDao.GetAll().Count == 91;

            // assert
            bool actualValue = (beforeDelete && afterDelete);
            Assert.AreEqual(expectedValue, actualValue);
        }
    }
}

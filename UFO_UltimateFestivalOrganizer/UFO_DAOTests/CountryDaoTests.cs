using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using swk5.UFO.DAL;
using System.Collections.Generic;

namespace UFO_DAOTests
{
    [TestClass]
    public class CountryDaoTests
    {
        private IDatabase database = DALFactory.CreateDatabase();
        private ICountryDao cDao;

        [TestInitialize]
        public void MyTestInitialize()
        {
            cDao = DALFactory.CreateCountryDao(database);
        }

        [TestMethod]
        public void TestGetByName()
        {
            // arrange
            string expectedValue = "AT";

            // act
            Country loadedCountry = cDao.GetByName("Österreich");

            // assert
            Assert.AreEqual(expectedValue, loadedCountry.Code);
        }

        [TestMethod]
        public void TestGetByCode()
        {
            // arrange
            string expectedValue = "Österreich";

            // act
            Country loadedCountry = cDao.GetByCode("AT");

            // assert
            Assert.AreEqual(expectedValue, loadedCountry.Name);
        }

        [TestMethod]
        public void TestGetAll()
        {
            // arrange
            int expectedValue = 247;

            //act
            IList<Country> CountryList = cDao.GetAll();

            // assert
            int actualValue = CountryList.Count;

            Assert.AreEqual(expectedValue, actualValue);
        }
    }
}

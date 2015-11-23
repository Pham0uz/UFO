using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using swk5.UFO.DAL;
using System.Collections.Generic;

namespace UFO_DAOTests
{
    [TestClass]
    public class CategoryDaoTests
    {
        private IDatabase database = DALFactory.CreateDatabase();
        private ICategoryDao cDao;

        [TestInitialize]
        public void MyTestInitialize()
        {
            cDao = DALFactory.CreateCategoryDao(database);
        }

        [TestMethod]
        public void TestGetByName()
        {
            // arrange
            string expectedValue = "Straßentheater";

            // act
            Category loadedCategory = cDao.GetByName("Straßentheater");

            // assert
            Assert.AreEqual(expectedValue, loadedCategory.CategoryName);
        }

        [TestMethod]
        public void TestGetByName2()
        {
            // arrange
            string expectedValue = "Straßentheater";

            // act
            Category loadedCategory = cDao.GetByName("Local Art");

            // assert
            Assert.AreNotEqual(expectedValue, loadedCategory.CategoryName);
        }

        [TestMethod]
        public void TestGetNameNotFound()
        {
            // act
            Category loadedCategory = cDao.GetByName("Babyweitwurf");

            // assert
            Assert.AreEqual(loadedCategory, null);
        }

        [TestMethod]
        public void TestGetAll()
        {
            // arrange
            int expectedValue = 11;

            //act
            IList<Category> categoryList = cDao.GetAll();

            // assert
            int actualValue = categoryList.Count;

            Assert.AreEqual(expectedValue, actualValue);
        }
    }
}

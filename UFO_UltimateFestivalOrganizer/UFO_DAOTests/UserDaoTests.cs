using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using swk5.UFO.DAL;
using System.Collections.Generic;

namespace UFO_DAOTests
{
    [TestClass]
    public class UserDaoTests
    {
        private IDatabase database = DALFactory.CreateDatabase();
        private IUserDao uDao;

        [TestInitialize]
        public void MyTestInitialize()
        {
            uDao = DALFactory.CreateUserDao(database);
        }

        [TestMethod]
        public void TestGetByName()
        {
            // arrange
            User user = new User("Ada", "3g!m4Cyr", "sogga@soto.co.uk");

            // act
            User loadedUser = uDao.GetByName("Ada");

            // assert
            Assert.AreEqual(user.UserName, loadedUser.UserName);
        }

        [TestMethod]
        public void TestGetByName2()
        {
            // arrange
            User user = new User("Ada", "3g!m4Cyr", "sogga@soto.co.uk");

            // act
            User loadedUser = uDao.GetByName("Albert");

            // assert
            Assert.AreNotEqual(user.UserName, loadedUser.UserName);
        }

        [TestMethod]
        public void TestGetNameNotFound()
        {
            // act
            User loadedUser = uDao.GetByName("Albertosc");

            // assert
            Assert.AreEqual(loadedUser, null);
        }

        [TestMethod]
        public void TestGetAll()
        {
            // arrange
            int expectedUserCount = 100;

            //act
            IList<User> userList = uDao.GetAll();

            // assert
            int actualUserCount = userList.Count;

            Assert.AreEqual(expectedUserCount, actualUserCount);
        }
    }
}

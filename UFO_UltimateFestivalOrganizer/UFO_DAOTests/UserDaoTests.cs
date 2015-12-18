using Microsoft.VisualStudio.TestTools.UnitTesting;
using swk5.ufo.dal;
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
        public void TestGetByEMail()
        {
            // arrange
            User user = new User("admin@gmx.at", "0ce16c9bcd892e45e837ac27ff9b5339c16a1b9e");
            string useremail = "admin@gmx.at";
            string userpwd = "0ce16c9bcd892e45e837ac27ff9b5339c16a1b9e";

            // act
            User loadedUser = uDao.GetByEMail("admin@gmx.at");

            // assert
            Assert.AreEqual(user.PasswordHash, loadedUser.PasswordHash);
            Assert.AreEqual(user.EMail, loadedUser.EMail);
            Assert.AreEqual($"User: E-Mail: {useremail,20} | Password: {userpwd,30} | ", loadedUser.ToString());
        }

        [TestMethod]
        public void TestGetEMailNotFound()
        {
            // act
            User loadedUser = uDao.GetByEMail("noreply@gmx.at");

            // assert
            Assert.AreEqual(loadedUser, null);
        }

        [TestMethod]
        public void TestGetAll()
        {
            // arrange
            int expectedUserCount = 1;

            //act
            IList<User> userList = uDao.GetAll();

            // assert
            int actualUserCount = userList.Count;

            Assert.AreEqual(expectedUserCount, actualUserCount);
        }
    }
}

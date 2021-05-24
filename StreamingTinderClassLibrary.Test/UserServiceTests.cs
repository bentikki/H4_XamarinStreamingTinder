using NUnit.Framework;
using StreaminTinderClassLibrary.Authentication;
using StreaminTinderClassLibrary.Hashing;
using StreaminTinderClassLibrary.Users;
using StreaminTinderClassLibrary.Users.Models;
using System;

namespace StreamingTinderClassLibrary.Test
{
    [TestFixture]
    public class UserServiceTests
    {


        private UserService _userService;

        [SetUp]
        public void SetUp()
        {
            _userService = new UserService();

        }


        [Test]
        [TestCase(0)]
        [TestCase(-5)]
        [TestCase(int.MinValue)]
        public void GetUser_IdBelow1_ShouldThrowArgumentException(int userID)
        {
            // Act and Assert
            Assert.Throws<ArgumentException>( () => _userService.GetUser(userID));
        }

        [Test]
        [TestCase(1)]
        [TestCase(2)]
        public void GetUser_ExistingID_ShouldReturnIUser(int userID)
        {
            // Arrange
            IUser user;

            // Act
            user = this._userService.GetUser(userID);

            // Assert
            Assert.IsNotNull(user);
            Assert.IsNotEmpty(user.Username);
            Assert.IsNotEmpty(user.Email);
            Assert.Greater(user.Id, 0);
        }

        [Test]
        public void CreateNewUser_CreateNewUniqueUser_ShouldReturnIUser()
        {
            // Arrange
            IUser createUserToBe;
            IUser createdUser;

            Random random = new Random();

            createUserToBe = new User()
            {
                Email = "testemail" + random.Next(0, 10000),
                Username = "testusername" + random.Next(0, 10000),
                Password = "password" + random.Next(0, 10000)
            };


            // Act
            createdUser = this._userService.CreateNewUser(createUserToBe);

            // Assert
            Assert.IsNotNull(createdUser);
            Assert.IsNotEmpty(createdUser.Username);
            Assert.IsNotEmpty(createdUser.Email);
            Assert.Greater(createdUser.Id, 0);
        }

        [Test]
        [TestCase("email")]
        [TestCase("username")]
        [TestCase("password")]
        public void CreateNewUser_CreateNewNullUser_ShouldReturnArgumentException(string missingProperty)
        {
            // Arrange
            IUser user;
            Random random = new Random();

            string email = missingProperty?.ToLower() == "email" ? null : "testemail" + random.Next(0, 10000);
            string username = missingProperty?.ToLower() == "username" ? null : "testusername" + random.Next(0, 10000);
            string password = missingProperty?.ToLower() == "password" ? null : "testpassword" + random.Next(0, 10000);

            user = new User()
            {
                Email = email,
                Username = username,
                Password = password
            };

            // Act and Assert
            Assert.Throws<ArgumentException>(() => this._userService.CreateNewUser(user));
        }

        [Test]
        [TestCase("email")]
        [TestCase("password")]
        public void VerifyUser_InvalidUserObject_ShouldReturnArgumentException(string missingProperty)
        {
            // Arrange
            IUser user;
            Random random = new Random();

            string email = missingProperty?.ToLower() == "email" ? null : "testemail" + random.Next(0, 10000);
            string password = missingProperty?.ToLower() == "password" ? null : "testpassword" + random.Next(0, 10000);

            user = new User()
            {
                Email = email,
                Password = password
            };

            // Act and Assert
            Assert.Throws<ArgumentException>(() => this._userService.VerifyUser(user));
        }

        [Test]
        [TestCase(null)]
        [TestCase("")]
        public void GetUserByEmail_EmptyOrNullStringProvided_ShouldReturnArgumentException(string emailTestString)
        {
            // Arrange
            string emailString = emailTestString;

            // Act and Assert
            Assert.Throws<ArgumentException>(() => this._userService.GetUserByEmail(emailString));
        }

    }
}

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


        private IUserService _userService;

        [SetUp]
        public void SetUp()
        {
            _userService = UserServiceFactory.GetUserService();

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
        [TestCase(1)]
        [TestCase(2)]
        public void GetUserAsync_ExistingID_ShouldReturnIUser(int userID)
        {
            // Arrange
            IUser user;

            // Act
            var userTask = this._userService.GetUserAsync(userID).Result;
            user = userTask;

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

            // Clean up
            this._userService.DeleteUser(createdUser.Id);

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
        public void VerifyUser_MatchingUser_ShouldReturnTrue()
        {
            // Arrange
            IUser createdUser;
            IUser verifyUser;
            bool verifySuccess = false;
            Random random = new Random();
            string password = "password" + random.Next(0, 100000);
            string email = "testemail" + random.Next(0, 100000);

            createdUser = new User()
            {
                Email = email,
                Username = "testusername" + random.Next(0, 100000),
                Password = password
            };

            // Act
            createdUser = this._userService.CreateNewUser(createdUser);


            verifyUser = new User()
            {
                Email = email,
                Password = password
            };

            verifySuccess = this._userService.VerifyUser(verifyUser);

            // Cleanup
            this._userService.DeleteUser(createdUser.Id);

            // Assert
            Assert.IsTrue(verifySuccess);
        }

        [Test]
        public void VerifyUser_NonMatchingUser_ShouldReturnFalse()
        {
            // Arrange
            IUser createdUser;
            IUser verifyUser;
            bool verifySuccess = false;
            Random random = new Random();
            string password = "password" + random.Next(0, 100000);
            string email = "testemail" + random.Next(0, 100000);

            createdUser = new User()
            {
                Email = email,
                Username = "testusername" + random.Next(0, 100000),
                Password = password
            };

            // Act
            createdUser = this._userService.CreateNewUser(createdUser);


            verifyUser = new User()
            {
                Email = email,
                Password = password + 1
            };

            verifySuccess = this._userService.VerifyUser(verifyUser);

            // Cleanup
            this._userService.DeleteUser(createdUser.Id);

            // Assert
            Assert.IsFalse(verifySuccess);
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

        [Test]
        public void GetUserByEmail_ExistingUser_ShouldReturnIUser()
        {
            // Arrange
            IUser createdUser;
            IUser retrievedUser;

            Random random = new Random();

            createdUser = new User()
            {
                Email = "testemail" + random.Next(0, 100000),
                Username = "testusername" + random.Next(0, 100000),
                Password = "password" + random.Next(0, 100000)
            };

            // Act
            createdUser = this._userService.CreateNewUser(createdUser);
            retrievedUser = this._userService.GetUserByEmail(createdUser.Email);

            // Cleanup
            this._userService.DeleteUser(createdUser.Id);
            this._userService.DeleteUser(retrievedUser.Id);

            // Act and Assert
            Assert.NotNull(createdUser);
            Assert.NotNull(retrievedUser);
        }

        [Test]
        public void LoginUser_ExistingUser_ShouldReturnTrueWithSatCurrentUser()
        {
            // Arrange
            IUser createdUser;
            IUser loggedInUser = null;
            bool userLoginSuccess = false;
            Random random = new Random();
            string emailString = "testemail" + random.Next(0, 100000);
            string passwordString = "password" + random.Next(0, 100000);

            createdUser = new User()
            {
                Email = emailString,
                Username = "testusername" + random.Next(0, 100000),
                Password = passwordString
            };

            // Create test user
            createdUser = this._userService.CreateNewUser(createdUser);

            // Act
            userLoginSuccess = this._userService.LoginUser(emailString, passwordString);

            if(userLoginSuccess)
                loggedInUser = this._userService.CurrentUser;

            // Cleanup - delete test user
            this._userService.DeleteUser(createdUser.Id);

            // Act and Assert
            Assert.IsTrue(userLoginSuccess);
            Assert.NotNull(loggedInUser);
        }

        [Test]
        public void LoginUser_ExistingUserWrongPassword_ShouldReturnFalseWithNullCurrentUser()
        {
            // Arrange
            IUser createdUser;
            IUser loggedInUser = null;
            bool userLoginSuccess = false;
            Random random = new Random();
            string emailString = "testemail" + random.Next(0, 100000);
            string passwordString = "password" + random.Next(0, 100000);

            createdUser = new User()
            {
                Email = emailString,
                Username = "testusername" + random.Next(0, 100000),
                Password = passwordString
            };

            // Create test user
            createdUser = this._userService.CreateNewUser(createdUser);

            // Act
            userLoginSuccess = this._userService.LoginUser(emailString, passwordString + "1");

            if (userLoginSuccess)
                loggedInUser = this._userService.CurrentUser;

            // Cleanup - delete test user
            this._userService.DeleteUser(createdUser.Id);

            // Act and Assert
            Assert.IsFalse(userLoginSuccess);
            Assert.IsNull(loggedInUser);
        }

        [Test]
        public void GetUserByEmail_NonExistingUser_ShouldReturnNull()
        {
            // Arrange
            IUser createdUser;
            IUser retrievedUser;

            Random random = new Random();

            createdUser = new User()
            {
                Email = "testemail" + random.Next(0, 100000),
                Username = "testusername" + random.Next(0, 100000),
                Password = "password" + random.Next(0, 100000)
            };

            // Act
            createdUser = this._userService.CreateNewUser(createdUser); // Create new user
            this._userService.DeleteUser(createdUser.Id); // Delete created user

            retrievedUser = this._userService.GetUserByEmail(createdUser.Email);

            // Act and Assert
            Assert.IsNull(retrievedUser);
        }

        [Test]
        public void DeleteUser_ExistingUser_ShouldReturnTrue()
        {
            // Arrange
            IUser createdUser;
            bool userDeletedSuccessfully;

            Random random = new Random();

            createdUser = new User()
            {
                Email = "testemail" + random.Next(0, 100000),
                Username = "testusername" + random.Next(0, 100000),
                Password = "password" + random.Next(0, 100000)
            };


            // Act
            createdUser = this._userService.CreateNewUser(createdUser);
            userDeletedSuccessfully = this._userService.DeleteUser(createdUser.Id);

            // Act and Assert
            Assert.IsTrue(userDeletedSuccessfully);
        }

        [Test]
        public void DeleteUser_NonExistingUser_ShouldReturnFalse()
        {
            // Arrange
            IUser createdUser;
            bool userDeletedSuccessfully;

            Random random = new Random();

            createdUser = new User()
            {
                Email = "testemail" + random.Next(0, 100000),
                Username = "testusername" + random.Next(0, 100000),
                Password = "password" + random.Next(0, 100000)
            };


            // Act
            createdUser = this._userService.CreateNewUser(createdUser);
            this._userService.DeleteUser(createdUser.Id);
            userDeletedSuccessfully = this._userService.DeleteUser(createdUser.Id);

            // Act and Assert
            Assert.IsFalse(userDeletedSuccessfully);
        }

    }
}

using NUnit.Framework;
using StreamingTinderClassLibrary.Validator;
using StreaminTinderClassLibrary.Users;
using StreaminTinderClassLibrary.Users.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace StreamingTinderClassLibrary.Test
{
    [TestFixture]
    class DefaultValidatorTests
    {
        private IUserService userService;

        [SetUp]
        public void SetUp()
        {
            this.userService = ServiceFactory.GetUserService();
        }

        [Test]
        [TestCase(null)]
        [TestCase("")]
        [TestCase("    ")]
        [TestCase(">'123123")]
        [TestCase("105 OR 1=1")]
        [TestCase("\" or \"\" = \"")]
        public void ValidEmailLogin_InvalidValues_ShouldReturnErrorList(string validateString)
        {
            // Arrange
            string testString = validateString;

            // Act
            var errorList = DefaultValidator.ValidEmailLogin(validateString);

            // Assert
            Assert.IsNotNull(errorList);
            Assert.IsNotEmpty(errorList);
        }

        [Test]
        public void ValidEmailLogin_LenghtOver_ShouldReturnErrorList()
        {
            // Arrange
            string testString = new String('a', 251);

            // Act
            var errorList = DefaultValidator.ValidEmailLogin(testString);

            // Assert
            Assert.IsNotNull(errorList);
            Assert.IsNotEmpty(errorList);
        }

        [Test]
        public void ValidEmailLogin_LenghtUnder_ShouldReturnErrorList()
        {
            // Arrange
            string testString = new String('a', 2);

            // Act
            var errorList = DefaultValidator.ValidEmailLogin(testString);

            // Assert
            Assert.IsNotNull(errorList);
            Assert.IsNotEmpty(errorList);
        }

        [Test]
        [TestCase("testemail@testemail.com")]
        public void ValidEmailCreateNew_ValidValues_ShouldReturnEmptyList(string validateString)
        {
            // Arrange
            string testString = validateString;

            // Act
            var errorList = DefaultValidator.ValidEmailCreateNew(validateString);

            // Assert
            Assert.IsEmpty(errorList);
        }

        [Test]
        public async Task ValidEmailCreateNew_ExistingUser_ShouldReturnErrorListWithUniqueUserError()
        {
            // Arrange
            IUser createdUser;
            bool deletedUserSuccess;
            long randomNumber = new Random().Next(0, 10000);
            string email = $"ValidEmailCreateNewExistingUser{randomNumber}@email.com";
            string password = "123456798!weqeAASD";
            string username = $"ValidEmailCreateNewExistingUser{randomNumber}";

            IUser createUser = new User(email, password, username);

            createdUser = await this.userService.CreateNewUserAsync(createUser);

            // Act
            var errorList = await DefaultValidator.ValidEmailCreateNewAsync(email);

            // Cleanup
            deletedUserSuccess = await this.userService.DeleteUserAsync(createdUser.Id);

            // Assert
            Assert.IsNotNull(createdUser);
            Assert.IsNotEmpty(errorList);
            Assert.IsTrue(deletedUserSuccess);

        }

        [Test]
        public void ValidEmailLogin_ExistingUser_ShouldReturnEmptyErrorList()
        {
            // Arrange
            Random random = new Random();
            IUser user = new User()
            {
                Email = "testemail" + random.Next(0, 10000),
                Username = "testusername" + random.Next(0, 10000),
                Password = "password" + random.Next(0, 10000)
            };

            // Act
            user = userService.CreateNewUser(user);

            var errorList = DefaultValidator.ValidEmailLogin(user.Email);

            // Cleanup
            userService.DeleteUser(user.Id);

            // Assert
            Assert.IsNotNull(errorList);
            Assert.IsEmpty(errorList);
        }

        [Test]
        [TestCase(null)]
        [TestCase("")]
        [TestCase("          ")]
        [TestCase("12345")]

        public void ValidPassword_InvalidValues_ShouldReturnErrorList(string validateString)
        {
            // Arrange
            string testString = validateString;

            // Act
            var errorList = DefaultValidator.ValidPassword(validateString);

            // Assert
            Assert.IsNotNull(errorList);
            Assert.IsNotEmpty(errorList);
        }

        [Test]
        [TestCase("ValidPassword123")]
        public void ValidPassword_ValidValues_ShouldReturnEmptyErrorList(string validateString)
        {
            // Arrange
            string testString = validateString;

            // Act
            var errorList = DefaultValidator.ValidPassword(validateString);

            // Assert
            Assert.IsEmpty(errorList);
        }

        [Test]
        [TestCase(null)]
        [TestCase("")]
        [TestCase("    ")]
        [TestCase(">'123123")]
        [TestCase("105 OR 1=1")]
        [TestCase("\" or \"\" = \"")]
        public void ValidRoomName_InvalidValues_ShouldReturnErrorList(string validateString)
        {
            // Arrange
            string testString = validateString;

            // Act
            var errorList = DefaultValidator.ValidRoomName(validateString);

            // Assert
            Assert.IsNotNull(errorList);
            Assert.IsNotEmpty(errorList);
        }

        [Test]
        [TestCase("ValidRoomName")]
        [TestCase("Valid room name")]
        [TestCase("My Room 123")]
        [TestCase("Living Room - 1")]
        public void ValidRoomName_ValidValues_ShouldReturnEmptyErrorList(string validateString)
        {
            // Arrange
            string testString = validateString;

            // Act
            var errorList = DefaultValidator.ValidRoomName(validateString);

            // Assert
            Assert.IsEmpty(errorList);
        }
    }
}

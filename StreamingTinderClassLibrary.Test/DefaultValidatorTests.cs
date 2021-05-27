using NUnit.Framework;
using StreamingTinderClassLibrary.Validator;
using StreaminTinderClassLibrary.Users;
using StreaminTinderClassLibrary.Users.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace StreamingTinderClassLibrary.Test
{
    [TestFixture]
    class DefaultValidatorTests
    {
        private IUserService userService;

        [SetUp]
        public void SetUp()
        {
            this.userService = UserServiceFactory.GetUserService();
        }

        [Test]
        [TestCase(null)]
        [TestCase("")]
        [TestCase("    ")]
        [TestCase(">'123123")]
        [TestCase("105 OR 1=1")]
        [TestCase("\" or \"\" = \"")]
        public void ValidEmail_InvalidValues_ShouldReturnErrorList(string validateString)
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
        public void ValidEmail_LenghtOver_ShouldReturnErrorList()
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
        public void ValidEmail_LenghtUnder_ShouldReturnErrorList()
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
        public void ValidEmail_ValidValues_ShouldReturnEmptyList(string validateString)
        {
            // Arrange
            string testString = validateString;

            // Act
            var errorList = DefaultValidator.ValidEmailCreateNew(validateString);

            // Assert
            Assert.IsEmpty(errorList);
        }


        [Test]
        public void ValidEmail_ExistingUser_ShouldReturnEmptyErrorList()
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


    }
}

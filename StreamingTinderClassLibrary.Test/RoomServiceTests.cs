using NUnit.Framework;
using StreamingTinderClassLibrary.Room;
using StreamingTinderClassLibrary.StreamingService;
using StreaminTinderClassLibrary.Authentication;
using StreaminTinderClassLibrary.Hashing;
using StreaminTinderClassLibrary.Users;
using StreaminTinderClassLibrary.Users.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StreamingTinderClassLibrary.Test
{
    [TestFixture]
    public class RoomServiceTests
    {


        private IRoomService _roomService;
        private IUserService _userService;

        [SetUp]
        public void SetUp()
        {
            _roomService = ServiceFactory.GetRoomService();
            _userService = ServiceFactory.GetUserService();
        }


        [Test]
        public async Task CreateRoom_ValidRoom_ShouldReturnIRoom()
        {
            // Arrange
            long randomNumber = new Random().Next(0, 10000);

            IUser roomOwner;
            string roomName = $"tstName{randomNumber}";
            string email = $"tstEmail{randomNumber}@email.com";
            string password = $"tstPassword{randomNumber}";
            string username = $"tstUsername{randomNumber}";
            List<IStreamingPlatform> streamingServices;

            // Setup Test StreamingServices

            // Create temporary room owner
            roomOwner = await this._userService.CreateNewUserAsync(email, password, username);

            // Act



            // Assert

            Assert.IsTrue(false);
        }

        [Test]
        public void CreateRoom_InvalidRoomNullOwner_ShouldThrowInvalidArgumentException()
        {
            // Arrange
            // Act
            // Assert
            Assert.IsTrue(false);

        }

        [Test]
        public void CreateRoom_InvalidEmptyStreamingList_ShouldThrowInvalidArgumentException()
        {
            // Arrange
            // Act
            // Assert
            Assert.IsTrue(false);

        }

        [Test]
        [TestCase("")]
        [TestCase(null)]
        [TestCase("abc")] // Minimum lenght for room name is 4 characters.
        public void CreateRoom_InvalidRoomName_ShouldThrowInvalidArgumentException()
        {
            // Arrange
            // Act
            // Assert
            Assert.IsTrue(false);

        }
    }
}

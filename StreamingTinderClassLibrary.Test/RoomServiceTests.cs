using NUnit.Framework;
using StreamingTinderClassLibrary.Rooms;
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
        private IStreamingPlatformService _streamingPlatformService;

        [SetUp]
        public void SetUp()
        {
            _roomService = ServiceFactory.GetRoomService();
            _userService = ServiceFactory.GetUserService();
            _streamingPlatformService = ServiceFactory.GetStreamingPlatformService();
        }


        [Test]
        public async Task CreateRoom_ValidRoom_ShouldReturnIRoom()
        {
            // Arrange
            long randomNumber = new Random().Next(0, 10000);

            IRoom createdRoom;
            IUser roomOwner;
            string roomName = $"tstName{randomNumber}";
            string email = $"tstEmail{randomNumber}@email.com";
            string password = $"tstPassword{randomNumber}";
            string username = $"tstUsername{randomNumber}";
            List<IStreamingPlatform> streamingServices = new List<IStreamingPlatform>();

            // Setup Test StreamingServices
            streamingServices.Add(this._streamingPlatformService.GetStreamingPlatformById(1));

            // Create temporary room owner
            roomOwner = await this._userService.CreateNewUserAsync(email, password, username);

            // Act
            createdRoom = this._roomService.CreateRoom(roomOwner, roomName, streamingServices);

            // Assert
            Assert.IsNotNull(roomOwner);
            Assert.IsNotNull(createdRoom);
        }

        [Test]
        public void CreateRoom_InvalidRoomNullOwner_ShouldThrowInvalidArgumentException()
        {
            // Arrange
            long randomNumber = new Random().Next(0, 10000);

            IUser roomOwner;
            string roomName = $"tstName{randomNumber}";
            string email = $"tstEmail{randomNumber}@email.com";
            string password = $"tstPassword{randomNumber}";
            string username = $"tstUsername{randomNumber}";
            List<IStreamingPlatform> streamingServices = new List<IStreamingPlatform>();

            // Setup Test StreamingServices
            streamingServices.Add(this._streamingPlatformService.GetStreamingPlatformById(1));

            // Create temporary room owner
            roomOwner = null;

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => this._roomService.CreateRoom(roomOwner, roomName, streamingServices));
        }

        [Test]
        public void CreateRoom_InvalidNullStreamingList_ShouldThrowInvalidArgumentException()
        {
            // Arrange
            long randomNumber = new Random().Next(0, 10000);

            IUser roomOwner;
            string roomName = $"tstName{randomNumber}";
            string email = $"tstEmail{randomNumber}@email.com";
            string password = $"tstPassword{randomNumber}";
            string username = $"tstUsername{randomNumber}";

            // Create temporary room owner
            roomOwner = this._userService.CreateNewUserAsync(email, password, username).Result;

            this._userService.DeleteUser(roomOwner.Id);

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => this._roomService.CreateRoom(roomOwner, roomName, null));

        }


        [Test]
        public void CreateRoom_InvalidEmptyStreamingList_ShouldThrowInvalidArgumentException()
        {
            // Arrange
            long randomNumber = new Random().Next(0, 10000);

            IUser roomOwner;
            string roomName = $"tstName{randomNumber}";
            string email = $"tstEmail{randomNumber}@email.com";
            string password = $"tstPassword{randomNumber}";
            string username = $"tstUsername{randomNumber}";
            List<IStreamingPlatform> streamingServices = new List<IStreamingPlatform>();

            // Create temporary room owner
            roomOwner = this._userService.CreateNewUserAsync(email, password, username).Result;

            this._userService.DeleteUser(roomOwner.Id);

            // Act & Assert
            Assert.Throws<ArgumentException>(() => this._roomService.CreateRoom(roomOwner, roomName, streamingServices));

        }

        [Test]
        [TestCase("")]
        [TestCase(null)]
        [TestCase("abc")] // Minimum lenght for room name is 4 characters.
        public void CreateRoom_InvalidRoomName_ShouldThrowInvalidArgumentException(string roomNameInput)
        {
            // Arrange
            long randomNumber = new Random().Next(0, 10000);

            IUser roomOwner;
            string roomName = roomNameInput;
            string email = $"tstEmail{randomNumber}@email.com";
            string password = $"tstPassword{randomNumber}";
            string username = $"tstUsername{randomNumber}";
            List<IStreamingPlatform> streamingServices = new List<IStreamingPlatform>();

            // Setup Test StreamingServices
            streamingServices.Add(this._streamingPlatformService.GetStreamingPlatformById(1));

            // Create temporary room owner
            roomOwner = this._userService.CreateNewUserAsync(email, password, username).Result;

            this._userService.DeleteUser(roomOwner.Id);

            // Act & Assert
            Assert.Throws<ArgumentException>(() => this._roomService.CreateRoom(roomOwner, roomName, streamingServices));
        }

        [Test]
        public async Task GetRoom_ValidID_ShouldReturnIRoomObject()
        {
            // Arrange
            long randomNumber = new Random().Next(0, 10000);

            IRoom createdRoom;
            IRoom requestedRoom;
            IUser roomOwner;
            string roomName = $"tstName{randomNumber}";
            string email = $"tstEmail{randomNumber}@email.com";
            string password = $"tstPassword{randomNumber}";
            string username = $"tstUsername{randomNumber}";
            List<IStreamingPlatform> streamingServices = new List<IStreamingPlatform>();

            // Setup Test StreamingServices
            streamingServices.Add(this._streamingPlatformService.GetStreamingPlatformById(1));

            // Create temporary DB objects
            roomOwner = await this._userService.CreateNewUserAsync(email, password, username);
            createdRoom = await this._roomService.CreateRoomAsync(roomOwner, roomName, streamingServices);

            // Act
            requestedRoom = this._roomService.GetRoom(createdRoom.Id);

            // Assert
            Assert.IsNotNull(requestedRoom);
            Assert.IsNotNull(requestedRoom.Id);
            Assert.IsNotNull(requestedRoom.Owner);
            Assert.IsNotNull(requestedRoom.StreamingServices);
            Assert.IsNotEmpty(requestedRoom.StreamingServices);
        }

        [Test]
        public async Task GetRoomByRoomKey_ValidKey_ShouldReturnIRoomObject()
        {
            // Arrange
            long randomNumber = new Random().Next(0, 10000);

            IRoom createdRoom;
            IRoom requestedRoom;
            IUser roomOwner;
            string roomName = $"tstName{randomNumber}";
            string email = $"tstEmail{randomNumber}@email.com";
            string password = $"tstPassword{randomNumber}";
            string username = $"tstUsername{randomNumber}";
            List<IStreamingPlatform> streamingServices = new List<IStreamingPlatform>();

            // Setup Test StreamingServices
            streamingServices.Add(this._streamingPlatformService.GetStreamingPlatformById(1));

            // Create temporary DB objects
            roomOwner = await this._userService.CreateNewUserAsync(email, password, username);
            createdRoom = await this._roomService.CreateRoomAsync(roomOwner, roomName, streamingServices);

            // Act
            requestedRoom = this._roomService.GetRoomByRoomKey(createdRoom.RoomKey);

            // Assert
            Assert.IsNotNull(requestedRoom);
            Assert.IsNotNull(requestedRoom.Id);
            Assert.IsNotNull(requestedRoom.Owner);
            Assert.IsNotNull(requestedRoom.StreamingServices);
            Assert.IsNotEmpty(requestedRoom.StreamingServices);
        }


    }
}

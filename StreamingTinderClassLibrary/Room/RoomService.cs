using StreamingTinderClassLibrary.Rooms.DataAccess;
using StreamingTinderClassLibrary.StreamingService;
using StreaminTinderClassLibrary.Users;
using StreamingTinderClassLibrary.Rooms.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using StreamingTinderClassLibrary.Rooms.KeyGenerator;

namespace StreamingTinderClassLibrary.Rooms
{
    /// <summary>
    /// Facade class used for managing Rooms
    /// </summary>
    internal class RoomService : IRoomService
    {
        private readonly IRoomDAO _roomDAO;

        public RoomService(IRoomDAO roomDAO)
        {
            this._roomDAO = roomDAO;
        }

        public bool AddStreamingServiceToRoom(IRoom room, IStreamingPlatform service)
        {
            throw new NotImplementedException();
        }

        public Task<bool> AddStreamingServiceToRoomAsync(IRoom room, IStreamingPlatform service)
        {
            throw new NotImplementedException();
        }

        public bool AddUserToRoom(IRoom room, IUser user)
        {
            throw new NotImplementedException();
        }

        public Task<bool> AddUserToRoomAsync(IRoom room, IUser user)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Creates a new room, based on provided parameters.
        /// Throws exception if values are invalid.
        /// </summary>
        /// <param name="owner">IUser object - Owner of the newly created room.</param>
        /// <param name="roomName">Requested room name.</param>
        /// <param name="services">List of streamingplatforms provided initially</param>
        /// <returns>Newly created room</returns>
        public IRoom CreateRoom(IUser owner, string roomName, List<IStreamingPlatform> services)
        {
            // Null checks
            if (owner == null) throw new ArgumentNullException("owner", "Owner must not be null.");
            if (services == null) throw new ArgumentNullException("services", "Services must not be null.");
            if (string.IsNullOrEmpty(roomName) || string.IsNullOrWhiteSpace(roomName)) throw new ArgumentNullException("roomName", "RoomName must contain a value.");

            // Value checks
            if (owner.Id == 0) throw new ArgumentException("Owner does not contain a valid Id.", "owner");
            if (services.Count < 1) throw new ArgumentException("Services must contain at least 1 streaming platform.", "services");

            // Fields and values
            IRoom roomToBeCreated;
            string roomKey;

            // Create room key.
            IKeyGenerator keyGenerator = KeyGeneratorFactory.GetKeyGenerator();
            roomKey = keyGenerator.GenerateKey();

            // Create room object.
            roomToBeCreated = new Room(roomName, owner, roomKey, services);

            // Generate room object on DAO
            roomToBeCreated = this._roomDAO.Create(roomToBeCreated);

            // Return created room
            return roomToBeCreated;
        }

        public Task<IRoom> CreateRoomAsync(IUser owner, string roomName, List<IStreamingPlatform> services)
        {
            throw new NotImplementedException();
        }

        public IRoom GetRoom(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IRoom> GetRoomAsync(int id)
        {
            throw new NotImplementedException();
        }

        public IRoom GetRoomByRoomKey(string roomKey)
        {
            throw new NotImplementedException();
        }

        public Task<IRoom> GetRoomByRoomKeyAsync(string roomKey)
        {
            throw new NotImplementedException();
        }

        public List<IRoom> GetUserRoomAccess(IUser user)
        {
            throw new NotImplementedException();
        }

        public Task<List<IRoom>> GetUserRoomAccessAsync(IUser user)
        {
            throw new NotImplementedException();
        }

        public List<IRoom> GetUserRoomsOwned(IUser user)
        {
            throw new NotImplementedException();
        }

        public Task<List<IRoom>> GetUserRoomsOwnedAsync(IUser user)
        {
            throw new NotImplementedException();
        }
    }
}

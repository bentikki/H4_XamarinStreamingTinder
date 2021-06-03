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

            // Value checks
            if (string.IsNullOrEmpty(roomName) || string.IsNullOrWhiteSpace(roomName)) throw new ArgumentException("roomName", "RoomName must contain a value.");
            if (owner.Id == 0) throw new ArgumentException("Owner does not contain a valid Id.", "owner");
            if (services.Count < 1) throw new ArgumentException("Services must contain at least 1 streaming platform.", "services");
            if (roomName.Length < 4) throw new ArgumentException("roomName", "RoomName must be atleast 4 characters long.");


            // Fields and values
            IRoom roomToBeCreated;
            string roomKey;

            // Create room key.
            IKeyGenerator keyGenerator = KeyGeneratorFactory.GetKeyGenerator();
            bool keyIsUnique = false;

            // Check that key is unique
            // Retry if the key is not unique
            do
            {
                // Generate key
                roomKey = keyGenerator.GenerateKey();

                // Check if the generated key exists.
                keyIsUnique = this._roomDAO.ValidateRoomKey(roomKey);

            } while (keyIsUnique);

            // Create room object.
            roomToBeCreated = new Room(roomName, owner, roomKey, services);

            // Generate room object on DAO
            roomToBeCreated = this._roomDAO.Create(roomToBeCreated);

            // Return created room
            return roomToBeCreated;
        }

        /// <summary>
        /// Creates a new room, based on provided parameters.
        /// Throws exception if values are invalid.
        /// </summary>
        /// <param name="owner">IUser object - Owner of the newly created room.</param>
        /// <param name="roomName">Requested room name.</param>
        /// <param name="services">List of streamingplatforms provided initially</param>
        /// <returns>Newly created room</returns>
        public async Task<IRoom> CreateRoomAsync(IUser owner, string roomName, List<IStreamingPlatform> services)
        {
            var task = Task.Run(() => this.CreateRoom(owner, roomName, services));

            return await task;
        }

        /// <summary>
        /// Gets Room object matching the provided ID.
        /// </summary>
        /// <param name="id">ID of requested Room</param>
        /// <returns>Room matching the provided ID.</returns>
        public IRoom GetRoom(int id)
        {
            // Value checks
            if (id == 0) throw new ArgumentException("Id is not valid.", "id");

            // Get the requested room from DAO.
            IRoom requestedRoom = this._roomDAO.Get(id);

            return requestedRoom;
        }

        /// <summary>
        /// Gets Room object matching the provided ID.
        /// </summary>
        /// <param name="id">ID of requested Room</param>
        /// <returns>Room matching the provided ID.</returns>
        public async Task<IRoom> GetRoomAsync(int id)
        {
            var task = Task.Run(() => this.GetRoom(id));

            return await task;
        }

        /// <summary>
        /// Gets Room object matching the provided Room key.
        /// </summary>
        /// <param name="roomKey">Room key of requested Room</param>
        /// <returns>Room matching the provided Room key.</returns>
        public IRoom GetRoomByRoomKey(string roomKey)
        {
            // Value checks
            if (roomKey == null) throw new ArgumentException("RoomKey is not valid.", "roomKey");
            if (string.IsNullOrEmpty(roomKey) || string.IsNullOrWhiteSpace(roomKey)) throw new ArgumentException("RoomKey must contain a value.", "roomKey");

            // Get the requested room from DAO.
            IRoom requestedRoom = this._roomDAO.GetByRoomKey(roomKey);

            return requestedRoom;
        }

        /// <summary>
        /// Gets Room object matching the provided Room key.
        /// </summary>
        /// <param name="roomKey">Room key of requested Room</param>
        /// <returns>Room matching the provided Room key.</returns>
        public async Task<IRoom> GetRoomByRoomKeyAsync(string roomKey)
        {
            var task = Task.Run(() => this.GetRoomByRoomKey(roomKey));

            return await task;
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
    }
}

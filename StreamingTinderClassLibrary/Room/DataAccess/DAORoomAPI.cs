using Newtonsoft.Json;
using StreamingTinderClassLibrary.Api;
using StreamingTinderClassLibrary.Rooms.Models;
using StreamingTinderClassLibrary.StreamingService;
using StreaminTinderClassLibrary.Rooms.Models;
using StreaminTinderClassLibrary.Users;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace StreamingTinderClassLibrary.Rooms.DataAccess
{
    internal class DAORoomAPI : ApiDAOMaster, IRoomDAO
    {
        private readonly string API_BASE = "Rooms";
        private IUserService _userService = ServiceFactory.GetUserService();
        private IStreamingPlatformService _platformService = ServiceFactory.GetStreamingPlatformService();


        public DAORoomAPI(string apidestination) : base(apidestination) { }

        public IRoom Create(IRoom room)
        {
            ApiCreateRoom apiCreateRoom = new ApiCreateRoom();

            apiCreateRoom.Name = room.Name;
            apiCreateRoom.RoomKey = room.RoomKey;
            apiCreateRoom.Owner_FK_Users_Id = room.Owner.Id;
            apiCreateRoom.StreamingPlatformIDs = new List<int>();

            foreach (IStreamingPlatform streamingPlatform in room.StreamingServices)
            {
                apiCreateRoom.StreamingPlatformIDs.Add(streamingPlatform.Id);
            }

            ApiCreateRoom apiRoom = this.PutIUser(apiCreateRoom).Result;

            if (apiRoom == null) throw new ArgumentNullException("apiRoom", "apiRoom must not be null. Invalid response received from API.");

            


            Room returnedRoom = new Room();
            returnedRoom.Id = apiRoom.Id;
            returnedRoom.Name = apiRoom.Name;
            returnedRoom.RoomKey = apiRoom.RoomKey;
            returnedRoom.Owner = _userService.GetUser(apiRoom.Owner_FK_Users_Id);
            returnedRoom.StreamingServices = new List<IStreamingPlatform>();

            foreach (int streamingPlatformID in apiRoom.StreamingPlatformIDs)
            {
                returnedRoom.StreamingServices.Add(_platformService.GetStreamingPlatformById(streamingPlatformID));
            }

            return returnedRoom;
        }

        private async Task<ApiCreateRoom> PutIUser(ApiCreateRoom room)
        {
            var content = JsonConvert.SerializeObject(room);
            HttpClient _client = new HttpClient();

            var httpResponse = await _client.PostAsync(this.apiString + API_BASE, new StringContent(content, Encoding.Default, "application/json"));

            if (!httpResponse.IsSuccessStatusCode)
            {
                throw new Exception("Cannot create a room");
            }

            var createdTask = JsonConvert.DeserializeObject<ApiCreateRoom>(await httpResponse.Content.ReadAsStringAsync());
            return createdTask;

        }

        public IRoom Get(int id)
        {
            IRoom room;
            ApiCreateRoom apiResponse;
            string apiUrlParam = API_BASE + "/" + id;

            try
            {
                apiResponse = apiRequester.GetObjectFromApi<ApiCreateRoom>(apiUrlParam);

                room = this.GetIRoomFromAPICreateResponse(apiResponse);
            }
            catch (Exception e)
            {
                room = null;
            }

            return room;
        }

        public IRoom GetByRoomKey(string roomKey)
        {
            IRoom room;
            ApiCreateRoom apiResponse;
            string apiUrlParam = API_BASE + "/key/" + roomKey;

            try
            {
                apiResponse = apiRequester.GetObjectFromApi<ApiCreateRoom>(apiUrlParam);

                room = this.GetIRoomFromAPICreateResponse(apiResponse);
            }
            catch (Exception e)
            {
                room = null;
            }

            return room;
        }
    

        public IRoom UpdateRoom(IRoom room)
        {
            throw new NotImplementedException();
        }

        private IRoom GetIRoomFromAPICreateResponse(ApiCreateRoom apiResponse)
        {
            Room room = new Room();
            room.Id = apiResponse.Id;
            room.Name = apiResponse.Name;
            room.RoomKey = apiResponse.RoomKey;
            room.Owner = _userService.GetUser(apiResponse.Owner_FK_Users_Id);
            room.StreamingServices = new List<IStreamingPlatform>();

            foreach (int streamingPlatformID in apiResponse.StreamingPlatformIDs)
            {
                room.StreamingServices.Add(_platformService.GetStreamingPlatformById(streamingPlatformID));
            }

            return room;
        }

        public bool ValidateRoomKey(string roomKey)
        {
            bool roomKeyExists;

            string apiUrlParam = $"{API_BASE}/RoomKeyExists/{roomKey}";

            roomKeyExists = apiRequester.GetObjectFromApi<bool>(apiUrlParam);

            return roomKeyExists;
        }
    }
}

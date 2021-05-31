using StreamingTinderClassLibrary.StreamingService;
using StreaminTinderClassLibrary.Users;
using System;
using System.Collections.Generic;
using System.Text;

namespace StreamingTinderClassLibrary.Rooms.Models
{
    public class Room : IRoom
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string RoomKey { get; set; }
        public IUser Owner { get; set; }
        public List<IUser> Members { get; set; }
        public List<IStreamingPlatform> StreamingServices { get; set; }

        public Room(string name, IUser owner, string roomKey, List<IStreamingPlatform> streamingServices)
        {
            Name = name;
            RoomKey = roomKey;
            Owner = owner;
            StreamingServices = streamingServices;
        }

    }
}

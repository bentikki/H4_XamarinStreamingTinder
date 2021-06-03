using System;
using System.Collections.Generic;
using StreamingTinderClassLibrary.StreamingService.Models;
using System.Text;
using StreaminTinderClassLibrary.Users.Models;
using System.Text.Json.Serialization;

namespace StreaminTinderClassLibrary.Rooms.Models
{
    public class ApiCreateRoom
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("roomKey")]
        public string RoomKey { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("owner_FK_Users_Id")]
        public int Owner_FK_Users_Id { get; set; }

        [JsonPropertyName("streamingPlatformIDs")]
        public List<int> StreamingPlatformIDs { get; set; }

        [JsonPropertyName("membersIDs")]
        public List<int> MembersIDs { get; set; }

    }
}

using StreamingTinderWepApi.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StreamingTinderWepApi.Models
{
    public class RoomResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string RoomKey { get; set; }
        public int Owner_FK_Users_Id { get; set; }
        public User Owner { get; set; }
        public List<StreamingPlatformEntity> StreamingPlatforms { get; set; }
    }
}
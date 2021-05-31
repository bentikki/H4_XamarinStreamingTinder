using StreamingTinderClassLibrary.StreamingService;
using StreaminTinderClassLibrary.Users;
using System;
using System.Collections.Generic;
using System.Text;

namespace StreamingTinderClassLibrary.Rooms
{
    public interface IRoom
    {
        int Id { get; }
        string Name { get; }
        string RoomKey { get; }
        IUser Owner { get; }
        List<IUser> Members { get; }
        List<IStreamingPlatform> StreamingServices { get; }
    }
}

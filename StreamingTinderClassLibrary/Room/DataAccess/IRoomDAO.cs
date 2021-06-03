using System;
using System.Collections.Generic;
using System.Text;

namespace StreamingTinderClassLibrary.Rooms.DataAccess
{
    public interface IRoomDAO
    {
        IRoom Get(int id);
        IRoom GetByRoomKey(string roomKey);
        IRoom Create(IRoom room);
        IRoom UpdateRoom(IRoom room);
        bool ValidateRoomKey(string roomKey);
    }
}

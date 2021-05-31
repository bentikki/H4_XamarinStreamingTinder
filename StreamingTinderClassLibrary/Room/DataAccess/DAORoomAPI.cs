using StreamingTinderClassLibrary.Api;
using System;
using System.Collections.Generic;
using System.Text;

namespace StreamingTinderClassLibrary.Rooms.DataAccess
{
    internal class DAORoomAPI : ApiDAOMaster, IRoomDAO
    {
        private readonly string API_BASE = "Rooms";

        public DAORoomAPI(string apidestination) : base(apidestination) { }

        public IRoom Create(IRoom room)
        {
            throw new NotImplementedException();
        }

        public IRoom Get(int id)
        {
            throw new NotImplementedException();
        }

        public IRoom GetByRoomKey(string roomKey)
        {
            throw new NotImplementedException();
        }

        public IRoom UpdateRoom(IRoom room)
        {
            throw new NotImplementedException();
        }
    }
}

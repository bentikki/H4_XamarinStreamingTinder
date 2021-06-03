using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StreamingTinderWepApi.Entities;
using Dapper;
using Dapper.Contrib;
using System.Data.SqlClient;
using Dapper.Contrib.Extensions;
using StreamingTinderWepApi.Models;
using Microsoft.EntityFrameworkCore;
using System.Data;
using StreaminTinderClassLibrary.Hashing;
using StreamingTinderClassLibrary.Rooms.Models;
using StreaminTinderClassLibrary.Rooms.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace StreamingTinderWepApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoomsController : ControllerBase
    {
        private readonly StreaminTinderContext _context;


        public RoomsController(StreaminTinderContext context)
        {
            this._context = context;
        }

        // GET: api/Users
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RoomEntity>>> Get()
        {
            return await _context.Rooms.ToListAsync();
        }

        // GET: api/Users/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ApiCreateRoom>> Get(int id)
        {
            ApiCreateRoom apiCreateRoom = new ApiCreateRoom();
            RoomEntity room = await _context.Rooms.FindAsync(id);

            if (room == null)
            {
                return NotFound();
            }

            apiCreateRoom.Id = room.Id;
            apiCreateRoom.Name = room.Name;
            apiCreateRoom.RoomKey = room.RoomKey;
            apiCreateRoom.Owner_FK_Users_Id = room.Owner_FK_Users_Id;
            apiCreateRoom.StreamingPlatformIDs = new List<int>();

            using (SqlConnection con = new SqlConnection("Data Source=MSI\\SQLExpress;Initial Catalog=StreamingTinder;Integrated Security=SSPI;"))
            {
                using (SqlCommand cmd = new SqlCommand("SelectRoomServices", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@RoomID", SqlDbType.Int).Value = apiCreateRoom.Id;

                    con.Open();
                    var reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        apiCreateRoom.StreamingPlatformIDs.Add(reader.GetInt32("Id"));
                    }
                }
                
            }

            return apiCreateRoom;
        }

        [HttpGet("key/{keyCode}")]
        public async Task<ActionResult<ApiCreateRoom>> Get(string keyCode)
        {
            ApiCreateRoom apiCreateRoom = new ApiCreateRoom();
            RoomEntity room = _context.Rooms.Where(x => x.RoomKey == keyCode).FirstOrDefault();

            if (room == null)
            {
                return NotFound();
            }

            apiCreateRoom.Id = room.Id;
            apiCreateRoom.Name = room.Name;
            apiCreateRoom.RoomKey = room.RoomKey;
            apiCreateRoom.Owner_FK_Users_Id = room.Owner_FK_Users_Id;
            apiCreateRoom.StreamingPlatformIDs = new List<int>();

            using (SqlConnection con = new SqlConnection("Data Source=MSI\\SQLExpress;Initial Catalog=StreamingTinder;Integrated Security=SSPI;"))
            {
                using (SqlCommand cmd = new SqlCommand("SelectRoomServices", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@RoomID", SqlDbType.Int).Value = apiCreateRoom.Id;

                    con.Open();
                    var reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        apiCreateRoom.StreamingPlatformIDs.Add(reader.GetInt32("Id"));
                    }
                }

            }

            return apiCreateRoom;
        }


        [HttpPost]
        public async Task<ActionResult<ApiCreateRoom>> Post([FromBody] ApiCreateRoom room)
        {
            ApiCreateRoom createdRoom = new ApiCreateRoom();
            RoomEntity roomEntity = new RoomEntity();

            roomEntity.Name = room.Name;
            roomEntity.RoomKey = room.RoomKey;
            roomEntity.Owner_FK_Users_Id = room.Owner_FK_Users_Id;

            var createdRoomEntity = this._context.Rooms.Add(roomEntity).Entity;
            this._context.SaveChanges();

            using (SqlConnection con = new SqlConnection("Data Source=MSI\\SQLExpress;Initial Catalog=StreamingTinder;Integrated Security=SSPI;"))
            {
                foreach (var platformID in room.StreamingPlatformIDs)
                {
                    using (SqlCommand cmd = new SqlCommand("AddStreamingServiceToRoom", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@roomID", SqlDbType.Int).Value = createdRoomEntity.Id;
                        cmd.Parameters.Add("@StreamingServiceID", SqlDbType.Int).Value = platformID;

                        con.Open();
                        cmd.ExecuteScalar();
                        con.Close();
                    }
                }
            }



            createdRoom.Id = createdRoomEntity.Id;
            createdRoom.Name = createdRoomEntity.Name;
            createdRoom.RoomKey = createdRoomEntity.RoomKey;
            createdRoom.Owner_FK_Users_Id = createdRoomEntity.Owner_FK_Users_Id;
            createdRoom.StreamingPlatformIDs = room.StreamingPlatformIDs;

            return createdRoom;
        }

        [HttpGet]
        [Route("RoomKeyExists/{keyCode}")]
        public async Task<ActionResult<bool>> RoomKeyExists(string keyCode)
        {
            RoomEntity matchingRoom = null;
            matchingRoom = _context.Rooms.Where(x => x.RoomKey == keyCode).FirstOrDefault();

            return matchingRoom != null;
        }
    }
}

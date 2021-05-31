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
        public async Task<ActionResult<IEnumerable<User>>> Get()
        {
            return await _context.Users.ToListAsync();
        }

        // GET: api/Users/5
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> Get(int id)
        {
            var movie = await _context.Users.FindAsync(id);

            if (movie == null)
            {
                return NotFound();
            }

            return movie;
        }

        [HttpGet("email/{value}")]
        public async Task<ActionResult<User>> GetByEmail(string value)
        {
            var user = _context.Users.Where(x => x.Email == value).FirstOrDefault();

            if (user == null)
            {
                return NotFound();
            }

            return user;
        }

        [HttpPost]
        public async Task<ActionResult<User>> Post([FromBody] Room room)
        {
            User newlyCreatedUser;
            int createdUserId = 0;

            using (SqlConnection con = new SqlConnection("Data Source=MSI\\SQLExpress;Initial Catalog=StreamingTinder;Integrated Security=SSPI;"))
            {
                using (SqlCommand cmd = new SqlCommand("CreateNewUser", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add("@Username", SqlDbType.VarChar).Value = authUser.Username;
                    cmd.Parameters.Add("@Email", SqlDbType.VarChar).Value = authUser.Email;
                    cmd.Parameters.Add("@Password", SqlDbType.VarChar).Value = authUser.Password;
                    cmd.Parameters.Add("@Salt", SqlDbType.VarChar).Value = authUser.Salt;

                    con.Open();
                    createdUserId = (int)cmd.ExecuteScalar();
                }
            }

            newlyCreatedUser = _context.Users.FindAsync(createdUserId).Result;

            return newlyCreatedUser;
        }



        // GET: api/Users/Delete/5
        [HttpGet("Delete/{id}")]
        public async Task<ActionResult<bool>> Delete(int id)
        {
            var user = await _context.Users.FindAsync(id);

            if (user != null)
            {

                using (SqlConnection con = new SqlConnection("Data Source=MSI\\SQLExpress;Initial Catalog=StreamingTinder;Integrated Security=SSPI;"))
                {
                    using (SqlCommand cmd = new SqlCommand("DeleteUser", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@ID", SqlDbType.Int).Value = user.Id;

                        con.Open();
                        cmd.ExecuteScalar();
                    }
                }

                return true;
            }
            
            return false;
        }

    }
}

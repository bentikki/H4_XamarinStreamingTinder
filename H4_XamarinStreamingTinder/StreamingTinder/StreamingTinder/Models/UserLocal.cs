using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace StreamingTinder.Models
{
    public class UserLocal
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        public UserLocal() { }

        public UserLocal(string email, string password)
        {
            Email = email;
            Password = password;
        }
    }
}

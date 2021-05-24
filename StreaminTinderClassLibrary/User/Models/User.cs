using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace StreaminTinderClassLibrary.Users.Models
{
    public class User : IUser
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("username")]
        public string Username { get; set; }

        [JsonPropertyName("email")]
        public string Email { get; set; }
        public string Password { get; set; }
        public string Salt { get; set; }
    }
}

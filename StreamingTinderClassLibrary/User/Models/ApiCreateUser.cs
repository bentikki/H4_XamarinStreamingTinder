using System;
using System.Collections.Generic;

using System.Text;

namespace StreaminTinderClassLibrary.Users.Models
{
    class ApiCreateUser
    {
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Salt { get; set; }
    }
}

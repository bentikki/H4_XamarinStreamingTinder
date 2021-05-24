using System;
using System.Collections.Generic;
using System.Text;

namespace StreaminTinderClassLibrary.Users
{
    public interface IUser
    {
        int Id { get; set; }
        string Username { get; set; }
        string Email { get; set; }
        string Password { get; set; }
        string Salt { get; set; }
    }
}

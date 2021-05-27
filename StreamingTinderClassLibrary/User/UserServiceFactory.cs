using StreaminTinderClassLibrary.Users;
using System;
using System.Collections.Generic;
using System.Text;

namespace StreaminTinderClassLibrary.Users
{
    public class UserServiceFactory
    {
        public static IUserService GetUserService()
        {
            string apiKey = @"https://52298d8798b4.ngrok.io";
            return new UserService(new DAOUserAPI(apiKey + "/api/"));
        }
    }
}

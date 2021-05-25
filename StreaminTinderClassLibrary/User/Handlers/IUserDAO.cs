using System;
using System.Collections.Generic;
using System.Text;

namespace StreaminTinderClassLibrary.Users.Handlers
{
    interface IUserDAO
    {
        IUser Get(int id);
        IUser GetByString(string columnName, string value);
        IUser Create(IUser user);
        IUser Update(IUser user);
        bool VerifyUserLogin(IUser user);
        bool Delete(int id);
    }
}

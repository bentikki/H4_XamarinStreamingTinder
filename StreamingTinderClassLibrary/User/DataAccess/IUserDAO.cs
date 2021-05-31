using System;
using System.Collections.Generic;
using System.Text;

namespace StreaminTinderClassLibrary.Users.DataAccess
{
    public interface IUserDAO
    {
        IUser Get(int id);
        IUser GetByString(string columnName, string value);
        IUser Create(IUser user);
        bool VerifyUserLogin(IUser user);
        bool Delete(int id);
    }
}

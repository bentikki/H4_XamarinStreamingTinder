using StreaminTinderClassLibrary.Users;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace StreaminTinderClassLibrary.Users
{
    public interface IUserService
    {
        IUser GetUser(int id);
        IUser CurrentUser { get; }
        Task<IUser> GetUserAsync(int id);
        IUser CreateNewUser(IUser user);
        Task<IUser> CreateNewUserAsync(IUser user);
        Task<IUser> CreateNewUserAsync(string email, string password, string username);
        bool VerifyUser(IUser user);
        Task<bool> VerifyUserAsync(IUser user);
        bool VerifyUser(string username, string password);
        Task<bool> VerifyUserAsync(string username, string password);
        bool LoginUser(string username, string password);
        Task<bool> LoginUserAsync(string username, string password);
        IUser GetUserByEmail(string email);
        Task<IUser> GetUserByEmailAsync(string email);
        bool DeleteUser(int id);
        Task<bool> DeleteUserAsync(int id);
    }
}

using StreaminTinderClassLibrary.Hashing;
using System;
using System.Collections.Generic;
using System.Text;

namespace StreaminTinderClassLibrary.Authentication.Models
{
    interface IHashingMethod
    {
        byte[] HashPasswordWithSalt(byte[] toBeHashed, byte[] salt);
        string GetHashedPasswordString(string inputPassword, string salt64string);
        IHashedUser HashUserObject(IHashedUser userToHash);
    }
}

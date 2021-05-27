using StreaminTinderClassLibrary.Authentication.Handlers;
using StreaminTinderClassLibrary.Authentication.Models;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace StreaminTinderClassLibrary.Authentication.Handlers.HashingMethods
{
    public class HashingSHA256 : HashingMaster, IHashingMethod
    {
        public HashingSHA256(int numberOfIterations) : base(numberOfIterations)
        {
        }

        public override byte[] HashPasswordWithSalt(byte[] toBeHashed, byte[] salt)
        {
            toBeHashed = this.HashPasswordWithKeyDerivation(toBeHashed, salt, this._numberOfIterations);

            using (var sha256 = SHA256.Create())
            {
                return sha256.ComputeHash(Combine(toBeHashed, salt));
            }
        }
    }
}

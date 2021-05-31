using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StreamingTinderClassLibrary.Rooms.KeyGenerator
{
    /// <summary>
    /// Generates a random string based on allowed characters.
    /// Uses Linq to generate string.
    /// </summary>
    class SimpleKeyGenerator : IKeyGenerator
    {
        private Random random = new Random();
        private int _keyLength;

        public SimpleKeyGenerator(int keyLength)
        {
            this._keyLength = keyLength;
        }


        /// <summary>
        /// Generates random string based on allowed characters.
        /// Requires a string lenght to be provided.
        /// </summary>
        /// <param name="keyLength">Length of requested random string.</param>
        /// <returns>Random string of requested length.</returns>
        public string GenerateKey(int keyLength = 0)
        {
            if (keyLength == 0)
            {
                keyLength = this._keyLength;
            }

            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789_";
            return new string(Enumerable.Repeat(chars, keyLength).Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }
}

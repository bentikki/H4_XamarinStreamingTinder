using System;
using System.Collections.Generic;
using System.Text;

namespace StreamingTinderClassLibrary.Rooms.KeyGenerator
{
    internal interface IKeyGenerator
    {
        string GenerateKey(int keyLength = 0);
    }
}

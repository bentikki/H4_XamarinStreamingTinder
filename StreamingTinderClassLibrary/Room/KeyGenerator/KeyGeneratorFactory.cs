using System;
using System.Collections.Generic;
using System.Text;

namespace StreamingTinderClassLibrary.Rooms.KeyGenerator
{
    internal static class KeyGeneratorFactory
    {
        public static IKeyGenerator GetKeyGenerator()
        {
            return new SimpleKeyGenerator(12);
        }
    }
}

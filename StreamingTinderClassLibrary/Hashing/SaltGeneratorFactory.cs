using StreaminTinderClassLibrary.Authentication.Handlers.SaltGenerators;
using StreaminTinderClassLibrary.Authentication.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace StreaminTinderClassLibrary.Authentication
{
    /// <summary>
    /// Returns salt generator in use
    /// </summary>
    static class SaltGeneratorFactory
    {
        public static ISaltGenerator GetSaltGenerator() => new SaltGeneratorRNGCryptoService();
    }
}

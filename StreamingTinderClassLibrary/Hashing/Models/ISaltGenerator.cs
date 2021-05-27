using System;
using System.Collections.Generic;
using System.Text;

namespace StreaminTinderClassLibrary.Authentication.Models
{
    public interface ISaltGenerator
    {
        byte[] GenerateSalt(int saltLength);
    }
}

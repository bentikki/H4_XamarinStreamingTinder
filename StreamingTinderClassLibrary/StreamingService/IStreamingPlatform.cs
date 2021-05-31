using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace StreamingTinderClassLibrary.StreamingService
{
    public interface IStreamingPlatform
    {
        int Id { get; }
        string Name { get; }
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace StreamingTinderClassLibrary.StreamingService.Models
{
    public class StreamingPlatform : IStreamingPlatform
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }
    }
}

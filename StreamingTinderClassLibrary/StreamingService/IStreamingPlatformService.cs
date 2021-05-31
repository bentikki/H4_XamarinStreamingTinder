using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace StreamingTinderClassLibrary.StreamingService
{
    public interface IStreamingPlatformService
    {
        List<IStreamingPlatform> GetStreamingPlatforms();
        Task<List<IStreamingPlatform>> GetStreamingPlatformsAsync();
        IStreamingPlatform GetStreamingPlatformById(int id);
        Task<IStreamingPlatform> GetStreamingPlatformByIdAsync(int id);
        IStreamingPlatform GetStreamingPlatformByName(string name);
        Task<IStreamingPlatform> GetStreamingPlatformByNameAsync(string name);
    }
}

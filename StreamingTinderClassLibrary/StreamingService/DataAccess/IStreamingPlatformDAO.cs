using System;
using System.Collections.Generic;
using System.Text;

namespace StreamingTinderClassLibrary.StreamingService.DataAccess
{
    public interface IStreamingPlatformDAO
    {
        List<IStreamingPlatform> GetAll();
        IStreamingPlatform Get(int id);
        IStreamingPlatform GetByName(string name);
    }
}

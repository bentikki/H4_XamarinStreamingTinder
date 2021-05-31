using StreamingTinderClassLibrary.Api;
using StreamingTinderClassLibrary.StreamingService.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace StreamingTinderClassLibrary.StreamingService.DataAccess
{
    internal class DAOStreamingPlatformAPI : ApiDAOMaster, IStreamingPlatformDAO
    {
        private readonly string API_BASE = "StreamingPlatforms";

        public DAOStreamingPlatformAPI(string apidestination) : base(apidestination) { }

        public List<IStreamingPlatform> GetAll()
        {
            List<IStreamingPlatform> streamingPlatforms = new List<IStreamingPlatform>();

            string apiUrlParam = API_BASE;
            streamingPlatforms.AddRange(apiRequester.GetObjectFromApi<List<StreamingPlatform>>(apiUrlParam));

            return streamingPlatforms;
        }

        public IStreamingPlatform Get(int id)
        {
            IStreamingPlatform streamingPlatform;
            string apiUrlParam = API_BASE + "/" + id;
            
            try
            {
                streamingPlatform = apiRequester.GetObjectFromApi<StreamingPlatform>(apiUrlParam);
            }
            catch (Exception e)
            {
                streamingPlatform = null;
            }

            return streamingPlatform;
        }

        public IStreamingPlatform GetByName(string name)
        {
            IStreamingPlatform streamingPlatform;

            string apiUrlParam = $"{API_BASE}/name/{name}";

            try
            {
                streamingPlatform = apiRequester.GetObjectFromApi<StreamingPlatform>(apiUrlParam);
            }
            catch (Exception e)
            {
                streamingPlatform = null;
            }

            return streamingPlatform;
        }
    }
}

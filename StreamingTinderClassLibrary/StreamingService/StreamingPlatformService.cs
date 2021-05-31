using StreamingTinderClassLibrary.StreamingService.DataAccess;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace StreamingTinderClassLibrary.StreamingService
{
    /// <summary>
    /// Facade handling class for streaming platforms.
    /// Used for getting all available, or specific by ID or Name.
    /// </summary>
    internal class StreamingPlatformService : IStreamingPlatformService
    {
        private IStreamingPlatformDAO _streamingPlatformDAO;

        /// <summary>
        /// Construct with IStreamingPlatformDAO
        /// </summary>
        /// <param name="streamingPlatformDAO">IStreamingPlatformDAO to be used for DAO operations</param>
        public StreamingPlatformService(IStreamingPlatformDAO streamingPlatformDAO)
        {
            this._streamingPlatformDAO = streamingPlatformDAO;
        }

        /// <summary>
        /// Get all available Streaming Platforms
        /// </summary>
        /// <returns>List<IStreamingPlatform> of available streaming platforms</returns>
        public List<IStreamingPlatform> GetStreamingPlatforms()
        {
            List<IStreamingPlatform> streamingPlatforms = new List<IStreamingPlatform>();

            streamingPlatforms = this._streamingPlatformDAO.GetAll();

            return streamingPlatforms;
        }

        /// <summary>
        /// Get all available Streaming Platforms ASYNC
        /// </summary>
        /// <returns>List<IStreamingPlatform> of available streaming platforms</returns>
        public async Task<List<IStreamingPlatform>> GetStreamingPlatformsAsync()
        {
            var task = Task.Run(() => this.GetStreamingPlatforms());

            return await task;
        }

        /// <summary>
        /// Returns IStreamingPlatform object matching the provided ID.
        /// Returns null if ID does not exist.
        /// </summary>
        /// <param name="id">ID of requested IStreamingPlatform</param>
        /// <returns>IStreamingPlatform matching the requested ID</returns>
        public IStreamingPlatform GetStreamingPlatformById(int id)
        {
            // Value check
            if (id <= 0) throw new ArgumentException("id must not be below 1", "id");

            IStreamingPlatform platform;

            // Get from DAO
            platform = this._streamingPlatformDAO.Get(id);

            return platform;
        }

        /// <summary>
        /// Returns IStreamingPlatform object matching the provided ID.
        /// Returns null if ID does not exist.
        /// </summary>
        /// <param name="id">ID of requested IStreamingPlatform</param>
        /// <returns>IStreamingPlatform matching the requested ID</returns>
        public async Task<IStreamingPlatform> GetStreamingPlatformByIdAsync(int id)
        {
            var task = Task.Run(() => this.GetStreamingPlatformById(id));

            return await task;
        }

        /// <summary>
        /// Returns IStreamingPlatform object matching the provided Name.
        /// Returns null if Name does not exist.
        /// </summary>
        /// <param name="id">Name of requested IStreamingPlatform</param>
        /// <returns>IStreamingPlatform matching the requested Name</returns>
        public IStreamingPlatform GetStreamingPlatformByName(string name)
        {
            // Value check
            if (name == null) throw new ArgumentException("Name must not be null", "name");
            if (string.IsNullOrEmpty(name) || string.IsNullOrWhiteSpace(name)) throw new ArgumentException("Name must contain a value", "name");

            IStreamingPlatform platform;

            // Get from DAO
            platform = this._streamingPlatformDAO.GetByName(name);

            return platform;
        }

        /// <summary>
        /// Returns IStreamingPlatform object matching the provided Name.
        /// Returns null if Name does not exist.
        /// </summary>
        /// <param name="id">Name of requested IStreamingPlatform</param>
        /// <returns>IStreamingPlatform matching the requested Name</returns>
        public async Task<IStreamingPlatform> GetStreamingPlatformByNameAsync(string name)
        {
            var task = Task.Run(() => this.GetStreamingPlatformByName(name));

            return await task;
        }

    }
}

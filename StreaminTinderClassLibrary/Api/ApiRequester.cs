using StreaminTinderClassLibrary.Users.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace StreaminTinderClassLibrary.Api
{
    class ApiRequester
    {
        private readonly string API_PATH = "https://localhost:44346/api/";
        private readonly HttpClient client = new HttpClient();

        public async Task<System.IO.Stream> ProcessAPI(string urlParams)
        {
            client.DefaultRequestHeaders.Accept.Clear();

            var streamTask = client.GetStreamAsync(API_PATH + urlParams);

            return await streamTask;
        }

        public T GetObjectFromApi<T>(string apiUrlParam)
        {
            var streamTask = this.ProcessAPI(apiUrlParam).Result;
            return JsonSerializer.DeserializeAsync<T>(streamTask).Result;
        }
    }
}

using StreaminTinderClassLibrary.Users.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace StreaminTinderClassLibrary.Api
{
    public class ApiRequester
    {
        private readonly string API_PATH;
        private readonly int TIMEOUT_WAIT = 10000;
        private readonly HttpClient client = new HttpClient();

        public ApiRequester(string api_path)
        {
            this.API_PATH = api_path;
        }

        public async Task<System.IO.Stream> ProcessAPI(string urlParams)
        {
            client.DefaultRequestHeaders.Accept.Clear();


            CancellationTokenSource cancellationToken = new CancellationTokenSource();

            try
            {
                cancellationToken.CancelAfter(this.TIMEOUT_WAIT);

                var streamTask = client.GetStreamAsync(this.API_PATH + urlParams);

                return await streamTask;
            }
            catch (System.Net.Http.HttpRequestException e)
            {
                throw new Exception("Could not connect to API. Site could not be reached", e);
            }
            catch (TaskCanceledException e)
            {
                throw new Exception("Could not connect to API. The request timed out.", e);
            }
            catch (Exception e)
            {
                throw new Exception("Could not connect to API. An unexpected error occured.", e);

            }
            finally
            {
                cancellationToken.Dispose();
            }
        }

        public T GetObjectFromApi<T>(string apiUrlParam)
        {
            var streamTask = this.ProcessAPI(apiUrlParam).Result;
            return JsonSerializer.DeserializeAsync<T>(streamTask).Result;
        }
    }
}

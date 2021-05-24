using Newtonsoft.Json;
using StreaminTinderClassLibrary.Api;
using StreaminTinderClassLibrary.Users.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace StreaminTinderClassLibrary.Users.Handlers
{
    class DAOUserAPI : IUserDAO
    {
        private ApiRequester apiRequester = new ApiRequester();

        public IUser Create(IUser user)
        {
            ApiCreateUser apiUser = new ApiCreateUser();

            apiUser.Email = user.Email;
            apiUser.Username = user.Username;
            apiUser.Password = user.Password;
            apiUser.Salt = user.Salt;

            IUser returnUser;
            returnUser = this.PutIUser(apiUser).Result;

            return returnUser;
        }

        private async Task<IUser> PutIUser(ApiCreateUser apiUser)
        {
            var content = JsonConvert.SerializeObject(apiUser);
            HttpClient _client = new HttpClient();

            var httpResponse = await _client.PostAsync("https://localhost:44346/api/users", new StringContent(content, Encoding.Default, "application/json"));

            if (!httpResponse.IsSuccessStatusCode)
            {
                throw new Exception("Cannot add a todo task");
            }

            var createdTask = JsonConvert.DeserializeObject<User>(await httpResponse.Content.ReadAsStringAsync());
            return createdTask;

        }

        public IUser Get(int id)
        {
            IUser user = new User();

            string apiUrlParam = "users/" + id;

            user = apiRequester.GetObjectFromApi<User>(apiUrlParam);
            return user;
        }

        public IUser GetByString(string columnName, string value)
        {
            if (columnName.ToLower() != "email" && columnName.ToLower() != "username") throw new ArgumentException("Column name is invalid", "columnName");

            IUser user;

            string apiUrlParam = $"users/{columnName}/{value}";

            user = apiRequester.GetObjectFromApi<User>(apiUrlParam);
            return user;
        }

        public IUser Update(IUser user)
        {
            throw new NotImplementedException();
        }


    }
}

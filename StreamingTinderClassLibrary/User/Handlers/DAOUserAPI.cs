using Newtonsoft.Json;
using StreaminTinderClassLibrary.Api;
using StreaminTinderClassLibrary.Users.Handlers;
using StreaminTinderClassLibrary.Users.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace StreaminTinderClassLibrary.Users
{
    public class DAOUserAPI : IUserDAO
    {
        private ApiRequester apiRequester;
        private string apiString;

        public DAOUserAPI(string apidestination)
        {
            this.apiString = apidestination;
            this.apiRequester = new ApiRequester(apidestination);
        }

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

            var httpResponse = await _client.PostAsync(this.apiString + "users", new StringContent(content, Encoding.Default, "application/json"));

            if (!httpResponse.IsSuccessStatusCode)
            {
                throw new Exception("Cannot create a user");
            }

            var createdTask = JsonConvert.DeserializeObject<User>(await httpResponse.Content.ReadAsStringAsync());
            return createdTask;

        }

        public IUser Get(int id)
        {
            IUser user;

            string apiUrlParam = "users/" + id;

            user = apiRequester.GetObjectFromApi<User>(apiUrlParam);
            return user;
        }

        public IUser GetByString(string columnName, string value)
        {
            if (columnName.ToLower() != "email" && columnName.ToLower() != "username") throw new ArgumentException("Column name is invalid", "columnName");

            IUser user;

            string apiUrlParam = $"users/{columnName}/{value}";

            try
            {
                user = apiRequester.GetObjectFromApi<User>(apiUrlParam);
            }
            catch (Exception e)
            {
                user = null;
            }

            return user;
        }

        public IUser Update(IUser user)
        {
            throw new NotImplementedException();
        }

        public bool Delete(int id)
        {
            bool deletedSuccess;

            string apiUrlParam = "users/delete/" + id;

            deletedSuccess = apiRequester.GetObjectFromApi<bool>(apiUrlParam);

            return deletedSuccess;
        }

        public bool VerifyUserLogin(IUser user)
        {
            ApiCreateUser apiUser = new ApiCreateUser();

            apiUser.Email = user.Email;
            apiUser.Password = user.Password;

            bool userVerfied;
            userVerfied = this.APIVerfiyUser(apiUser).Result;

            return userVerfied;
        }

        private async Task<bool> APIVerfiyUser(ApiCreateUser apiUser)
        {
            var content = JsonConvert.SerializeObject(apiUser);
            HttpClient _client = new HttpClient();

            var httpResponse = await _client.PostAsync(this.apiString + "users/verify", new StringContent(content, Encoding.Default, "application/json"));

            if (!httpResponse.IsSuccessStatusCode)
            {
                throw new Exception("An error occured. Cannot verfiy user");
            }

            var createdTask = JsonConvert.DeserializeObject<bool>(await httpResponse.Content.ReadAsStringAsync());
            return createdTask;

        }
    }
}

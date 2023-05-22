using ExerciseProject.Core.Contracts;
using ExerciseProject.Core.Models.Accounts;
using Newtonsoft.Json;
using System.Text;

namespace ExerciseProject.Core.Services
{
    public class UserService : IUserService
    {
        private readonly HttpClient httpClient;
        private readonly string BaseUrl = "https://localhost:7072";

        public UserService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<bool> RegisterUser(RegisterViewModel user)
        {
            var userCreated = false;

            var serializedUser = JsonConvert.SerializeObject(user);
            var content = new StringContent(serializedUser, Encoding.UTF8, "application/json");
            var response = await this.httpClient.PostAsync($"{BaseUrl}/Users/Create", content);

            if (response.IsSuccessStatusCode)
            {
                string responseData = await response.Content.ReadAsStringAsync();
                userCreated = JsonConvert.DeserializeObject<bool>(responseData);
            }
            else
            {
                throw new InvalidOperationException();
            }

            return userCreated;
        }

        public async Task<int> UserExists(LoginViewModel user)
        {
            var userId = -1;

            var serializedUser = JsonConvert.SerializeObject(user);
            var content = new StringContent(serializedUser, Encoding.UTF8, "application/json");
            var response = await this.httpClient.PostAsync($"{BaseUrl}/Users/ValidateUser", content);

            if (response.IsSuccessStatusCode)
            {
                string responseData = await response.Content.ReadAsStringAsync();
                userId = JsonConvert.DeserializeObject<int>(responseData);
            }
            else
            {
                throw new InvalidOperationException();
            }

            return userId;
        }
    }
}
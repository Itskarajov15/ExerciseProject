using ExerciseProject.Core.Contracts;
using ExerciseProject.Core.Models.Contragents;
using Newtonsoft.Json;
using System.Text;

namespace ExerciseProject.Core.Services
{
    public class ContragentsService : IContragentsService
    {
        private readonly HttpClient httpClient;
        private readonly string BaseUrl = "https://localhost:7072";

        public ContragentsService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<bool> Create(ContragentViewModel contragent)
        {
            var contragentCreated = false;

            var serializedUser = JsonConvert.SerializeObject(contragent);
            var content = new StringContent(serializedUser, Encoding.UTF8, "application/json");
            var response = await this.httpClient.PostAsync($"{BaseUrl}/Contragents/Create", content);

            if (response.IsSuccessStatusCode)
            {
                string responseData = await response.Content.ReadAsStringAsync();
                contragentCreated = JsonConvert.DeserializeObject<bool>(responseData);
            }
            else
            {
                throw new InvalidOperationException();
            }

            return contragentCreated;
        }

        public async Task<IEnumerable<ContragentViewModel>> GetAllByUserId(int userId)
        {
            var contragents = new List<ContragentViewModel>();

            var response = await this.httpClient.GetAsync($"{BaseUrl}/Contragents/GetAllByUserId?userId={userId}");

            if (response.IsSuccessStatusCode)
            {
                string responseData = await response.Content.ReadAsStringAsync();
                contragents = JsonConvert.DeserializeObject<List<ContragentViewModel>>(responseData);
            }
            else
            {
                throw new InvalidOperationException();
            }

            return contragents;
        }
    }
}
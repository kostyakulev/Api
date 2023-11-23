using Newtonsoft.Json;
using System;
using System.Globalization;
using System.Net.Http;
using System.Text.Json;

namespace Api.Services.Http
{
    public class UserServicesHttp : IUserServicesHttp
    {
        private readonly TestDatabaseContext _context;
        private readonly HttpClient _httpClient;

        public UserServicesHttp(TestDatabaseContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _httpClient = new HttpClient
            {
                BaseAddress = new Uri("https://jsonplaceholder.typicode.com")
            };
            _httpClient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
        }
        public async Task<UserHttp> AddUserAsync(UserHttp userhttp)
        {
            HttpResponseMessage response = await _httpClient.PostAsJsonAsync("/users", userhttp);
            response.EnsureSuccessStatusCode();

            return await response.Content.ReadFromJsonAsync<UserHttp>();
        }

        public async Task<UserHttp>? DeleteUserAsync(int id)
        {
            HttpResponseMessage response = await _httpClient.DeleteAsync($"users/{id}");
            response.EnsureSuccessStatusCode();

            return await response.Content.ReadFromJsonAsync<UserHttp>();
        }

        public async Task<List<UserHttp>> GetAllUsers()
        {

            HttpResponseMessage response = await _httpClient.GetAsync("/users");

            response.EnsureSuccessStatusCode();

            return await response.Content.ReadFromJsonAsync<List<UserHttp>>();
        }

        public async Task<UserHttp?> GetSingleUser(int id)
        {
            HttpResponseMessage response = await _httpClient.GetAsync($"users/{id}");
            response.EnsureSuccessStatusCode();

            return await response.Content.ReadFromJsonAsync<UserHttp>();
        }

        public async Task<UserHttp>? UpdateUserAsync(int id, UserHttp userhttp)
        {
            HttpResponseMessage response = await _httpClient.PutAsJsonAsync($"users/{id}", userhttp);
            response.EnsureSuccessStatusCode();

            return await response.Content.ReadFromJsonAsync<UserHttp>();
        }
    }
}

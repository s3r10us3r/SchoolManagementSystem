using SchoolManagementSystem.AdminPanel.Services.Interfaces;
using System.Net.Http;

namespace SchoolManagementSystem.AdminPanel.Services
{
    public class BearerLoginService : ILoginService
    {
        private readonly HttpClient _httpClient;
        public BearerLoginService(HttpClient httpClient) 
        {
            _httpClient = httpClient;
        }

        public void SetLoginHeader(string token)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
        }
    }
}

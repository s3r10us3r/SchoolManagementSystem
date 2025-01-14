namespace SchoolManagementSystem.WebClient.Services
{
    public class BearerLoginService
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

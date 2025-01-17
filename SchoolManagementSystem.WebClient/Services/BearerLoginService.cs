using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Security.Claims;
using SchoolManagementSystem.Shared.Dtos;

public class BearerLoginService
{
    private readonly HttpClient _httpClient;
    private string? _token;
    private ClaimsPrincipal _currentUser = new(new ClaimsIdentity());

    public BearerLoginService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<bool> Login(string login, string password)
    {
        var response = await _httpClient.PostAsJsonAsync("/Users/login", new { Login = login, Password = password });
        if (!response.IsSuccessStatusCode)
        {
            return false;
        }

        var data = await response.Content.ReadFromJsonAsync<LoginResponseDto>();
        if (data == null)
        {
            return false;
        }

        _token = data.Token;
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _token);
        _currentUser = new ClaimsPrincipal(new ClaimsIdentity(new[]
        {
            new Claim(ClaimTypes.Role, data.Role)
        }, "jwt"));

        return true;
    }

    public void Logout()
    {
        _token = null;
        _httpClient.DefaultRequestHeaders.Authorization = null;
        _currentUser = new ClaimsPrincipal(new ClaimsIdentity());
    }

    public bool IsAuthenticated => _currentUser.Identity?.IsAuthenticated ?? false;
    public string? UserRole => _currentUser.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value;
}

using SchoolManagementSystem.Models;
using SchoolManagementSystem.Shared.Dtos;
using SchoolManagementSystem.Shared.WebServices.Interfaces;
using System.Net.Http.Json;

namespace SchoolManagementSystem.Shared.WebServices
{
    public class RequestsWebService : IRequestsWebService
    {
        private readonly HttpClient _httpClient;

        public RequestsWebService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<Result<RegisterUserResponse>> CreateRequest(NewUserDto newUserDto)
        {
            var response = await _httpClient.PostAsJsonAsync("/Requests", newUserDto);
            if (response.IsSuccessStatusCode)
            {
                var data = await response.Content.ReadFromJsonAsync<RegisterUserResponse>();
                if (data == null)
                    return Result<RegisterUserResponse>.Failure("Failed serialize the response", (int)response.StatusCode);
                return Result<RegisterUserResponse>.Success(data, ((int)response.StatusCode));
            }
            if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
            {
                var message = await response.Content.ReadFromJsonAsync<MessageDto>();
                return Result<RegisterUserResponse>.Failure(message?.Message ?? "Failed to deserialize message.", (int)response.StatusCode);
            }
            else
            {
                return Result<RegisterUserResponse>.Failure("Failed to create user", (int)response.StatusCode);
            }
        }

        public async Task<Result<object>> DeleteRequest(string code)
        {
            var response = await _httpClient.DeleteAsync($"/Requests/{code}");
            if (response.IsSuccessStatusCode)
                return Result<object>.Success(new object(), (int)response.StatusCode);
            var message = await response.Content.ReadFromJsonAsync<MessageDto>();
            return Result<object>.Failure(message?.Message ?? "Failed to deserialize message.", (int)response.StatusCode);
        }

        public async Task<Result<List<UserRequest>>> GetAll()
        {
            var response = await _httpClient.GetAsync("/Requests");
            if (response.IsSuccessStatusCode)
            {
                var requests = await response.Content.ReadFromJsonAsync<List<UserRequest>>();
                if (requests == null)
                    return Result<List<UserRequest>>.Failure("Failed serialize the response", (int)response.StatusCode);
                return Result<List<UserRequest>>.Success(requests, (int)response.StatusCode);
            }
            else
            {
                return Result<List<UserRequest>>.Failure("Failed to get requests", (int)response.StatusCode);
            }
        }
    }
}

using SchoolManagementSystem.Models;
using SchoolManagementSystem.Shared.Dtos;
using SchoolManagementSystem.Shared.WebServices.Interfaces;
using System.Net.Http.Json;

namespace SchoolManagementSystem.Shared.WebServices
{
    public class UserWebService : IUserWebService
    {
        private readonly HttpClient _httpClient;

        public UserWebService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<Result<object>> Delete(int id)
        {
            try
            {
                var response = await _httpClient.DeleteAsync($"/Users/{id}");
                if (response.IsSuccessStatusCode)
                {
                    return Result<object>.Success(null, (int)response.StatusCode);
                }
                else
                {
                    return Result<object>.Failure("Failed to delete user", (int)response.StatusCode);
                }
            } catch (Exception e)
            {
                return Result<object>.Failure(e.Message, 500);
            }
        }

        public async Task<Result<List<User>>> GetAll()
        {
            try
            {
                var response = await _httpClient.GetAsync("/Users");
                if (response.IsSuccessStatusCode)
                {
                    var data = await response.Content.ReadFromJsonAsync<List<User>>();
                    if (data == null)
                        return Result<List<User>>.Failure("Failed serialize the response", (int)response.StatusCode);
                    return Result<List<User>>.Success(data, ((int)response.StatusCode));
                }
                else
                {
                    return Result<List<User>>.Failure("Failed to get users", (int)response.StatusCode);
                }
            }
            catch (Exception e)
            {
                return Result<List<User>>.Failure(e.Message, 500);
            }
        }

        public async Task<Result<List<UserWithDataDto>>> GetAllWithData()
        {
            try
            {
                var response = await _httpClient.GetAsync("/Users/get-all-with-data");
                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadFromJsonAsync<List<UserWithDataDto>>();
                    if (result == null)
                    {
                        return Result<List<UserWithDataDto>>.Failure("Failed to parse result", (int)response.StatusCode);
                    }
                    else
                    {
                        return Result<List<UserWithDataDto>>.Success(result, (int)response.StatusCode);
                    }
                }
                else
                {
                    return Result<List<UserWithDataDto>>.Failure("Failed to get users with data", (int)response.StatusCode);
                }
            }
            catch (Exception e)
            {
                return Result<List<UserWithDataDto>>.Failure(e.Message, 500);
            }
        }


        public async Task<Result<bool>> IsInitialized()
        {
            var response = await _httpClient.GetAsync("/Users/is-initialized");
            if (response.IsSuccessStatusCode)
            {
                var data = await response.Content.ReadFromJsonAsync<IsInitializedDto>();
                if (data == null)
                    return Result<bool>.Failure("Failed to parse result", (int)response.StatusCode);
                return Result<bool>.Success(data.Result, ((int)response.StatusCode));
            }
            else
            {
                return Result<bool>.Failure("Failed to check if the system is initialized", (int)response.StatusCode);
            }
        }

        public async Task<Result<LoginResponseDto>> Login(LoginDto loginDto)
        {
            var response = await _httpClient.PostAsJsonAsync("/Users/login", loginDto);
            if (response.IsSuccessStatusCode)
            {
                var data = await response.Content.ReadFromJsonAsync<LoginResponseDto>();
                if (data is null)
                    return Result<LoginResponseDto>.Failure("Failed serialize the response", (int)response.StatusCode);
                return Result<LoginResponseDto>.Success(data, ((int)response.StatusCode));
            }
            else
            {
                return Result<LoginResponseDto>.Failure("Failed to login", (int)response.StatusCode);
            }
        }

        public async Task<Result<bool>> RegisterAdmin(RegisterAdminDto adminDto)
        {
            var response = await _httpClient.PostAsJsonAsync("/Users/register-admin", adminDto);
            if (response.IsSuccessStatusCode)
            {
                return Result<bool>.Success(true, ((int)response.StatusCode));
            }
            else if (response.StatusCode == System.Net.HttpStatusCode.Forbidden)
            {
                return Result<bool>.Failure("Admin already exists", (int)response.StatusCode);
            }
            else if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
            {
                var message = await response.Content.ReadFromJsonAsync<MessageDto>();
                return Result<bool>.Failure(message?.Message ?? "Failed to deserialize message.", (int)response.StatusCode);
            }
            else
            {
                return Result<bool>.Failure("Failed to register admin", (int)response.StatusCode);
            }
        }

        public async Task<Result<object>> RegisterUser(RegisterUserDto userDto)
        {
            var response = await _httpClient.PostAsJsonAsync("/Users/register", userDto);
            if (response.IsSuccessStatusCode)
                return Result<object>.Success(null, ((int)response.StatusCode));

            var message = await response.Content.ReadFromJsonAsync<MessageDto>();
            return Result<object>.Failure(message?.Message ?? "Failed to deserialize message.", (int)response.StatusCode);
        }
    }
}

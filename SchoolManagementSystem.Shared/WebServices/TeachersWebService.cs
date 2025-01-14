using SchoolManagementSystem.Models;
using SchoolManagementSystem.Shared.Dtos;
using SchoolManagementSystem.Shared.WebServices.Interfaces;
using System.Net.Http.Json;

namespace SchoolManagementSystem.Shared.WebServices
{
    public class TeachersWebService : ITeachersWebService
    {
        private readonly HttpClient _httpClient;

        public TeachersWebService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<Result<List<TeacherDto>>> GetAll()
        {
            try
            {
                var response = await _httpClient.GetAsync("/Teachers/get-all");
                if (response.IsSuccessStatusCode)
                {
                    var teachers = await response.Content.ReadFromJsonAsync<List<TeacherDto>>();
                    if (teachers == null)
                        return Result<List<TeacherDto>>.Failure("An error occurred while fetching teachers.", (int)response.StatusCode);
                    return Result<List<TeacherDto>>.Success(teachers, (int)response.StatusCode);
                }
                else
                {
                    return Result<List<TeacherDto>>.Failure("An error occurred while fetching teachers.", (int)response.StatusCode);
                }
            }
            catch (Exception ex)
            {
                return Result<List<TeacherDto>>.Failure(ex.Message, 500);
            }
        }

        public async Task<Result<Teacher>> GetById(int id)
        {
            try
            {
                var response = await _httpClient.GetAsync($"/Teachers/{id}");
                if (response.IsSuccessStatusCode)
                {
                    var teacher = await response.Content.ReadFromJsonAsync<Teacher>();
                    if (teacher == null)
                        return Result<Teacher>.Failure("An error occurred while fetching teacher.", (int)response.StatusCode);
                    return Result<Teacher>.Success(teacher, (int)response.StatusCode);
                }
                else
                {
                    return Result<Teacher>.Failure("An error occurred while fetching teacher.", (int)response.StatusCode);
                }
            }
            catch (Exception ex)
            {
                return Result<Teacher>.Failure(ex.Message, 500);
            }
        }
    }
}

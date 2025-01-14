using SchoolManagementSystem.Shared.Dtos;
using SchoolManagementSystem.Shared.WebServices.Interfaces;
using System.Net.Http.Json;

namespace SchoolManagementSystem.Shared.WebServices
{
    public class StudentsWebService : IStudentsWebService
    {
        private readonly HttpClient _httpClient;

        public StudentsWebService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<Result<List<StudentDto>>> GetAll()
        {
            try
            {
                var response = await _httpClient.GetAsync("/Students/get-all");
                if (response.IsSuccessStatusCode)
                {
                    var students = await response.Content.ReadFromJsonAsync<List<StudentDto>>();
                    if (students == null)
                        return Result<List<StudentDto>>.Failure("An error occurred while fetching students.", (int)response.StatusCode);
                    return Result<List<StudentDto>>.Success(students, (int)response.StatusCode);
                }
                else
                {
                    return Result<List<StudentDto>>.Failure("An error occurred while fetching students.", (int)response.StatusCode);
                }
            }
            catch (Exception ex)
            {
                return Result<List<StudentDto>>.Failure(ex.Message, 500);
            }
        }

        public async Task<Result<StudentDto>> GetById(int id)
        {
            try
            {
                var response = await _httpClient.GetAsync($"/Students/{id}");
                if (response.IsSuccessStatusCode)
                {
                    var student = await response.Content.ReadFromJsonAsync<StudentDto>();
                    if (student == null)
                        return Result<StudentDto>.Failure("An error occurred while fetching student.", (int)response.StatusCode);
                    return Result<StudentDto>.Success(student, (int)response.StatusCode);
                }
                else
                {
                    return Result<StudentDto>.Failure("An error occurred while fetching student.", (int)response.StatusCode);
                }
            }
            catch (Exception ex)
            {
                return Result<StudentDto>.Failure(ex.Message, 500);
            }
        }
    }
}

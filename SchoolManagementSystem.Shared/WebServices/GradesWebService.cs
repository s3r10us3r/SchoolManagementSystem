using SchoolManagementSystem.Shared.Dtos;
using SchoolManagementSystem.Shared.WebServices.Interfaces;
using System.Net.Http.Json;

namespace SchoolManagementSystem.Shared.WebServices
{
    public class GradesWebService : IGradesWebService
    {
        private readonly HttpClient _httpClient;

        public GradesWebService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<Result<List<GradeWithDataDto>>> GetAllGradesAsync()
        {
            try
            {
                var response = await _httpClient.GetAsync("api/Grades/getAll");
                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadFromJsonAsync<List<GradeWithDataDto>>();
                    if (result != null)
                        return Result<List<GradeWithDataDto>>.Success(result, (int)response.StatusCode);
                    return Result<List<GradeWithDataDto>>.Failure("Failed to deserialize hte response!", (int)response.StatusCode);
                }
                return Result<List<GradeWithDataDto>>.Failure("Failed to get data!", (int)response.StatusCode);
            }
            catch (Exception e)
            {
                return Result<List<GradeWithDataDto>>.Failure(e.Message, 500);
            }
        }

        public async Task<Result> UpdateGrade(int id, GradeDto gradeDto)
        {
            try
            {
                var response = await _httpClient.PutAsJsonAsync($"api/Grades/update/{id}", gradeDto);
                if (response.IsSuccessStatusCode)
                    return Result.Success((int)response.StatusCode);
                return Result.Failure("Failed to update grade!", (int)response.StatusCode);
            }
            catch (Exception e)
            {
                return Result.Failure(e.Message, 500);
            }
        }
    }
}

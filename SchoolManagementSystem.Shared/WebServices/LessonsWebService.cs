using SchoolManagementSystem.Shared.Dtos;
using SchoolManagementSystem.Shared.WebServices.Interfaces;
using System.Net;
using System.Net.Http.Json;

namespace SchoolManagementSystem.Shared.WebServices
{
    public class LessonsWebService : ILessonsWebService
    {
        private readonly HttpClient _httpClient;

        public LessonsWebService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<Result> Create(NewLessonDto dto)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync("/Lessons", dto);
                if (response.IsSuccessStatusCode)
                {
                    return Result.Success((int)response.StatusCode);
                }
                else if (response.StatusCode == HttpStatusCode.BadRequest)
                {
                    var message = await response.Content.ReadFromJsonAsync<MessageDto>();
                    return Result.Failure(message?.Message ?? "Failed to deserialize message.", (int)response.StatusCode);
                }
                else
                {
                    return Result.Failure("Failed to create lesson", (int)response.StatusCode);
                }
            }
            catch (Exception ex)
            {
                return Result.Failure(ex.Message, 500);
            }
        }

        public async Task<Result> Delete(int id)
        {
            try
            {
                var response = await _httpClient.DeleteAsync($"/Lessons/{id}");
                if (response.IsSuccessStatusCode)
                {
                    return Result.Success((int)response.StatusCode);
                }
                return Result.Failure("Failed to delete lesson", (int)response.StatusCode);
            }
            catch (Exception ex)
            {
                return Result.Failure(ex.Message, 500);
            }
        }

        public async Task<Result<LessonDto>> Get(int id)
        {
            try
            {
                var response = await _httpClient.GetAsync($"/Lessons/{id}");
                if (response.IsSuccessStatusCode)
                {
                    var data = await response.Content.ReadFromJsonAsync<LessonDto>();
                    if (data == null)
                    {
                        return Result<LessonDto>.Failure("Failed to serialize the response", (int)response.StatusCode);
                    }
                    return Result<LessonDto>.Success(data, (int)response.StatusCode);
                }
                else if (response.StatusCode == HttpStatusCode.BadRequest)
                {
                    var message = await response.Content.ReadFromJsonAsync<MessageDto>();
                    return Result<LessonDto>.Failure(message?.Message ?? "Failed to deserialize message.", (int)response.StatusCode);
                }
                else
                {
                    return Result<LessonDto>.Failure("Failed to get lesson", (int)response.StatusCode);
                }
            }
            catch (Exception ex)
            {
                return Result<LessonDto>.Failure(ex.Message, 500);
            }
        }

        public async Task<Result<List<LessonWithTeacherDto>>> GetYour()
        {
            var response = await _httpClient.GetAsync("/Lessons/your");
            if (response.IsSuccessStatusCode)
            {
                var data = await response.Content.ReadFromJsonAsync<List<LessonWithTeacherDto>>();
                if (data == null)
                {
                    return Result<List<LessonWithTeacherDto>>.Failure("Failed to serialize the response", (int)response.StatusCode);
                }
                return Result<List<LessonWithTeacherDto>>.Success(data, (int)response.StatusCode);
            }
            else if (response.StatusCode == HttpStatusCode.BadRequest)
            {
                var message = await response.Content.ReadFromJsonAsync<MessageDto>();
                return Result<List<LessonWithTeacherDto>>.Failure(message?.Message ?? "Failed to deserialize message.", (int)response.StatusCode);
            }
            else
            {
                return Result<List<LessonWithTeacherDto>>.Failure("Failed to get lessons", (int)response.StatusCode);
            }
        }

        public async Task<Result> Update(LessonDto dto)
        {
            try
            {
                var response = await _httpClient.PutAsJsonAsync("/Lessons", dto);
                if (response.IsSuccessStatusCode)
                {
                    return Result.Success((int)response.StatusCode);
                }
                else if (response.StatusCode == HttpStatusCode.BadRequest)
                {
                    var message = await response.Content.ReadFromJsonAsync<MessageDto>();
                    return Result.Failure(message?.Message ?? "Failed to deserialize message.", (int)response.StatusCode);
                }
                else
                {
                    return Result.Failure("Failed to update lesson", (int)response.StatusCode);
                }
            }
            catch (Exception ex)
            {
                return Result.Failure(ex.Message, 500);
            }
        }
    }
}

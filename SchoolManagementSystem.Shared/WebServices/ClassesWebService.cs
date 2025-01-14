using SchoolManagementSystem.Shared.Dtos;
using SchoolManagementSystem.Shared.WebServices.Interfaces;
using System.Net.Http.Json;

namespace SchoolManagementSystem.Shared.WebServices
{
    public class ClassesWebService : IClassesWebService
    {
        private readonly HttpClient _httpClient;
        public ClassesWebService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<Result<ClassDto>> Create(ClassDto dto)
        {
            try
            {
                var result = await _httpClient.PostAsJsonAsync("/Classes", dto);
                if (result.IsSuccessStatusCode)
                {
                    var data = await result.Content.ReadFromJsonAsync<ClassDto>();
                    if (data == null)
                        return Result<ClassDto>.Failure("Failed to serialize the response", (int)result.StatusCode);
                    return Result<ClassDto>.Success(data, (int)result.StatusCode);
                }
                else if (result.StatusCode == System.Net.HttpStatusCode.BadRequest)
                {
                    var message = await result.Content.ReadFromJsonAsync<MessageDto>();
                    return Result<ClassDto>.Failure(message?.Message ?? "Failed to deserialize message.", (int)result.StatusCode);
                }
                else
                {
                    return Result<ClassDto>.Failure("Failed to create class", (int)result.StatusCode);
                }
            }
            catch (Exception ex)
            {
                return Result<ClassDto>.Failure(ex.Message, 500);
            }
        }

        public async Task<Result> Delete(int id)
        {
            try
            {
                var result = await _httpClient.DeleteAsync($"/Classes/{id}");
                if (result.IsSuccessStatusCode)
                    return Result.Success((int)result.StatusCode);
                return Result.Failure("Failed to delete class", (int)result.StatusCode);
            }
            catch (Exception ex)
            {
                return Result.Failure(ex.Message, 500);
            }
        }

        public async Task<Result<ClassDto>> Get(int id)
        {
            try
            {
                var result = await _httpClient.GetAsync($"/Classes/{id}");
                if (result.IsSuccessStatusCode)
                {
                    var data = await result.Content.ReadFromJsonAsync<ClassDto>();
                    if (data == null)
                        return Result<ClassDto>.Failure("Failed to serialize the response", (int)result.StatusCode);
                    return Result<ClassDto>.Success(data, (int)result.StatusCode);
                }
                else if (result.StatusCode == System.Net.HttpStatusCode.BadRequest)
                {
                    var message = await result.Content.ReadFromJsonAsync<MessageDto>();
                    return Result<ClassDto>.Failure(message?.Message ?? "Failed to deserialize message.", (int)result.StatusCode);
                }
                else
                {
                    return Result<ClassDto>.Failure("Failed to get class", (int)result.StatusCode);
                }
            }
            catch (Exception ex)
            {
                return Result<ClassDto>.Failure(ex.Message, 500);
            }
        }


        public async Task<Result<List<ClassDto>>> GetAll()
        {
            try
            {
                var result = await _httpClient.GetAsync("/Classes/all");
                if (result.IsSuccessStatusCode)
                {
                    var data = await result.Content.ReadFromJsonAsync<List<ClassDto>>();
                    if (data == null)
                        return Result<List<ClassDto>>.Failure("Failed to serialize the response", (int)result.StatusCode);
                    return Result<List<ClassDto>>.Success(data, (int)result.StatusCode);
                }
                else
                {
                    return Result<List<ClassDto>>.Failure("Failed to get classes", (int)result.StatusCode);
                }
            }
            catch (Exception ex)
            {
                return Result<List<ClassDto>>.Failure(ex.Message, 500);
            }
        }

        public async Task<Result<List<LessonWithDataDto>>> LoadLessonsWithData(int id)
        {
            try
            {
                var result = await _httpClient.GetAsync($"/Classes/load-lessons/{id}");
                if (result.IsSuccessStatusCode)
                {
                    var data = await result.Content.ReadFromJsonAsync<List<LessonWithDataDto>>();
                    if (data == null)
                        return Result<List<LessonWithDataDto>>.Failure("Failed to serialize the response", (int)result.StatusCode);
                    return Result<List<LessonWithDataDto>>.Success(data, (int)result.StatusCode);
                }
                else
                {
                    return Result<List<LessonWithDataDto>>.Failure("Failed to get lessons with data", (int)result.StatusCode);
                }
            }
            catch (Exception ex)
            {
                return Result<List<LessonWithDataDto>>.Failure(ex.Message, 500);
            }
        }

        public async Task<Result<ClassDto>> Update(ClassDto dto)
        {
            try
            {
                var result = await _httpClient.PutAsJsonAsync("/Classes", dto);
                if (result.IsSuccessStatusCode)
                {
                    var data = await result.Content.ReadFromJsonAsync<ClassDto>();
                    if (data == null)
                        return Result<ClassDto>.Failure("Failed to serialize the response", (int)result.StatusCode);
                    return Result<ClassDto>.Success(data, (int)result.StatusCode);
                }
                else if (result.StatusCode == System.Net.HttpStatusCode.BadRequest)
                {
                    var message = await result.Content.ReadFromJsonAsync<MessageDto>();
                    return Result<ClassDto>.Failure(message?.Message ?? "Failed to deserialize message.", (int)result.StatusCode);
                }
                else
                {
                    return Result<ClassDto>.Failure("Failed to update class", (int)result.StatusCode);
                }
            }
            catch (Exception ex)
            {
                return Result<ClassDto>.Failure(ex.Message, 500);
            }
        }
    }
}

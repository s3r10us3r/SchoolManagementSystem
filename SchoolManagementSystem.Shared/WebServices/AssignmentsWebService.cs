using SchoolManagementSystem.Shared.Dtos;
using SchoolManagementSystem.Shared.WebServices.Interfaces;
using System.Net.Http.Json;

namespace SchoolManagementSystem.Shared.WebServices
{
    public class AssignmentsWebService : IAssignmentsWebService
    {
        private readonly HttpClient _httpClient;
        public async Task<Result> Create(AssignmentDto dto)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync("/Assignments", dto);
                if (response.IsSuccessStatusCode)
                {
                    return Result.Success((int)response.StatusCode);
                }
                else
                {
                    return Result.Failure("Failed to create assignment", (int)response.StatusCode);
                }
            }
            catch (Exception e)
            {
                return Result.Failure(e.Message, 500);
            }
        }

        public async Task<Result> Delete(int id)
        {
            var response = await _httpClient.DeleteAsync($"/Assignments/{id}");
            if (response.IsSuccessStatusCode)
            {
                return Result.Success((int)response.StatusCode);
            }
            else
            {
                return Result.Failure("Failed to delete assignment", (int)response.StatusCode);
            }
        }

        public async Task<Result<AssignmentDto>> Get(int id)
        {
            var response = await _httpClient.GetAsync($"/Assignments/{id}");
            if (response.IsSuccessStatusCode)
            {
                var assignment = await response.Content.ReadFromJsonAsync<AssignmentDto>();
                if (assignment == null)
                {
                    return Result<AssignmentDto>.Failure("Failed to get assignment", (int)response.StatusCode);
                }
                return Result<AssignmentDto>.Success(assignment, (int)response.StatusCode);
            }
            else
            {
                return Result<AssignmentDto>.Failure("Failed to get assignment", (int)response.StatusCode);
            }
        }

        public async Task<Result<List<AssignmentDto>>> GetRecent()
        {
            var response = await _httpClient.GetAsync("/Assignments/getRecent");
            if (response.IsSuccessStatusCode)
            {
                var assignments = await response.Content.ReadFromJsonAsync<List<AssignmentDto>>();
                if (assignments == null)
                {
                    return Result<List<AssignmentDto>>.Failure("Failed to get assignments", (int)response.StatusCode);
                }
                return Result<List<AssignmentDto>>.Success(assignments, (int)response.StatusCode);
            }
            else
            {
                return Result<List<AssignmentDto>>.Failure("Failed to get assignments", (int)response.StatusCode);
            }
        }

        public async Task<Result> GradeAssignment(List<GradeDto> grades, int id)
        {
            var response = await _httpClient.PostAsJsonAsync($"/Assignments/{id}/grade", grades);
            if (response.IsSuccessStatusCode)
            {
                return Result.Success((int)response.StatusCode);
            }
            else
            {
                return Result.Failure("Failed to grade assignment", (int)response.StatusCode);
            }
        }

        public async Task<Result> Update(AssignmentDto assignmentDto)
        {
            var response = await _httpClient.PutAsJsonAsync($"/Assignments", assignmentDto);
            if (response.IsSuccessStatusCode)
            {
                return Result.Success((int)response.StatusCode);
            }
            else
            {
                return Result.Failure("Failed to update assignment", (int)response.StatusCode);
            }
        }
    }
}

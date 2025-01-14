using SchoolManagementSystem.Shared.Dtos;

namespace SchoolManagementSystem.Shared.WebServices.Interfaces
{
    public interface IAssignmentsWebService
    {
        Task<Result> Create(AssignmentDto dto);
        Task<Result<AssignmentDto>> Get(int id);
        Task<Result> Update(AssignmentDto assignmentDto);
        Task<Result> Delete(int id);
        Task<Result> GradeAssignment(List<GradeDto> grades, int id);
        Task<Result<List<AssignmentDto>>> GetRecent();
    }
}

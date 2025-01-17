using SchoolManagementSystem.Shared.Dtos;

namespace SchoolManagementSystem.Shared.WebServices.Interfaces
{
    public interface IGradesWebService
    {
        Task<Result<List<GradeWithDataDto>>> GetAllGradesAsync();
        Task<Result> UpdateGrade(int id, GradeDto gradeDto);
    }
}

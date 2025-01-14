using SchoolManagementSystem.Shared.Dtos;

namespace SchoolManagementSystem.Shared.WebServices.Interfaces
{
    public interface ILessonsWebService
    {
        Task<Result> Create(NewLessonDto dto);
        Task<Result<LessonDto>> Get(int id);
        Task<Result> Update(LessonDto dto);
        Task<Result> Delete(int id);
        Task<Result<List<LessonWithTeacherDto>>> GetYour();
    }
}

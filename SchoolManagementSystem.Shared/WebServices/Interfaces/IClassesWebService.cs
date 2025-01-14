using SchoolManagementSystem.Shared.Dtos;

namespace SchoolManagementSystem.Shared.WebServices.Interfaces
{
    public interface IClassesWebService
    {
        Task<Result<ClassDto>> Create(ClassDto dto);
        Task<Result<List<ClassDto>>> GetAll();
        Task<Result<ClassDto>> Get(int id);
        Task<Result<ClassDto>> Update(ClassDto dto);
        Task<Result> Delete(int id);
        Task<Result<List<LessonWithDataDto>>> LoadLessonsWithData(int id);
    }
}

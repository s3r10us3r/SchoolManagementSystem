using SchoolManagementSystem.Models;
using SchoolManagementSystem.Shared.Dtos;

namespace SchoolManagementSystem.Shared.WebServices.Interfaces
{
    public interface ITeachersWebService
    {
        Task<Result<Teacher>> GetById(int id);
        Task<Result<List<TeacherDto>>> GetAll();
    }
}

using SchoolManagementSystem.Shared.Dtos;

namespace SchoolManagementSystem.Shared.WebServices.Interfaces
{
    public interface IStudentsWebService
    {
        public Task<Result<StudentDto>> GetById(int id);
        public Task<Result<List<StudentDto>>> GetAll();
    }
}

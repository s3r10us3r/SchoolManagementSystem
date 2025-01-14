using SchoolManagementSystem.Api.Services.Interfaces;
using SchoolManagementSystem.Dal.Interfaces;
using SchoolManagementSystem.Models;
using SchoolManagementSystem.Shared.Dtos;
using SchoolManagementSystem.Shared.Extensions;

namespace SchoolManagementSystem.Api.Services
{
    public class UserRequestService : IUserRequestService
    {
        private readonly IUserRequestRepo _repo;
        private readonly ITeacherRepo _teacherRepo;
        private readonly IStudentRepo _studentRepo;

        public UserRequestService(IUserRequestRepo repo, ITeacherRepo teacherRepo, IStudentRepo studentRepo)
        {
            _repo = repo;
            _teacherRepo = teacherRepo;
            _studentRepo = studentRepo;
        }

        public async Task<string> CreateRequestAsync(NewUserDto newUserDto)
        {
            var bindingId = 0;
            if (newUserDto.Role == "Teacher")
            {
                var teacher = new Teacher
                {
                    FirstName = newUserDto.FirstName,
                    LastName = newUserDto.LastName,
                    BirthDate = newUserDto.DateOfBirth
                };
                await _teacherRepo.CreateAsync(teacher);
                bindingId = teacher.Id;
            }
            else if (newUserDto.Role == "Student")
            {
                var student = new Student
                {
                    FirstName = newUserDto.FirstName,
                    LastName = newUserDto.LastName,
                    BirthDate = newUserDto.DateOfBirth
                };
                await _studentRepo.CreateAsync(student);
                bindingId = student.Id;
            }

            var request = new UserRequest
            {
                FirstName = newUserDto.FirstName,
                LastName = newUserDto.LastName,
                Role = newUserDto.Role.ToUserRole() ?? UserRole.Student,
                BirthDate = newUserDto.DateOfBirth,
                Code = Guid.NewGuid().ToString(),
                IssuedAt = DateTime.Now,
                BindingId = bindingId
            };

            await _repo.CreateAsync(request);
            return request.Code;
        }

        public async Task DeleteRequestAsync(string code)
        {
            var request = await _repo.FindAsync(r => r.Code == code);
            if (request == null)
                return;
            if (request.Role == UserRole.Teacher)
            {
                var teacher = await _teacherRepo.FindAsync(t => t.Id == request.BindingId);
                if (teacher != null)
                    await _teacherRepo.DeleteAsync(teacher);
            }
            else if (request.Role == UserRole.Student)
            {
                var student = await _studentRepo.FindAsync(s => s.Id == request.BindingId);
                if (student != null)
                    await _studentRepo.DeleteAsync(student);
            }
            await _repo.DeleteAsync(request);
        }

        public async Task<UserRequest?> GetRequestAsync(string code)
        {
            var result = await _repo.FindAsync(r => r.Code == code);
            return result;
        }
    }
}

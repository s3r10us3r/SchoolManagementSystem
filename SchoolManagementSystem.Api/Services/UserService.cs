using Microsoft.AspNetCore.Identity;
using SchoolManagementSystem.Api.Services.Interfaces;
using SchoolManagementSystem.Dal.Interfaces;
using SchoolManagementSystem.Models;
using SchoolManagementSystem.Shared.Dtos;

namespace SchoolManagementSystem.Api.Services
{
    public class UserService : IUserService
    {
        private readonly IJwtService _jwtService;
        private readonly IUserRepo _userRepo;
        private readonly IUserRequestRepo _userRequestRepo;
        private readonly IStudentRepo _studentRepo;
        private readonly ITeacherRepo _teacherRepo;
        private readonly IPasswordHasher<object> _passwordHasher;

        public UserService(IJwtService jwtService, IUserRepo userRepo, IUserRequestRepo userRequestRepo, 
            IStudentRepo studentRepo, ITeacherRepo teacherRepo, IPasswordHasher<object> passwordHasher) 
        { 
            _jwtService = jwtService;
            _userRepo = userRepo;
            _userRequestRepo = userRequestRepo;
            _studentRepo = studentRepo;
            _teacherRepo = teacherRepo;
            _passwordHasher = passwordHasher;
        }

        public async Task CreateUser(UserRequest request, string login, string password)
        {
            var user = new User
            {
                Login = login,
                PasswordHash = _passwordHasher.HashPassword(null!, password),
                Role = request.Role
            };

            user = await _userRepo.CreateAsync(user);
            if (request.Role == UserRole.Student)
            {
                var student = await _studentRepo.GetByIdAsync(request.BindingId);
                if (student == null)
                    throw new InvalidOperationException("Student not found");
                student.UserId = user.Id;
                await _studentRepo.UpdateAsync(student);
            }
            else if (request.Role == UserRole.Teacher)
            {
                var teacher = await _teacherRepo.GetByIdAsync(request.BindingId);
                if (teacher == null)
                    throw new InvalidOperationException("Teacher not found");
                teacher.UserId = user.Id;
                await _teacherRepo.UpdateAsync(teacher);
            }
        }

        public async Task DeleteUser(int userId)
        {
            var user = _userRepo.GetByIdAsync(userId);
            var student = await _studentRepo.FindAsync(s => s.UserId == userId);
            var teacher = await _teacherRepo.FindAsync(t => t.UserId == userId);
            if (student != null)
                await _studentRepo.DeleteAsync(student);

            if (teacher != null)
                await _teacherRepo.DeleteAsync(teacher);
        }

        public async Task<(string token, string role)?> LoginUser(LoginDto loginDto)
        {
            var user = await _userRepo.FindAsync(u => u.Login == loginDto.Login);
            if (user == null || _passwordHasher.VerifyHashedPassword(null!, user.PasswordHash, loginDto.Password) != PasswordVerificationResult.Success)
                return null;
            return (_jwtService.GetJwtForUser(user), user.Role.ToString());
        }
    }
}

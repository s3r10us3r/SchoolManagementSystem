using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SchoolManagementSystem.Api.Services.Interfaces;
using SchoolManagementSystem.Dal.Interfaces;
using SchoolManagementSystem.Models;
using SchoolManagementSystem.Shared.Dtos;
using SchoolManagementSystem.Shared.Services;

namespace SchoolManagementSystem.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsersController : ControllerJwtAuthBase
    {
        private readonly IUserRepo _userRepo;
        private readonly IPasswordHasher<object> _passwordHasher;
        private readonly IUserRequestService _userRequestService;
        private readonly IUserService _userService;
        private readonly IStudentRepo _studentRepo;
        private readonly ITeacherRepo _teacherRepo;
        private readonly PasswordValidator _passwordValidator;

        public UsersController(IUserRepo userRepo, IPasswordHasher<object> passwordHasher, IUserRequestService userRequestService,
            IUserService userService, IStudentRepo studentRepo, ITeacherRepo teacherRepo)
        {
            _userRepo = userRepo;
            _passwordHasher = passwordHasher;
            _userRequestService = userRequestService;
            _userService = userService;
            _passwordValidator = new PasswordValidator();
            _studentRepo = studentRepo;
            _teacherRepo = teacherRepo;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {
            var nullableResult = await _userService.LoginUser(loginDto);
            if (!nullableResult.HasValue)
                return Unauthorized();
            var result = nullableResult.Value;
            return Ok(new LoginResponseDto() { Token = result.token, Role = result.role });
        }

        [HttpGet("is-initialized")]
        public async Task<IActionResult> IsInitialized()
        {
            return Ok(new IsInitializedDto { Result = await DoesAdminExist()});
        }

        [HttpPost("register-admin")]
        public async Task<IActionResult> RegisterAdmin([FromBody] RegisterAdminDto adminDto)
        {
            if (await DoesAdminExist())
            {
                return Forbid();
            }

            if (!_passwordValidator.IsPasswordValid(adminDto.Password, out var message))
            {
                return BadRequest(new { Message = message });
            }

            var admin = new User
            {
                Login = adminDto.Login,
                PasswordHash = _passwordHasher.HashPassword(null!, adminDto.Password),
                Role = UserRole.Admin
            };

            var result = await _userRepo.CreateAsync(admin);
            return Ok();
        }

        [HttpPost("register")]
        public async Task<IActionResult> RegisterUser([FromBody] RegisterUserDto registerUserDto)
        {
            var userFromLogin = await _userRepo.FindAsync(u => u.Login == registerUserDto.Login);
            if (userFromLogin != null)
                return Conflict(new MessageDto("User with this login already exists."));

            if (!_passwordValidator.IsPasswordValid(registerUserDto.Password, out var message))
                return BadRequest(new MessageDto(message));

            if (registerUserDto.Login.Length < 6)
                return BadRequest(new MessageDto("Login is too short."));

            var code = registerUserDto.Code;
            var registerRequest = await _userRequestService.GetRequestAsync(code);
            if (registerRequest == null)
                return Unauthorized( new MessageDto("Invalid code."));

            await _userService.CreateUser(registerRequest, registerUserDto.Login, registerUserDto.Password);
            await _userRequestService.DeleteRequestAsync(code);
            return Ok();
        }


        [Authorize(Roles = "Admin,Teacher")]
        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            var users = await _userRepo.GetAllAsync();
            users.RemoveAll(u => u.Role == UserRole.Admin);
            return Ok(users);
        }

        [HttpDelete("{id:int}")]
        [Authorize(Roles = "Admin,Teacher")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var user = await _userRepo.GetByIdAsync(id);
            if (user == null)
                return NotFound();
            await _userService.DeleteUser(id);
            return Ok();
        }

        [HttpGet("get-all-with-data")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAllWithData()
        {
            var students = await _studentRepo.GetAllAsync();
            var teachers = await _teacherRepo.GetAllAsync();
            List<UserWithDataDto> result = [];
            foreach (var student in students)
            {
                result.Add(new UserWithDataDto
                {
                    Id = student.Id,
                    Login = student.User?.Login ?? "",
                    FirstName = student.FirstName,
                    LastName = student.LastName,
                    DateOfBirth = student.BirthDate,
                    Role = "Student"
                });
            }
            foreach (var teacher in teachers)
            {
                result.Add(new UserWithDataDto
                {
                    Id = teacher.Id,
                    Login = teacher.User?.Login,
                    FirstName = teacher.FirstName,
                    LastName = teacher.LastName,
                    DateOfBirth = teacher.BirthDate,
                    Role = "Teacher"
                });
            }
            return Ok(result);
        }

        private async Task<bool> DoesAdminExist()
        {
            return (await _userRepo.FindAsync(u => u.Role == UserRole.Admin)) != null;
        }
    }
}

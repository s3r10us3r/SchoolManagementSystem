using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SchoolManagementSystem.Api.Services.Interfaces;
using SchoolManagementSystem.Dal.Interfaces;
using SchoolManagementSystem.Models;
using SchoolManagementSystem.Models.Extensions;
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
        private readonly PasswordValidator _passwordValidator;

        public UsersController(IUserRepo userRepo, IPasswordHasher<object> passwordHasher, IUserRequestService userRequestService, IUserService userService)
        {
            _userRepo = userRepo;
            _passwordHasher = passwordHasher;
            _userRequestService = userRequestService;
            _userService = userService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {
            var token = await _userService.LoginUser(loginDto);
            if (token == null)
                return Unauthorized();
            return Ok(new { Token = token });
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
                return Forbid();

            if (!_passwordValidator.IsPasswordValid(adminDto.Password, out var message))
                return BadRequest(new { Message = message });

            var admin = new User
            {
                Login = adminDto.Login,
                PasswordHash = _passwordHasher.HashPassword(null!, adminDto.Password)
            };

            var result = await _userRepo.CreateAsync(admin);
            return Ok();
        }

        [Authorize]
        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] NewUserDto newUserDto)
        {
            var roleString = User.Claims.FirstOrDefault(c => c.Type == "role")?.Value;
            var role = roleString?.ToUserRole();
            if (role == null || role != UserRole.Admin)
                return Forbid();

            var newUserRole = newUserDto.Role.ToUserRole();
            if (newUserRole == null)
                return BadRequest(new { Message = $"Invalid user role {newUserDto.Role}. Accepted values are 'student', 'teacher' and 'admin'" });
            var code = await _userRequestService.CreateRequestAsync(newUserDto.FirstName, newUserDto.LastName, (UserRole)newUserRole);
            return Ok(new RegisterUserResponse() { FirstName = newUserDto.FirstName, LastName = newUserDto.LastName, Code = code });
        }

        [HttpPost("register")]
        public async Task<IActionResult> RegisterUser([FromBody] RegisterUserDto registerUserDto)
        {
            var userFromLogin = _userRepo.FindAsync(u => u.Login == registerUserDto.Login);
            if (userFromLogin != null)
                return Conflict(new MessageDto("User with this login already exists."));

            var code = registerUserDto.Code;
            var registerRequest = await _userRequestService.GetRequestAsync(code);
            if (registerRequest == null)
                return Unauthorized( new MessageDto("Invalid code."));

            if (!_passwordValidator.IsPasswordValid(registerUserDto.Password, out var message))
                return BadRequest(new MessageDto(message));

            await _userService.CreateUser(registerRequest, registerUserDto.Login, registerUserDto.Password);
            return Ok();
        }

        [HttpPost("get-all")]
        [Authorize]
        public async Task<IActionResult> GetUsers()
        {
            var role = GetUserRole();
            if (role is null or UserRole.Student)
                return Forbid();

            var users = await _userRepo.GetAllAsync();
            users.RemoveAll(u => u.Role == UserRole.Admin);
            return Ok(users);
        }

        [HttpDelete("delete")]
        [Authorize]
        public async Task<IActionResult> DeleteUser()
        {
            var role = GetUserRole();
            if (role is null or UserRole.Student)
                return Forbid();
            var userId = int.Parse(User.Claims.FirstOrDefault(c => c.Type == "id")?.Value);
            await _userService.DeleteUser(userId);
            return Ok();
        }

        private async Task<bool> DoesAdminExist()
        {
            return (await _userRepo.FindAsync(u => u.Role == UserRole.Admin)) == null;
        }
    }
}

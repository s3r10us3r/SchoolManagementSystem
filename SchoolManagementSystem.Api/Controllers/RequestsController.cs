using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SchoolManagementSystem.Api.Services.Interfaces;
using SchoolManagementSystem.Dal.Interfaces;
using SchoolManagementSystem.Shared.Dtos;
using SchoolManagementSystem.Shared.Extensions;

namespace SchoolManagementSystem.Api.Controllers
{
    [ApiController]
    [Authorize(Roles = "Admin")]
    [Route("[controller]")]
    public class RequestsController : ControllerBase
    {
        private readonly IUserRequestService _userRequestService;
        private readonly IUserRequestRepo _userRequestRepo;

        public RequestsController(IUserRequestService userRequestService, IUserRequestRepo userRequestRepo)
        {
            _userRequestService = userRequestService;
            _userRequestRepo = userRequestRepo;
        }

        [HttpPost]
        public async Task<IActionResult> CreateRequest([FromBody] NewUserDto userRequest)
        {
            var roleEnum = userRequest.Role.ToUserRole();
            if (roleEnum == null)
                return BadRequest("Invalid user role, accepted values are 'Admin', 'Teacher' and 'Student'");
            var code = await _userRequestService.CreateRequestAsync(userRequest);
            return Ok(new RegisterUserResponse() { FirstName = userRequest.FirstName, LastName = userRequest.LastName, Code = code });
        }

        [HttpDelete("{code}")]
        public async Task<IActionResult> DeleteRequest(string code)
        {
            await _userRequestService.DeleteRequestAsync(code);
            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _userRequestRepo.GetAllAsync();
            result = result.OrderByDescending(r => r.IssuedAt).ToList();
            return Ok(result);
        }
    }
}

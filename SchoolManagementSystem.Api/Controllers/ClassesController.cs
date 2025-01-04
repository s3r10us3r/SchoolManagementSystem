using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SchoolManagementSystem.Dal.Interfaces;
using SchoolManagementSystem.Models;
using SchoolManagementSystem.Models.Extensions;
using SchoolManagementSystem.Shared.Dtos;

namespace SchoolManagementSystem.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ClassesController : ControllerJwtAuthBase
    {
        private readonly IClassRepo _classRepo;
        private readonly IUserRepo _userRepo;

        public ClassesController(IClassRepo classRepo, IUserRepo userRepo)
        {
            _classRepo = classRepo;
            _userRepo = userRepo;
        }

        [Authorize(Roles = "Admin,Teacher")]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] NewClassDto classDto)
        {
            var userLogin = GetUserLogin();
            var classEntity = new Class
            {
                Name = classDto.Name,
                Grade = classDto.Grade,
                TeacherId = classDto.TeacherId,
            };
            var model = await _classRepo.CreateAsync(classEntity);
            return Ok(model.ToDto());
        }

        [Authorize]
        [HttpGet("{id:int}")]
        public async Task<IActionResult> Get(int id)
        {
            var classEntity = await _classRepo.GetByIdAsync(id);
            if (classEntity == null)
                return NotFound();
            return Ok(classEntity.ToDto());
        }

        [Authorize(Roles = "Admin,Teacher")]
        [HttpPut]
        public async Task<IActionResult> Update([FromBody] ClassDto classDto)
        {
            var userLogin = GetUserLogin();
            var user = await _userRepo.FindAsync(u => u.Login == userLogin);
            var classEntity = await _classRepo.GetByIdAsync(classDto.Id);
            if (classEntity == null)
                return NotFound();
            if (user == null || (user.Role == UserRole.Teacher && user.Id != classEntity.Id))
                return Forbid();

            classEntity.Name = classDto.Name;
            await _classRepo.UpdateAsync(classEntity);
            return Ok();
        }

        [Authorize(Roles = "Admin,Teacher")]
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var userLogin = GetUserLogin();
            var user = await _userRepo.FindAsync(u => u.Login == userLogin);
            var classEntity = await _classRepo.GetByIdAsync(id);
            if (classEntity == null)
                return NotFound();
            if (user == null || (user.Role == UserRole.Teacher && user.Id != classEntity.Id))
                return Forbid();
            await _classRepo.DeleteAsync(classEntity);
            return Ok();
        }
    }
}

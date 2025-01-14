using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SchoolManagementSystem.Dal.Interfaces;
using SchoolManagementSystem.Shared.Dtos;

namespace SchoolManagementSystem.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TeachersController : ControllerBase
    {
        private readonly ITeacherRepo _teacherRepo;
        public TeachersController(ITeacherRepo teacherRepo)
        {
            _teacherRepo = teacherRepo;
        }

        [Authorize]
        [HttpGet("get-all")]
        public async Task<IActionResult> GetTeachers()
        {
            var teachers = await _teacherRepo.GetAllAsync();
            var result = teachers.Select(t => new TeacherDto
            {
                Id = t.Id,
                FirstName = t.FirstName,
                LastName = t.LastName,
                BirthDate = t.BirthDate
            }).ToList();
            return Ok(teachers);
        }

        [Authorize]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetTeacherById(int id)
        {
            var teacher = await _teacherRepo.GetByIdAsync(id);
            if (teacher == null)
            {
                return new NotFoundResult();
            }
            return new OkObjectResult(teacher);
        }
    }
}

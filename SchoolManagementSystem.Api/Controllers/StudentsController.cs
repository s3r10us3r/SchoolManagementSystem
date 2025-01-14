using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SchoolManagementSystem.Dal.Interfaces;
using SchoolManagementSystem.Shared.Extensions;

namespace SchoolManagementSystem.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class StudentsController : ControllerBase
    {
        private readonly IStudentRepo _studentRepo;

        public StudentsController(IStudentRepo studentRepo)
        {
            _studentRepo = studentRepo;
        }


        [Authorize(Roles = "Admin,Teacher")]
        [HttpGet("get-all")]
        public async Task<IActionResult> GetAll()
        {
            var students = await _studentRepo.GetAllAsync();
            var result = students.Select(s => s.ToDto()).ToList();
            return Ok(result);
        }

        [Authorize(Roles = "Admin,Teacher")]
        [HttpGet("{id:int}")]
        public async Task<IActionResult> Get(int id)
        {
            var result = await _studentRepo.GetByIdAsync(id);
            if (result == null)
                return NotFound();
            return Ok(result.ToDto());
        }
    }
}

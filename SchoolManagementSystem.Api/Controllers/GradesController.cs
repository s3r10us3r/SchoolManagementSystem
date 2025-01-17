using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SchoolManagementSystem.Dal.Interfaces;
using SchoolManagementSystem.Models;
using SchoolManagementSystem.Shared.Dtos;

namespace SchoolManagementSystem.Api.Controllers
{
    [Route("api/[controller]")]
    public class GradesController : ControllerJwtAuthBase
    {
        private readonly IGradeRepo _gradeRepo;
        private readonly IStudentRepo _studentRepo;

        public GradesController(IGradeRepo gradeRepo, IStudentRepo studentRepo)
        {
            _gradeRepo = gradeRepo;
            _studentRepo = studentRepo;
        }

        [HttpGet("getAll")]
        [Authorize]
        public async Task<IActionResult> GetAll()
        {
            var idNullable = GetUserId();
            if (idNullable == null)
                return Unauthorized();
            var id = idNullable.Value;
            var student = await _studentRepo.FindAsync(s => s.UserId == id);
            if (student == null)
                return NotFound();
            var result = student.Grades.Select(GetGradeWithDataDto).ToList();
            return Ok(result);
        }

        [HttpPut("{id:int}")]
        [Authorize(Roles = "Admin,Teacher")]
        public async Task<IActionResult> UpdateGrade(int id, GradeDto gradeDto)
        {
            var grade = await _gradeRepo.GetByIdAsync(id);
            if (grade == null)
                return NotFound();
            grade.Value = gradeDto.Value;
            grade.Date = DateOnly.FromDateTime(DateTime.Now);
            return NoContent();
        }

        private GradeWithDataDto GetGradeWithDataDto(Grade grade)
        {
            return new GradeWithDataDto
            {
                Value = grade.Value,
                Date = grade.Date,
                AssignmentName = grade.Assignment.Name,
                ClassName = grade.Assignment.Lesson.Name,
            };
        }
    }
}

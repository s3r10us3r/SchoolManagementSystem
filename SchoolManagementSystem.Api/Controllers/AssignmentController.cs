using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SchoolManagementSystem.Dal.Interfaces;
using SchoolManagementSystem.Models;
using SchoolManagementSystem.Shared.Dtos;
using SchoolManagementSystem.Shared.Extensions;

namespace SchoolManagementSystem.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AssignmentController : ControllerJwtAuthBase
    {
        private readonly IAssignmentRepo _assignmentRepo;
        private readonly IGradeRepo _gradeRepo;
        private readonly ILessonRepo _lessonRepo;
        private readonly ITeacherRepo _teacherRepo;

        public AssignmentController(IAssignmentRepo assignmentRepo, ILessonRepo lessonRepo, IGradeRepo gradeRepo, ITeacherRepo teacherRepo)
        {
            _assignmentRepo = assignmentRepo;
            _lessonRepo = lessonRepo;
            _gradeRepo = gradeRepo;
            _teacherRepo = teacherRepo;
        }

        [HttpPost]
        [Authorize(Roles = "Admin,Teacher")]
        public async Task<IActionResult> CreateAssignment([FromBody] AssignmentDto assignmentDto)
        {
            var model = new Assignment
            {
                Name = assignmentDto.Name,
                Date = assignmentDto.Date,
                LessonId = assignmentDto.LessonId,
            };

            if (await _lessonRepo.GetByIdAsync(assignmentDto.LessonId) == null)
                return NotFound(new MessageDto("Invalid lesson"));
            await _assignmentRepo.CreateAsync(model);
            return CreatedAtAction(nameof(Get), model.Id);
        }

        [HttpGet("{id:int}")]
        [Authorize]
        public async Task<IActionResult> Get(int id)
        {
            var assignment = await _assignmentRepo.GetByIdAsync(id);
            if (assignment == null)
                return NotFound();
            return Ok(assignment.ToDto());
        }

        [HttpPut("{id:int}")]
        [Authorize(Roles = "Admin,Teacher")]
        public async Task<IActionResult> Update([FromBody] AssignmentDto assignmentDto, int id)
        {
            var assignment = await _assignmentRepo.GetByIdAsync(id);
            if (assignment == null)
                return NotFound();
            assignment.Name = assignmentDto.Name;
            assignment.Date = assignmentDto.Date;
            assignment.LessonId = assignmentDto.LessonId;
            await _assignmentRepo.UpdateAsync(assignment);
            return NoContent();
        }

        [HttpDelete("{id:int}")]
        [Authorize(Roles = "Admin,Teacher")]
        public async Task<IActionResult> Delete(int id)
        {
            var assignment = await _assignmentRepo.GetByIdAsync(id);
            if (assignment == null)
                return NotFound();
            await _assignmentRepo.DeleteAsync(assignment);
            return NoContent();
        }

        [HttpPost("{id:int}/grade")]
        [Authorize(Roles = "Admin,Teacher")]
        public async Task<IActionResult> GradeAssignment([FromBody] List<GradeDto> grades, int id)
        {
            var assignment = await _assignmentRepo.GetByIdAsync(id);
            if (assignment == null)
                return NotFound();
            foreach (var gradeDto in grades)
            {
                var existingGrade = assignment.Grades.FirstOrDefault(g => g.StudentId == gradeDto.StudentId);
                if (existingGrade != null)
                {
                    existingGrade.Value = gradeDto.Value;
                    await _gradeRepo.UpdateAsync(existingGrade);
                }
                else
                {
                    var grade = new Grade
                    {
                        Value = gradeDto.Value,
                        AssignmentId = id,
                        StudentId = gradeDto.StudentId,
                        Date = DateOnly.FromDateTime(DateTime.Today),
                    };
                }
            }
            return NoContent();
        }

        [HttpGet("getRecent")]
        [Authorize]
        public async Task<IActionResult> GetRecent()
        {
            var userIdNullable = GetUserId();
            if (userIdNullable == null)
                return Unauthorized();
            var userId = userIdNullable.Value;
            var roleNullable = GetUserRole();
            if (roleNullable == null)
                return Unauthorized();
            var role = roleNullable.Value;
            if (role == UserRole.Teacher)
                return await GetTeachersRecentAssignment(userId);
            return Ok();
        }

        private async Task<IActionResult> GetTeachersRecentAssignment(int userId)
        {
            var teacher = await _teacherRepo.GetByIdAsync(userId);
            if (teacher == null)
                return Unauthorized();
            var lessons = await _lessonRepo.FilterAsync(l => l.TeacherId == teacher.Id);
            var assignments = lessons.SelectMany(l => l.Assignments).ToList();
            return Ok(assignments.OrderByDescending(a => a.Date).Take(5).Select(a => a.ToDto()).ToList());
        }
    }
}

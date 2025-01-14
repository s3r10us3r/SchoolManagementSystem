using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SchoolManagementSystem.Dal.Interfaces;
using SchoolManagementSystem.Models;
using SchoolManagementSystem.Shared.Dtos;
using SchoolManagementSystem.Shared.Extensions;
using System.Diagnostics;

namespace SchoolManagementSystem.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LessonsController : ControllerJwtAuthBase
    {
        private readonly ILessonRepo _lessonRepo;
        private readonly ITeacherRepo _teacherRepo;
        private readonly IClassRepo _classRepo;
        private readonly IStudentRepo _studentRepo;
        private readonly IUserRepo _userRepo;

        public LessonsController(ILessonRepo lessonRepo, ITeacherRepo teacherRepo, IClassRepo classRepo, IStudentRepo studentRepo
            ,IUserRepo userRepo)
        {
            _lessonRepo = lessonRepo;
            _teacherRepo = teacherRepo;
            _classRepo = classRepo;
            _studentRepo = studentRepo;
            _userRepo = userRepo;
        }

        [Authorize]
        [HttpGet("{id:int}")]
        public async Task<IActionResult> Get(int id)
        {
            var lesson = await _lessonRepo.GetByIdAsync(id);
            if (lesson == null)
                return NotFound();
            return Ok(lesson.ToDto());
        }

        [Authorize(Roles = "Admin,Teacher")]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] NewLessonDto lessonDto)
        {
            var model = new Lesson
            {
                Name = lessonDto.Name,
                TeacherId = lessonDto.TeacherId,
                ClassId = lessonDto.ClassId,
                Occurences = lessonDto.Occurrences.Select(o => o.ToModel()).ToList(),
            };
            var class_ = await _classRepo.GetByIdAsync(lessonDto.ClassId);
            var teacher = await _teacherRepo.GetByIdAsync(lessonDto.TeacherId);
            if (class_ == null || teacher == null)
                return NotFound(new MessageDto("Invalid class or teacher"));  
            if (lessonDto.Occurrences.Count == 0)
                return BadRequest(new MessageDto("At least one occurrence is required"));
            await _lessonRepo.CreateAsync(model);
            return Ok();
        }

        [Authorize(Roles = "Admin,Teacher")]
        [HttpPut]
        public async Task<IActionResult> UpdateLesson([FromBody] LessonDto lessonDto)
        {
            var lesson = await _lessonRepo.GetByIdAsync(lessonDto.Id);
            if (lesson == null)
                return NotFound();
            lesson.Name = lessonDto.Name;
            lesson.TeacherId = lessonDto.TeacherId;
            lesson.ClassId = lessonDto.ClassId;
            lesson.Occurences = lessonDto.Occurances.Select(o => o.ToModel()).ToList();
            var class_ = await _classRepo.GetByIdAsync(lessonDto.ClassId);
            var teacher = await _teacherRepo.GetByIdAsync(lessonDto.TeacherId);
            if (class_ == null || teacher == null)
                return NotFound(new MessageDto("Invalid class or teacher"));
            await _lessonRepo.UpdateAsync(lesson);
            return Ok();
        }

        [Authorize(Roles = "Admin,Teacher")]
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var lesson = await _lessonRepo.GetByIdAsync(id);

            if (lesson == null)
                return NotFound();
            await _lessonRepo.DeleteAsync(lesson);
            return Ok();
        }

        [Authorize]
        [HttpGet("your")]
        public async Task<IActionResult> GetYour()
        {
            var role = GetUserRole();
            var id = GetUserId();
            if (role == null || id == null)
                return Unauthorized();
            else if (role == UserRole.Teacher)
                return await GetTeachersPlan(id.Value);
            else if (role == UserRole.Student)
                return await GetStudentsPlan(id.Value);
            else return NotFound();
        }

        private async Task<IActionResult> GetTeachersPlan(int id)
        {
            var user = await  _userRepo.GetByIdAsync(id);
            var teacher = await _teacherRepo.FindAsync(t => t.UserId == id);
            if (teacher == null)
                return Unauthorized();
            var lessons = await _lessonRepo.FilterAsync(l => l.TeacherId == teacher.Id);
            Console.WriteLine(lessons.Count);
            return Ok(lessons.Select(l => l.ToLessonWithTeacher()).ToList());
        }

        private async Task<IActionResult> GetStudentsPlan(int id)
        {
            var user = await _userRepo.GetByIdAsync(id);
            var student = await _studentRepo.FindAsync(s => s.UserId == id);
            if (student?.Class == null)
            {
                return NoContent();
            }
            return Ok(student.Class.Lessons.Select(l => l.ToLessonWithTeacher()).ToList());
        }
    }
}

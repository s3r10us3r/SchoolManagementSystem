using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata;
using SchoolManagementSystem.Dal.Interfaces;
using SchoolManagementSystem.Models;
using SchoolManagementSystem.Shared.Dtos;
using SchoolManagementSystem.Shared.Extensions;

namespace SchoolManagementSystem.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ClassesController : ControllerJwtAuthBase
    {
        private readonly IClassRepo _classRepo;
        private readonly IUserRepo _userRepo;
        private readonly IStudentRepo _studentRepo;

        public ClassesController(IClassRepo classRepo, IUserRepo userRepo, IStudentRepo studentRepo)
        {
            _classRepo = classRepo;
            _userRepo = userRepo;
            _studentRepo = studentRepo;
        }

        [Authorize(Roles = "Admin,Teacher")]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ClassDto classDto)
        {
            var userLogin = GetUserLogin();
            var user = await _userRepo.FindAsync(u => u.Login == userLogin);
            if (user == null || (user.Role == UserRole.Teacher && user.Id != classDto.Teacher.Id))
                return Forbid();
            var studentIds = classDto.Students.Select(s => s.Id).ToList();
            var classStudents = await _studentRepo.FilterAsync(u => studentIds.Contains(u.Id));
            var classEntity = new Class
            {
                Name = classDto.Name,
                Grade = classDto.Grade,
                TeacherId = classDto.Teacher.Id,
            };
            var model = await _classRepo.CreateAsync(classEntity);
            model.Students = classStudents;
            model = await _classRepo.UpdateAsync(model);
            return Ok(model.ToDto());
        }

        [Authorize]
        [HttpGet("all")]
        public async Task<IActionResult> GetAll()
        {
            var classes = await _classRepo.GetAllAsync();
            return Ok(classes.Select(c => c.ToDto()));
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
            foreach (var student in classEntity.Students)
            {
                student.ClassId = null;
                await _studentRepo.UpdateAsync(student);
            }
            classEntity.Students = [];
            await _classRepo.DeleteAsync(classEntity);
            return Ok();
        }

        [Authorize(Roles = "Admin,Teacher")]
        [HttpGet("load-lessons/{id:int}")]
        public async Task<IActionResult> LoadLessons(int id)
        {
            var classEntity = await _classRepo.GetByIdAsync(id);
            if (classEntity == null)
                return NotFound();
            var lessons = classEntity.Lessons.ToList();
            var lessonsDto = lessons.Select(l =>
            {
                var dto = new LessonWithDataDto
                {
                    Id = l.Id,
                    Name = l.Name,
                    TeachersName = $"{l.Teacher.FirstName} {l.Teacher.LastName}",
                    TimeStrings = l.Occurences.Select(o => {
                        return $"{NumToDayOfWeek(o.DayOfTheWeek)} {o.StartTime:HH:mm}-{o.StartTime.Add(o.Duration):HH:mm}";
                    }).ToList(),
                };
                return dto;
            });
            return Ok(lessonsDto);
        }

        private string NumToDayOfWeek(int num)
        {
            switch (num)
            {
                case 1:
                    return "Monday";
                case 2:
                    return "Tuesday";
                case 3:
                    return "Wednesday";
                case 4:
                    return "Thursday";
                case 5:
                    return "Friday";
                case 6:
                    return "Saturday";
                case 7:
                    return "Sunday";
                default:
                    return "";
            }
        }
    }
}


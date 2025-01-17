using SchoolManagementSystem.Models;
using SchoolManagementSystem.Shared.Dtos;
using System.Runtime.CompilerServices;

namespace SchoolManagementSystem.Shared.Extensions
{
    public static class Conversions
    {
        public static UserRole? ToUserRole(this string role)
        {
            return role switch
            {
                "Admin" => UserRole.Admin,
                "Teacher" => UserRole.Teacher,
                "Student" => UserRole.Student,
                _ => null,
            };
        }

        public static StudentDto ToDto(this Student student)
        {
            return new StudentDto
            {
                Id = student.Id,
                FirstName = student.FirstName,
                LastName = student.LastName,
                BirthDate = student.BirthDate,
                ClassName = student.Class?.Name ?? "",
            };
        }

        public static TeacherDto ToDto(this Teacher teacher)
        {
            return new TeacherDto
            {
                Id = teacher.Id,
                FirstName = teacher.FirstName,
                LastName = teacher.LastName,
                BirthDate = teacher.BirthDate,
            };
        }

        public static ClassDto ToDto(this Class classModel)
        {
            return new ClassDto
            {
                Id = classModel.Id,
                Grade = classModel.Grade,
                Name = classModel.Name,
                Teacher = classModel.Teacher?.ToDto(),
                Students = classModel.Students.Select(s => s.ToDto()).ToList(),
            };
        }

        public static Occurence ToModel(this NewOccuranceDto dto)
        {
            return new Occurence
            {
                StartTime = dto.StartTime,
                Duration = dto.Duration,
                DayOfTheWeek = dto.DayOfWeek,
                LessonId = dto.LessonId,
            };
        }

        public static LessonWithDataDto ToLessonWithData(this Lesson model)
        {
            return new LessonWithDataDto
            {
                Id = model.Id,
                Name = model.Name,
                TeachersName = $"{model.Teacher.FirstName} {model.Teacher.LastName}",
                TimeStrings = model.Occurences.Select(o =>
                {
                    return $"{NumToDayOfWeek(o.DayOfTheWeek)} {o.StartTime:HH:mm}-{o.StartTime.Add(o.Duration):HH:mm}";
                }).ToList(),
            };
        }

        public static LessonWithTeacherDto ToLessonWithTeacher(this Lesson model)
        {
            return new LessonWithTeacherDto
            {
                Id = model.Id,
                Name = model.Name,
                Teacher = model.Teacher.ToDto(),
                Occurances = model.Occurences.Select(o => o.ToDto()).ToList(),
            };
        }

        private static string NumToDayOfWeek(int num)
        {
            return num switch
            {
                0 => "Monday",
                1 => "Tuesday",
                2 => "Wednesday",
                3 => "Thursday",
                4 => "Friday",
                5 => "Saturday",
                6 => "Sunday",
                _ => "",
            };
        }

        public static Occurence ToModel(this OccuranceDto dto)
        {
            return new Occurence
            {
                Id = dto.Id,
                StartTime = dto.StartTime,
                Duration = dto.Duration,
                DayOfTheWeek = dto.DayOfWeek,
                LessonId = dto.LessonId,
            };
        }

        public static OccuranceDto ToDto(this Occurence occurence)
        {
            return new OccuranceDto
            {
                Id = occurence.Id,
                StartTime = occurence.StartTime,
                Duration = occurence.Duration,
                DayOfWeek = occurence.DayOfTheWeek,
                LessonId = occurence.LessonId,
            };
        }

        public static LessonDto ToDto(this Lesson lesson)
        {
            return new LessonDto
            {
                Id = lesson.Id,
                Name = lesson.Name,
                ClassId = lesson.ClassId,
                TeacherId = lesson.TeacherId,
                Occurances = lesson.Occurences.Select(o => o.ToDto()).ToList(),
            };
        }

        public static AssignmentDto ToDto(this Assignment assignment)
        {
            return new AssignmentDto
            {
                Name = assignment.Name,
                LessonId = assignment.LessonId,
                Date = assignment.Date,
                Id = assignment.Id,
            };
        }
    }
}

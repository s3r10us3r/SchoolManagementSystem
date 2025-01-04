using SchoolManagementSystem.Shared.Dtos;

namespace SchoolManagementSystem.Models.Extensions
{
    public static class Conversions
    {
        public static UserRole? ToUserRole(this string role)
        {
            return role switch
            {
                "admin" => UserRole.Admin,
                "teacher" => UserRole.Teacher,
                "student" => UserRole.Student,
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
                Teacher = classModel.Teacher.ToDto(),
                Students = classModel.Students.Select(s => s.ToDto()).ToList(),
            };
        }
    }
}

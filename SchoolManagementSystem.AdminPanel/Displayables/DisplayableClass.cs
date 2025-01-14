using SchoolManagementSystem.Shared.Dtos;

namespace SchoolManagementSystem.AdminPanel.Displayables
{
    public class DisplayableClass
    {
        public DisplayableClass()
        {
        }

        public DisplayableClass(ClassDto dto)
        {
            Id = dto.Id;
            GradeAndName = $"{dto.Grade}{dto.Name}";
            StudentCount = dto.Students.Count;
            TeacherName = $"{dto.Teacher.FirstName} {dto.Teacher.LastName}";
        }

        public int Id { get; set; }
        public string GradeAndName { get; set; } = "";
        public int StudentCount { get; set; }
        public string TeacherName { get; set; } = "";
    }
}

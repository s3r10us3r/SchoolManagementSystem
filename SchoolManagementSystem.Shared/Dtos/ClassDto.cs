using System.ComponentModel.DataAnnotations;

namespace SchoolManagementSystem.Shared.Dtos
{
    public class ClassDto
    {
        public int Id { get; set; }
        [Range(1, 6)]
        public int Grade { get; set; }
        [MinLength(1)]
        public string Name { get; set; } = "";
        public TeacherDto Teacher { get; set; }
        public List<StudentDto> Students { get; set; } = [];
    }
}

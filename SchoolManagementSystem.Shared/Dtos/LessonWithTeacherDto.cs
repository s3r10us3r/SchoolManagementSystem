namespace SchoolManagementSystem.Shared.Dtos
{
    public class LessonWithTeacherDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = "";
        public TeacherDto Teacher { get; set; } = new TeacherDto();
        public List<OccuranceDto> Occurances { get; set; } = [];
    }
}

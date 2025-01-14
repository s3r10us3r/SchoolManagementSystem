namespace SchoolManagementSystem.Shared.Dtos
{
    public class LessonWithDataDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = "";
        public string TeachersName { get; set; } = "";
        public List<string> TimeStrings { get; set; } = new List<string>();
    }
}

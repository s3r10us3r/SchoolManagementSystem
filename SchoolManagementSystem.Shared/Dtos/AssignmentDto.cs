namespace SchoolManagementSystem.Shared.Dtos
{
    public class AssignmentDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = "";
        public int LessonId { get; set; }
        public DateOnly Date { get; set; }
    }
}

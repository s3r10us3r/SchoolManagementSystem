namespace SchoolManagementSystem.Shared.Dtos
{
    public class GradeWithDataDto
    {
        public int Value { get; set; }
        public DateOnly Date { get; set; }
        public string AssignmentName { get; set; } = "";
        public string ClassName { get; set; } = "";
    }
}

namespace SchoolManagementSystem.Shared.Dtos
{
    public class StudentDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = "";
        public string LastName { get; set; } = "";
        public string ClassName { get; set; } = "";
        public DateOnly BirthDate { get; set; }
    }
}

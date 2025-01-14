using SchoolManagementSystem.Models;

namespace SchoolManagementSystem.Shared.Dtos
{
    public class UserWithDataDto
    {
        public int? Id { get; set; }
        public string? Login { get; set; } = ""; 
        public string FirstName { get; set; } = "";
        public string LastName { get; set; } = "";
        public DateOnly DateOfBirth { get; set; }
        public string Role { get; set; } = "";
    }
}

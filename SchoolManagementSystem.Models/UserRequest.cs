using Microsoft.EntityFrameworkCore;

namespace SchoolManagementSystem.Models
{
    [Index(nameof(Code))]
    public class UserRequest : Entity
    {
        public string FirstName { get; set; } = "";
        public string LastName { get; set; } = "";
        public UserRole Role { get; set; } = UserRole.Student;
        public string Code { get; set; } = "";
        public DateOnly BirthDate { get; set; }
        public DateTime IssuedAt { get; set; } = DateTime.Now;
        public int BindingId { get; set; }
    }
}

using Microsoft.EntityFrameworkCore;

namespace SchoolManagementSystem.Models
{
    public class Student : Entity
    {
        public int UserId { get; set; }
        public string FirstName { get; set; } = "";
        public string LastName { get; set; } = "";
        public DateOnly BirthDate { get; set; }

        public virtual User User { get; set; }
        public virtual ICollection<Grade> Grades { get; set; } 
        public virtual ICollection<Attendance> Attendances { get; set; }
    }
}

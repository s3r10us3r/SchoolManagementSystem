namespace SchoolManagementSystem.Models
{
    public class Teacher : Entity
    {
        public int? UserId { get; set; }
        public string FirstName { get; set; } = "";
        public string LastName { get; set; } = "";
        public DateOnly BirthDate { get; set; }

        public virtual User? User { get; set; }
    }
}

namespace SchoolManagementSystem.Models
{
    public class User : Entity
    {
        public string Login { get; set; } = "";
        public string PasswordHash { get; set; } = "";
        public UserRole Role { get; set; } = UserRole.Student;
    }

    public enum UserRole
    {
        Admin, Teacher, Student
    }
}

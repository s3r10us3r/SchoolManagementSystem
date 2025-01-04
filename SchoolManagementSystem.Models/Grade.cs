namespace SchoolManagementSystem.Models
{
    public class Grade : Entity
    {
        public int Value { get; set; }
        public int AssignmentId { get; set; }
        public int StudentId { get; set; }
        public DateOnly Date { get; set; }

        public virtual User Student { get; set; }
        public virtual Assignment Assignment { get; set; }
    }
}

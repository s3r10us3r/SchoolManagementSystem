namespace SchoolManagementSystem.Models
{
    public class Assignment : Entity
    {
        public string Name { get; set; } = "";
        public int LessonId { get; set; }
        public DateOnly Date { get; set; }

        public virtual Lesson Lesson { get; set; }
        public virtual ICollection<Grade> Grades { get; set; }
    }
}

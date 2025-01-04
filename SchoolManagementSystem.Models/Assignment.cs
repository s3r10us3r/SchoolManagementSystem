namespace SchoolManagementSystem.Models
{
    public class Assignment : Entity
    {
        public string Name { get; set; } = "";
        public int LessonId { get; set; }
        public int? FinalGrade { get; set; }

        public virtual Lesson Lesson { get; set; }
        public virtual ICollection<Grade> Grades { get; set; }
    }
}

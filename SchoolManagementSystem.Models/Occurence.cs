namespace SchoolManagementSystem.Models
{
    public class Occurence : Entity
    {
        public DateTime StartTime { get; set; }
        public TimeSpan Duration { get; set; }
        public int LessonId{ get; set; }

        public virtual Lesson Lesson { get; set; }
        public virtual ICollection<Attendance> Attendances { get; set; } = [];
    }
}

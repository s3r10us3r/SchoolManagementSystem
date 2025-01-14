using System.ComponentModel.DataAnnotations;

namespace SchoolManagementSystem.Models
{
    public class Occurence : Entity
    {
        public TimeOnly StartTime { get; set; }
        public TimeSpan Duration { get; set; }
        [Range(1,7)]
        public int DayOfTheWeek { get; set; }
        public int LessonId{ get; set; }

        public virtual Lesson Lesson { get; set; }
    }
}

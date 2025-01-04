using System.ComponentModel.DataAnnotations;

namespace SchoolManagementSystem.Models
{
    public class Class : Entity
    {
        [Range(1, 6)]
        public int Grade { get; set; }
        [MinLength(1)]
        public string Name { get; set; } = "";
        public int TeacherId { get; set; }


        public virtual Teacher Teacher { get; set; }
        public virtual ICollection<Student> Students { get; set; }
        public virtual ICollection<Assignment> Assignments { get; set; }
        public virtual ICollection<Lesson> Lessons { get; set; }
    }
}

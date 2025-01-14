using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolManagementSystem.Models
{
    public class Lesson : Entity
    {
        public string Name { get; set; } = "";
        [ForeignKey(nameof(Teacher))]
        public int TeacherId { get; set; }
        public int ClassId { get; set; }

        public virtual Teacher Teacher { get; set; }
        public virtual Class Class { get; set; }
        public virtual ICollection<Assignment> Assignments { get; set; } = [];
        public virtual ICollection<Occurence> Occurences { get; set; } = [];
    }
}

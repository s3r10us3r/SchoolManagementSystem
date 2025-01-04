namespace SchoolManagementSystem.Models
{
    public class Attendance : Entity
    {
        public bool Present { get; set; }
        public int OccuranceId { get; set; }
        public int StudentId { get; set; }

        public virtual Occurence Occurence { get; set; }
        public virtual Student Student { get; set; }
    }
}

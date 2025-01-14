using System.ComponentModel.DataAnnotations;

namespace SchoolManagementSystem.Shared.Dtos
{
    public class OccuranceDto
    {
        public int Id { get; set; }
        public TimeOnly StartTime { get; set; }
        public TimeSpan Duration { get; set; }
        [Range(1,7)]
        public int DayOfWeek { get; set; } = 1;
        public int LessonId { get; set; }
    }
}

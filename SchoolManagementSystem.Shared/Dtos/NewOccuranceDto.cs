using System.ComponentModel.DataAnnotations;

namespace SchoolManagementSystem.Shared.Dtos
{
    public class NewOccuranceDto
    {
        [Required]
        public TimeOnly StartTime { get; set; }
        [Required]
        public TimeSpan Duration { get; set; }
        [Required]
        public int LessonId { get; set; }
        [Required]
        [Range(1, 7)]
        public int DayOfWeek { get; set; } = 1;
    }
}

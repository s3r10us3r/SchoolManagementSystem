using System.ComponentModel.DataAnnotations;

namespace SchoolManagementSystem.Shared.Dtos
{
    public class GradeDto
    {
        [Required]
        [Range(1, 6)]
        public int Value { get; set; }
        [Required]
        public int AssignmentId { get; set; }
        [Required]
        public int StudentId { get; set; }
    }
}

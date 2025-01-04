using System.ComponentModel.DataAnnotations;

namespace SchoolManagementSystem.Shared.Dtos
{
    public class NewClassDto
    {
        [Required]
        public int Grade { get; set; }
        [Required]
        public string Name { get; set; } = "";
        [Required]
        public int TeacherId { get; set; }
    }
}

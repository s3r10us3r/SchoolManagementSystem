using System.ComponentModel.DataAnnotations;

namespace SchoolManagementSystem.Shared.Dtos
{
    public class RegisterUserDto
    {
        [Required]
        [StringLength(maximumLength: 128, MinimumLength = 6)]
        public string Login { get; set; } = "";
        [Required]
        [StringLength(128, MinimumLength = 10)]
        public string Password { get; set; } = "";
        [Required]
        public string Code { get; set; } = "";
    }
}

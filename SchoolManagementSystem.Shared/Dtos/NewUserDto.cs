using System.ComponentModel.DataAnnotations;

namespace SchoolManagementSystem.Shared.Dtos
{
    public class NewUserDto
    {
        [Required] public string FirstName { get; set; } = "";
        [Required] public string LastName { get; set; } = "";
        [Required] public DateOnly DateOfBirth { get; set; }
        [Required] public string Role { get; set; } = "";
    }
}

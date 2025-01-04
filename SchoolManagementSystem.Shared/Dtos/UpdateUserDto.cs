using System.ComponentModel.DataAnnotations;

namespace SchoolManagementSystem.Shared.Dtos
{
    public class UpdateUserDto
    {
        [Required] public string FirstName { get; set; }
        [Required] public string LastName { get; set; }
        [Required] public string Login { get; set; }
    }
}

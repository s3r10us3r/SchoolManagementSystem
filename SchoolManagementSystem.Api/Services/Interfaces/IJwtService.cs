using SchoolManagementSystem.Models;

namespace SchoolManagementSystem.Api.Services.Interfaces
{
    public interface IJwtService
    {
        public string GetJwtForUser(User user);
    }
}

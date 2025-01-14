using SchoolManagementSystem.Models;
using SchoolManagementSystem.Shared.Dtos;

namespace SchoolManagementSystem.Api.Services.Interfaces
{
    public interface IUserService
    {
        public Task CreateUser(UserRequest request, string login, string password);
        public Task<(string token, string role)?> LoginUser(LoginDto loginDto);
        public Task DeleteUser(int userId);
    }
}

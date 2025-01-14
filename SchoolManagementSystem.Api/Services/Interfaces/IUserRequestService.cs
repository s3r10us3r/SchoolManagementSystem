using SchoolManagementSystem.Models;
using SchoolManagementSystem.Shared.Dtos;

namespace SchoolManagementSystem.Api.Services.Interfaces
{
    public interface IUserRequestService
    {
        public Task<UserRequest?> GetRequestAsync(string code);
        public Task<string> CreateRequestAsync(NewUserDto request);
        public Task DeleteRequestAsync(string code);
    }
}

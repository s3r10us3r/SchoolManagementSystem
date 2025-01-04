using SchoolManagementSystem.Models;

namespace SchoolManagementSystem.Api.Services.Interfaces
{
    public interface IUserRequestService
    {
        public Task<UserRequest?> GetRequestAsync(string code);
        public Task<string> CreateRequestAsync(string firstName, string lastName, UserRole role);
        public Task DeleteRequestAsync(string code);
    }
}

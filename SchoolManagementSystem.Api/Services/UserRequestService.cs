using SchoolManagementSystem.Api.Services.Interfaces;
using SchoolManagementSystem.Dal.Interfaces;
using SchoolManagementSystem.Models;

namespace SchoolManagementSystem.Api.Services
{
    public class UserRequestService : IUserRequestService
    {
        private readonly IUserRequestRepo _repo;

        public UserRequestService(IUserRequestRepo repo)
        {
            _repo = repo;
        }

        public async Task<string> CreateRequestAsync(string firstName, string lastName, UserRole role)
        {
            var request = new UserRequest
            {
                FirstName = firstName,
                LastName = lastName,
                Role = role,
                Code = Guid.NewGuid().ToString(),
            };

            await _repo.CreateAsync(request);
            return request.Code;
        }

        public async Task DeleteRequestAsync(string code)
        {
            var request = await _repo.FindAsync(r => r.Code == code);
            if (request != null)
                await _repo.DeleteAsync(request);
        }

        public async Task<UserRequest?> GetRequestAsync(string code)
        {
            var result = await _repo.FindAsync(r => r.Code == code);
            return result;
        }
    }
}

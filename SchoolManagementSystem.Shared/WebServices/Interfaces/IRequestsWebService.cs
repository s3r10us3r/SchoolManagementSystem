using SchoolManagementSystem.Models;
using SchoolManagementSystem.Shared.Dtos;

namespace SchoolManagementSystem.Shared.WebServices.Interfaces
{
    public interface IRequestsWebService
    {
        Task<Result<RegisterUserResponse>> CreateRequest(NewUserDto newUserDto);
        Task<Result<List<UserRequest>>> GetAll();
        Task<Result<object>> DeleteRequest(string code);
    }
}

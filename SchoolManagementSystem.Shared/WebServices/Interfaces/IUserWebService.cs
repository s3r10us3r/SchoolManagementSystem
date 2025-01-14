using SchoolManagementSystem.Shared.Dtos;
using SchoolManagementSystem.Models;

namespace SchoolManagementSystem.Shared.WebServices.Interfaces
{
    public interface IUserWebService
    {
        Task<Result<LoginResponseDto>> Login(LoginDto loginDto);
        Task<Result<bool>> IsInitialized();
        Task<Result<bool>> RegisterAdmin(RegisterAdminDto adminDto);
        Task<Result<object>> RegisterUser(RegisterUserDto userDto);
        Task<Result<List<UserWithDataDto>>> GetAllWithData();
        Task<Result<List<User>>> GetAll();
        Task<Result<object>> Delete(int id);
    }
}

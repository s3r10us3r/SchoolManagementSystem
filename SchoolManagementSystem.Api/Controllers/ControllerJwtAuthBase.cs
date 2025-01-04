using Microsoft.AspNetCore.Mvc;
using SchoolManagementSystem.Models;
using SchoolManagementSystem.Models.Extensions;

namespace SchoolManagementSystem.Api.Controllers
{
    public abstract class ControllerJwtAuthBase : ControllerBase
    {
        protected UserRole? GetUserRole()
        {
            return User.Claims
                .FirstOrDefault(c => c.Type == "role")?
                .Value
                .ToUserRole();
        }

        protected string? GetUserLogin()
        {
            return User.Claims
                .FirstOrDefault(c => c.Type == "login")?
                .Value;
        }   
    }
}

using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.JsonWebTokens;
using SchoolManagementSystem.Models;
using SchoolManagementSystem.Shared.Extensions;

namespace SchoolManagementSystem.Api.Controllers
{
    public abstract class ControllerJwtAuthBase : ControllerBase
    {
        protected UserRole? GetUserRole()
        {
            var chuj = User.Claims
                .FirstOrDefault(c => c.Type == "role")?
                .Value;
            Console.WriteLine($"KURWA TOKEN {chuj ?? "nie ma"}");
            return User.Claims
                .FirstOrDefault(c => c.Type == "role")?
                .Value
                .ToUserRole();
        }

        protected string? GetUserLogin()
        {
            return User.Claims
                .FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Name)?
                .Value;
        }

        protected int? GetUserId()
        {
            var subString = User.Claims
                .FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Sub)?
                .Value;
            if (subString == null)
                return null;
            return int.Parse(subString);
        }

        protected void DisplayClaims()
        {
            List<string> roles = ["Admin", "Student", "Teacher"];
            foreach (var role in roles)
            {
                Console.WriteLine($"Role {role}, {User.IsInRole(role)}");
            }
            foreach (var claim in User.Claims)
            {
                Console.WriteLine($"Claim type: {claim.Type}; Claim value: {claim.Value}");
            }
            foreach(var identity in User.Identities)
            {
                Console.WriteLine($"Identity Authentication Type: {identity.AuthenticationType}");
                foreach (var claim in identity.Claims)
                {
                    Console.WriteLine($"Claim type: {claim.Type}; Claim value {claim.Value}");
                }
            }
        }
    }
}

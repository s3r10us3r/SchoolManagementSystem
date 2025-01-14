using Microsoft.AspNetCore.Identity;
using SchoolManagementSystem.Api.Services.Interfaces;

namespace SchoolManagementSystem.Api.Services.Extensions
{
    public static class DependencyInjection
    {
        public static void AddServices(this IServiceCollection services)
        {
            services.AddScoped<IPasswordHasher<object>, PasswordHasher<object>>();
            services.AddScoped<IJwtService, JwtService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IUserRequestService, UserRequestService>();
        }
    }
}

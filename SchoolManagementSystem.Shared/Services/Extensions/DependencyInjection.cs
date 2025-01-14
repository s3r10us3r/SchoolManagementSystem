using Microsoft.Extensions.DependencyInjection;
using SchoolManagementSystem.Shared.WebServices;
using SchoolManagementSystem.Shared.WebServices.Interfaces;

namespace SchoolManagementSystem.Shared.Services.Extensions
{
    public static class DependencyInjection  
    {
        public static void AddSharedServices(this IServiceCollection services)
        {
            services.AddSingleton<PasswordValidator>();
        }
    }
}

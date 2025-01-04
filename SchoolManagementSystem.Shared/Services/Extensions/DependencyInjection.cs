using Microsoft.Extensions.DependencyInjection;

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

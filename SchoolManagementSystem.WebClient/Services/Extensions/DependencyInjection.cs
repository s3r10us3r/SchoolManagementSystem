namespace SchoolManagementSystem.WebClient.Services.Extensions
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddScoped<BearerLoginService>();
            return services;
        }
    }
}

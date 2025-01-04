using Microsoft.Extensions.DependencyInjection;
using SchoolManagementSystem.Dal.Interfaces;

namespace SchoolManagementSystem.Dal.Extensions
{
    public static class DependencyInjection
    {
        public static void AddRepos(this IServiceCollection services)
        {
            services.AddScoped<IAssignmentRepo, AssignmentRepo>();
            services.AddScoped<ILessonRepo, LessonRepo>();
            services.AddScoped<IGradeRepo, GradeRepo>();
            services.AddScoped<IUserRepo, UserRepo>();
            services.AddScoped<IUserRequestRepo, UserRequestRepo>();
        }
    }
}

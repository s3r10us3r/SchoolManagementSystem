using Microsoft.Extensions.DependencyInjection;
using SchoolManagementSystem.Dal.Interfaces;

namespace SchoolManagementSystem.Dal.Extensions
{
    public static class DependencyInjection
    {
        public static void AddRepos(this IServiceCollection services)
        {
            services.AddDbContext<SmsDbContext>();
            services.AddScoped<IAssignmentRepo, AssignmentRepo>();
            services.AddScoped<IAttendanceRepo, AttendanceRepo>();
            services.AddScoped<IClassRepo, ClassRepo>();
            services.AddScoped<IGradeRepo, GradeRepo>();
            services.AddScoped<ILessonRepo, LessonRepo>();
            services.AddScoped<IOccuranceRepo, OccuranceRepo>();
            services.AddScoped<IStudentRepo, StudentRepo>();
            services.AddScoped<ITeacherRepo, TeacherRepo>();
            services.AddScoped<IUserRepo, UserRepo>();
            services.AddScoped<IUserRequestRepo, UserRequestRepo>();
        }
    }
}

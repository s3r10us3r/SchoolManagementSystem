using Microsoft.Extensions.DependencyInjection;
using SchoolManagementSystem.Shared.WebServices.Interfaces;

namespace SchoolManagementSystem.Shared.WebServices.Extensions
{
    public static class DependencyInjection
    {
        public static void AddWebServices(this IServiceCollection services)
        {
            services.AddTransient<IRequestsWebService, RequestsWebService>();
            services.AddTransient<IUserWebService, UserWebService>();
            services.AddTransient<IClassesWebService, ClassesWebService>();
            services.AddTransient<ITeachersWebService, TeachersWebService>();
            services.AddTransient<IStudentsWebService, StudentsWebService>();
            services.AddTransient<ILessonsWebService, LessonsWebService>();
            services.AddTransient<IAssignmentsWebService, AssignmentsWebService>();
            services.AddTransient<IGradesWebService, GradesWebService>();
        }
    }
}

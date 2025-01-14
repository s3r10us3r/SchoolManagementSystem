using System.Net.Http;
using System.Windows;
using Microsoft.Extensions.DependencyInjection;
using SchoolManagementSystem.AdminPanel.Factories;
using SchoolManagementSystem.AdminPanel.Pages;
using SchoolManagementSystem.AdminPanel.Services;
using SchoolManagementSystem.AdminPanel.Services.Interfaces;
using SchoolManagementSystem.AdminPanel.ViewModels;
using SchoolManagementSystem.Shared.Services.Extensions;
using SchoolManagementSystem.Shared.WebServices.Extensions;
using SchoolManagementSystem.Shared.WebServices.Interfaces;

namespace SchoolManagementSystem.AdminPanel
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static IServiceProvider ServiceProvider { get; private set; }

        protected override void OnStartup(StartupEventArgs e)
        {
            MessageBox.Show("App started");
            try
            {
                var serviceCollection = new ServiceCollection();
                ConfigureServices(serviceCollection);
                ServiceProvider = serviceCollection.BuildServiceProvider();
                ServiceProvider.GetService<IClassesWebService>();
                var navigationService = ServiceProvider.GetRequiredService<INavigationService>();
                navigationService.ChangeWindow<MainWindow>();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occured {ex.Message}");
                Shutdown();
            }
            base.OnStartup(e);
        }

        private void ConfigureServices(ServiceCollection services)
        {
            services.AddTransient<MainWindow>();
            services.AddSingleton<DataStore>();
            services.AddSingleton<INavigationService, NavigationService>();
            services.AddSingleton<ILoginService, BearerLoginService>();
            services.AddSharedServices();
            services.AddWebServices();
            AddViewModels(services);
            AddPages(services);
            AddHttpClient(services);
        }

        private void AddViewModels(ServiceCollection services)
        {
            services.AddTransient<RegisterAdministratorViewModel>();
            services.AddTransient<LoginPageViewModel>();
            services.AddTransient<LoggedInWindowViewModel>();
            services.AddTransient<UserPageViewModel>();
            services.AddTransient<NewUserViewModel>();
            services.AddTransient<PendingRequestsViewModel>();
            services.AddTransient<ClassesViewModel>();
            services.AddTransient<NewClassViewModel>();
        }

        private void AddPages(ServiceCollection services)
        {
            services.AddTransient<LoginPage>();
            services.AddTransient<RegisterAdminPage>();
            services.AddTransient<LoggedInWindow>();
            services.AddTransient<UsersPage>();
            services.AddTransient<NewUserPage>();
            services.AddTransient<UserRegisteredPageFactory>();
            services.AddTransient<ClassDetailsPageFactory>();
            services.AddTransient<PendingRequestsPage>();
            services.AddTransient<ClassesPage>();
            services.AddTransient<NewClassPage>();
            services.AddTransient<AddLessonWindowFactory>();
        }

        private void AddHttpClient(ServiceCollection services)
        {
            var client = new HttpClient()
            {
                BaseAddress = new Uri("https://localhost:7191/")
            };
            services.AddSingleton(client);
        }
    }

}

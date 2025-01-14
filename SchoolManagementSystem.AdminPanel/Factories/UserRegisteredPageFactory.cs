using SchoolManagementSystem.AdminPanel.Pages;
using SchoolManagementSystem.AdminPanel.Services.Interfaces;
using SchoolManagementSystem.AdminPanel.ViewModels;
using SchoolManagementSystem.Shared.Dtos;

namespace SchoolManagementSystem.AdminPanel.Factories
{
    public class UserRegisteredPageFactory
    {
        private readonly INavigationService _navigationService;

        public UserRegisteredPageFactory(INavigationService navigationService)
        {
            _navigationService = navigationService;
        }

        public UserRegisteredPage CreatePage(RegisterUserResponse data)
        {
            var vm = new RegistrationSuccessfulViewModel(data, _navigationService);
            var page = new UserRegisteredPage(vm);
            return page;
        }
    }
}

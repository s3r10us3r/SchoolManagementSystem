using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SchoolManagementSystem.AdminPanel.Pages;
using SchoolManagementSystem.AdminPanel.Services.Interfaces;
using SchoolManagementSystem.Shared.Dtos;

namespace SchoolManagementSystem.AdminPanel.ViewModels
{
    public partial class RegistrationSuccessfulViewModel : ObservableObject
    {
        private readonly INavigationService _navigationService;

        public RegistrationSuccessfulViewModel(RegisterUserResponse data, INavigationService navigationService) 
        {
            _navigationService = navigationService;

            Code = data.Code;
            FirstName = data.FirstName;
            LastName = data.LastName;
        }

        [ObservableProperty]
        private string code = "";
        [ObservableProperty]
        private string firstName = "";
        [ObservableProperty]
        private string lastName = "";

        [RelayCommand]
        public void Done()
        {
            _navigationService.NavigateTo<UsersPage>();
        }
    }
}

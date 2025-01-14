using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SchoolManagementSystem.AdminPanel.Services;
using SchoolManagementSystem.AdminPanel.Services.Interfaces;
using SchoolManagementSystem.Shared.Dtos;
using SchoolManagementSystem.Shared.WebServices.Interfaces;
using System.Windows;

namespace SchoolManagementSystem.AdminPanel.ViewModels
{
    public partial class LoginPageViewModel : ObservableObject
    {
        private readonly INavigationService _navigationService;
        private readonly IUserWebService _userWebService;
        private readonly DataStore _dataStore;
        private readonly ILoginService _loginService;

        public LoginPageViewModel(INavigationService navigationService, IUserWebService userWebService, 
            DataStore dataStore, ILoginService loginService)
        {
            _navigationService = navigationService;
            _userWebService = userWebService;
            _dataStore = dataStore;
            _loginService = loginService;
        }

        [ObservableProperty]
        private string login = "";
        [ObservableProperty]
        private string password = "";
        [ObservableProperty]
        private string errorMessage = "";

        [RelayCommand]
        public async Task SubmitLogin()
        {
            if (string.IsNullOrWhiteSpace(Login) || string.IsNullOrWhiteSpace(Password))
            {
                ErrorMessage = "Login and password are required";
                return;
            }

            var result = await _userWebService.Login(new LoginDto { Login = Login, Password = Password});
            if (result.IsSuccess)
            {
                var dto = result.Data!;
                if (dto.Role != "Admin")
                {
                    ErrorMessage = "You are not authorized to access this application";
                    return;
                }
                _dataStore.Jwt = dto.Token;
                _loginService.SetLoginHeader(dto.Token);

                await Application.Current.Dispatcher.InvokeAsync(_navigationService.ChangeWindow<LoggedInWindow>);
            }
            else
            {
                ErrorMessage = result.Message!;
            }
        }
    }
}

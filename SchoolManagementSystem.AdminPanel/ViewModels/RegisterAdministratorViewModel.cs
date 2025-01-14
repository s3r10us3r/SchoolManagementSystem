using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SchoolManagementSystem.AdminPanel.Pages;
using SchoolManagementSystem.AdminPanel.Services.Interfaces;
using SchoolManagementSystem.Shared.Dtos;
using SchoolManagementSystem.Shared.Services;
using SchoolManagementSystem.Shared.WebServices.Interfaces;
using System.Diagnostics;
using System.Windows;

namespace SchoolManagementSystem.AdminPanel.ViewModels
{
    public partial class RegisterAdministratorViewModel : ObservableObject
    {
        private readonly IUserWebService _userWebService;
        private readonly PasswordValidator _passwordValidator = new PasswordValidator();
        private readonly INavigationService _navigationService;

        public RegisterAdministratorViewModel(IUserWebService userWebService, INavigationService navigationService)
        {
            _userWebService = userWebService;
            _navigationService = navigationService;
        }

        [ObservableProperty]
        private string login = "";
        [ObservableProperty]
        private string password = "";
        [ObservableProperty]
        private string repeatPassword = "";
        [ObservableProperty]
        private string errorMessage = "";
        [ObservableProperty]
        private int passwordStrength = 0;
        [ObservableProperty]
        private string passwordStrengthText = "";

        [RelayCommand]
        public async Task RegisterAdmin()
        {
            Debug.WriteLine("RegisterAdmin method called");

            if (Login.Length < 8)
            {
                ErrorMessage = "Username is too short.";
                return;
            }
            if (Password != RepeatPassword)
            {
                ErrorMessage = "Passwords do not match.";
                return;
            }
            if (!_passwordValidator.IsPasswordValid(Password, out var message))
            {
                ErrorMessage = message;
                return;
            }

            ErrorMessage = "";

            var accepted = ShowDialog();
            if (!accepted)
            {
                return;
            }

            var dto = new RegisterAdminDto
            {
                Login = Login,
                Password = Password
            };
            var result = await _userWebService.RegisterAdmin(dto);
            if (result.IsFailure)
            {
                ErrorMessage = result.Message!;
            }
            else
            {
                await Application.Current.Dispatcher.InvokeAsync(() => _navigationService.NavigateTo<LoginPage>());
            }
        }

        private bool ShowDialog()
        {
            MessageBoxResult result = MessageBox.Show(
                """
                    You are about to register the administrator account.
                    This action is irreversible and you can't retrieve administrator password later.
                    Do you want to proceed?
                    """,
                "Confirmation",
                MessageBoxButton.OKCancel,
                MessageBoxImage.Question);
            return result == MessageBoxResult.OK;
        }

        [RelayCommand]
        public void PasswordBox_PasswordChanged(string password)
        {
            Password = password;
        }

        [RelayCommand]
        public void RepeatPasswordBox_PasswordChanged(string password)
        {
            RepeatPassword = password;
        }
    }
}

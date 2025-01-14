using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SchoolManagementSystem.AdminPanel.Factories;
using SchoolManagementSystem.AdminPanel.Pages;
using SchoolManagementSystem.AdminPanel.Services.Interfaces;
using SchoolManagementSystem.Shared.Dtos;
using SchoolManagementSystem.Shared.WebServices.Interfaces;
using System.Windows;

namespace SchoolManagementSystem.AdminPanel.ViewModels
{
    public partial class NewUserViewModel : ObservableObject
    {
        private readonly INavigationService _navigationService;
        private readonly UserRegisteredPageFactory _userRegisteredPageFactory;
        private readonly IRequestsWebService _requestsWebService;

        public NewUserViewModel(INavigationService navigationService, IRequestsWebService requestsWebService, UserRegisteredPageFactory userRegisteredPageFactory)
        {
            _navigationService = navigationService;
            _requestsWebService = requestsWebService;
            _userRegisteredPageFactory = userRegisteredPageFactory;
        }

        [ObservableProperty]
        private string firstName = "";
        [ObservableProperty]
        private string lastName = "";
        [ObservableProperty]
        private DateTime? dateOfBirth;
        [ObservableProperty]
        private string role = "";

        [RelayCommand]
        public async Task Submit()
        {
            MessageBox.Show($"Birthdate set {(DateOfBirth is not null ? DateOfBirth.Value.ToString() : "dupa")}");
            var minDate = DateOnly.FromDateTime(new DateTime(1900, 1, 1));
            var convertedDateOfBirth = DateOnly.FromDateTime(DateOfBirth.Value);
            if (string.IsNullOrWhiteSpace(FirstName) || string.IsNullOrWhiteSpace(LastName) || string.IsNullOrWhiteSpace(Role) || DateOfBirth is null)
            {
                MessageBox.Show($"Please fill all fields correctly. {FirstName}, {LastName}, {DateOfBirth}, {Role}", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            var newUserDto = new NewUserDto
            {
                FirstName = FirstName,
                LastName = LastName,
                DateOfBirth = convertedDateOfBirth,
                Role = Role
            };

            var result = await _requestsWebService.CreateRequest(newUserDto);
            if (result.IsSuccess)
            {
                var data = result.Data;
                var newPage = _userRegisteredPageFactory.CreatePage(data!);
                _navigationService.NavigateTo(newPage);
            }
            else
            {
                MessageBox.Show($"An error occured while creating user. Error code {result.StatusCode}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        [RelayCommand]
        public void Cancel()
        {
            _navigationService.NavigateTo<UsersPage>();
        }
    }
}

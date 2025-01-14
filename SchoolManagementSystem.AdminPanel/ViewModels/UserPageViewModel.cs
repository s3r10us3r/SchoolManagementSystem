using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SchoolManagementSystem.AdminPanel.Pages;
using SchoolManagementSystem.AdminPanel.Services.Interfaces;
using SchoolManagementSystem.Shared.Dtos;
using SchoolManagementSystem.Shared.WebServices.Interfaces;
using System.Collections.ObjectModel;
using System.Windows;

namespace SchoolManagementSystem.AdminPanel.ViewModels
{
    public partial class UserPageViewModel : ObservableObject
    {
        private readonly INavigationService _navigationService;
        private readonly IUserWebService _userWebService;

        public UserPageViewModel(INavigationService navigationService, IUserWebService userWebService) 
        {
            _navigationService = navigationService;
            _userWebService = userWebService;
            LoadUsers();
        }

        [ObservableProperty]
        private ObservableCollection<UserWithDataDto> students = [];
        [ObservableProperty]
        private ObservableCollection<UserWithDataDto> teachers = [];

        [RelayCommand]
        public void AddUser()
        {
            _navigationService.NavigateTo<NewUserPage>();
        }

        [RelayCommand]
        public void Refresh()
        {
            LoadUsers();
        }

        private async Task LoadUsers()
        {
            var result = await _userWebService.GetAllWithData();
            if (result.IsFailure)
            {
                MessageBox.Show("Failed to load users", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                var users = result.Data!;
                var teachers = users.Where(u => u.Role == "Teacher").ToList();
                var students = users.Where(u => u.Role == "Student").ToList();
                Teachers = new ObservableCollection<UserWithDataDto>(teachers);
                Students = new ObservableCollection<UserWithDataDto>(students);
            }
        }
    }
}

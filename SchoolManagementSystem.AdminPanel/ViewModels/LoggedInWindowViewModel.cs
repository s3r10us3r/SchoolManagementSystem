using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SchoolManagementSystem.AdminPanel.Pages;
using SchoolManagementSystem.AdminPanel.Services.Interfaces;
using System.Windows;

namespace SchoolManagementSystem.AdminPanel.ViewModels
{
    public partial class LoggedInWindowViewModel : ObservableObject
    {
        private readonly INavigationService _navigationService;

        public LoggedInWindowViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
        }

        [RelayCommand]
        public void NavigateToUserList()
        {
            _navigationService.NavigateTo<UsersPage>();
        }

        [RelayCommand]
        public void NavigateToPendingRequests()
        {
            _navigationService.NavigateTo<PendingRequestsPage>();
        }

        [RelayCommand]
        public void NavigateToClasses()
        {
            _navigationService.NavigateTo<ClassesPage>();
        }
    }
}

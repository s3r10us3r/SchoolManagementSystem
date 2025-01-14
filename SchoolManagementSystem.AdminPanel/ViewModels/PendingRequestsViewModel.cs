using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SchoolManagementSystem.Models;
using SchoolManagementSystem.Shared.WebServices.Interfaces;
using System.Collections.ObjectModel;
using System.Windows;

namespace SchoolManagementSystem.AdminPanel.ViewModels
{
    public partial class PendingRequestsViewModel : ObservableObject
    {
        private readonly IRequestsWebService _requestsWebService;

        public PendingRequestsViewModel(IRequestsWebService requestsWebService)
        {
            _requestsWebService = requestsWebService;
            LoadRequests();
        }

        private async Task LoadRequests()
        {
            var requests = await _requestsWebService.GetAll();
            if (requests.IsFailure)
            {
                await App.Current.Dispatcher.InvokeAsync(() => MessageBox.Show("Failed to get requests", "Error", MessageBoxButton.OK, MessageBoxImage.Error));
            }
            else
            {
                PendingRequests = new ObservableCollection<UserRequest>(requests.Data!);
            }
        }

        [ObservableProperty]
        ObservableCollection<UserRequest> pendingRequests = [];

        [RelayCommand]

        public void Delete(UserRequest request)
        {
            var result = _requestsWebService.DeleteRequest(request.Code).Result;
            if (result.IsFailure)
            {
                MessageBox.Show("Failed to delete request", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                PendingRequests.Remove(request);
            }
        }

        [RelayCommand]
        public void CopyCode(UserRequest request)
        {
            Clipboard.SetText(request.Code);
            MessageBox.Show("Code copied to clipboard", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }
}

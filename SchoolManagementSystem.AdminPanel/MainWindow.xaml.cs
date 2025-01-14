using SchoolManagementSystem.AdminPanel.Pages;
using SchoolManagementSystem.AdminPanel.Services.Interfaces;
using SchoolManagementSystem.Shared.WebServices.Interfaces;
using System.Windows;

namespace SchoolManagementSystem.AdminPanel
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly INavigationService _navigationService;
        private readonly IUserWebService _userWebService;

        public MainWindow(INavigationService navigationService, IUserWebService userWebService)
        {
            _navigationService = navigationService;
            _userWebService = userWebService;
            InitializeComponent();
            navigationService.SetFrame(MainFrame);
            NavigateToFirstPage();
        }

        private async Task NavigateToFirstPage()
        {
            try
            {
                var systemInitializedResult = await _userWebService.IsInitialized();
                if (systemInitializedResult.IsSuccess && !systemInitializedResult.Data)
                {
                    _navigationService.NavigateTo<RegisterAdminPage>();
                    MessageBox.Show("System is not initialized");
                }
                else
                {
                    _navigationService.NavigateTo<LoginPage>();
                    MessageBox.Show("System is initialized");
                }
            }
            catch (Exception e)
            {
                MessageBox.Show($"An error occured {e.Message}");
            }
        }
    }
}
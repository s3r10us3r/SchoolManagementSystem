using SchoolManagementSystem.AdminPanel.ViewModels;
using System.Windows;
using System.Windows.Controls;

namespace SchoolManagementSystem.AdminPanel.Pages
{
    /// <summary>
    /// Interaction logic for LoginPage.xaml
    /// </summary>
    public partial class LoginPage : Page
    {
        private readonly LoginPageViewModel _viewModel;

        public LoginPage(LoginPageViewModel viewModel)
        {
            DataContext = viewModel;
            _viewModel = viewModel;
            InitializeComponent();
        }

        void PasswordChangedHandle(object sender, RoutedEventArgs e)
        {
            if (sender is PasswordBox passwordBox)
            {
                _viewModel.Password = passwordBox.Password;
            }
        }
    }
}

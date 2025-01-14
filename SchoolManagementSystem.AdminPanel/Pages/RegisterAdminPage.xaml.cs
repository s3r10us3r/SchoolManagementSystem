using SchoolManagementSystem.AdminPanel.ViewModels;
using System.Windows.Controls;

namespace SchoolManagementSystem.AdminPanel.Pages
{
    /// <summary>
    /// Interaction logic for RegisterAdminPage.xaml
    /// </summary>
    public partial class RegisterAdminPage : Page
    {
        private readonly RegisterAdministratorViewModel _viewModel;

        public RegisterAdminPage(RegisterAdministratorViewModel registerAdministratorViewModel)
        {
            _viewModel = registerAdministratorViewModel;
            DataContext = _viewModel;
            InitializeComponent();
        }

        void PasswordChangedHandler(object sender, System.Windows.RoutedEventArgs e)
        {
            if (sender is PasswordBox passwordBox)
            {
                _viewModel.Password = passwordBox.Password;
            }
        }

        void RepeatPasswordChangedHandler(object sender, System.Windows.RoutedEventArgs e)
        {
            if (sender is PasswordBox passwordBox)
            {
                _viewModel.RepeatPassword = passwordBox.Password;
            }
        }
    }
}

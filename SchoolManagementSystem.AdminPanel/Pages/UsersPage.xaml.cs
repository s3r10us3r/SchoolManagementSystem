using SchoolManagementSystem.AdminPanel.ViewModels;
using System.Windows.Controls;

namespace SchoolManagementSystem.AdminPanel.Pages
{
    /// <summary>
    /// Interaction logic for UsersPage.xaml
    /// </summary>
    public partial class UsersPage : Page
    {
        public UsersPage(UserPageViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }
    }
}

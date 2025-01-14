using SchoolManagementSystem.AdminPanel.ViewModels;
using System.Windows.Controls;

namespace SchoolManagementSystem.AdminPanel.Pages
{
    /// <summary>
    /// Interaction logic for UserRegisteredPage.xaml
    /// </summary>
    public partial class UserRegisteredPage : Page
    {
        public UserRegisteredPage(RegistrationSuccessfulViewModel vm)
        {
            InitializeComponent();
            DataContext = vm;
        }
    }
}

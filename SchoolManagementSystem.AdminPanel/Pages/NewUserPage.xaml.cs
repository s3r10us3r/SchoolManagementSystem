using SchoolManagementSystem.AdminPanel.ViewModels;
using System.Windows.Controls;

namespace SchoolManagementSystem.AdminPanel.Pages
{
    /// <summary>
    /// Interaction logic for NewUserPage.xaml
    /// </summary>
    public partial class NewUserPage : Page
    {
        public NewUserPage(NewUserViewModel vieModel)
        {
            InitializeComponent();
            DataContext = vieModel;
        }
    }
}

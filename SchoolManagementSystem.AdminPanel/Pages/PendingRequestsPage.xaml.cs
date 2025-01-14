using SchoolManagementSystem.AdminPanel.ViewModels;
using System.Windows.Controls;

namespace SchoolManagementSystem.AdminPanel.Pages
{
    /// <summary>
    /// Interaction logic for PendingRequestsPage.xaml
    /// </summary>
    public partial class PendingRequestsPage : Page
    {
        public PendingRequestsPage(PendingRequestsViewModel vm)
        {
            DataContext = vm;
            InitializeComponent();
        }
    }
}

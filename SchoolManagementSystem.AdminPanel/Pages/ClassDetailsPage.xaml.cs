using SchoolManagementSystem.AdminPanel.ViewModels;
using System.Windows.Controls;

namespace SchoolManagementSystem.AdminPanel.Pages
{
    /// <summary>
    /// Interaction logic for ClassDetailsPage.xaml
    /// </summary>
    public partial class ClassDetailsPage : Page
    {
        public ClassDetailsPage(ClassDetailsViewModel vm)
        {
            DataContext = vm;
            InitializeComponent();
        }
    }
}

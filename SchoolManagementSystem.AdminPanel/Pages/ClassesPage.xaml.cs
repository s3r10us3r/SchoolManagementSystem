using SchoolManagementSystem.AdminPanel.ViewModels;
using System.Windows.Controls;

namespace SchoolManagementSystem.AdminPanel.Pages
{
    /// <summary>
    /// Interaction logic for ClassesPage.xaml
    /// </summary>
    public partial class ClassesPage : Page
    {
        public ClassesPage(ClassesViewModel vm)
        {
            InitializeComponent();
            DataContext = vm;
        }
    }
}

using SchoolManagementSystem.AdminPanel.ViewModels;
using System.Windows;

namespace SchoolManagementSystem.AdminPanel
{
    /// <summary>
    /// Interaction logic for AddLessonWindow.xaml
    /// </summary>
    public partial class AddLessonWindow : Window
    {
        public AddLessonWindow(AddLessonViewModel vm)
        {
            DataContext = vm;
            InitializeComponent();

            vm.RequestClose += (s, e) => Close();
        }
    }
}

using SchoolManagementSystem.AdminPanel.Services.Interfaces;
using SchoolManagementSystem.AdminPanel.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace SchoolManagementSystem.AdminPanel
{
    /// <summary>
    /// Interaction logic for LoggedInWindow.xaml
    /// </summary>
    public partial class LoggedInWindow : Window
    {
        public LoggedInWindow(LoggedInWindowViewModel loggedInWindowViewModel, INavigationService navigationService)
        {
            InitializeComponent();
            navigationService.SetFrame(MainFrame);
            DataContext = loggedInWindowViewModel;
        }
    }
}

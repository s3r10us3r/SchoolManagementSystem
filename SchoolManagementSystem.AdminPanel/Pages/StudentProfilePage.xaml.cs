using SchoolManagementSystem.AdminPanel.ViewModels;
using SchoolManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SchoolManagementSystem.AdminPanel.Pages
{
    /// <summary>
    /// Interaction logic for StudentProfilePage.xaml
    /// </summary>
    public partial class StudentProfilePage : Page
    {
        public StudentProfilePage(StudentProfileViewModel viewModel)
        {
            InitializeComponent();
        }
    }
}

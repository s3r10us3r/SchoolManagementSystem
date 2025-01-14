using SchoolManagementSystem.AdminPanel.ViewModels;
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
    /// Interaction logic for NewClass.xaml
    /// </summary>
    public partial class NewClassPage : Page
    {
        public NewClassPage(NewClassViewModel newClassViewModel)
        {
            DataContext = newClassViewModel;
            InitializeComponent();
        }
    }
}

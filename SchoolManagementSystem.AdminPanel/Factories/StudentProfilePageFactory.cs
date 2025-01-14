using SchoolManagementSystem.AdminPanel.Pages;
using SchoolManagementSystem.AdminPanel.ViewModels;
using SchoolManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagementSystem.AdminPanel.Factories
{
    public class StudentProfilePageFactory
    {
        public StudentProfilePageFactory()
        {
        }

        public StudentProfilePage CreatePage(Student student)
        {
            var vm = new StudentProfileViewModel(student);
            return new StudentProfilePage(vm);
        }
    }
}

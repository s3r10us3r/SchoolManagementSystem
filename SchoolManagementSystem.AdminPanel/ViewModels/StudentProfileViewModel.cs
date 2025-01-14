using CommunityToolkit.Mvvm.ComponentModel;
using SchoolManagementSystem.Models;
using System.Collections.ObjectModel;

namespace SchoolManagementSystem.AdminPanel.ViewModels
{
    public partial class StudentProfileViewModel : ObservableObject
    {
        [ObservableProperty]
        private string userId;

        [ObservableProperty]
        private string firstName;

        [ObservableProperty]
        private string lastName;

        [ObservableProperty]
        private DateOnly birthDate;

        [ObservableProperty]
        private Class studentClass;

        public StudentProfileViewModel(Student student)
        {
            UserId = student.UserId?.ToString() ?? "None";
            FirstName = student.FirstName;
            LastName = student.LastName;
            BirthDate = student.BirthDate;
            studentClass = student.Class;
        }
    }
}

using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SchoolManagementSystem.AdminPanel.Pages;
using SchoolManagementSystem.AdminPanel.Services.Interfaces;
using SchoolManagementSystem.Shared.Dtos;
using SchoolManagementSystem.Shared.WebServices.Interfaces;
using System.Collections;
using System.Collections.ObjectModel;
using System.Windows.Controls;

namespace SchoolManagementSystem.AdminPanel.ViewModels
{
    public partial class NewClassViewModel : ObservableObject
    {
        private readonly ITeachersWebService _teachersWebService;
        private readonly IStudentsWebService _studentsWebService;
        private readonly IClassesWebService _classesWebService;
        private readonly INavigationService _navigationService;

        public NewClassViewModel(ITeachersWebService teachersWebService, INavigationService navigationService, IStudentsWebService studentsWebService, IClassesWebService classesWebService)
        {
            _teachersWebService = teachersWebService;
            _navigationService = navigationService;
            _studentsWebService = studentsWebService;
            _classesWebService = classesWebService;

            LoadTeachers();
            LoadStudents();
        }

        [ObservableProperty]
        private int grade = 1;
        [ObservableProperty]
        private string name = "";
        [ObservableProperty]
        private TeacherDto selectedTeacher = new TeacherDto();
        [ObservableProperty]
        private string chosenTeacherName = "";
        [ObservableProperty]
        private ObservableCollection<TeacherDto> filteredTeachers = [];
        [ObservableProperty]
        private string teacherSearchText = "";
        [ObservableProperty]
        private string studentSearchText = "";
        [ObservableProperty]
        private ObservableCollection<StudentDto> filteredStudents = [];
        [ObservableProperty]
        private ObservableCollection<StudentDto> chosenStudents = [];

        private List<TeacherDto> teachers = [];
        private List<StudentDto> students = [];


        partial void OnTeacherSearchTextChanged(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                FilteredTeachers = new ObservableCollection<TeacherDto>(teachers);
            }
            else
            {
                FilteredTeachers = new ObservableCollection<TeacherDto>(teachers.Where(t => t.FirstName.Contains(value) || t.LastName.Contains(value) || SelectedTeacher == t));
            }
        }

        partial void OnStudentSearchTextChanged(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                FilteredStudents = new ObservableCollection<StudentDto>(students);
            }
            else
            {
                FilteredStudents = new ObservableCollection<StudentDto>(students.Where(s => s.FirstName.Contains(value) || s.LastName.Contains(value) || ChosenStudents.Contains(s)));
            }
        }

        private async Task LoadTeachers()
        {
            var result = await _teachersWebService.GetAll();
            if (result.IsSuccess)
            {
                teachers = result.Data!;
                FilteredTeachers = new ObservableCollection<TeacherDto>(teachers);
            }
        }

        private async Task LoadStudents()
        {
            var result = await _studentsWebService.GetAll();
            if (result.IsSuccess)
            {
                students = result.Data!.Where(s => string.IsNullOrWhiteSpace(s.ClassName)).ToList();
                FilteredStudents = new ObservableCollection<StudentDto>(students);
            }
        }

        [RelayCommand]
        private void SelectTeacher(TeacherDto teacher)
        {
            SelectedTeacher = teacher;
            ChosenTeacherName = $"{teacher.FirstName} {teacher.LastName}";
        }

        [RelayCommand]
        private void StudentsChanged(IList selectedStudents)
        {
            var newList = new List<StudentDto>();
            foreach (var student in selectedStudents)
            {
                if (student is StudentDto studentDto)
                    newList.Add(studentDto);
            }
            ChosenStudents = new(newList);
        }

        [RelayCommand]
        private void Cancel()
        {
            _navigationService.NavigateTo<ClassesPage>();
        }

        [RelayCommand]
        private async Task Submit()
        {
            if (SelectedTeacher == null)
            {
                await App.Current.Dispatcher
                    .InvokeAsync(
                    () => System.Windows.MessageBox.Show("Please select a teacher", "Error", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Error));
                return;
            }
            if (string.IsNullOrWhiteSpace(Name))
            {
                await App.Current.Dispatcher
                    .InvokeAsync(
                    () => System.Windows.MessageBox.Show("Please enter a name", "Error", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Error));
                return;
            }
            var newClass = new ClassDto
            {
                Grade = Grade,
                Name = Name,
                Teacher = SelectedTeacher,
                Students = ChosenStudents.ToList()
            };
            await _classesWebService.Create(newClass);
            _navigationService.NavigateTo<ClassesPage>();
        }
    }
}

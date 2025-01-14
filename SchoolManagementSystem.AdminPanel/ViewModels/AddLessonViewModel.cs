using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SchoolManagementSystem.AdminPanel.Displayables;
using SchoolManagementSystem.Shared.Dtos;
using SchoolManagementSystem.Shared.WebServices.Interfaces;
using System.Collections.ObjectModel;
using System.Windows;

namespace SchoolManagementSystem.AdminPanel.ViewModels
{
    public partial class AddLessonViewModel : ObservableObject
    {
        public event EventHandler? RequestClose;

        private readonly ClassDto _class;
        private readonly ITeachersWebService _teachersWebService;
        private readonly ILessonsWebService _lessonsWebService;

        public AddLessonViewModel(ClassDto classDto, ITeachersWebService teachersWebService, ILessonsWebService lessonsWebService)
        {
            _class = classDto;
            SelectedClassName = _class.Grade.ToString() + _class.Name;
            _teachersWebService = teachersWebService;
            _lessonsWebService = lessonsWebService;
            LoadTeachers();
        }

        [ObservableProperty]
        private string selectedClassName = "";
        [ObservableProperty]
        private NewLessonDto newLesson = new();
        [ObservableProperty]
        private ObservableCollection<TeacherDto> allTeachers = [];
        [ObservableProperty]
        private TeacherDto? selectedTeacher;
        [ObservableProperty]
        private string selectedTeacherName = "";
        [ObservableProperty]
        private ObservableCollection<OccurenceCreation> occurrences = [];

        [RelayCommand]
        private void AddOccurrence()
        {
            Occurrences.Add(new OccurenceCreation());
        }

        [RelayCommand]
        private void RemoveOccurrence(OccurenceCreation occurrence)
        {
            Occurrences.Remove(occurrence);
        }

        [RelayCommand]
        private void Cancel()
        {
            RequestClose?.Invoke(this, EventArgs.Empty);
        }

        [RelayCommand]
        private async Task Submit()
        {
            var lesson = new NewLessonDto()
            {
                Name = NewLesson.Name,
                ClassId = _class.Id,
                TeacherId = SelectedTeacher!.Id,
                Occurrences = Occurrences.Select(o => new NewOccuranceDto()
                {
                    DayOfWeek = o.DayOfWeek,
                    StartTime = new TimeOnly(o.StartTimeHours, o.StartTimeMinutes),
                    Duration = new TimeSpan(o.DurationHours, o.DurationMinutes, 0)
                }).ToList()
            };
            var result = await _lessonsWebService.Create(lesson);
            if (result.IsSuccess)
            {
                RequestClose?.Invoke(this, EventArgs.Empty);
            }
            else
            {
                MessageBox.Show("Failed to create lesson", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            };
        }

        partial void OnSelectedTeacherChanged(TeacherDto? value)
        {
            if (value != null)
                selectedTeacherName = $"{value.FirstName} {value.LastName}";
        }

        private async Task LoadTeachers()
        {
            var result = await _teachersWebService.GetAll();
            if (result.IsFailure)
            {
                MessageBox.Show("Failed to load teachers", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                AllTeachers = new ObservableCollection<TeacherDto>(result.Data!);
            }
        }
    }
}

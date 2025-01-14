using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SchoolManagementSystem.AdminPanel.Factories;
using SchoolManagementSystem.AdminPanel.Pages;
using SchoolManagementSystem.AdminPanel.Services.Interfaces;
using SchoolManagementSystem.Shared.Dtos;
using SchoolManagementSystem.Shared.WebServices.Interfaces;
using System.Windows;

namespace SchoolManagementSystem.AdminPanel.ViewModels
{
    public partial class ClassDetailsViewModel : ObservableObject
    {
        private readonly INavigationService _navigationService;
        private readonly IClassesWebService _classesWebService;
        private readonly ILessonsWebService _lessonsWebService;
        private readonly AddLessonWindowFactory _addLessonWindowFactory;

        public ClassDetailsViewModel(ClassDto classDto, INavigationService navigationService, AddLessonWindowFactory addLessonWindowFactory, IClassesWebService webService
            ,ILessonsWebService lessonsWebService)
        {
            ClassDto = classDto;
            _navigationService = navigationService;
            _addLessonWindowFactory = addLessonWindowFactory;
            _classesWebService = webService;
            _lessonsWebService = lessonsWebService;
            LoadLessons();
        }

        [ObservableProperty]
        private ClassDto classDto;

        [ObservableProperty]
        public List<LessonWithDataDto> displayableLessons = [];

        [RelayCommand]
        public void AddLesson()
        {
            var window = _addLessonWindowFactory.Create(ClassDto);
            _navigationService.OpenWindow(window);
        }

        [RelayCommand]
        public async Task RemoveLesson(LessonWithDataDto lesson)
        {
            var result = await _lessonsWebService.Delete(lesson.Id);
            if (result.IsSuccess)
            {
                await LoadLessons();
            }
            else
            {
                MessageBox.Show("Failed to delete lesson", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        [RelayCommand]
        public void GoBack()
        {
            _navigationService.NavigateTo<ClassesPage>();
        }

        [RelayCommand]
        public void DeleteClass()
        {
            var result = MessageBox.Show("Are you sure you want to delete this class?", "Delete class", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                _classesWebService.Delete(ClassDto.Id);
                _navigationService.NavigateTo<ClassesPage>();
            }
        }

        public async Task LoadLessons()
        {
            var result = await _classesWebService.LoadLessonsWithData(ClassDto.Id);
            if (result.IsSuccess)
            {
                DisplayableLessons = result.Data!;
            }
            else
            {
                MessageBox.Show("Failed to load lessons", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}

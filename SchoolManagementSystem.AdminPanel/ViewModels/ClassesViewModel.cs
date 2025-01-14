using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SchoolManagementSystem.AdminPanel.Displayables;
using SchoolManagementSystem.AdminPanel.Factories;
using SchoolManagementSystem.AdminPanel.Pages;
using SchoolManagementSystem.AdminPanel.Services.Interfaces;
using SchoolManagementSystem.Shared.Dtos;
using SchoolManagementSystem.Shared.WebServices.Interfaces;
using System.Collections.ObjectModel;
using System.Windows;

namespace SchoolManagementSystem.AdminPanel.ViewModels
{
    public partial class ClassesViewModel : ObservableObject
    {
        private readonly IClassesWebService _classesWebService;
        private readonly INavigationService _navigationService;
        private readonly ClassDetailsPageFactory _classDetailsPageFactory;

        public ClassesViewModel(IClassesWebService classesWebService, INavigationService navigationService, ClassDetailsPageFactory classDetailsPageFactory)
        {
            _classesWebService = classesWebService;
            _navigationService = navigationService;
            _classDetailsPageFactory = classDetailsPageFactory;
            LoadClasses();
        }

        [ObservableProperty]
        private ObservableCollection<DisplayableClass> classes = [];

        private List<ClassDto> classList = [];

        [RelayCommand]
        public async Task ReloadClasses()
        {
            await LoadClasses();
        }

        [RelayCommand]
        public void AddNewClass()
        {
            _navigationService.NavigateTo<NewClassPage>();
        }

        private async Task LoadClasses()
        {
            var result = await _classesWebService.GetAll();
            if (result.IsFailure)
            {
                MessageBox.Show("Failed to load classes", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                classList = result.Data!;
                var displayable = classList.Select(c => new DisplayableClass(c)).ToList();
                Classes = new ObservableCollection<DisplayableClass>(displayable);
            }
        }

        [RelayCommand]
        private void ShowClassDetails(DisplayableClass displayableClass)
        {
            var classDto = classList.First(c => c.Id == displayableClass.Id);
            var page = _classDetailsPageFactory.CreatePage(classDto);
            _navigationService.NavigateTo(page);
        }
    }
}

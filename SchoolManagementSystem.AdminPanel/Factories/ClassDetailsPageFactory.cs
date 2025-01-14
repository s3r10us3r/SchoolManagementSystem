using SchoolManagementSystem.AdminPanel.Pages;
using SchoolManagementSystem.AdminPanel.Services.Interfaces;
using SchoolManagementSystem.AdminPanel.ViewModels;
using SchoolManagementSystem.Shared.Dtos;
using SchoolManagementSystem.Shared.WebServices.Interfaces;

namespace SchoolManagementSystem.AdminPanel.Factories
{
    public class ClassDetailsPageFactory
    {
        private readonly INavigationService _navigationService;
        private readonly AddLessonWindowFactory _addLessonWindowFactory;
        private readonly IClassesWebService _classesWebService;
        private readonly ILessonsWebService _lessonsWebService;

        public ClassDetailsPageFactory(INavigationService navigationService, AddLessonWindowFactory addLessonWindowFactory, IClassesWebService classesWebService,
            ILessonsWebService lessonsWebService)
        {
            _navigationService = navigationService;
            _addLessonWindowFactory = addLessonWindowFactory;
            _classesWebService = classesWebService;
            _lessonsWebService = lessonsWebService;
        }

        public ClassDetailsPage CreatePage(ClassDto classDto)
        {
            var vm = new ClassDetailsViewModel(classDto, _navigationService, _addLessonWindowFactory, _classesWebService, _lessonsWebService);
            return new ClassDetailsPage(vm);
        }
    }
}


using SchoolManagementSystem.AdminPanel.ViewModels;
using SchoolManagementSystem.Shared.Dtos;
using SchoolManagementSystem.Shared.WebServices.Interfaces;

namespace SchoolManagementSystem.AdminPanel.Factories
{
    public class AddLessonWindowFactory
    {
        private readonly ITeachersWebService _teachersWebService;
        private readonly ILessonsWebService _lessonsWebService;

        public AddLessonWindowFactory(ITeachersWebService teachersWebService, ILessonsWebService lessonsWebService)
        {
            _teachersWebService = teachersWebService;
            _lessonsWebService = lessonsWebService;
        }

        public AddLessonWindow Create(ClassDto classDto)
        {
            var vm = new AddLessonViewModel(classDto, _teachersWebService, _lessonsWebService);
            var window = new AddLessonWindow(vm);
            return window;
        }
    }
}

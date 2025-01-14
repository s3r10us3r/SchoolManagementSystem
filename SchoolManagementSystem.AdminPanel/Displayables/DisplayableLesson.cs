using SchoolManagementSystem.Shared.Dtos;

namespace SchoolManagementSystem.AdminPanel.Displayables
{
    public class DisplayableLesson
    {
        public string Name { get; set; } = "";
        public string TeachersName { get; set; } = "";
        public List<string> TimeStrings { get; set; } = [];

        public DisplayableLesson(LessonDto dto)
        {
        }
    }
}

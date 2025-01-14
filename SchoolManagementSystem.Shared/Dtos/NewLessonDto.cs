namespace SchoolManagementSystem.Shared.Dtos
{
    public class NewLessonDto
    {
        public string Name { get; set; } = "";
        public int ClassId { get; set; }
        public int TeacherId { get; set; }
        public List<NewOccuranceDto> Occurrences { get; set; } = [];
    }
}

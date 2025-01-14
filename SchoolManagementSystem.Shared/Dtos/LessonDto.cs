namespace SchoolManagementSystem.Shared.Dtos
{
    public class LessonDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = "";
        public int ClassId { get; set; }
        public int TeacherId { get; set; }
        public List<OccuranceDto> Occurances { get; set; } = [];
    }
}

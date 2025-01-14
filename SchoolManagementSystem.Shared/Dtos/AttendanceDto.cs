namespace SchoolManagementSystem.Shared.Dtos
{
    public class AttendanceDto
    {
        public int StudentId { get; set; }
        public int OccuranceId { get; set; }
        public bool IsPresent { get; set; }
    }
}

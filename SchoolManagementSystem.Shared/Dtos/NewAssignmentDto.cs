﻿namespace SchoolManagementSystem.Shared.Dtos
{
    public class NewAssignmentDto
    {
        public string Name { get; set; } = "";
        public string Description { get; set; } = "";
        public DateTime DueDate { get; set; }
        public int LessonId { get; set; }
    }
}

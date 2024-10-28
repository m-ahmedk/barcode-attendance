namespace SchoolAttendance.Data.Dtos
{
    public class CourseTimetableDto
    {
        public int TimetableId { get; set; }
        public string? ClassName { get; set; }
        public string? StartTime { get; set; }
        public string? EndTime { get; set; }
        public string? Day { get; set; }
        public bool IsClassActive { get; set; } = false;
    }
}
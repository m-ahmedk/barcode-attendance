using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolAttendance.Data.Entities
{
    public class Attendance
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int AttendanceId { get; set; }

        [Required]
        public int StudentId { get; set; }

        [Required]
        public int TimetableId { get; set; }

        [Required]
        public DateTime CheckInTime { get; set; }

        public bool? isPushed { get; set; } = false;

        public Student? Student { get; set; }

        // Navigation property
        public Timetable? Timetable { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolAttendance.Data.Entities
{
    public class Timetable
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int TimetableId { get; set; }

        [Required]
        public int CourseId { get; set; }

        [Required]
        public int CampusroomId { get; set; }

        public string? Day { get; set; }

        public TimeSpan StartTime { get; set; }

        public TimeSpan EndTime { get; set; }


        // Navigation property

        public Course? Course { get; set; }

        public Campusroom? Campusroom { get; set; }

        public ICollection<Attendance>? Attendances { get; set; }

    }
}

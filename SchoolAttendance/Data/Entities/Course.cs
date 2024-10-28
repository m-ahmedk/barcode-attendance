using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolAttendance.Data.Entities
{
    public class Course
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CourseId { get; set; }

        [Required]
        public int ClassId { get; set; }

        [Required]
        public string Name { get; set; } = string.Empty;

        public string? Code { get; set; }

        public string? Description { get; set; }

        // Course is associated to one Class
        public Class? Class { get; set; }

        // A course has many class schedules i.e. Timetable
        public ICollection<Timetable>? Timetables { get; set; }

        public ICollection<StudentCourse>? StudentCourses { get; set; }

    }
}

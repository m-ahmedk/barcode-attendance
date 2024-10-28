using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolAttendance.Data.Entities
{
    public class StudentCourse
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int StudentCourseId { get; set; }
        
        public int StudentId { get; set; }

        public int CourseId { get; set; }

        // Navigation property
        public Student? Student { get; set; }

        public Course? Course { get; set; }
    }
}

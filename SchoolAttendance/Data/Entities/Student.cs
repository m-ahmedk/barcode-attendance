using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolAttendance.Data.Entities
{
    public class Student
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int StudentId { get; set; }

        [Required]
        public string Barcode { get; set; } = string.Empty;

        [Required]
        public string RegistrationNo { get; set; } = string.Empty;

        [Required]
        public string Name { get; set; } = string.Empty;

        [Required]
        public int ClassId { get; set; }

        [Required]
        public string FatherName { get; set; } = string.Empty;

        [Required]
        public string Address { get; set; } = string.Empty;

        [Required]
        public DateTime DOB { get; set; }

        [Required]
        public string Gender { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public string? ImageURL { get; set; }

        // Student can have one or more attendance records
        public ICollection<Attendance>? Attendances { get; set; }

        public ICollection<StudentCourse>? StudentCourses { get; set; }
        public Class? Class { get; set; }
    }
}

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolAttendance.Data.Entities
{
    public class Campusroom
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CampusRoomId { get; set; }

        [Required]
        public string Name { get; set; } = string.Empty;

        [Required]
        public string MacAddress { get; set; } = string.Empty;

        public string? Description { get; set; }

        // Naviagtion property
        public ICollection<Timetable>? Timetables { get; set; }
    }
}

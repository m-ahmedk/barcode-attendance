using Microsoft.EntityFrameworkCore;
using System.IO;

namespace SchoolAttendance.Models
{
    public class AppDbContext : DbContext
    {
        public DbSet<Attendance> Attendance { get; set; }
        public DbSet<Class> Class { get; set; }
        public DbSet<Campusroom> Campusroom { get; set; }
        public DbSet<Course> Course { get; set; }
        public DbSet<Student> Student { get; set; }
        public DbSet<StudentCourse> StudentCourse { get; set; }
        public DbSet<SystemConfig> SystemConfig { get; set; }
        public DbSet<Timetable> Timetable { get; set; }

        // Constructor that accepts DbContextOptions and passes it to the base DbContext class
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Attendance>()
                .HasOne(x => x.Student)
                .WithMany(x => x.Attendances)
                .HasForeignKey(x => x.StudentId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Attendance>()
                .HasOne(x => x.Timetable)
                .WithMany(x => x.Attendances)
                .HasForeignKey(x => x.TimetableId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Course>()
                .HasOne(x => x.Class)
                .WithMany(x => x.Courses)
                .HasForeignKey(x => x.ClassId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Student>()
                .HasOne(x => x.Class)
                .WithMany(x => x.Students)
                .HasForeignKey(x => x.ClassId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<StudentCourse>()
                .HasOne(x => x.Student)
                .WithMany(x => x.StudentCourses)
                .HasForeignKey(x => x.StudentId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<StudentCourse>()
                .HasOne(x => x.Course)
                .WithMany(x => x.StudentCourses)
                .HasForeignKey(x => x.CourseId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<StudentCourse>()
              .HasIndex(x => new { x.StudentId, x.CourseId })
              .IsUnique();

            modelBuilder.Entity<Timetable>()
                .HasOne(x => x.Course)
                .WithMany(x => x.Timetables)
                .HasForeignKey(x => x.CourseId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Timetable>()
                .HasOne(x => x.Campusroom)
                .WithMany(x => x.Timetables)
                .HasForeignKey(x => x.CampusroomId)
                .OnDelete(DeleteBehavior.Cascade);

            base.OnModelCreating(modelBuilder);
        }

    }
}

namespace SchoolAttendance.Infrastructure.Interface
{
    public interface IUnitOfWork
    {
        IAttendanceRepository Attendances { get; }
        ICourseRepository Courses { get; }
        IStudentRepository Students { get; }
        IStudentCourseRepository StudentCourses { get; }
        ISystemConfig SystemConfigs { get; }
        ITimetableRepository Timetables { get; }
    }
}

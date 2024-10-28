namespace SchoolAttendance.Infrastructure.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        // Using short live db context means not sharing a db context between repositories
        // Unit of work maintains single db context
        // Hence the Dispose and SaveChangesAsync code is not required

        private readonly IDbContextFactory _dbContextFactory;

        public IAttendanceRepository Attendances { get; private set; }
        public ICourseRepository Courses { get; private set; }
        public IStudentRepository Students { get; private set; }
        public IStudentCourseRepository StudentCourses { get; private set; }
        public ISystemConfig SystemConfigs { get; private set; }
        public ITimetableRepository Timetables { get; private set; }

        public UnitOfWork(IDbContextFactory dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;

            Attendances = new AttendanceRepository(_dbContextFactory);
            Courses = new CourseRepository(_dbContextFactory);
            Students = new StudentRepository(_dbContextFactory);
            StudentCourses = new StudentCourseRepository(_dbContextFactory);
            SystemConfigs = new SystemConfigRepository(_dbContextFactory);
            Timetables = new TimetableRepository(_dbContextFactory);
        }
    }
}

namespace SchoolAttendance.Infrastructure.Repository
{
    public class StudentCourseRepository : GenericRepository<StudentCourse>, IStudentCourseRepository
    {
        public StudentCourseRepository(IDbContextFactory dbContextFactory) : base(dbContextFactory)
        {
        }
    }
}

namespace SchoolAttendance.Infrastructure.Repository
{
    public class CourseRepository : GenericRepository<Course>, ICourseRepository
    {
        public CourseRepository(IDbContextFactory dbContextFactory) : base(dbContextFactory)
        {
        }
    }
}

namespace SchoolAttendance.Infrastructure.Repository
{
    public class AttendanceRepository : GenericRepository<Attendance>, IAttendanceRepository
    {
        public AttendanceRepository(IDbContextFactory dbContextFactory) : base(dbContextFactory)
        {
        }
    }
}

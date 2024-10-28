namespace SchoolAttendance.Infrastructure.Repository
{
    public class SystemConfigRepository : GenericRepository<SystemConfig>, ISystemConfig
    {
        public SystemConfigRepository(IDbContextFactory dbContextFactory) : base(dbContextFactory)
        {
        }
    }
}

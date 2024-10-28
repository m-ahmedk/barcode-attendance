namespace SchoolAttendance.Infrastructure.Repository
{
    public class CampusRoomRepository : GenericRepository<Campusroom>, ICampusRoomRepository
    {
        public CampusRoomRepository(IDbContextFactory dbContextFactory) : base(dbContextFactory)
        {
        }
    }
}

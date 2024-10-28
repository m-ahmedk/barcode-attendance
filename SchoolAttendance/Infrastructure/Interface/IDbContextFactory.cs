namespace SchoolAttendance.Infrastructure.Interface
{
    public interface IDbContextFactory
    {
        AppDbContext CreateDbContext();
    }
}

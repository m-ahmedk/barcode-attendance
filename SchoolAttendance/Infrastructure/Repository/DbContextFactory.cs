using Microsoft.EntityFrameworkCore;

namespace SchoolAttendance.Infrastructure.Repository
{
    public class DbContextFactory : IDbContextFactory
    {
        private readonly DbContextOptions<AppDbContext> _options;

        // Receive the DbContextOptions from DI
        public DbContextFactory(DbContextOptions<AppDbContext> options)
        {
            _options = options;
        }

        // Create new instance of AppDbContext
        public AppDbContext CreateDbContext()
        {
            return new AppDbContext(_options);
        }
    }
}

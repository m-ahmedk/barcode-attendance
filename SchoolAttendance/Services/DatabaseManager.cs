using Microsoft.EntityFrameworkCore;
using System.IO;

namespace SchoolAttendance.Services
{
    public class DatabaseManager
    {
        private readonly IDbContextFactory _dbContextFactory;

        public DatabaseManager(IDbContextFactory dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
        }

        public void InitializeDatabase()
        {
            using (var dbContext = _dbContextFactory.CreateDbContext())
            {
                // Ensure the directory for the database exists
                var directory = Path.GetDirectoryName(dbContext.Database.GetDbConnection().DataSource);
                if (!Directory.Exists(directory))
                {
                    Directory.CreateDirectory(directory);
                }

                // Ensure the database is created
                dbContext.Database.EnsureCreated();
            }
        }

    }
}

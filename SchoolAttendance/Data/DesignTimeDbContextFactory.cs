using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.IO;

namespace SchoolAttendance.Data
{
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
    {
        // Runs on design-time/compile time for ef core's migration and scaffoliding purposes
        public AppDbContext CreateDbContext(string[] args)
        {
            // Use the output (bin) directory similar to runtime behavior
            var basePath = AppContext.BaseDirectory;
            var dbFilePath = Path.Combine(basePath, "Database", "attendance.db");

            // Ensure the Database folder exists
            var directory = Path.GetDirectoryName(dbFilePath);
            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }

            // Configure the DbContextOptionsBuilder
            var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
            optionsBuilder.UseSqlite($"Data Source={dbFilePath}");

            // Return a new instance of AppDbContext with the configured options
            return new AppDbContext(optionsBuilder.Options);
        }
    }
}

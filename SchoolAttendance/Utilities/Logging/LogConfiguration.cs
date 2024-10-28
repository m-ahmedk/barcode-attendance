using Serilog;
using Serilog.Events;
using System;
using System.IO;

public static class LogConfiguration
{
    public static void ConfigureLogging()
    {
        // Define the log directory and ensure it exists
        var logDirectory = Path.Combine(Directory.GetCurrentDirectory(), "Logging");
        Directory.CreateDirectory(logDirectory);

        // Set file path
        var logPath = Path.Combine(logDirectory, "log.txt");

        // Configure Serilog
        Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Information()
            .MinimumLevel.Override("Microsoft", LogEventLevel.Warning) // Remove logs except for warnings
            .MinimumLevel.Override("Microsoft.EntityFrameworkCore.Database.Command", LogEventLevel.Warning) // No EF Core command logs
            .WriteTo.File(logPath, rollingInterval: RollingInterval.Day)
            .CreateLogger();

        // Delete older log files
        DeleteOldLogFiles(logDirectory);
    }

    private static void DeleteOldLogFiles(string logDirectory, int days = 3)
    {
        var files = Directory.EnumerateFiles(logDirectory);

        foreach (var file in files)
        {
            var fileInfo = new FileInfo(file);
            if (fileInfo.LastWriteTime < DateTime.Now.AddDays(-days))
            {
                try
                {
                    fileInfo.Delete();
                    Log.Information("Deleted old log file: {FileName}", fileInfo.Name);
                }
                catch (Exception ex)
                {
                    Log.Error(ex, "Error deleting log file: {FileName}", fileInfo.Name);
                }
            }
        }
    }
}
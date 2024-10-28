using Avalonia;
using SchoolAttendance.Services;
using SchoolAttendance.Utilities.Extension;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using System;
using SchoolAttendanceSystem.Mappings;

namespace SchoolAttendance
{
    internal class Program
    {
        // Initialization code. Don't use any Avalonia, third-party APIs or any
        // SynchronizationContext-reliant code before AppMain is called: things aren't initialized
        // yet and stuff might break.
        [STAThread]
        public static void Main(string[] args)
        {
            // Build the service collection
            var services = new ServiceCollection();

            var path = AppContext.BaseDirectory;

            var configuration = new ConfigurationBuilder()
                .SetBasePath(
                    System.IO.Path.Combine(
                        path
                    )
                )
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();

            LogConfiguration.ConfigureLogging();

            // Register services
            ConfigureServices(services, configuration);

            // Build the service provider
            var serviceProvider = services.BuildServiceProvider();

            // Get DatabaseManager & BackroundSyncService from DI and inject into App
            var dbManager = serviceProvider.GetRequiredService<DatabaseManager>();

            BuildAvaloniaApp(dbManager, serviceProvider)
                .StartWithClassicDesktopLifetime(args);
        }

        private static void ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<IConfiguration>(configuration);

            services.AddDbContext<AppDbContext>(options =>
            {
                var dbFilePath = System.IO.Path.Combine(
                    System.IO.Directory.GetCurrentDirectory(),
                    "Database",
                    "attendance.db");
                options.UseSqlite($"Data Source={dbFilePath}");
            });

            services.AddAutoMapper(typeof(MappingProfile).Assembly);

            services.AddDIRepositories();

            services.AddDIServices();

            services.AddLogging(builder =>
            {
                builder.AddSerilog();  // Use Serilog for logging
            });    
        }

        // Avalonia configuration, don't remove; also used by visual designer.
        public static AppBuilder BuildAvaloniaApp(DatabaseManager dbManager, IServiceProvider serviceProvider)
        => AppBuilder.Configure(() => new App(dbManager, serviceProvider))
            .UsePlatformDetect()
            .WithInterFont()
            .LogToTrace();
    }
}

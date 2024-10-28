using SchoolAttendance.Infrastructure.Repository;
using SchoolAttendance.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace SchoolAttendance.Utilities.Extension
{
    public static class ServicesExtension
    {
        public static IServiceCollection AddDIRepositories(this IServiceCollection services)
        {
            services.AddSingleton<IDbContextFactory>(sp =>
            {
                var options = sp.GetRequiredService<DbContextOptions<AppDbContext>>();
                return new DbContextFactory(options);
            });

            services.AddScoped<IAttendanceRepository, AttendanceRepository>();
            services.AddScoped<ICourseRepository, CourseRepository>();
            services.AddScoped<IStudentRepository, StudentRepository>();
            services.AddScoped<ISystemConfig, SystemConfigRepository>();
            services.AddScoped<ITimetableRepository, TimetableRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            return services;
        }

        public static IServiceCollection AddDIServices(this IServiceCollection services)
        {
            services.AddScoped<AttendanceService>();
            services.AddScoped<CourseService>();
            services.AddScoped<StudentService>();
            services.AddScoped<SystemConfigService>();
            services.AddScoped<TimetableService>();

            services.AddScoped<DatabaseManager>();

            return services;
        }
    }
}
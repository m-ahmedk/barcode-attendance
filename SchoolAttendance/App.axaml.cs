using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using SchoolAttendance.Services;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace SchoolAttendance
{
    public partial class App : Application
    {
        private readonly DatabaseManager _dbManager;
        private readonly IServiceProvider _serviceProvider;

        public App(DatabaseManager dbManager, IServiceProvider serviceProvider)
        {
            _dbManager = dbManager;
            _serviceProvider = serviceProvider;
        }

        public override void Initialize()
        {
            AvaloniaXamlLoader.Load(this);
        }

        public override async void OnFrameworkInitializationCompleted()
        {
            // db file and folder creation
            _dbManager.InitializeDatabase();

            // Get services to pass to UI / System Config Service to set MAC address
            var systemConfigService = _serviceProvider.GetRequiredService<SystemConfigService>();
            var attendanceService = _serviceProvider.GetRequiredService<AttendanceService>();
            var studentService = _serviceProvider.GetRequiredService<StudentService>();
            var timetableService = _serviceProvider.GetRequiredService<TimetableService>();

            await systemConfigService.MacAddressInit();

            if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
            {
                var mainWindow = new MainWindow(attendanceService, studentService, timetableService)
                {
                    //DataContext = _dbManager; // Pass DbManager as needed
                };

                desktop.MainWindow = mainWindow;

                // Set focus to BarcodeTextBox
                mainWindow.Opened += (sender, e) =>
                {
                    mainWindow.FindControl<TextBox>("BarcodeTextBox").Focus();
                };

            }

            base.OnFrameworkInitializationCompleted();
        }
    }
}
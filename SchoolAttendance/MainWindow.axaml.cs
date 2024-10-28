using Avalonia;
using Avalonia.Controls;
using Avalonia.Media;
using Avalonia.Media.Imaging;
using Avalonia.Threading;
using SchoolAttendance.Services;
using System;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace SchoolAttendance
{
    public partial class MainWindow : Window
    {
        private readonly HttpClient _httpClient = new HttpClient();
        private readonly DispatcherTimer _timer;
        private readonly AttendanceService _attendanceService;
        private readonly StudentService _studentService;
        private readonly TimetableService _timetableService;
        private int? timetableId;
        private CancellationTokenSource _cancellationTokenSource;
        private bool _barcodeNotRecognized = false;

        public MainWindow(AttendanceService attendanceService, StudentService studentService,
            TimetableService timetableService)
        {
            InitializeComponent();
            FocusBarcode();

            BarcodeTextBox.PropertyChanged += OnBarcodeTextChanged;
            
            // Display time on-screen with interval of every n seconds
            _timer = new DispatcherTimer
            {
                Interval = TimeSpan.FromSeconds(1)
            };

            _timer.Tick += UpdateClock;
            _timer.Start();

            _attendanceService = attendanceService;
            _studentService = studentService;
            _timetableService = timetableService;

            // Start background task to check for class updates
            _cancellationTokenSource = new CancellationTokenSource();
            Task.Run(() => MonitorClassDetailsAsync(_cancellationTokenSource.Token));
        }

        private async Task MonitorClassDetailsAsync(CancellationToken cancellationToken)
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                var timetable = await _timetableService.FetchCurrentSchedule(DateTime.Now);

                if (timetable.Data != null && timetable.Data.IsClassActive)
                {
                    timetableId = timetable.Data.TimetableId;

                    // Update the UI on the main thread
                    await Dispatcher.UIThread.InvokeAsync(() =>
                    {
                        CourseNameTextBox.Text = timetable.Data.ClassName;
                        CourseFromTextBox.Text = $"Start Time: {timetable.Data.StartTime}";
                        CourseToTextBox.Text = $"End Time: {timetable.Data.EndTime}";
                        ClassDetailsGrid.IsVisible = true;
                        BarcodeTextBox.IsEnabled = true;
                    });
                }
                else
                {
                    timetableId = null;

                    // Hide the class details on the main thread
                    await Dispatcher.UIThread.InvokeAsync(() =>
                    {
                        ClassDetailsGrid.IsVisible = false;
                        BarcodeTextBox.IsEnabled = false;
                    });
                }

                // Check every minute
                await Task.Delay(TimeSpan.FromMinutes(1), cancellationToken);
            }
        }

        private void UpdateClock(object? sender, EventArgs e)
        {
            ClockTextBlock.Text = DateTime.Now.ToString("F");
        }

        private async void OnBarcodeTextChanged(object? sender, AvaloniaPropertyChangedEventArgs e)
        {
            if (e.Property.Name == nameof(BarcodeTextBox.Text))
            {
                if (_barcodeNotRecognized)
                {
                    // Clear the BarcodeTextBox if the previous scan was not recognized
                    BarcodeTextBox.Text = string.Empty;
                    _barcodeNotRecognized = false; 
                }

                var barcode = BarcodeTextBox.Text;

                if (!string.IsNullOrWhiteSpace(barcode))
                {
                    await FetchDetailsAsync(barcode);
                }
            }
        }

        private async Task FetchDetailsAsync(string barcode)
        {
            try
            {
                // Simulate fetching details from a file or database
                var details = await GetDetailsFromApiAsync(barcode);

                if (details != null)
                {
                    NameTextBox.Text = details.Name;
                    ClassTextBox.Text = details.Class;
                    FatherNameTextBox.Text = details.Age.ToString();
                    AddressTextBox.Text = details.Email;
                    TimeInLabel.Text = $"Check in: {details.TimeIn}";

                    if (!string.IsNullOrEmpty(details.studentImageUrl))
                    {
                        await LoadImageAsync(details.studentImageUrl);
                    }
                    else
                    {
                        await LoadImageAsync("https://upload.wikimedia.org/wikipedia/en/b/b1/Portrait_placeholder.png");
                    }

                    AppendLog($"[CheckInTime-->{DateTime.Now:HH:mm:ss}] Successfully Checked In! Marked for Barcode: {barcode}", Colors.Green);
                    BarcodeTextBox.Text = string.Empty;
                }
                else
                {
                    ClearDetails();
                    _barcodeNotRecognized = true; // the next scan clears the text box
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                AppendLog($"[{DateTime.Now:HH:mm:ss}] Failed to Mark Attendance.. Barcode: {barcode}", Colors.Red);
            }
        }

        private void AppendLog(string message, Color color)
        {
            var textBlock = new TextBlock
            {
                Text = $"{message}",
                Foreground = new SolidColorBrush(color),
                TextWrapping = TextWrapping.Wrap
            };
            LogStackPanel.Children.Add(textBlock);
            LogScrollViewer.ScrollToEnd();
        }

        private async Task<StudentDetails?> GetDetailsFromApiAsync(string barcode)
        {
            try
            {
                var student = await _studentService.GetByBarcode(barcode);
                var timetable = await _timetableService.GetTimetableById(timetableId);

                if (student.Data != null)
                {
                    var attendance = await _attendanceService.MarkAttendance(student.Data, DateTime.Now, timetable.Data);

                    if (attendance.Data != null)
                    {
                        var details = new StudentDetails()
                        {
                            Name = student.Data.Name,
                            Class = student.Data.Class.Name,
                            Email = student.Data.Email,
                            studentImageUrl = student.Data.ImageURL ?? null,
                            Age = _studentService.CalculateAge(student.Data.DOB),
                            TimeIn = Convert.ToDateTime(attendance.Data.CheckInTime),
                        };

                        return details;
                    }
                    else
                    {
                        AppendLog($"{attendance.Errors.FirstOrDefault()}", Colors.Red);
                    }
                }
                else
                {
                    AppendLog($"[{DateTime.Now:HH:mm:ss}] Failed to Mark Attendance.. Barcode: {barcode}", Colors.Red);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return null;
        }

        private async Task LoadImageAsync(string url)
        {
            try
            {
                var response = await _httpClient.GetAsync(url);
                if (response.IsSuccessStatusCode)
                {
                    var stream = await response.Content.ReadAsStreamAsync();
                    var bitmap = new Bitmap(stream);
                    DisplayImage.Source = bitmap;
                }
                else
                {
                    DisplayImage.Source = null;
                }
            }
            catch
            {
                DisplayImage.Source = null;
            }
        }

        private void ClearDetails()
        {
            BarcodeTextBox.Text = string.Empty;
            NameTextBox.Text = string.Empty;
            ClassTextBox.Text = string.Empty;
            FatherNameTextBox.Text = string.Empty;
            AddressTextBox.Text = string.Empty;
            DisplayImage.Source = null;
            TimeInLabel.Text = string.Empty;
        }

        private void FocusBarcode()
        {
            // Maintain focus if it loses focus
            BarcodeTextBox.LostFocus += (sender, e) =>
            {
                BarcodeTextBox.Focus();
            };
        }

        protected override void OnClosed(EventArgs e)
        {
            // Cancel the monitoring task when the window is closed
            _cancellationTokenSource.Cancel();
            _cancellationTokenSource.Dispose();

            base.OnClosed(e);
        }
    }
}

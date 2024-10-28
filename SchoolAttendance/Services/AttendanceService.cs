using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SchoolAttendance.Services
{
    public class AttendanceService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<AttendanceService> _logger;

        public AttendanceService(IUnitOfWork unitOfWork, ILogger<AttendanceService> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public async Task<AppResponse<Attendance>> MarkAttendance(Student student, DateTime scannedTime, Timetable? timetable)
        {
            try
            {
                if (timetable == null) { throw new ArgumentNullException(nameof(timetable), "Timetable cannot be null."); }

                var attendance = await _unitOfWork.Attendances.GetByIdAsync(x =>
                    x.StudentId == student.StudentId &&
                    x.TimetableId == timetable.TimetableId &&
                    x.CheckInTime.Date == scannedTime.Date);

                if (attendance == null)
                {
                    attendance = new Attendance()
                    {
                        StudentId = student.StudentId,
                        TimetableId = timetable.TimetableId,
                        isPushed = true,
                        CheckInTime = scannedTime,
                    };

                    await _unitOfWork.Attendances.CreateAsync(attendance);

                    return new AppResponse<Attendance>(attendance);
                }
                else
                {
                    string error = $"[{DateTime.Now:HH:mm:ss}] {student.Name} ({student.RegistrationNo}) is already marked present for {timetable.Course.Name}.";
                    _logger.LogError(error);
                    return new AppResponse<Attendance>(new List<string> { error }, false);
                }
            }
            catch (Exception ex)
            {
                string error = $"An error occurred while marking Attendance: {ExceptionHelper.GetExceptionDetails(ex)}";
                _logger.LogError(error);
                return new AppResponse<Attendance> 
                    (new List<string> { $"[{DateTime.Now:HH:mm:ss}] Failed to Mark Attendance for Barcode: {student.Barcode}" }, false);
            }
        }
    }
}
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SchoolAttendance.Services
{
    public class StudentService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<StudentService> _logger;

        public StudentService(IUnitOfWork unitOfWork, ILogger<StudentService> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public async Task<AppResponse<Student>> GetByBarcode(string barcode)
        {
            try {
                var student = await _unitOfWork.Students
                    .GetByIdAsync(x => x.Barcode == barcode, x=>x.Class);
                return new AppResponse<Student>(student);
            }
            catch(Exception ex)
            {
                string error = $"An error occurred while fetching student barcode: {ExceptionHelper.GetExceptionDetails(ex)}";
                _logger.LogError(error);
                return new AppResponse<Student>(new List<string> { error }, false);
            }
        }

        public int CalculateAge(DateTime dob)
        {
            DateTime today = DateTime.Today;
            int age = today.Year - dob.Year;

            if (dob.Date > today.AddYears(-age))
            {
                age--;
            }

            return age;
        }
    }
}
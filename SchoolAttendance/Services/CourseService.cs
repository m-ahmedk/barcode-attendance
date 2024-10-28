using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SchoolAttendance.Services
{
    public class CourseService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<CourseService> _logger;

        public CourseService(IUnitOfWork unitOfWork, ILogger<CourseService> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public async Task<AppResponse<Course?>> GetCourse(int id)
        {
            try
            {
                var course = await _unitOfWork.Courses.GetByIdAsync(id);
                return new AppResponse<Course?>(course, "Course retrieved from database");
            }
            catch(Exception ex)
            {
                string error = $"An error occurred while fetching Course: {ExceptionHelper.GetExceptionDetails(ex)}";
                _logger.LogError(error);
                return new AppResponse<Course?>(new List<string> { error }, false);
            }
        }
    }
}

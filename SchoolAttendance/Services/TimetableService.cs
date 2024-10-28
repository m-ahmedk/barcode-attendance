using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using SchoolAttendance.Data.Dtos;

namespace SchoolAttendance.Services
{
    public class TimetableService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<TimetableService> _logger;
        private readonly IMapper _mapper;

        public TimetableService(IUnitOfWork unitOfWork, ILogger<TimetableService> logger, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<AppResponse<Timetable>> GetTimetableById(int? id)
        {
            try
            {
                if (id == null) { throw  new ArgumentNullException(nameof(id)); }

                var timetable = await _unitOfWork.Timetables.GetByIdAsync(x=> x.TimetableId == id, x=>x.Course);

                return new AppResponse<Timetable>(timetable);
            }
            catch (Exception ex)
            {
                string error = $"An error occurred in Timetable while getting by id: {ExceptionHelper.GetExceptionDetails(ex)}";
                _logger.LogError(error);
                return new AppResponse<Timetable>(new List<string> { error }, false);
            }
        }

        public async Task<AppResponse<CourseTimetableDto>> FetchCurrentSchedule(DateTime dateTime)
        {
            try
            {

                var timetable = await _unitOfWork.Timetables.GetCurrentClass(dateTime);

                if (timetable == null)
                {
                    return new AppResponse<CourseTimetableDto>(new List<string> { "No timetable found"}, false);
                }

                TimeSpan currenttime = dateTime.TimeOfDay;

                var coursedto = _mapper.Map<CourseTimetableDto>(timetable);

                return new AppResponse<CourseTimetableDto>(coursedto);
            }
            catch (Exception ex)
            {
                string error = $"An error occurred while fetching Timetable in UI: {ExceptionHelper.GetExceptionDetails(ex)}";
                _logger.LogError(error);
                return new AppResponse<CourseTimetableDto>(new List<string> { error }, false);
            }
        }
    }
}
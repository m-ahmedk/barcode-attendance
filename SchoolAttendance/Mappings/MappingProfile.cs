using AutoMapper;
using SchoolAttendance.Data.Dtos;
using System;

namespace SchoolAttendanceSystem.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Timetable, CourseTimetableDto>()
                .ForMember(dest => dest.IsClassActive, opt =>
                    opt.MapFrom(src => src.StartTime != null && src.EndTime != null
                    && DateTime.Now.TimeOfDay >= src.StartTime && DateTime.Now.TimeOfDay <= src.EndTime))
                .ForMember(dest => dest.StartTime, opt =>
                    opt.MapFrom(src => src.StartTime != null
                    ? DateTime.Today.Add(src.StartTime).ToString("hh:mm tt") : ""))
                .ForMember(dest => dest.EndTime, opt =>
                    opt.MapFrom(src => src.EndTime != null
                    ? DateTime.Today.Add(src.EndTime).ToString("hh:mm tt") : ""));
        }
    }
}

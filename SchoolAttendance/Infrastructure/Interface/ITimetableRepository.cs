using System;
using System.Threading.Tasks;

namespace SchoolAttendance.Infrastructure.Interface
{
    public interface ITimetableRepository : IGenericRepository<Timetable>
    {
        Task<Timetable?> GetCurrentClass(DateTime curDateTime);
    }
}

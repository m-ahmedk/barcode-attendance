using System.Threading.Tasks;
using System;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace SchoolAttendance.Infrastructure.Repository
{
    public class TimetableRepository : GenericRepository<Timetable>, ITimetableRepository
    {
        public TimetableRepository(IDbContextFactory dbContextFactory) : base(dbContextFactory)
        {
        }

        public async Task<Timetable?> GetCurrentClass(DateTime curDateTime)
        {
            TimeSpan currentTime = curDateTime.TimeOfDay;
            string currentDay = curDateTime.DayOfWeek.ToString();

            using (var _dbContext = _dbContextFactory.CreateDbContext())
            {
                // TimeSpan issue with LINQ query, so pull all data into memory
                var res = await _dbContext.Timetables
                   .Include(c => c.Course)
                   .AsNoTracking()
                   .ToListAsync();

                // Then assert conditions, alternate is to store DateTime
                return res.
                    FirstOrDefault(x => currentTime >= x.StartTime 
                    && currentTime <= x.EndTime
                    && currentDay.Equals(x.Day));
            }
        }
    }
}

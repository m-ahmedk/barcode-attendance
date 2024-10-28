using System;

namespace SchoolAttendance.Infrastructure.Repository
{
    public class StudentRepository : GenericRepository<Student>, IStudentRepository
    {
        public StudentRepository(IDbContextFactory dbContextFactory) : base(dbContextFactory)
        {
        }

        public int CalculateAge(DateTime dob)
        {
            DateTime today = DateTime.Today;
            int age = today.Year - dob.Year;

            if (dob.Date > today.AddYears(-age)) {
                age--;
            }

            return age;
        }
    }
}

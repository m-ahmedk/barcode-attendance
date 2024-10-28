using System;

namespace SchoolAttendance.Infrastructure.Interface
{
    public interface IStudentRepository : IGenericRepository<Student>
    {
        int CalculateAge(DateTime dob);
    }
}

using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SchoolAttendance.Services
{
    public class StudentCourseService
    {
        private readonly IUnitOfWork _unitOfWork;

        public StudentCourseService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

    }
}

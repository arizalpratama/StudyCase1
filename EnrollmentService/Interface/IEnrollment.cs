using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EnrollmentService.Models;

namespace EnrollmentService.Interface
{
    public interface IEnrollment
    {
        bool SaveChanges();
        IEnumerable<Enrollment> GetAllEnrollments();
        Enrollment GetEnrollmentById(int id);
        void CreateEnrollment(Enrollment enrol);
    }
}
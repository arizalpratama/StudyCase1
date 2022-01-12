using EnrollmentService.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EnrollmentService.Data
{
    public interface IEnrollment
    {
        bool SaveChanges();
        Task<IEnumerable<Enrollment>> GetAllEnrollments();
        Task<Enrollment> GetEnrollmentById(string id);
        Task<Enrollment> CreateEnrollment(Enrollment enroll);
        Task DeleteEnrollment(string id);
    }
}

/*using System;
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
        Task Delete(string id);
    }
}*/
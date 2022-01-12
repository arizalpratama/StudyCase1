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
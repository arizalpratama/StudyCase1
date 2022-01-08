using EnrollmentService.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

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

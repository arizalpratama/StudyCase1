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
        void CreateEnrollment(Enrollment enroll);
        //Task<Enrollment> CreateEnrollment(Enrollment enroll);
        void UpdateEnrollment(int id, Enrollment obj);
        Task DeleteEnrollment(string id);
    }
}
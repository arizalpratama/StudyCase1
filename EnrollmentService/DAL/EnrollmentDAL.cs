using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EnrollmentService.Data;
using EnrollmentService.Interface;
using EnrollmentService.Models;

namespace EnrollmentService.DAL
{
    public class EnrollmentDAL : IEnrollment
    {
        private readonly ApplicationDbContext _context;
        public EnrollmentDAL(ApplicationDbContext context)
        {
            _context = context;
        }
        public void CreateEnrollment(Enrollment enrol)
        {
            if (enrol == null)
            {
                throw new ArgumentNullException(nameof(enrol));
            }
            _context.Enrollments.Add(enrol);
        }

        public IEnumerable<Enrollment> GetAllEnrollments()
        {
            return _context.Enrollments.ToList();
        }

        public Enrollment GetEnrollmentById(int id)
        {
            return _context.Enrollments.FirstOrDefault(p => p.EnrollmentId == id);
        }

        public bool SaveChanges()
        {
            return (_context.SaveChanges() >= 0);
        }
    }
}
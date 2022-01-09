using EnrollmentService.Data;
using EnrollmentService.Interface;
using EnrollmentService.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EnrollmentService.DAL
{
    public class EnrollmentDAL : IEnrollment
    {
        private readonly ApplicationDbContext _context;
        public EnrollmentDAL(ApplicationDbContext context)
        {
            _context = context;
        }
        //Create
        public void CreateEnrollment(Enrollment enrol)
        {
            if (enrol == null)
            {
                throw new ArgumentNullException(nameof(enrol));
            }
            _context.Enrollments.Add(enrol);
        }
        //Get All
        public IEnumerable<Enrollment> GetAllEnrollments()
        {
            return _context.Enrollments.ToList();
        }
        //Get By Id
        public Enrollment GetEnrollmentById(int id)
        {
            return _context.Enrollments.FirstOrDefault(p => p.EnrollmentId == id);
        }
        //Save Changes
        public bool SaveChanges()
        {
            return (_context.SaveChanges() >= 0);
        }
    }
}
using EnrollmentService.Data;
using EnrollmentService.Interface;
using EnrollmentService.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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

        //Delete
        public async Task Delete(string id)
        {
            var result = await _context.Enrollments.Where
                (e => e.EnrollmentId == Convert.ToInt32(id))
                .SingleOrDefaultAsync<Enrollment>();
            if (result == null) throw new Exception("Data tidak ditemukan !");
            try
            {
                _context.Enrollments.Remove(result);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException dbEx)
            {
                throw new Exception($"Error: {dbEx.Message}");
            }
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
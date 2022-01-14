using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using EnrollmentService.Data;
using EnrollmentService.Models;
using System;
using System.Linq;

namespace EnrollmentService.DAL
{
    public class EnrollmentDAL : IEnrollment
    {

        private ApplicationDbContext _db;
        public EnrollmentDAL(ApplicationDbContext db)
        {
            _db = db;
        }

        //Create
        public void CreateEnrollment(Enrollment enrollment)
        {
            if (enrollment == null)
            {
                throw new ArgumentNullException(nameof(enrollment));
            }
            _db.Enrollments.Add(enrollment);
        }

        //Delete
        public async Task DeleteEnrollment(string id)
        {
            var result = await GetEnrollmentById(id);
            if (result == null) throw new Exception("Data tidak ditemukan !");
            try
            {
                _db.Enrollments.Remove(result);
                await _db.SaveChangesAsync();
            }
            catch (DbUpdateException dbEx)
            {
                throw new Exception($"Error: {dbEx.Message}");
            }
        }
        //Get All
        public async Task<IEnumerable<Enrollment>> GetAllEnrollments()
        {
            var results = await _db.Enrollments.Include(e => e.Student)
                .Include(e => e.Course).AsNoTracking().ToListAsync();
            return results;
        }
        //Get By Id
        public async Task<Enrollment> GetEnrollmentById(string id)
        {
            var result = await _db.Enrollments.Include(e => e.Student)
                .Include(e => e.Course).Where(s => s.EnrollmentID == Convert.ToInt32(id)).SingleOrDefaultAsync<Enrollment>();
            if (result != null)
                return result;
            else
                throw new Exception("Data tidak Ditemukan");
        }

        //Update
        public void UpdateEnrollment(int id, Enrollment obj)
        {
            var result = _db.Enrollments.FirstOrDefault(p => p.EnrollmentID == id);
            result.StudentID = obj.StudentID;
            result.CourseID = obj.CourseID;
            _db.SaveChanges();
        }

        //Save
        public bool SaveChanges()
        {
            return (_db.SaveChanges() >= 0);
        }
    }
}
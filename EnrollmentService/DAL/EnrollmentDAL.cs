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

        public async Task<Enrollment> CreateEnrollment(Enrollment enroll)
        {
            try
            {
                _db.Enrollments.Add(enroll);
                await _db.SaveChangesAsync();
                var result = await _db.Enrollments.Include(e => e.Student)
                .Include(e => e.Course).Where(s => s.EnrollmentId == enroll.EnrollmentId).SingleOrDefaultAsync<Enrollment>();
                return result;
            }
            catch (DbUpdateException dbEx)
            {
                throw new Exception($"Error : {dbEx.Message}");
            }
        }

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

        public async Task<IEnumerable<Enrollment>> GetAllEnrollments()
        {
            var results = await _db.Enrollments.Include(e => e.Student)
                .Include(e => e.Course).AsNoTracking().ToListAsync();
            return results;
        }

        public async Task<Enrollment> GetEnrollmentById(string id)
        {
            var result = await _db.Enrollments.Include(e => e.Student)
                .Include(e => e.Course).Where(s => s.EnrollmentId == Convert.ToInt32(id)).SingleOrDefaultAsync<Enrollment>();
            if (result != null)
                return result;
            else
                throw new Exception("Data tidak Ditemukan");
        }

        public bool SaveChanges()
        {
            return (_db.SaveChanges() >= 0);
        }
    }
}

/*using EnrollmentService.Data;
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
}*/
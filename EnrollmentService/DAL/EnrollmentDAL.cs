using Microsoft.EntityFrameworkCore;
using EnrollmentService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EnrollmentService.Data;
using EnrollmentService.Interface;

namespace EnrollmentService.DAL
{
    public class EnrollmentDAL : IEnrollment
    {
        private ApplicationDbContext _db;
        public EnrollmentDAL(ApplicationDbContext db)
        {
            _db = db;
        }

        //Delete
        public async Task Delete(string id)
        {
            var result = await GetById(id);
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
        public async Task<IEnumerable<Enrollment>> GetAll()
        {
            var results = await (from s in _db.Enrollments
                                 orderby s.EnrollmentId ascending
                                 select s).ToListAsync();
            return results;
        }

        //Get By Id
        public async Task<Enrollment> GetById(string id)
        {
            var result = await _db.Enrollments.Where
                (s => s.EnrollmentId == Convert.ToInt32(id)).SingleOrDefaultAsync<Enrollment>();
            if (result != null)
                return result;
            else
                throw new Exception("Data tidak ditemukan !");
        }

        //Insert
        public async Task<Enrollment> Insert(Enrollment obj)
        {
            try
            {
                _db.Enrollments.Add(obj);
                await _db.SaveChangesAsync();
                return obj;
            }
            catch (DbUpdateException dbEx)
            {
                throw new Exception($"Error: {dbEx.Message}");
            }
        }

        //Update
        public async Task<Enrollment> Update(string id, Enrollment obj)
        {
            try
            {
                var result = await GetById(id);
                result.StudentId = obj.StudentId;
                result.CourseId = obj.CourseId;
                await _db.SaveChangesAsync();
                obj.EnrollmentId = Convert.ToInt32(id);
                return obj;
            }
            catch (DbUpdateException dbEx)
            {
                throw new Exception($"Error: {dbEx.Message}");
            }
        }
    }
}

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
    public class CourseDAL : ICourse
    {
        private ApplicationDbContext _db;
        public CourseDAL(ApplicationDbContext db)
        {
            _db = db;
        }

        //Delete
        public async Task Delete(string id)
        {
            try
            {
                var result = await GetById(id);
                if (result == null) throw new Exception($"Data course {id} tidak ditemukan !");
                _db.Courses.Remove(result);
                await _db.SaveChangesAsync();
            }
            catch (DbUpdateException dbEx)
            {
                throw new Exception($"Error: {dbEx.Message}");
            }
        }

        //Get All
        public async Task<IEnumerable<Course>> GetAll()
        {
            var results = await _db.Courses.OrderBy(c => c.Title).ToListAsync();
            return results;
        }

        //Get By Id
        public async Task<Course> GetById(string id)
        {
            var results = await(from c in _db.Courses
                                where c.Id == Convert.ToInt32(id)
                                select c).AsNoTracking().SingleOrDefaultAsync();
            if (results == null) throw new Exception($"Data {id} tidak temukan !");

            return results;
        }

        //Insert
        public async Task<Course> Insert(Course obj)
        {
            try
            {
                _db.Courses.Add(obj);
                await _db.SaveChangesAsync();
                return obj;
            }
            catch (DbUpdateException dbEx)
            {
                throw new Exception($"Error: {dbEx.Message}");
            }
        }

        //Update
        public async Task<Course> Update(string id, Course obj)
        {
            try
            {
                var result = await GetById(id);
                result.Title = obj.Title;
                result.Description = obj.Description;
                await _db.SaveChangesAsync();
                obj.Id = Convert.ToInt32(id);
                return obj;
            }
            catch (DbUpdateException dbEx)
            {
                throw new Exception($"Error: {dbEx.Message}");
            }
        }
    }
}
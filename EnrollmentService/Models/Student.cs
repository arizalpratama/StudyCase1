using System;
using System.Collections.Generic;

namespace EnrollmentService.Models
{
    public class Student
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime EnrollmentDate { get; set; }
        //one to many with course
        public ICollection<Course> Courses { get; set; }
        //relation with enrollment
        public ICollection<Enrollment> Enrollments { get; set; }
    }
}

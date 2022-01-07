using System.Collections.Generic;

namespace EnrollmentService.Models
{
    public class Course
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        //relation with enrollment tbl 
        public ICollection<Enrollment> Enrollments { get; set; }
    }
}

using EnrollmentService.Models;

namespace EnrollmentService.Dtos
{
    public class EnrollmentCreateDto
    {
        public int CourseID { get; set; }
        public int StudentID { get; set; }
    } 
}
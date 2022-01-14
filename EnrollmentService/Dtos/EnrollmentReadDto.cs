using EnrollmentService.Models;

namespace EnrollmentService.Dtos
{
    public class EnrollmentReadDto
    {
        public int EnrollmentID { get; set; }
        public string CourseID { get; set; }
        public string StudentID { get; set; }
    } 
}
using EnrollmentService.Models;

namespace EnrollmentService.Dtos
{
    public class EnrollmentReadDto
    {
        public int EnrollmentId { get; set; }
        public string CourseId { get; set; }
        public string StudentId { get; set; }
    }
}
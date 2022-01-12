using System.ComponentModel.DataAnnotations;

namespace PaymentService.Models
{
    public class Payment
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double TotalAmount { get; set; }
        [Required]
        public int EnrollmentId { get; set; }
        public Enrollment Enrollment { get; set; }
    }
}
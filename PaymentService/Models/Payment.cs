using System.ComponentModel.DataAnnotations;

namespace PaymentService.Models
{
    public class Payment
    {
        [Key]
        [Required]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public double TotalAmount { get; set; }
        [Required]
        public int EnrollmentId { get; set; }
        public Enrollment Enrollment { get; set; }
    }
}
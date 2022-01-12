using System.ComponentModel.DataAnnotations;

namespace PaymentService.Dtos
{
    public class PaymentCreateDto
    {
        public int EnrollmentId { get; set; }
        public double TotalAmount { get; set; }
    }
}
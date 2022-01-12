namespace PaymentService.Dtos
{
    public class PaymentReadDto
    {
        public int Id { get; set; }
        public string TotalAmount { get; set; }
        public int EnrollmentId { get; set; }
    }
}
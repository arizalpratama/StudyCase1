using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PaymentService.Models
{
    public class Enrollment
    {
        [Key]
        [Required]
        public int Id { get; set; }

        [Required]
        public int ExternalID { get; set; }

        [Required]
        public string Name { get; set; }
        public ICollection<Payment> Payments { get; set; } =
        new List<Payment>();
    }
}
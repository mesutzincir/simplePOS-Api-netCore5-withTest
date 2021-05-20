using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SimplePOS.DTOs
{
    public class TransactionDto
    {
        [Required]
        [RegularExpression(@"(PAYMENT|ADJUSTMENT)",ErrorMessage = "MessageType should be PAYMENT or ADJUSTMENT")]
        public String MessageType { get; set; }
        [Required]
        public string TransactionId { get; set; }
        [Required]
        [Range(1000, 9999)]
        public long AccountId { get; set; }
        [Required]
        [RegularExpression(@"(VISA|MASTER)", ErrorMessage = "Origin should be VISA or MASTER")]
        public string Origin { get; set; }
        [Required]
        [Range(0.01, 9999999999)]
        public decimal Amount { get; set; }
    }
}

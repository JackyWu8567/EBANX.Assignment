using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EBANX.Assignment.Api.Dtos
{
    public class PaymentDto
    {
        [Required]
        public string Type { get; set; }
        public string Origin { get; set; }
        public string Destination { get; set; }
        [Required]
        public decimal Amount { get; set; }
    }
}

using EBANX.Assignment.Application.Models.Base;
using System;
using System.Collections.Generic;
using System.Text;
namespace EBANX.Assignment.Application.Models
{
    public class TransferModel : BaseModel
    {
        public string Destination { get; set; }
        public decimal DestinationBalance { get; set; }
    }
}

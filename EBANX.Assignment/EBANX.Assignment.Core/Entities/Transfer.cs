using System;
using System.Collections.Generic;
using System.Text;

namespace EBANX.Assignment.Core.Entities
{
    public class Transfer : Base
    {
        public string Destination { get; set; }
        public decimal DestinationBalance { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EBANX.Assignment.Api.Dtos
{
    public class TransferDto
    {
        public BaseDto Origin { get; set; }
        public BaseDto Destination { get; set; }
    }
}

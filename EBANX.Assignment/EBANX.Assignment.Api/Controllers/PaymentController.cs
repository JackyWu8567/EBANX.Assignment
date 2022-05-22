using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

using EBANX.Assignment.Application.Interfaces;
using EBANX.Assignment.Api.Dtos;

namespace EBANX.Assignment.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PaymentController : ControllerBase
    {
        private readonly ILogger<PaymentController> _logger;
        private readonly IPaymentService _paymentService;

        public PaymentController(IPaymentService paymentService, ILogger<PaymentController> logger)
        {
            _paymentService = paymentService;
            _logger = logger;
        }

        [HttpGet("balance")]
        public ActionResult<decimal> Balance(string account_id)
        {
            try
            {
                return _paymentService.GetBalance(account_id);
            }
            catch (ArgumentException)
            {
                return NotFound(0);
            }
        }

        [HttpPost("event")]
        public ActionResult<object> Event(PaymentDto payment)
        {
            object result = null;
            try
            {
                if (payment != null)
                {
                    var eventType = Enum.Parse(typeof(EventType), payment.Type);
                    switch (eventType)
                    {
                        case EventType.deposit:
                            {
                                var deposit = _paymentService.Deposit(payment.Destination, payment.Amount);
                                result = new DepositDto { Destination = new BaseDto { Id = deposit.Origin, Balance = deposit.OriginBalance } };
                                break;
                            }
                        case EventType.withdraw:
                            {
                                var withdraw = _paymentService.Withdraw(payment.Origin, payment.Amount);
                                result = new WithdrawDto { Origin = new BaseDto { Id = withdraw.Origin, Balance = withdraw.OriginBalance } };
                                break;
                            }
                        case EventType.transfer:
                            {
                                var transfer = _paymentService.Transfer(payment.Origin, payment.Destination, payment.Amount);
                                result = new TransferDto
                                {
                                    Origin = new BaseDto { Id = transfer.Origin, Balance = transfer.OriginBalance },
                                    Destination = new BaseDto { Id = transfer.Destination, Balance = transfer.DestinationBalance }
                                };
                                break;
                            }
                    }
                }
                return result;
            }
            catch (ArgumentException)
            {
                return NotFound(0);
            }
        }
    }

    public enum EventType
    {
        deposit,
        withdraw,
        transfer
    }
}

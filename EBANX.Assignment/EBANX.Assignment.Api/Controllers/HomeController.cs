using EBANX.Assignment.Api.Domains;
using EBANX.Assignment.IRepository.Event.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EBANX.Assignment.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HomeController : ControllerBase
    {
        private readonly ILogger<HomeController> _logger;
        private readonly PaymentManager _paymentManager;

        public HomeController(PaymentManager paymentManager, ILogger<HomeController> logger)
        {
            _paymentManager = paymentManager;
            _logger = logger;
        }

        [HttpGet]
        public decimal Balance(string account_id)
        {
            return _paymentManager.GetBalance(account_id);
        }

        [HttpPost]
        public object Event(string type, string origin, string destination, decimal amount)
        {
            object result = null;
            if (string.IsNullOrEmpty(type))
            {
                var eventType = Enum.Parse(typeof(EventType), type);
                switch(eventType)
                {
                    case EventType.deposit:
                        {
                            result = _paymentManager.Deposit(new Deposit { Destination = destination, Amount = amount });
                            break;
                        }
                    case EventType.withdraw:
                        {
                            result = _paymentManager.Withdraw(new Withdraw { Origin = origin, Amount = amount });
                            break;
                        }
                    case EventType.transfer:
                        {
                            result = _paymentManager.Transfer(new Transfer { Origin = origin, Destination = destination, Amount = amount });
                            break;
                        }
                }
            }
            return result;
        }
    }

    public enum EventType
    {
        deposit,
        withdraw,
        transfer
    }
}

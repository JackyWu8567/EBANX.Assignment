using EBANX.Assignment.IRepository.Event.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EBANX.Assignment.Api.Domains
{
    public class PaymentManager
    {
        private readonly IRepository.Event.IPaymentRepository _eventRepository;
        public PaymentManager(IRepository.Event.IPaymentRepository eventRepository)
        {
            _eventRepository = eventRepository;
        }

        private bool ValidateAccount(string accountId)
        {
            if (accountId == "100")
                return true;
            if (accountId == "200")
                return false;
            return false;
        }

        public decimal GetBalance(string accountId)
        {
            if(ValidateAccount(accountId))
            {
                return _eventRepository.GetBalance(accountId);
            }
            return 0;
        }

        public Deposit Deposit(Event deposit)
        {
            return _eventRepository.Deposit(deposit as Deposit);
        }

        public Withdraw Withdraw(Event withdraw)
        {
            return _eventRepository.Withdraw(withdraw as Withdraw);
        }

        public Transfer Transfer(Event transfer)
        {
            return _eventRepository.Transfer(transfer as Transfer);
        }
    }
}

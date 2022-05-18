using EBANX.Assignment.Core.Entities;
using EBANX.Assignment.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace EBANX.Assignment.Infrastructure.Repositories
{
    public class PaymentRepository : IPaymentRepository
    {
        public Deposit Deposit(string accountId, decimal amount)
        {
            return null;
        }

        public decimal GetBalance(string accountId)
        {
            return 0;
        }

        public Transfer Transfer(string originAccountId, string destinationAccountId, decimal amount)
        {
            return null;
        }

        public Withdraw Withdraw(string accountId, decimal amount)
        {
            return null;
        }
    }
}

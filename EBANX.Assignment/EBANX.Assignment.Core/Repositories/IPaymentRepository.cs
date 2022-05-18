using EBANX.Assignment.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace EBANX.Assignment.Core.Repositories
{
    public interface IPaymentRepository
    {
        decimal GetBalance(string accountId);
        Deposit Deposit(string accountId, decimal amount);
        Withdraw Withdraw(string accountId, decimal amount);
        Transfer Transfer(string originAccountId, string destinationAccountId, decimal amount);
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EBANX.Assignment.Application.Interfaces
{
    public interface IPaymentService
    {
        Task<decimal> GetBalance(string accountId);
        Task Deposit(string accountId, decimal amount);
        Task Withdraw(string accountId, decimal amount);
        Task Transfer(string originAccountId, string destinationAccountId, decimal amount);
    }
}

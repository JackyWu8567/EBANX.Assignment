using EBANX.Assignment.Application.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EBANX.Assignment.Application.Interfaces
{
    public interface IPaymentService
    {
        decimal GetBalance(string accountId);
        DepositModel Deposit(string accountId, decimal amount);
        WithdrawModel Withdraw(string accountId, decimal amount);
        TransferModel Transfer(string originAccountId, string destinationAccountId, decimal amount);
        void Reset();
    }
}

using EBANX.Assignment.Application.Interfaces;
using EBANX.Assignment.Application.Mapper;
using EBANX.Assignment.Application.Models;
using EBANX.Assignment.Application.Models.Base;
using EBANX.Assignment.Core.Repositories;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EBANX.Assignment.Application.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly IPaymentRepository _paymentRepository;
        private readonly ILogger<PaymentService> _logger;        
        public PaymentService(IPaymentRepository paymentRepository, ILogger<PaymentService> logger)
        {
            _paymentRepository = paymentRepository ?? throw new ArgumentNullException(nameof(paymentRepository));
            _logger = logger;
        }

        public decimal GetBalance(string accountId)
        {
            var account = _paymentRepository.GetAccount(accountId);
            if (account is null)
            {
                throw new ArgumentException("Invalid accountId");
            }
            var balance = account.Balance;
            _paymentRepository.DeleteAccount(accountId);
            return balance;
        }

        public DepositModel Deposit(string accountId, decimal amount)
        {
            if (amount <= 0)
                throw new ArgumentException("Invalid amount");
            var account = _paymentRepository.GetAccount(accountId);
            if(account is null)
            {
                account = _paymentRepository.CreateAccount(accountId, amount);
            }
            else
            {
                account.Balance += amount;
            }
            account = _paymentRepository.UpdateAccount(account.AccountId, account.Balance);

            return new DepositModel { Origin = account.AccountId, OriginBalance = account.Balance };
        }

        public WithdrawModel Withdraw(string accountId, decimal amount)
        {
            if (amount <= 0)
                throw new ArgumentException("Invalid amount");

            var account = _paymentRepository.GetAccount(accountId);

            if (account is null)
            {
                throw new ArgumentException("Invalid accountId");
            }
            if (account.Balance - amount < 0)
                throw new ArgumentException("Insufficient Balance");

            account.Balance -= amount;
            account = _paymentRepository.UpdateAccount(account.AccountId, account.Balance);

            return new WithdrawModel { Origin = account.AccountId, OriginBalance = account.Balance };
        }

        public TransferModel Transfer(string originAccountId, string destinationAccountId, decimal amount)
        {
            if (amount <= 0)
                throw new ArgumentException("Invalid amount");

            var originAccount = _paymentRepository.GetAccount(originAccountId);
            if (originAccount is null)
            {
                throw new ArgumentException("Invalid originAccountId");
            }

            if (originAccount.Balance - amount < 0)
                throw new ArgumentException("Insufficient Balance");

            originAccount.Balance -= amount;
            var destinationAccount = _paymentRepository.GetAccount(destinationAccountId);
            if(destinationAccount is null)
            {
                destinationAccount = _paymentRepository.CreateAccount(destinationAccountId, amount);
            }
            else
            {
                destinationAccount.Balance += amount;
            }
            originAccount = _paymentRepository.UpdateAccount(originAccount.AccountId, originAccount.Balance);
            destinationAccount = _paymentRepository.UpdateAccount(destinationAccount.AccountId, destinationAccount.Balance);

            return new TransferModel 
            {
                Origin = originAccount.AccountId,
                OriginBalance = originAccount.Balance,
                Destination = destinationAccount.AccountId,
                DestinationBalance = destinationAccount.Balance
            };
        }
    }
}

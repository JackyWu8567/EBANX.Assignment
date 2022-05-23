using EBANX.Assignment.Core.Entities;
using EBANX.Assignment.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EBANX.Assignment.Infrastructure.Repositories
{
    public class PaymentRepository : IPaymentRepository
    {
        private List<Account> accounts;
        public PaymentRepository()
        {
            accounts = new List<Account>();
        }

        public Account GetAccount(string accountId)
        {
            if (!accounts.Any(a => a.AccountId == accountId))
            {
                return null;
            }
            return accounts.FirstOrDefault(a => a.AccountId == accountId);
        }

        public Account CreateAccount(string accountId, decimal amount)
        {
            var acount = new Account { AccountId = accountId, Balance = amount };
            accounts.Add(acount);
            return acount;
        }

        public Account UpdateAccount(string accountId, decimal balance)
        {
            if (!accounts.Any(a => a.AccountId == accountId))
            {
                return null;
            }
            var account = accounts.FirstOrDefault(a => a.AccountId == accountId);
            account.Balance = balance;

            return account;
        }

        public List<Account> GetAllAccount()
        {
            return accounts;
        }

        public void DeleteAccount(string accountId)
        {
            var account = GetAccount(accountId);
            if(!(account is null))
            {
                accounts.Remove(account);
            }
        }

        public void Reset()
        {
            if(accounts!=null && accounts.Any())
            {
                accounts.Clear();
            }
        }
    }
}

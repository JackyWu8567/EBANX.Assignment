using EBANX.Assignment.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace EBANX.Assignment.Core.Repositories
{
    public interface IPaymentRepository
    {        
        Account GetAccount(string accountId);
        Account CreateAccount(string accountId, decimal balance);
        Account UpdateAccount(string accountId, decimal balance);
        void DeleteAccount(string accountId);
        List<Account> GetAllAccount();
    }
}

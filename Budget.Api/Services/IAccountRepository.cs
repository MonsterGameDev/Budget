using Budget.Api.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Budget.Api.Services
{
    public interface IAccountRepository
    {
        Boolean AccountExists(int accountId);
        IEnumerable<Account> GetAccounts(Boolean includeSubAccounts);
        Account GetAccount(int accountId, Boolean includeSubAccounts);
       
        
        
        
        //IEnumerable<SubAccount> GetSubAccountsFromAccount(int accountId);
        //SubAccount GetSubAccount(int accountId, int subAccountId);


    }
}

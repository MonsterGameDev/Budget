using Budget.Api.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Budget.Api.Services
{
    public interface ISubAccountRepository
    {
        Boolean AccountExists(int accountID);
        Boolean SubAccountExists(int accountId, int subAccountId);
        IEnumerable<SubAccount> GetSubAccounts(int accountId, 
            Boolean includePostingLines);
        SubAccount GetSubAccount(int accountId, int subAccountId, 
            Boolean includePostingLines);
        SubAccount CreateSubAccount(int accountId, SubAccount subAccount);
        void UpdateSubAccount(int subAccountId, SubAccount subAccount);
        void DeleteSubAccount(int subAccountId);
    }
}

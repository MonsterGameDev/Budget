using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Budget.Api.Entities;
using Microsoft.EntityFrameworkCore;

namespace Budget.Api.Services
{
    public class SubAccountRepository : ISubAccountRepository
    {
        private BudgetDbContext _budgetDbContext;
        public SubAccountRepository(BudgetDbContext ctx)
        {
            _budgetDbContext = ctx;
        }

        public bool AccountExists(int accountId)
        {
            return !(_budgetDbContext.Accounts.Find(accountId) == null);
        }

        public bool SubAccountExists(int accountId, int subAccountId)
        {
            var result = _budgetDbContext.SubAccounts
                            .Where(sa => sa.AccountId == accountId);
            return !(result == null);
        }
        
        public IEnumerable<SubAccount> GetSubAccounts(int accountId, bool includePostingLines)
        {
            if (includePostingLines)
            {
                var result = _budgetDbContext.SubAccounts
                            .Include("PostingLines")
                            .Where(sa => sa.AccountId == accountId)
                            .ToList();
                return result;
            }
            var resultWithout = _budgetDbContext.SubAccounts
                            .Where(sa => sa.AccountId == accountId)
                            .ToList();
            return resultWithout;

        }

        public SubAccount GetSubAccount(int accountId, int subAccountId, bool includePostingLines)
        {
            var subAccount = _budgetDbContext.SubAccounts
                    .Where(sa => sa.AccountId == accountId)
                    .Where(sa => sa.Id == subAccountId)
                    .Include("Account");

            if (subAccount == null)
            {
                throw new Exception("Not found");
            }

            if (includePostingLines)
            {
                return (SubAccount)subAccount.Include("PostingLines");
            }

            
            return (SubAccount)subAccount;
        }

        public SubAccount CreateSubAccount(int accountId, SubAccount subAccount)
        {
            throw new NotImplementedException();
        }

        public void UpdateSubAccount(int subAccountId, SubAccount subAccount)
        {
            throw new NotImplementedException();
        }

        public void DeleteSubAccount(int subAccountId)
        {
            throw new NotImplementedException();
        }
    }
}

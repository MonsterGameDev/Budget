using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Budget.Api.Entities;
using Budget.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace Budget.Api.Services
{
    public class AccountRepository : IAccountRepository
    {
        private BudgetDbContext _budgetDbContext;
        public AccountRepository(BudgetDbContext ctx)
        {
            _budgetDbContext = ctx;
        }

        // Burde være en property
        public Boolean AccountExists(int accountId)
        {
            var account = _budgetDbContext.Accounts.FirstOrDefault(a => a.Id == accountId);

            return !(account == null);
        }

        public IEnumerable<Account> GetAccounts(Boolean includeSubAccounts)
        {
            if (includeSubAccounts) {

                var result = _budgetDbContext.Accounts
                .Include(a => a.SubAccounts)
                .ThenInclude(a => a.PostingLines)
                .OrderBy(a => a.Name)
                .ToList();
                return result;
            }

            return _budgetDbContext.Accounts.OrderBy(a => a.Name).ToList();
        }

        public Account GetAccount(int accountId, Boolean includeSubAccounts)
        {
            if (includeSubAccounts)
            {
                var resultWithout = _budgetDbContext.Accounts.Include(a => a.SubAccounts)
                    .Where(a => a.Id == accountId).FirstOrDefault();
                return resultWithout;
            }
            var resultWith = _budgetDbContext.Accounts.Where(a => a.Id == accountId).FirstOrDefault();
            return resultWith;
        }

        public void CreateAccount(Account account)
        {
            _budgetDbContext.Accounts.Add(account);
        }

        public void UpdateAccount(int id, Account account)
        {
            throw new NotImplementedException();
        }

        public void DeleteAccount(Account account)
        {
            _budgetDbContext.Remove(account);   
        }

        public bool Save()
        {
            return (_budgetDbContext.SaveChanges() >= 0);
        }




        //public SubAccount GetSubAccount(int accountId, int subAccountId)
        //{
        //    return _budgetDbContext.SubAccounts
        //       .Where(sa => sa.AccountId == accountId && sa.Id == subAccountId)
        //       .FirstOrDefault();
        //}

        //public IEnumerable<SubAccount> GetSubAccountsFromAccount(int accountId)
        //{
        //    return _budgetDbContext.SubAccounts
        //         .Where(sa => sa.AccountId == accountId)
        //         .ToList();
        //}

        
    }
}


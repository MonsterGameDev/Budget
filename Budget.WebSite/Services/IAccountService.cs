using Budget.WebSite.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Budget.WebSite.Services
{
    public interface IAccountService
    {
        Task<List<Account>> GetAccountsAsync(Boolean includeSubAccounts);
        Task<Account> GetAccountAsync(int id, Boolean includeSubAccounts);
        Task<Uri> CreateAccountAsync(AccountForEditingDto account);
        Task UpdateAccountAsync(int accountId, AccountForUpdatingDto account);
        Task DeleteAccountAsync(int accountId);
    }
}

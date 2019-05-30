using Budget.Api.Mock;
using Budget.Api.Models;
using Budget.Api.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Budget.Api.Controllers
{
    [Route("api/accounts")]
    public class SubAccountsController: Controller
    {
        private ISubAccountRepository _subAccountRepo;
        public SubAccountsController(ISubAccountRepository repo)
        {
            _subAccountRepo = repo;
        }

        [HttpGet("{accountId}/subaccounts")]
        public IActionResult GetSubAccounts(int accountId, bool includePostingLines)
        {
            if (!_subAccountRepo.AccountExists(accountId)) return NotFound();

            var subAccounts = _subAccountRepo.GetSubAccounts(accountId, includePostingLines);

            return Ok(subAccounts);
            

            //var account = AccountsDataStore.Current.Accounts.FirstOrDefault(a => a.Id == accountId);
            //if (account == null)
            //{
            //    return NotFound();
            //}

            //return Ok(account.SubAccounts);
        }

        [HttpGet("{accountId}/subaccounts/{id}", Name ="GetSubAccount")]
        public IActionResult GetSubAccount(int accountId, int id)
        {
            var account = AccountsDataStore.Current.Accounts.FirstOrDefault(a => a.Id == accountId);
            if (account == null)
            {
                return NotFound();
            }
            var subAccount = account.SubAccounts.FirstOrDefault(sa => sa.Id == id);
            if(subAccount == null)
            {
                return NotFound();
            }

            return Ok(subAccount);
        }
        [HttpPost("{accountId}/subaccounts")]
        public IActionResult CreateSubAccount(int accountId,
            [FromBody] SubAccountForEditingDto newSubAccount)
        {
            var accountFromStore = AccountsDataStore.Current.Accounts
                .FirstOrDefault(sa => sa.Id == accountId);
            if(accountFromStore== null)
            {
                return NotFound();
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            //ToDo Change
            var maxId= AccountsDataStore.Current.Accounts
                .SelectMany(a => a.SubAccounts)
                .Max(sa => sa.Id);

            var finishedSubAccount = new SubAccountDto()
            {
                Id = ++maxId,
                Name = newSubAccount.Name,
                Description = newSubAccount.Description
            };

            accountFromStore.SubAccounts.Add(finishedSubAccount);
            
            return CreatedAtRoute("GetSubAccount", new {
                accountId = accountId,
                id = finishedSubAccount.Id

            }, finishedSubAccount);
        }
        [HttpPut("{accountId}/subaccounts/{id}")]
        public IActionResult UpdateSubAccount(int accountId, int id, 
            [FromBody] SubAccountForEditingDto editedSubAccount) {
            var account = AccountsDataStore.Current.Accounts
                .FirstOrDefault(a => a.Id == accountId);
            if(account == null)
            {
                return NotFound();
            }
            var subAccount = account.SubAccounts.FirstOrDefault(sa => sa.Id == id);
            if (subAccount == null)
            {
                return NotFound();
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            subAccount.Name = editedSubAccount.Name;
            subAccount.Description = editedSubAccount.Description;

            return NoContent();
                
        }
        [HttpDelete("{accountId}/subaccounts/{id}")]
        public IActionResult DeleteSubAccount(int accountId,int id)
        {
            var account = AccountsDataStore.Current.Accounts
                .FirstOrDefault(a => a.Id == accountId);
            if(account == null)
            {
                return NotFound();
            }
            var subAccount = account.SubAccounts.FirstOrDefault(sa => sa.Id == id);
            if(subAccount == null)
            {
                return NotFound();
            }
            account.SubAccounts.Remove(subAccount);

            return NoContent();
        }
    }
}

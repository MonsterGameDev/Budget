using Budget.Api.Entities;
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
    public class AccountsController: Controller
    {
        private IAccountRepository _accountRepo;
        public AccountsController(IAccountRepository repo)
        {
            _accountRepo = repo;
        }

        [HttpGet()]
        public IActionResult GetAccounts(Boolean includeSubAccounts = false)
        {

            var accountEntities = _accountRepo.GetAccounts(includeSubAccounts);
            // Vælg om responset skal indeholde subAccounts
            if (!includeSubAccounts)
            {
                var result = new List<AccountWithoutSubAccountsDto>();

                foreach (var accountEntity in accountEntities)
                {
                    result.Add(new AccountWithoutSubAccountsDto()
                    {
                        Id = accountEntity.Id,
                        Name = accountEntity.Name,
                        Description = accountEntity.Description
                    });
                }

                return Ok(result);
            }

            // Med subaccounts (men uden PostingLines)
            var resultWith = new List<AccountWithSubAccountsDto>();

            foreach (var accountEntity in accountEntities)
            {
                resultWith.Add(new AccountWithSubAccountsDto()
                {
                    Id = accountEntity.Id,
                    Name = accountEntity.Name,
                    Description = accountEntity.Description,
                    SubAccounts = _mapSubAccountsToDto(accountEntity.SubAccounts.ToList())
                });
            }

            return Ok(resultWith);



            // return Ok(AccountsDataStore.Current);
        }

        [HttpGet("{id}", Name="GetAccount")]
        public IActionResult GetAccount(int id, bool includeSubAccounts = false)
        {
            if (!_accountRepo.AccountExists(id)) return NotFound();

            var account = _accountRepo.GetAccount(id, includeSubAccounts);

            if (!includeSubAccounts)
            {
                var resultsWithout = new AccountWithoutSubAccountsDto()
                {
                    Id = account.Id,
                    Name = account.Name,
                    Description = account.Description
                };
                return Ok(resultsWithout);
            }

            var resultWith = new AccountWithSubAccountsDto()
            {
                Id = account.Id,
                Name = account.Name,
                Description = account.Description,
                SubAccounts = _mapSubAccountsToDto(account.SubAccounts.ToList())
            };

            return Ok(resultWith);
        }

        [HttpPost()]
        public IActionResult CreateAccount(
            [FromBody] AccountForEditingDto account)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            //ToDo Change
            var maxId = AccountsDataStore.Current.Accounts.Max(a => a.Id);

            var finishedAccount = new AccountDto()
            {
                Id = ++maxId,
                Name = account.Name,
                Description = account.Description
            };

            AccountsDataStore.Current.Accounts.Add(finishedAccount);

            return CreatedAtRoute("GetAccount", new {id=finishedAccount.Id}, finishedAccount);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateAccount(int id,
            [FromBody] AccountForEditingDto editedAccount)
        {
            var accountFromStore = AccountsDataStore.Current.Accounts
                .FirstOrDefault(a => a.Id == id);
            if (accountFromStore == null)
            {
                return NotFound();
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            accountFromStore.Name = editedAccount.Name;
            accountFromStore.Description = editedAccount.Description;

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteAccount(int id)
        {
            var account = AccountsDataStore.Current.Accounts
                .FirstOrDefault(a => a.Id == id);
            if(account == null)
            {
                return NotFound();
            }

            AccountsDataStore.Current.Accounts.Remove(account);

            return NoContent();
        }


        // Mappers
        private List<SubAccountWithoutPostingLinesDto> _mapSubAccountsToDto(List<SubAccount> sas)
        {
            var result = new List<SubAccountWithoutPostingLinesDto>();
            foreach (var sa in sas)
            {
                result.Add(new SubAccountWithoutPostingLinesDto()
                {
                    Id = sa.Id,
                    Name = sa.Name,
                    Description = sa.Description
                });
            }

            return result;
        }
    }
}

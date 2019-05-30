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
    public class AccountsController : Controller
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
                List<AccountWithoutSubAccountsDto> result 
                    = _mapAccountsToDtoWithout(_accountRepo.GetAccounts(false));
                return Ok(result);
            }

            // Med subaccounts (men uden PostingLines)

            var resultWith = _mapAccountsToDtoWith(_accountRepo.GetAccounts(true));
            return Ok(resultWith);



            // return Ok(AccountsDataStore.Current);
        }

        [HttpGet("{id}", Name = "GetAccount")]
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

            var finalAccount = _mapAccountToEntity(account);
            _accountRepo.CreateAccount(finalAccount);


            if (!_accountRepo.Save())
            {
                return StatusCode(500, "A problem occured handling your request");
            }

            // Todo finalAccount.id = 0 her...

            return CreatedAtRoute("GetAccount", new { id = finalAccount.Id }, finalAccount);



            ////ToDo Change
            //var maxId = AccountsDataStore.Current.Accounts.Max(a => a.Id);

            //var finishedAccount = new AccountDto()
            //{
            //    Id = ++maxId,
            //    Name = account.Name,
            //    Description = account.Description
            //};

            //AccountsDataStore.Current.Accounts.Add(finishedAccount);


        }

        [HttpPut("{id}")]
        public IActionResult UpdateAccount(int id,
            [FromBody] AccountForUpdatingDto editedAccount)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!_accountRepo.AccountExists(id))
            {
                return NotFound();
            }

            Account accountFromStore = _accountRepo.GetAccount(id, false);

            accountFromStore.Id = editedAccount.Id;
            accountFromStore.Name = editedAccount.Name;
            accountFromStore.Description = editedAccount.Description;

            if (!_accountRepo.Save())
            {
                return StatusCode(500, "A problem happened while handling your request");
            }

            return NoContent();
                    
            //var accountFromStore = AccountsDataStore.Current.Accounts
            //    .FirstOrDefault(a => a.Id == id);
            //if (accountFromStore == null)
            //{
            //    return NotFound();
            //}
            //if (!ModelState.IsValid)
            //{
            //    return BadRequest(ModelState);
            //}

            //accountFromStore.Name = editedAccount.Name;
            //accountFromStore.Description = editedAccount.Description;

            //return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteAccount(int id, bool deleteSubTree=false)
        {
            if (!_accountRepo.AccountExists(id)) return NotFound();

            var accountToDelete = _accountRepo.GetAccount(id, true);
            bool accountSubTreeExists = accountToDelete.SubAccounts.Count > 0;

            if (!deleteSubTree && accountSubTreeExists)
            {
                return StatusCode(500, "Account constains subAccounts!");
            }

            _accountRepo.DeleteAccount(accountToDelete);

            if (!_accountRepo.Save())
            {
                return StatusCode(500, "A problem happened while handling your request");
            }


            return NoContent();

            //var account = AccountsDataStore.Current.Accounts
            //    .FirstOrDefault(a => a.Id == id);
            //if (account == null)
            //{
            //    return NotFound();
            //}

            //AccountsDataStore.Current.Accounts.Remove(account);

            //return NoContent();
        }


        // Mappers
        private List<AccountWithoutSubAccountsDto> _mapAccountsToDtoWithout(IEnumerable<Account> accountsEntities) {
            var result = new List<AccountWithoutSubAccountsDto>();

            foreach (var accountEntity in accountsEntities)
            {
                result.Add(new AccountWithoutSubAccountsDto()
                {
                    Id = accountEntity.Id,
                    Name = accountEntity.Name,
                    Description = accountEntity.Description
                });
            }

            return result;
        }

        private List<AccountWithSubAccountsDto> _mapAccountsToDtoWith(IEnumerable<Account> accountEntities)
        {
            var result = new List<AccountWithSubAccountsDto>();

            foreach (var accountEntity in accountEntities)
            {
                result.Add(new AccountWithSubAccountsDto()
                {
                    Id = accountEntity.Id,
                    Name = accountEntity.Name,
                    Description = accountEntity.Description,
                    SubAccounts = _mapSubAccountsToDto(accountEntity.SubAccounts.ToList())
                });
            }

            return result;
        }

        private List<SubAccountWithoutPostingLinesDto>
            _mapSubAccountsToDto(List<SubAccount> sas)
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

        private Account _mapAccountToEntity(AccountForEditingDto account)
        {

            var newAccount = new Account()
            {
                Id = 0,
                Name = account.Name,
                Description = account.Description
            };

            return newAccount;
        }
    }
}

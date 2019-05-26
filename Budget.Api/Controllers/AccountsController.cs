using Budget.Api.Mock;
using Budget.Api.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Budget.Api.Controllers
{
    [Route("api/accounts")]
    public class Accounts: Controller
    {
        [HttpGet()]
        public IActionResult GetAccounts() 
        {
            return Ok(AccountsDataStore.Current);
        }

        [HttpGet("{id}", Name="GetAccount")]
        public IActionResult GetAccount(int id)
        {
            var account = AccountsDataStore.Current.Accounts
                .FirstOrDefault(a => a.Id == id); 

            if (account == null) {
                return NotFound();
            }
            return Ok(account);
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
    }
}

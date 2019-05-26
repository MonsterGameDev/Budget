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
    public class PostingLinesController: Controller
    {
        [HttpGet("{accountId}/subaccounts/{subAccountId}/postinglines")]
        public IActionResult GetPostingLines(int accountId, int subAccountId)
        {
            var account = AccountsDataStore.Current.Accounts
                .FirstOrDefault(a => a.Id == accountId);
            if (account == null)
            {
                return NotFound();
            }
            var subAccount = account.SubAccounts
                .FirstOrDefault(sa => sa.Id == subAccountId);
            
            if (subAccount == null)
            {
                return NotFound();
            }

            return Ok(subAccount.PostingLines);
        }
        
        [HttpGet("{accountId}/subAccounts/{subAccountId}/postinglines/{id}", Name="GetPostingLine")]
        public IActionResult GetPostingLine(int accountId, int subAccountId, int id)
        {
            var account = AccountsDataStore.Current.Accounts
                .FirstOrDefault(a => a.Id == accountId);
            if (account == null)
            {
                return NotFound();
            }
            var subAccount = account.SubAccounts
                .FirstOrDefault(sa => sa.Id == subAccountId);

            if (subAccount == null)
            {
                return NotFound();
            }
            var postingLine = subAccount.PostingLines.FirstOrDefault(p => p.Id == id);
            if (postingLine == null)
            {
                return NotFound();
            }
            return Ok(postingLine);
        }

        [HttpPost("{accountId}/subaccounts/{subAccountId}/postinglines")]
        public IActionResult CreatePostingLine(int accountId, int subAccountId,
            [FromBody] PostingLineForEditingDto newPostingLine)
        {
            var account = AccountsDataStore.Current.Accounts
                .FirstOrDefault(a => a.Id == accountId);
            if (account == null)
            {
                return NotFound();
            }
            var subAccount = account.SubAccounts.FirstOrDefault(sa => sa.Id == subAccountId);
            if(subAccount == null)
            {
                return NotFound();
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // ToDo Change
            var maxId = AccountsDataStore.Current.Accounts
               .SelectMany(a => a.SubAccounts)
               .SelectMany(sa => sa.PostingLines)
               .Max(sa => sa.Id);




            var finishedPostingLine = new PostingLineDto()
            {
                Id = ++maxId,
                Description = newPostingLine.Description,
                Amount = newPostingLine.Amount,
                Location = newPostingLine.Location,
                Created = DateTime.Now

            };

            subAccount.PostingLines.Add(finishedPostingLine);

            return CreatedAtRoute("GetPostingLine", new
            {
                accountId = accountId,
                subAccountId = subAccountId,
                id = finishedPostingLine.Id
            }, finishedPostingLine);


        }
        [HttpDelete("{accountId}/subaccounts/{subAccountId}/postingLines/{id}")]
        public IActionResult DeletePostingLine(int accountId, int subAccountId, int id)
        {
            var account = AccountsDataStore.Current.Accounts
                .FirstOrDefault(a => a.Id == accountId);
            if(account == null)
            {
                return NotFound();
            }
            var subAccount = account.SubAccounts.FirstOrDefault(sa => sa.Id == subAccountId);
            if (subAccount == null)
            {
                return NotFound();
            }
            var postingLine = subAccount.PostingLines.FirstOrDefault(p => p.Id == id);
            if(postingLine == null)
            {
                return NotFound();
            }

            subAccount.PostingLines.Remove(postingLine);

            return NoContent();

        }

    }
}

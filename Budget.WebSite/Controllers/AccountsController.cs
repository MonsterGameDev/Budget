using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Budget.WebSite.Models;
using Budget.WebSite.Services;
using Microsoft.AspNetCore.Mvc;

namespace Budget.WebSite.Controllers
{
    public class AccountsController : Controller
    {
        private IAccountService _svc;
        public AccountsController(IAccountService svc)
        {
            _svc = svc;
        }
        public async Task<ActionResult> Index()
        {
            var model = await _svc.GetAccountsAsync(false);

            return View(model);
        }

        public async Task<ActionResult> Details(int id)
        {
            var model = await _svc.GetAccountAsync(id, false);

            return View(model);

        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(AccountForEditingDto account)
        {

            if (ModelState.IsValid)
            {
                // Send til service
                Uri uri = await _svc.CreateAccountAsync(account);

                var parts = uri.AbsolutePath.ToString().Split('/');
                var idFromUri = parts.Last();


                return RedirectToAction("Details", new { id = idFromUri });
            }
            else
            {
                return View(account);
            }
        }


        [HttpGet]
        public async Task<IActionResult> Edit(int id) {
            var account = await _svc.GetAccountAsync(id, false);
            if (account == null) return NotFound();

            var editedAccount = new AccountForUpdatingDto()
            {
                Id = account.Id,
                Name = account.Name,
                Description = account.Description
            };
            return View(editedAccount);
        }

        [HttpPost()]
        public async Task<IActionResult> Edit(int id, [FromForm]AccountForUpdatingDto update)
        {
            if (id != update.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                await _svc.UpdateAccountAsync(id, update);


                return RedirectToAction("Index");

            }

            return View(update);
        }
        
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {


            await _svc.DeleteAccountAsync(id);

            return RedirectToAction("Index");
        }


    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
            var model = await _svc.GetAccountAsync(id, true);

            return View(model);
        }

        // return masterList.SelectMany(m => new SiloNode[] { m }.Concat(m.Children));
    }
}
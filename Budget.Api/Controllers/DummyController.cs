using Budget.Api.Entities;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Budget.Api.Controllers
{
    public class DummyController: Controller
    {
        private BudgetDbContext _ctx;
        public DummyController(BudgetDbContext ctx)
        {
            _ctx = ctx;
        }

        [HttpGet]
        [Route("api/testDatabase")]
        public IActionResult TestDatabase()
        {
            return Ok();
        }
    }
}

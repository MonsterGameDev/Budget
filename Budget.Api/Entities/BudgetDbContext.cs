using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Budget.Api.Entities
{
    public class BudgetDbContext: DbContext
    {
        public BudgetDbContext(DbContextOptions<BudgetDbContext> options)
            :base(options)
        {
            Database.Migrate();
        }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<SubAccount> SubAccounts { get; set; }
        public DbSet<PostingLine> PostingLines { get; set; }
    }
}

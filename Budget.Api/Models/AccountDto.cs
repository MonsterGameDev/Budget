using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Budget.Api.Models
{
    public class AccountDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public List<SubAccountDto> SubAccounts { get; set; }
            = new List<SubAccountDto>();
        public int NumberOfSubAccounts { get {
                return SubAccounts.Count();
            }
        }
        public double Total { get
            {
                // ToDo - get sum of all postinglines in database
                return 0;
            }
        }
    }
}

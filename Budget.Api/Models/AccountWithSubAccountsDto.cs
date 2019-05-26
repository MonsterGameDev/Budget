using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Budget.Api.Models
{
    public class AccountWithSubAccountsDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public List<SubAccountWithoutPostingLinesDto> SubAccounts { get; set; }
            = new List<SubAccountWithoutPostingLinesDto>();
        public int NumberOfSubAccounts { get {
                return SubAccounts.Count;
            }
        }

    }
}

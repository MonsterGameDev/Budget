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
        public ICollection<SubAccountDto> SubAccounts { get; set; }
            = new List<SubAccountDto>();
        public ICollection<PostingLineDto> PostingLines { get; set; }
            = new List<PostingLineDto>();
        public int NumberOfSubAccounts { get {
                return SubAccounts.Count;
            }
        }
        public int NumberOfPostingLines { get
            {
                return PostingLines.Count;
            }
        }
        public double Total
        {
            get
            {
                return PostingLines.Sum(pl => pl.Amount);
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Budget.Api.Models
{
    public class SubAccountDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public ICollection<PostingLineDto> PostingLines { get; set; }
            = new List<PostingLineDto>();
        public int NumberOfPostingLines { get {
                return PostingLines.Count;
            }
        }
        public double Total { get {
                return PostingLines.Sum(pl => pl.Amount);
            } }
    }
}

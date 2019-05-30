using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Budget.WebSite.Models
{
    public class SubAccount
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public string Name { get; set; }
        public List<PostingLine> PostingLines { get; set; }
            = new List<PostingLine>();
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Budget.WebSite.Models
{
    public class PostingLine
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public double Amount { get; set; }
        public string Location { get; set; }
        public DateTime Created { get; set; }
    }
}

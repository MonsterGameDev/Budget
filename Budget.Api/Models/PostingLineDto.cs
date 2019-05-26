using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Budget.Api.Models
{
    public class PostingLineDto
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public string Location { get; set; }
        public DateTime Created { get; set; }
        public Double Amount { get; set; }
    }
}

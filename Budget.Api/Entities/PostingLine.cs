using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Budget.Api.Entities
{
    public class PostingLine
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Description { get; set; }

        public decimal Amount { get; set; }

        public string Location { get; set; }

        public DateTime Created { get; set; }

        [ForeignKey("SubAccountId")]
        public SubAccount SubAccount { get; set; }
        public int SubAccountId { get; set; }
    }
}

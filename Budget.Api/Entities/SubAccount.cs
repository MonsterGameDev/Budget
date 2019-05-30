using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Budget.Api.Entities
{
    public class SubAccount
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        [MaxLength(200)]
        public string Description { get; set; }

        [ForeignKey("AccountId")]
        public Account Account { get; set; }

        public int AccountId { get; set; }

        public ICollection<PostingLine> PostingLines { get; set; } 
            = new List<PostingLine>();
    }
}

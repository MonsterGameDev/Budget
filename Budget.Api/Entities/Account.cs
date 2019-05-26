using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Budget.Api.Entities
{
    public class Account
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }
        [MaxLength(200)]
        public string Description { get; set; }
        public ICollection<SubAccount> SubAccounts { get; set; } = 
            new List<SubAccount>();

    }
}

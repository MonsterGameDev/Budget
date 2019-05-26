using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Budget.Api.Models
{
    public class PostingLineForEditingDto
    {
        [Required(ErrorMessage="Du skal angive Description")]
        [MaxLength(50)]
        public string Description { get; set; }
        public string Location { get; set; }
        [Required(ErrorMessage = "Du skal angive et beløb")]
        public Double Amount { get; set; }
    }
}

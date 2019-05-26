using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Budget.Api.Models
{
    public class SubAccountForEditingDto
    {   
        [Required(ErrorMessage ="Du skal angive navn")]
        [MaxLength(50)]
        public string Name { get; set; }
        [MaxLength(200)]
        public string Description { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce.Models.ViewModel
{
    public class EditGenderModel
    {
        [Required]
        public int GenderId { get; set; }
        [Required]
        public string Gender { get; set; }
    }
}

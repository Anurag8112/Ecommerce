using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce.Models.ViewModel
{
    public class EditColorModel
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string Color { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce.Models.ViewModel
{
    public class EditSizeModel
    {
        [Required]
        public int SizeId { get; set; }
        [Required]
        public string Size { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce.Models.ViewModel
{
    public class DeleteDpHub
    {
        [Required]
        public int DpHubId { get; set; }
    }
}

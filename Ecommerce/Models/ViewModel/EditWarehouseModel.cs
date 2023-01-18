using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce.Models.ViewModel
{
    public class EditWarehouseModel
    {
        [Required]
        public int WarehouseId { get; set; }
        [Required]
        public string WarehouseName { get; set; }
    }
}

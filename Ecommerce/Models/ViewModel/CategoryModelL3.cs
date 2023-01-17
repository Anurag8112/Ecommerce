using System.ComponentModel.DataAnnotations;

namespace Ecommerce.Models.ViewModel
{
    public class CategoryModelL3
    {
        [Required]
        public int CategoryL2Id { get; set; }
        [Required]
        public string CategoryL3Name { get; set; }
    }
}

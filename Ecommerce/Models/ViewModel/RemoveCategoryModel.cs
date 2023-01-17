using System.ComponentModel.DataAnnotations;

namespace Ecommerce.Models.ViewModel
{
    public class RemoveCategoryModel
    {
        [Required]
        public int Id { get; set; }
    }
}

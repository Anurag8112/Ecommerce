using System.ComponentModel.DataAnnotations;

namespace Ecommerce.Models.ViewModel
{
    public class BrandModel
    {
        [Required]
        public string BrandName { get; set; }
    }

    public class RemoveBrandModel
    {
        [Required]
        public int Id { get; set; }
    }

    public class ShowBrands
    {

        public int BrandId { get; set; }
        public string BrandName { get; set; }
        public int ItemCount { get; set; }
    }
}

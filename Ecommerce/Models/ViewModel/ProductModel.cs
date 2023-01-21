using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Ecommerce.Models.ViewModel
{
    public class ProductModel
    {
        [Required]
        public int UserId { get; set; }
        [Required]
        public string ProductName { get; set; }
        [Required]
        public string ProductDescription { get; set; }
        [Required]
        public int CategoryLevel1Id { get; set; }
        [Required]
        public int CategoryLevel2Id { get; set; }
        [Required]
        public int CategoryLevel3Id { get; set; }
        [Required]
        public int BrandId { get; set; }
        [Required]
        public List<string> Images { get; set; }
        [Required]
        public int Price { get; set; }
        [Required]
        public int SizeId { get; set; }
        [Required]
        public int ColorId { get; set; }
        [Required]
        public int WarehouseId { get; set; }
        [Required]
        public int TotalStock { get; set; }
    }
}

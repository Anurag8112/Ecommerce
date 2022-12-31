using Ecommerce.Models.DbModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce.Models.ViewModel
{
    public class ProductModel
    {
        public string ProductName { get; set; }
        public string ProductDescription { get; set; }
        public CategoryLevel1 CategoryLevel1 { get; set; }
        public CategoryLevel2 CategoryLevel2 { get; set; }
        public CategoryLevel3 CategoryLevel3 { get; set; }
        public Brand BrandName { get; set; }
        public ProductImage Images{get;set;}
        public int Price { get; set; }
        public Size Size { get; set; }
        public Color Color { get; set; }
    }
}

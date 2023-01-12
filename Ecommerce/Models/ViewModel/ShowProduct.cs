using Ecommerce.Models.DbModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce.Models.ViewModel
{
    public class ShowProduct
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public ProductData productData {get;set;}
    }

    public class ProductData
    {
        public string productName { get; set; }
        public string productDesc { get; set; }
        public ProdDetail productDetail { get; set; }
        public ProductImages productImage { get; set; }
        public Brands brand { get; set; }
        public CategoryL1 categoryL1 { get; set; }
        public CategoryL2 categoryL2 { get; set; }
        public CategoryL3 categoryL3 { get; set; }
    }

    public class ProdDetail
    {
        public int price { get; set; }
        public ProductSizes productSize { get; set; }
        public ProductColors productColor { get; set; }
    }

    public class ProductImages
    {
        public List<string> image { get; set; }
    }

    public class Brands
    {
        public string brandName { get; set; }
    }

    public class CategoryL1 
    {
        public string categoryL1 { get; set; }
    }

    public class CategoryL2 
    {
        public string categoryL2 { get; set; }
    }

    public class CategoryL3 
    {
        public string categoryL3 { get; set; }
    }

    public class ProductSizes
    {
        public string sizeName { get; set; }
    }

    public class ProductColors
    {
        public string colorName { get; set; }
    }

}

using Ecommerce.Models.DbModel;
using Ecommerce.Models.ViewModel;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace Ecommerce.Interface
{
    public interface IProductRepository
    {
        public bool AddProduct(ProductModel product);
        public string DeleteProduct(int UserId, int ProdId);
        public string UpdateProduct(int UserId, ProductModel product);
        public List<ShowProduct> ShowAllProducts();
        public List<ShowProduct> ShowMyProducts(int id);
    }
}

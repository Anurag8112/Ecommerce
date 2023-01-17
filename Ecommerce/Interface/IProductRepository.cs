using Ecommerce.Models.ViewModel;
using System.Collections.Generic;

namespace Ecommerce.Interface
{
    public interface IProductRepository
    {
        public bool AddProduct(ProductModel product);
        public bool DeleteProduct(DeleteProductModel model);
        public string UpdateProduct(int UserId, ProductModel product);
        public List<ShowProduct> ShowAllProducts();
        public List<ShowProduct> ShowMyProducts(int id);
    }
}

using Ecommerce.Interface;
using Ecommerce.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce.Repository
{
    public class ProductRepository : IProductRepository
    {
        public string AddProduct(int UserId, ProductModel product)
        {
            throw new NotImplementedException();
        }

        public List<ProductModel> ShowAllProducts(int UserId)
        {
            throw new NotImplementedException();
        }

        public string DeleteProduct(int UserId, int ProdId)
        {
            throw new NotImplementedException();
        }

        public string UpdateProduct(int UserId, ProductModel product)
        {
            throw new NotImplementedException();
        }
    }
}

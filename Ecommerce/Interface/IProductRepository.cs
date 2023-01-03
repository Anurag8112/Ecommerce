﻿using Ecommerce.Models.ViewModel;
using System.Collections.Generic;

namespace Ecommerce.Interface
{
    public interface IProductRepository
    {
        public string AddProduct(int UserId, ProductModel product);
        public string DeleteProduct(int UserId, int ProdId);
        public string UpdateProduct(int UserId, ProductModel product);
        public List<ProductModel> ShowAllProducts(int UserId);
    }
}

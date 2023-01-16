using Ecommerce.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce.Interface
{
    public interface IBrandRepository
    {
        public bool AddBrands(BrandModel model);
        public bool RemoveBrand(RemoveBrandModel model);
        public List<ShowBrands> ShowAllBrands();
    }
}

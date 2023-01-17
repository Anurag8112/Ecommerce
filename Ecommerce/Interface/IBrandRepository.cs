using Ecommerce.Models.ViewModel;
using System.Collections.Generic;

namespace Ecommerce.Interface
{
    public interface IBrandRepository
    {
        public bool AddBrands(BrandModel model);
        public bool RemoveBrand(RemoveBrandModel model);
        public List<ShowBrands> ShowAllBrands();
    }
}

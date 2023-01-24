using Ecommerce.Interface;
using Ecommerce.Models.DbModel;
using Ecommerce.Models.ViewModel;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Ecommerce.Repository
{
    public class BrandRepository : IBrandRepository
    {
        private readonly ILogger<BrandRepository> _logger;
        public BrandRepository(IBrandRepository brandRepository, ILogger<BrandRepository> logger)
        {
            _logger = logger;
        }
        public bool AddBrands(BrandModel model)
        {
            try
            {
                EcommerceContext db = new EcommerceContext();

                var brand = new Brand()
                {
                    BrandName = model.BrandName
                };

                db.Brands.Add(brand);
                db.SaveChanges();
                _logger.LogInformation("---------------Brand Added---------------");
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex.ToString());
                throw new Exception(ex.Message);
            }
        }

        public bool RemoveBrand(RemoveBrandModel model)
        {
            try
            {
                EcommerceContext db = new EcommerceContext();

                var isValidId = db.Brands.FirstOrDefault(x => x.Id == model.Id);

                if (isValidId == null)
                {
                    _logger.LogInformation("---------------Invalid BrandId---------------");
                    throw new Exception("Invalid BrandId");
                }

                var brandCount = db.Products.Count(x => x.BrandId == model.Id);

                if (brandCount == 0)
                {
                    var DeleteBrand = db.Brands.FirstOrDefault(x => x.Id == model.Id);

                    db.Brands.Remove(DeleteBrand);
                    db.SaveChanges();
                    return true;
                }
                else
                {
                    throw new Exception("There Is Many Product With This Brand. Please Remove All Products With This Brand Before Deleting This Brand");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<ShowBrands> ShowAllBrands()
        {
            try
            {
                EcommerceContext db = new EcommerceContext();

                List<ShowBrands> BrandList = new List<ShowBrands>();

                var AllBrand = db.Brands.Select(x => new ShowBrands { BrandId = x.Id, BrandName = x.BrandName });

                foreach (var brands in AllBrand)
                {
                    BrandList.Add(brands);
                }

                foreach (var brands in BrandList)
                {
                    brands.ItemCount = db.Products.Count(x => brands.BrandId == x.BrandId);
                }

                return BrandList;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}

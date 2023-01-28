using Ecommerce.Interface;
using Ecommerce.Models.DbModel;
using Ecommerce.Models.ViewModel;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Ecommerce.Repository
{
    public class SizeRepository : ISizeRepository
    {
        private readonly ILogger<SizeRepository> logger;

        public SizeRepository(ILogger<SizeRepository> logger)
        {
            this.logger = logger;
        }

        public bool AddSize(AddSizeModel model)
        {
            try
            {
                EcommerceContext db = new EcommerceContext();
                var IsExist = db.Sizes.FirstOrDefault(x => x.Size1 == model.Size);

                if (IsExist != null)
                {
                    logger.LogError("-----Size Already Exist-----");
                    throw new Exception("Size Already Exist");
                }

                logger.LogInformation("-----DB Connection Established-----");
                var Size = new Size()
                {
                    Size1 = model.Size
                };

                db.Sizes.Add(Size);
                db.SaveChanges();
                logger.LogInformation("-----Size Added To DB Successfully------");
                return true;
            }
            catch (Exception ex)
            {
                logger.LogError(ex.InnerException.ToString());
                throw new Exception(ex.InnerException.ToString());
            }
        }

        public bool DeleteSize(DeleteSizeModel model)
        {
            try
            {
                EcommerceContext db = new EcommerceContext();
                var Size = db.Sizes.FirstOrDefault(x => x.Id == model.SizeId);

                if (Size == null)
                {
                    logger.LogError("-----Size Doesn't Exist-----");
                    throw new Exception("Size Doesn't Exist");
                }

                db.Sizes.Remove(Size);
                db.SaveChanges();

                logger.LogInformation("-----Size Deleted Successfully-----");
                return true;
            }
            catch (Exception ex)
            {
                logger.LogError(ex.InnerException.ToString());
                throw new Exception(ex.InnerException.ToString());
            }
        }

        public bool EditSize(EditSizeModel model)
        {
            try
            {
                EcommerceContext db = new EcommerceContext();
                var Size = db.Sizes.FirstOrDefault(x => x.Id == model.SizeId);
                
                if (Size == null)
                {
                    logger.LogError("-----Invalid Size Id-----");
                    throw new Exception("Invalid Size Id");
                }
                Size.Size1 = model.Size;

                db.Sizes.Update(Size);
                db.SaveChanges();
                logger.LogInformation("Size Edited Successfully");
                return true;
            }
            catch (Exception ex)
            {
                logger.LogError(ex.InnerException.ToString());
                throw new Exception(ex.InnerException.ToString());
            }
        }

        public List<ShowSizeModel> ShowAllSizes()
        {
            try
            {
                EcommerceContext db = new EcommerceContext();
                List<ShowSizeModel> SizeList = new List<ShowSizeModel>();

                foreach (var size in db.Sizes)
                {
                    var Size = new ShowSizeModel()
                    {
                        Id = size.Id,
                        Size = size.Size1
                    };
                    SizeList.Add(Size);
                }

                logger.LogInformation("-----Size Added to Size List-----");
                return SizeList;
            }
            catch (Exception ex)
            {
                logger.LogError(ex.InnerException.ToString());
                throw new Exception(ex.InnerException.ToString());
            }
        }
    }
}

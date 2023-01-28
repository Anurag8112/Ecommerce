using Ecommerce.Interface;
using Ecommerce.Models.DbModel;
using Ecommerce.Models.ViewModel;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;

namespace Ecommerce.Repository
{
    public class ColorRepository : IColorRepository
    {
        private readonly ILogger<ColorRepository> logger;

        public ColorRepository(ILogger<ColorRepository> logger)
        {
            this.logger = logger;
        }


        public bool AddColor(AddColorModel model)
        {
            try
            {
                EcommerceContext db = new EcommerceContext();

                var color = new Color()
                {
                    Color1 = model.Color
                };

                db.Colors.Add(color);
                db.SaveChanges();
                logger.LogInformation("-----Color Added to DB-----");
                return true;
            }
            catch (Exception ex)
            {
                logger.LogError(ex.InnerException.ToString());
                throw new Exception(ex.InnerException.ToString());
            }
        }

        public bool DeleteColor(DeleteColorModel model)
        {
            throw new NotImplementedException();
        }

        public bool EditColor(EditColorModel model)
        {
            throw new NotImplementedException();
        }

        public List<ShowAllColor> ShowAllColor()
        {
            throw new NotImplementedException();
        }
    }
}

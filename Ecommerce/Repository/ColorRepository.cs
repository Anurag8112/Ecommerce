using Ecommerce.Interface;
using Ecommerce.Models.DbModel;
using Ecommerce.Models.ViewModel;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;

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
            try
            {
                EcommerceContext db = new EcommerceContext();
                var Color = db.Colors.FirstOrDefault(x => x.Id == model.Id);

                if (Color == null)
                {
                    logger.LogInformation("-----Invalid Color Id-----");
                    throw new Exception("Invalid Color Id");
                }
                db.Colors.Remove(Color);
                db.SaveChanges();
                logger.LogInformation("-----Color Deleted-----");
                return true;
            }
            catch (Exception ex)
            {
                logger.LogError(ex.InnerException.ToString());
                throw new Exception(ex.InnerException.ToString());
            }
        }

        public bool EditColor(EditColorModel model)
        {
            try
            {
                EcommerceContext db = new EcommerceContext();
                var Color = db.Colors.FirstOrDefault(x => x.Id == model.Id);

                if (Color == null)
                {
                    logger.LogError("------Invalid Color Id-------");
                    throw new Exception("Invalid Color Id");
                }

                Color.Color1 = model.Color;

                db.Colors.Update(Color);
                db.SaveChanges();
                logger.LogInformation("-----Color Edited Successfully-----");
                return true;
            }
            catch (Exception ex)
            {
                logger.LogError(ex.InnerException.ToString());
                throw new Exception(ex.InnerException.ToString());
            }
        }

        public List<ShowAllColor> ShowAllColor()
        {
            try
            {
                EcommerceContext db = new EcommerceContext();
                List<ShowAllColor> ColorList = new List<ShowAllColor>();

                foreach (var color in db.Colors)
                {
                    var colors = new ShowAllColor()
                    {
                        Id = color.Id,
                        Color = color.Color1
                    };

                    ColorList.Add(colors);
                }

                logger.LogInformation("-----Color Added to List-----");
                return ColorList;
            }
            catch (Exception ex)
            {
                logger.LogError(ex.InnerException.ToString());
                throw new Exception(ex.InnerException.ToString());
            }
        }
    }
}

using Ecommerce.Interface;
using Ecommerce.Models.DbModel;
using Ecommerce.Models.ViewModel;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Ecommerce.Repository
{
    public class DeliveryPartnerRepository : IDeliveryPartnerRepository
    {
        private readonly ILogger<DeliveryPartnerRepository> _logger;

        public DeliveryPartnerRepository(ILogger<DeliveryPartnerRepository> logger)
        {
            _logger = logger;
        }

        public bool AddDeliveryPartners(DeliveryPartnerModel model)
        {
            try
            {
                EcommerceContext db = new EcommerceContext();
                _logger.LogInformation("-----------DB Connection Established----------");
                var DeliveryPartner = new DeliveryPartner()
                {
                    DeliveryPartnerName = model.DeliveryPartnerName
                };
                db.DeliveryPartners.Add(DeliveryPartner);
                db.SaveChanges();
                _logger.LogInformation("-------------Delivery Partner Added----------");
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.InnerException.ToString());
                throw new Exception(ex.Message);
            }
        }

        public bool DeleteDeliveryPartner(DeleteDeliveryPartnerModel model)
        {
            try
            {
                EcommerceContext db = new EcommerceContext();
                _logger.LogInformation("--------------DB Connection Established--------------");
                var DeliveryPartner = db.DeliveryPartners.FirstOrDefault(x => x.Id == model.DeliveryPartnerId);
                if (DeliveryPartner == null)
                {
                    _logger.LogError("----------------Invalid Delivery Partner Id----------------");
                    throw new Exception("Invalid Delivery Partner Id");
                }
                else
                {
                    db.DeliveryPartners.Remove(DeliveryPartner);
                    db.SaveChanges();
                    _logger.LogInformation("----------Delivery Partner Removed------------");
                    return true;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.InnerException.ToString());
                throw new Exception(ex.Message);
            }
        }

        public bool EditDeliveryPartnerName(EditDeliveryPartnerModel model)
        {
            try
            {
                EcommerceContext db = new EcommerceContext();
                _logger.LogInformation("----------DB Connection Established--------------");
                var DeliveryPartner = db.DeliveryPartners.FirstOrDefault(x => x.Id == model.DeliveryPartnerId);
                if (DeliveryPartner == null)
                {
                    _logger.LogError("-------------Invalid Delivery Partner Id-------------");
                    throw new Exception("Invalid Delivery Partner Id");
                }
                else
                {
                    DeliveryPartner.DeliveryPartnerName = model.DeliveryPartnerName;
                    db.DeliveryPartners.Update(DeliveryPartner);
                    db.SaveChanges();
                    _logger.LogInformation("-------------Delivery Partner Name Edited Successfully------------");
                    return true;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.InnerException.ToString());
                throw new Exception(ex.Message);
            }
        }

        public List<ShowDeliveryPartner> ShowDeliveryPartners()
        {
            try
            {
                EcommerceContext db = new EcommerceContext();
                _logger.LogInformation("------------DB Connection Established-------------");
                List<ShowDeliveryPartner> DeliveryPartnerList = new List<ShowDeliveryPartner>();
                foreach (var DeliveryPartner in db.DeliveryPartners)
                {
                    var DeliveryPartnerModel = new ShowDeliveryPartner()
                    {
                        DeliveryPartnerId = DeliveryPartner.Id,
                        DeliveryPartnerName = DeliveryPartner.DeliveryPartnerName
                    };
                    DeliveryPartnerList.Add(DeliveryPartnerModel);
                }
                return DeliveryPartnerList;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}

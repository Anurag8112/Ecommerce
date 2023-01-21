using Ecommerce.Interface;
using Ecommerce.Models.DbModel;
using Ecommerce.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce.Repository
{
    public class DeliveryPartnerRepository : IDeliveryPartnerRepository
    {
        public bool AddDeliveryPartners(DeliveryPartnerModel model)
        {
            try
            {
                EcommerceContext db = new EcommerceContext();
                var DeliveryPartner = new DeliveryPartner()
                {
                    DeliveryPartnerName=model.DeliveryPartnerName
                };
                db.DeliveryPartners.Add(DeliveryPartner);
                db.SaveChanges();
                return true;
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public bool DeleteDeliveryPartner(DeleteDeliveryPartnerModel model)
        {
            try
            {
                EcommerceContext db = new EcommerceContext();
                var DeliveryPartner = db.DeliveryPartners.FirstOrDefault(x => x.Id == model.DeliveryPartnerId);
                if (DeliveryPartner == null)
                {
                    throw new Exception("Invalid Delivery Partner Id");
                }
                else
                {
                    db.DeliveryPartners.Remove(DeliveryPartner);
                    db.SaveChanges();
                    return true;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public bool EditDeliveryPartnerName(EditDeliveryPartnerModel model)
        {
            try
            {
                EcommerceContext db = new EcommerceContext();
                var DeliveryPartner = db.DeliveryPartners.FirstOrDefault(x => x.Id == model.DeliveryPartnerId);
                if (DeliveryPartner == null)
                {
                    throw new Exception("Invalid Delivery Partner Id");
                }
                else
                {
                    DeliveryPartner.DeliveryPartnerName = model.DeliveryPartnerName;
                    db.DeliveryPartners.Update(DeliveryPartner);
                    db.SaveChanges();
                    return true;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public List<ShowDeliveryPartner> ShowDeliveryPartners()
        {
            try
            {
                EcommerceContext db = new EcommerceContext();
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

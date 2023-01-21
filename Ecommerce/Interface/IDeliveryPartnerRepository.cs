using Ecommerce.Models.DbModel;
using Ecommerce.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce.Interface
{
    public interface IDeliveryPartnerRepository
    {
        public bool AddDeliveryPartners(DeliveryPartnerModel model);
        public bool DeleteDeliveryPartner(DeleteDeliveryPartnerModel model);
        public bool EditDeliveryPartnerName(EditDeliveryPartnerModel model);
        public List<ShowDeliveryPartner> ShowDeliveryPartners();
    }
}

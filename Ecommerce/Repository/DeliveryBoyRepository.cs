using Ecommerce.Interface;
using Ecommerce.Models.DbModel;
using Ecommerce.Models.ViewModel;
using System;
using System.Linq;

namespace Ecommerce.Repository
{
    public class DeliveryBoyRepository : IDeliveryBoyRepository
    {
        public bool AddDeliveryBoy(AddDeliveryBoyModel model)
        {
            try
            {
                EcommerceContext db = new EcommerceContext();
                var IsDeliveryBoy = db.UserRoleMappings.FirstOrDefault(x => x.UserId == model.UserId);
                if (IsDeliveryBoy == null)
                {
                    throw new Exception("Invalid UserId");
                }

                if (IsDeliveryBoy.Role.Role == "DeliveryBoy")
                {
                    var DeliveryBoy = new DeliveryBoy()
                    {
                        UserRoleMappingId = IsDeliveryBoy.Id,
                        AssignedHubId = model.AssignedHubId
                    };
                    db.DeliveryBoys.Add(DeliveryBoy);
                    db.SaveChanges();
                    return true;
                }
                else
                {
                    throw new Exception("This User Is Not a Delivery Boy");
                }
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool ChangeDeliveryHub()
        {
            throw new NotImplementedException();
        }

        public bool RemoveDeliveryBoy()
        {
            throw new NotImplementedException();
        }
    }
}

using Ecommerce.Interface;
using Ecommerce.Models.DbModel;
using Ecommerce.Models.ViewModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;

namespace Ecommerce.Repository
{
    public class DeliveryBoyRepository : IDeliveryBoyRepository
    {
        private readonly ILogger<DeliveryBoyRepository> _logger;

        public DeliveryBoyRepository(ILogger<DeliveryBoyRepository> logger)
        {
            _logger = logger;
        }

        public bool AddDeliveryBoy(AddDeliveryBoyModel model)
        {
            try
            {
                EcommerceContext db = new EcommerceContext();
                _logger.LogInformation("------------DB Connection Established--------------");
                var IsDeliveryBoy = db.UserRoleMappings.Include(x => x.Role).FirstOrDefault(x => x.UserId == model.UserId);
                if (IsDeliveryBoy == null)
                {
                    _logger.LogError("----------Invalid UserId-----------");
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
                    _logger.LogInformation("-------------Delivery Boy Added-------------");
                    return true;
                }
                else
                {
                    _logger.LogError("---------------Invalid Role------------");
                    throw new Exception("This User Is Not a Delivery Boy");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.InnerException.ToString());
                throw new Exception(ex.Message);
            }
        }

        public bool ChangeDeliveryHub(ChangeDeliveryHubModel model)
        {
            try
            {
                EcommerceContext db = new EcommerceContext();
                var userRoleMapping = db.UserRoleMappings.Include(x => x.Role).FirstOrDefault(x => x.UserId == model.UserId && x.RoleId == 5);
                var DeliveryBoy = db.DeliveryBoys.FirstOrDefault(x => x.UserRoleMappingId == userRoleMapping.Id);

                DeliveryBoy.AssignedHubId = model.DPHubId;

                db.DeliveryBoys.Update(DeliveryBoy);
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.InnerException.ToString());
            }
        }

        public bool RemoveDeliveryBoy(int UserId)
        {
            try
            {
                EcommerceContext db = new EcommerceContext();
                var UserRoleMapping = db.UserRoleMappings.FirstOrDefault(x => x.UserId == UserId);
                var DeliveryBoy = db.DeliveryBoys.FirstOrDefault(x => x.UserRoleMappingId == UserRoleMapping.Id);

                db.DeliveryBoys.Remove(DeliveryBoy);
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.InnerException.ToString());
            }
        }
    }
}

using Ecommerce.Interface;
using Ecommerce.Models.DbModel;
using Ecommerce.Models.ViewModel;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Ecommerce.Repository
{
    public class RoleRepository : IRoleRepository
    {
        private readonly ILogger<RoleRepository> _logger;

        public RoleRepository(ILogger<RoleRepository> logger)
        {
            _logger = logger;
        }

        public bool AddRole(RoleModel model)
        {
            try
            {
                EcommerceContext db = new EcommerceContext();
                _logger.LogInformation("--------DB COnnection Established--------");
                var Role = new UserRole()
                {
                    Role = model.Role
                };

                db.UserRoles.Add(Role);
                db.SaveChanges();
                _logger.LogInformation("---------Role Added---------");
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.InnerException.ToString());
                throw new Exception(ex.Message);
            }
        }

        public bool DeleteRole(DeleteRoleModel model)
        {
            try
            {
                EcommerceContext db = new EcommerceContext();
                _logger.LogInformation("----------DB Connection Established---------");
                var isValidId = db.UserRoles.FirstOrDefault(x => x.Id == model.RoleId);

                if (isValidId == null)
                {
                    _logger.LogError("-------Invalid Role Id-------");
                    throw new Exception("Invalid RoleId");
                }

                var userCount = db.UserRoleMappings.Count(x => x.RoleId == model.RoleId);

                if (userCount == 0)
                {
                    var DeleteRole = db.UserRoles.FirstOrDefault(x => x.Id == model.RoleId);

                    db.UserRoles.Remove(DeleteRole);
                    db.SaveChanges();
                    _logger.LogInformation("----------Role Removed--------");
                    return true;
                }
                else
                {
                    _logger.LogInformation("----------------Role Can not be Deleted--------");
                    throw new Exception("There Is Many User With This Role. Please Remove All User With This Role Before Deleting This Role");
                }

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.InnerException.ToString();
                throw new Exception(ex.Message);
            }
        }

        public List<ShowRoles> GetAllRoles()
        {
            try
            {
                EcommerceContext db = new EcommerceContext();
                _logger.LogInformation("--------DB Connection Established---------");
                List<ShowRoles> RoleList = new List<ShowRoles>();

                var AllRoles = db.UserRoles.Select(x => new ShowRoles { RoleId = x.Id, Role = x.Role });

                foreach (var Role in AllRoles)
                {
                    RoleList.Add(Role);
                }

                foreach (var Role in RoleList)
                {
                    Role.UserCount = db.UserRoleMappings.Count(x => Role.RoleId == x.RoleId);
                }

                _logger.LogInformation("---------Roles Retrieved---------");
                return RoleList;

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.InnerException.ToString());
                throw new Exception(ex.Message);
            }
        }
    }
}

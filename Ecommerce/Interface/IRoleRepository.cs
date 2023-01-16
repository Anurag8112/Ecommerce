using Ecommerce.Models.DbModel;
using Ecommerce.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce.Interface
{
    public interface IRoleRepository
    {
        public bool AddRole(RoleModel model);

        public bool DeleteRole(DeleteRoleModel model);

        public List<ShowRoles> GetAllRoles();
    }
}

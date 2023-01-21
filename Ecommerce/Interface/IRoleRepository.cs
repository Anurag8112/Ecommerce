using Ecommerce.Models.ViewModel;
using System.Collections.Generic;

namespace Ecommerce.Interface
{
    public interface IRoleRepository
    {
        public bool AddRole(RoleModel model);
        public bool DeleteRole(DeleteRoleModel model);
        public List<ShowRoles> GetAllRoles();
    }
}

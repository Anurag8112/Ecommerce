using Microsoft.AspNetCore.Authorization;


namespace Ecommerce.Services
{
    public class AppRoleProvider : AuthorizeAttribute
    {

        public AppRoleProvider(params string[] roles) : base()
        {
            Roles = string.Join(",", roles);
        }

    }
}

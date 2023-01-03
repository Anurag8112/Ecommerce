using Ecommerce.Encript_Decrypt;
using Ecommerce.Models.DbModel;
using Ecommerce.Models.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace Ecommerce.Services
{
    public class AppRoleProvider : AuthorizeAttribute
    {

        public AppRoleProvider(params string[] roles) : base()
        {
            Roles = string.Join(",",roles);
        }

    }
}

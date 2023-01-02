using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce.Interface
{
    public interface IRefreshTokenGenerator
    {
        public string GenerateToken();
    }
}

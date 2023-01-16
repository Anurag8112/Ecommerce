using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce.Interface
{
    public interface IDeliveryBoyRepository
    {
        public bool AddDeliveryBoy();
        public bool RemoveDeliveryBoy();
        public bool ChangeDeliveryHub();
    }
}

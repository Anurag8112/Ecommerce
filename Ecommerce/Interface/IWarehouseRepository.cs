using Ecommerce.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce.Interface
{
    public interface IWarehouseRepository
    {
        public bool AddWarehouse(WarehouseModel model);
        public bool EditWarehouseName(EditWarehouseModel model);
        public bool DeleteWarehouse(DeleteWarehouseModel model);
        public List<ShowWarehouse> GetAllWarehouse();
    }
}

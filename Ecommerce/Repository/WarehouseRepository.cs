using Ecommerce.Interface;
using Ecommerce.Models.DbModel;
using Ecommerce.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Ecommerce.Repository
{
    public class WarehouseRepository : IWarehouseRepository
    {
        public bool AddWarehouse(WarehouseModel model)
        {
            try
            {
                EcommerceContext db = new EcommerceContext();
                var IsWarehouseExist = db.Warehouses.FirstOrDefault(x => x.WarehouseName == model.WarehouseName);

                if (IsWarehouseExist != null)
                {
                    throw new Exception("Warehouse Already Exist");
                }

                var warehouse = new Warehouse()
                {
                    WarehouseName = model.WarehouseName
                };

                db.Warehouses.Add(warehouse);
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public bool EditWarehouseName(EditWarehouseModel model)
        {
            try
            {
                EcommerceContext db = new EcommerceContext();
                var warehouse = db.Warehouses.FirstOrDefault(x => x.Id == model.WarehouseId);
                if (warehouse == null)
                {
                    throw new Exception("Invalid Warehouse Id");
                }
                warehouse.WarehouseName = model.WarehouseName;

                db.Warehouses.Update(warehouse);
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public bool DeleteWarehouse(DeleteWarehouseModel model)
        {
            try
            {
                EcommerceContext db = new EcommerceContext();
                var warehouse = db.Warehouses.FirstOrDefault(x => x.Id == model.WarehouseId);
                if (warehouse == null)
                {
                    throw new Exception("Warehouse is not Exist");
                }
                else
                {
                    db.Warehouses.Remove(warehouse);
                    db.SaveChanges();
                    return true;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public List<ShowWarehouse> GetAllWarehouse()
        {
            try
            {
                EcommerceContext db = new EcommerceContext();
                List<ShowWarehouse> WarehouseList = new List<ShowWarehouse>();
                foreach (var warehouse in db.Warehouses)
                {
                    var newWarehouse = new ShowWarehouse()
                    {
                        WarehouseId = warehouse.Id,
                        WarehouseName = warehouse.WarehouseName
                    };
                    WarehouseList.Add(newWarehouse);
                }
                if (WarehouseList != null)
                {
                    return WarehouseList;
                }
                else
                {
                    throw new Exception("There is no Warehouse");
                } 
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}

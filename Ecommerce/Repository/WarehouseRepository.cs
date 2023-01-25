using Ecommerce.Interface;
using Ecommerce.Models.DbModel;
using Ecommerce.Models.ViewModel;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Ecommerce.Repository
{
    public class WarehouseRepository : IWarehouseRepository
    {
        private readonly ILogger<WarehouseRepository> _logger;
        public WarehouseRepository(ILogger<WarehouseRepository> logger)
        {
            _logger = logger;
        }

        public bool AddWarehouse(WarehouseModel model)
        {
            try
            {
                EcommerceContext db = new EcommerceContext();
                _logger.LogInformation("-----DB Connetion Established-----");
                var IsWarehouseExist = db.Warehouses.FirstOrDefault(x => x.WarehouseName == model.WarehouseName);

                if (IsWarehouseExist != null)
                {
                    _logger.LogError("-----Warehouse Already Esist-----");
                    throw new Exception("Warehouse Already Exist");
                }

                var warehouse = new Warehouse()
                {
                    WarehouseName = model.WarehouseName
                };

                db.Warehouses.Add(warehouse);
                db.SaveChanges();
                _logger.LogInformation("-----Warehouse Added-----");
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.InnerException.ToString());
                throw new Exception(ex.Message);
            }
        }
        public bool EditWarehouseName(EditWarehouseModel model)
        {
            try
            {
                EcommerceContext db = new EcommerceContext();
                _logger.LogInformation("-----DB Connection Established-----");
                var warehouse = db.Warehouses.FirstOrDefault(x => x.Id == model.WarehouseId);
                if (warehouse == null)
                {
                    _logger.LogError("-----Invalid Warehouse Id-----");
                    throw new Exception("Invalid Warehouse Id");
                }
                warehouse.WarehouseName = model.WarehouseName;

                db.Warehouses.Update(warehouse);
                db.SaveChanges();
                _logger.LogInformation("-----Warehouse Edited Succesfully-----");
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.InnerException.ToString());
                throw new Exception(ex.Message);
            }
        }
        public bool DeleteWarehouse(DeleteWarehouseModel model)
        {
            try
            {
                EcommerceContext db = new EcommerceContext();
                _logger.LogInformation("-----Db Connection Established-----");
                var warehouse = db.Warehouses.FirstOrDefault(x => x.Id == model.WarehouseId);
                if (warehouse == null)
                {
                    throw new Exception("Warehouse is not Exist");
                }
                else
                {
                    db.Warehouses.Remove(warehouse);
                    db.SaveChanges();
                    _logger.LogInformation("-----Warehouse Deleted-----");
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
                _logger.LogInformation("-----DB Connection Established-----");
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
                    _logger.LogInformation("-----Warehouse Retrieved-----");
                    return WarehouseList;
                }
                else
                {
                    _logger.LogError("-----There is no Warehouse-----");
                    throw new Exception("There is no Warehouse");
                } 
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.InnerException.ToString());
                throw new Exception(ex.Message);
            }
        }
    }
}

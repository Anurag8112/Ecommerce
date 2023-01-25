using Ecommerce.Interface;
using Ecommerce.Models.DbModel;
using Ecommerce.Models.ViewModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Ecommerce.Repository
{
    public class HubRepository : IHubRepository
    {
        private readonly ILogger<HubRepository> _logger;
        public HubRepository(ILogger<HubRepository> logger)
        {
            _logger = logger;
        }

        public bool AddDPHub(AddDpHubModel model)
        {
            try
            {
                EcommerceContext db = new EcommerceContext();
                _logger.LogInformation("---------------DB COnnection Established------------");
                var DpHub = new DpHub()
                {
                    DpId = model.DPId,
                    HubName = model.HubName,
                    DpHubAddress = new DpHubAddress()
                    {
                        AddressLine1 = model.HubAddress.AddressLine1,
                        AddressLine2 = model.HubAddress.AddressLine2,
                        City = model.HubAddress.City,
                        Country = model.HubAddress.Country,
                        State = model.HubAddress.State,
                        Phone = model.HubAddress.Phone,
                        PostalCode = model.HubAddress.PostalCode
                    }
                };
                db.DpHubs.Add(DpHub);
                db.SaveChanges();
                _logger.LogInformation("-------------DP Hub Added--------------");
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.InnerException.ToString());
                throw new Exception(ex.Message);
            }
        }
        public bool EditDpHub(EditDpHub model)
        {
            try
            {
                EcommerceContext db = new EcommerceContext();
                _logger.LogInformation("------------DB Connection Established----------");
                var DpHub = db.DpHubs.FirstOrDefault(x => x.Id == model.DpHubId);
                var DpHubAddress = db.DpHubAddresses.FirstOrDefault(x => x.DpHubId == model.DpHubId);

                DpHub.DpId = model.DeliveryPartnerId;
                DpHub.Id = model.DpHubId;
                DpHub.HubName = model.DpHubName;

                DpHubAddress.AddressLine1 = model.HubAddress.AddressLine1;
                DpHubAddress.AddressLine2 = model.HubAddress.AddressLine2;
                DpHubAddress.City = model.HubAddress.City;
                DpHubAddress.State = model.HubAddress.State;
                DpHubAddress.Country = model.HubAddress.Country;
                DpHubAddress.Phone = model.HubAddress.Phone;
                DpHubAddress.PostalCode = model.HubAddress.PostalCode;


                db.DpHubAddresses.Update(DpHubAddress);
                db.DpHubs.Update(DpHub);
                db.SaveChanges();
                _logger.LogInformation("----------DP Hub Edited Successfully-----------");
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.InnerException.ToString());
                throw new Exception(ex.Message);
            }
        }
        public bool RemoveDpHub(DeleteDpHub model)
        {
            try
            {
                EcommerceContext db = new EcommerceContext();
                var DpHub = db.DpHubs.FirstOrDefault(x => x.Id == model.DpHubId);
                if (DpHub == null)
                {
                    throw new Exception("Invalid Hub Id");
                }
                else
                {
                    var hubAddress = db.DpHubAddresses.FirstOrDefault(x => x.DpHubId == DpHub.Id);
                    db.DpHubAddresses.Remove(hubAddress);
                    db.DpHubs.Remove(DpHub);
                    db.SaveChanges();
                    return true;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public List<ShowDpHub> ShowDpHub()
        {
            try
            {
                EcommerceContext db = new EcommerceContext();
                _logger.LogInformation("-----------DB Connection established-------------");
                List<ShowDpHub> DpHubList = new List<ShowDpHub>();
                foreach (var hub in db.DpHubs.Include(x=>x.DpHubAddress))
                {
                    var ShowDpHub = new ShowDpHub()
                    {
                        DpHubId = hub.Id,
                        DpHubName = hub.HubName,
                        HubAddress=new ShowDpHubAddressModel()
                        {
                            Id=hub.DpHubAddress.Id,
                            AddressLine1=hub.DpHubAddress.AddressLine1,
                            AddressLine2=hub.DpHubAddress.AddressLine2,
                            City=hub.DpHubAddress.City,
                            State=hub.DpHubAddress.State,
                            Country=hub.DpHubAddress.Country,
                            Phone=hub.DpHubAddress.Phone,
                            PostalCode=hub.DpHubAddress.PostalCode,
                        }
                    };
                    DpHubList.Add(ShowDpHub);
                }
                _logger.LogInformation("-------------Product Added to list-------------");
                return DpHubList;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.InnerException.ToString());
                throw new Exception(ex.Message);
            }
        }
    }
}

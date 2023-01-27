using Ecommerce.Interface;
using Ecommerce.Models.DbModel;
using Ecommerce.Models.ViewModel;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce.Repository
{
    public class GenderRepository : IGenderRepository
    {
        private readonly ILogger<GenderRepository> logger;

        public GenderRepository(ILogger<GenderRepository> logger)
        {
            this.logger = logger;
        }

        public bool AddGender(AddGenderModel model)
        {
            try
            {
                EcommerceContext db = new EcommerceContext();
                logger.LogInformation("-----DB COnnection Established-----");
                var gender = new UserGender()
                {
                    Gender = model.Gender
                };

                db.UserGenders.Add(gender);
                db.SaveChanges();
                logger.LogInformation("-----API Responded Succesfully-----");
                return true;

            }catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool DeleteGender(DeleteGenderModel model)
        {
            try
            {
                EcommerceContext db = new EcommerceContext();
                logger.LogInformation("-----DB Connection Established-----");

                var Gender = db.UserGenders.FirstOrDefault(x => x.Id == model.Id);
                db.UserGenders.Remove(Gender);
                db.SaveChanges();

                logger.LogInformation("-----User Gender Deleted-----");
                return true;

            }catch(Exception ex)
            {
                throw new Exception(ex.InnerException.ToString());
            }
        }

        public bool EditGender(EditGenderModel model)
        {
            try
            {
                EcommerceContext db = new EcommerceContext();
                logger.LogInformation("-----DB Connection Established-----");

                var Gender = db.UserGenders.FirstOrDefault(x => x.Id == model.GenderId);
                Gender.Gender = model.Gender;

                db.UserGenders.Update(Gender);
                db.SaveChanges();

                logger.LogInformation("-----API Responded Successfully-----");
                return true;

            }catch(Exception ex)
            {
                logger.LogError(ex.InnerException.ToString());
                throw new Exception(ex.InnerException.ToString());
            }
        }

        public List<ShowGendersModel> ShowGenders()
        {
            try
            {
                EcommerceContext db = new EcommerceContext();
                logger.LogInformation("-----DB Connection Established-----");

                List<ShowGendersModel> GenderList = new List<ShowGendersModel>();
                
                foreach (var gender in db.UserGenders)
                {
                    var Genders = new ShowGendersModel()
                    {
                        Id = gender.Id,
                        Gender = gender.Gender
                    };
                    GenderList.Add(Genders);
                }

                logger.LogInformation("-----Gender Added to List-----");
                return GenderList;

            }catch(Exception ex)
            {
                logger.LogError(ex.InnerException.ToString());
                throw new Exception(ex.InnerException.ToString());
            }
        }
    }
}

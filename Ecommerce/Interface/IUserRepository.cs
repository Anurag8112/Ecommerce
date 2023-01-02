using Ecommerce.Models.DbModel;
using Ecommerce.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce.Interface
{
    public interface IUserRepository
    {
        public string UserSignUp(SignUpModel user);
        public UserGender GetGender(int id);
        public UserRole GetRole(int id);
        public bool VerifyUser(String userotp);
        public string ResendOtp(string number);
        public UserDetailsModel Login(LoginModel credentials);
        public string AddUserAddress(int userId,AddressModel userAddress);
        public List<UserDetailsModel> GetAllUsers();
      //  public string DeleteUser(int userId);
    }
}

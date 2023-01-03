using Ecommerce.Models.DbModel;
using Ecommerce.Models.ViewModel;
using System;
using System.Collections.Generic;

namespace Ecommerce.Interface
{
    public interface IUserRepository
    {
        public string UserSignUp(SignUpModel user);
        public UserGender GetGender(int id);
        public UserRole GetRole(int id);
        public bool VerifyUser(String userotp);
        public bool SendOtp(string number);
        public UserDetailsModel Login(LoginModel credentials);
        public string AddUserAddress(int userId, AddressModel userAddress);
        public List<UserDetailsModel> GetAllUsers();
        public UserDetailsModel GetUserById(int id);
        public List<AddressModel> GetUserAddresses(int id);
        public bool DeactivateUser(int id, string password);
    }
}

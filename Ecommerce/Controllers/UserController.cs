using Ecommerce.Interface;
using Ecommerce.Models.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce.Controllers
{
    [ApiController]
    [Route("api/v1")]
    public class UserController : Controller
    {
        private readonly IUserRepository _userRepository;

        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpPost]
        [Route("Sign-up")]
        public string SignUp(SignUpModel user)
        {
            string result;
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState).ToString();
                }

                result = _userRepository.UserSignUp(user);
            }
            catch (Exception ex)
            {
                return "Error occurred: " + ex.Message.ToString();
            }

            return result;
        }


        [HttpPost]
        [Route("Verify-User")]
        public bool VerifyUser(string otp)
        {
            var res = _userRepository.VerifyUser(otp);

            return res;
        }


        [HttpPost]
        [Route("Sign-in")]
        public UserDetailsModel Login(LoginModel credentials)
        {
            var details = _userRepository.Login(credentials);

            if (details == null)
            {
                details.ExceptionMessage = "Invalid UserName or Password";

                return details;
            }

            if (details.isVerified == false)
            {
                details.UserName = credentials.UserName;
                details.ExceptionMessage = "User Not Verified Please redirect to Verify API";

                return details; 
            }

            return details;
        }


        [HttpPost]
        [Route("Update-Address")]
        public string UpdateUserAddress(int userId, AddressModel userAddress)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState).ToString();
            }

            var result = _userRepository.AddUserAddress(userId, userAddress);

            return result;
        }

    }
}

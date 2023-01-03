using Ecommerce.Interface;
using Ecommerce.Models.ViewModel;
using Ecommerce.Repository;
using Ecommerce.Services;
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
        public IActionResult SignUp(SignUpModel user)
        {
            string result;
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                result = _userRepository.UserSignUp(user);
            }
            catch (Exception ex)
            {
                return BadRequest("Error occurred: " + ex.Message);
            }

            return Ok(result);
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
        public IActionResult Login(LoginModel credentials)
        {
            try
            {
                var details = _userRepository.Login(credentials);

                if (details == null)
                {
                    details.ExceptionMessage = "Invalid UserName or Password";

                    throw new Exception(details.ExceptionMessage);
                }

                if (details.IsActive == false)
                {
                    throw new Exception(details.ExceptionMessage);
                }

                if (details.isVerified == false)
                {
                    details.UserName = credentials.UserName;
                    details.ExceptionMessage = "User Not Verified Please redirect to Verify API";

                    throw new Exception(details.ExceptionMessage);
                }

                return Ok(details);
            }
            catch(Exception ex)
            {
                return BadRequest("Error occurred: " + ex.Message);
            }
        }


        [HttpPost]
        [Route("Update-Address")]
        [Authorize (Roles ="SuperAdmin")]
        public IActionResult UpdateUserAddress(int userId, AddressModel userAddress)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = _userRepository.AddUserAddress(userId, userAddress);

            return Ok(result);
        }


        [HttpGet]
        [Route("GetAllUsers")]
        [Authorize(Roles="SuperAdmin")]
        public IActionResult GetAllUsers()
        {
            try
            {
                var Result = _userRepository.GetAllUsers();
                return Ok(Result);

            }catch(Exception ex)
            {
                return BadRequest("Error occurred: " + ex.Message);
            }
        }

        [HttpGet]
        [Route("User/{id}")]
        [Authorize(Roles = "SuperAdmin")]
        public IActionResult GetUserById(int id)
        {
            try
            {
                var result = _userRepository.GetUserById(id);

                return Ok(result);

            }
            catch (Exception ex)
            {
                return BadRequest("Error occurred: " + ex.Message);
            }
        }


    }
}

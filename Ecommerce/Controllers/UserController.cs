using Ecommerce.Interface;
using Ecommerce.Models.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;

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


                if (details.isVerified == false)
                {
                    details.UserName = credentials.UserName;
                    details.ExceptionMessage = "User Not Verified Please redirect to Verify API";

                    return Ok(details.ExceptionMessage);
                }

                return Ok(details);
            }
            catch (Exception ex)
            {
                return BadRequest("Error occurred: " + ex.Message);
            }
        }



        [HttpPost]
        [Route("Update-Address")]
        [Authorize(Roles = "SuperAdmin")]
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
        [Authorize(Roles = "SuperAdmin")]
        public IActionResult GetAllUsers()
        {
            try
            {
                var Result = _userRepository.GetAllUsers();
                return Ok(Result);

            }
            catch (Exception ex)
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



        [HttpGet]
        [Route("User/{id}/Addresses")]
        [Authorize]
        public IActionResult GetUserAddresses(int id)
        {

            try
            {
                var Result = _userRepository.GetUserAddresses(id);
                return Ok(Result);

            }
            catch (Exception ex)
            {
                return BadRequest("Error occurred: " + ex.Message);
            }
        }



        [HttpDelete]
        [Route("User/{id}/Delete")]
        [Authorize]
        public IActionResult DeactivateUser(int id, string password)
        {
            try
            {
                var Result = _userRepository.DeactivateUser(id, password);
                return Ok(Result);
            }
            catch (Exception ex)
            {
                return BadRequest("Error occurred: " + ex.Message);
            }
        }


    }
}

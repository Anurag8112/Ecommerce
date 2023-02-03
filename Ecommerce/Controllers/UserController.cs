using Ecommerce.Interface;
using Ecommerce.Models.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;

namespace Ecommerce.Controllers
{
    [ApiController]
    [Route("api/v1")]
    public class UserController : Controller
    {
        private readonly IUserRepository _userRepository;
        private readonly ILogger<UserController> _logger;
        public UserController(IUserRepository userRepository, ILogger<UserController> logger)
        {
            _logger = logger;
            _userRepository = userRepository;
        }

        [HttpPost]
        [Route("Sign-up")]
        [Authorize]
        public IActionResult SignUp(SignUpModel user)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                var result = _userRepository.UserSignUp(user);
                _logger.LogInformation("-------------API Respond Successfully-------------");
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest("Error occurred: " + ex.Message);
            }
        }

        [HttpPost]
        [Route("Verify-User")]
        [Authorize]
        public IActionResult VerifyUser(OtpModel model)
        {
            var res = _userRepository.VerifyUser(model.Otp);
            _logger.LogInformation("-------------API Respond Successfully-------------");
            return Ok(res);
        }

        [HttpPost]
        [Route("ResendOTP")]
        [Authorize]
        public IActionResult ResendOTP(string MobileNo)
        {
            try
            {
                var Result = _userRepository.SendOtp(MobileNo);
                _logger.LogInformation("-------------API Respond Successfully-------------");
                return Ok(Result);

            }catch(Exception ex)
            {
                return BadRequest("Error occurred: " + ex.Message);
            }
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
                _logger.LogInformation("-------------API Respond Successfully-------------");
                return Ok(details);
            }
            catch (Exception ex)
            {
                return BadRequest("Error occurred: " + ex.Message);
            }
        }

        [HttpPost]
        [Route("Update-Address/{userId}")]
        [Authorize(Roles = "Buyer")]
        public IActionResult UpdateUserAddress(int userId, AddressModel userAddress)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = _userRepository.AddUserAddress(userId, userAddress);
            _logger.LogInformation("-------------API Respond Successfully-------------");
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
                _logger.LogInformation("-------------API Respond Successfully-------------");
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
                _logger.LogInformation("-------------API Respond Successfully-------------");
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
                _logger.LogInformation("-------------API Respond Successfully-------------");
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
                _logger.LogInformation("-------------API Respond Successfully-------------");
                return Ok(Result);
            }
            catch (Exception ex)
            {
                return BadRequest("Error occurred: " + ex.Message);
            }
        }
    }
}

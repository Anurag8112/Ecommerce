using Ecommerce.Encript_Decrypt;
using Ecommerce.Interface;
using Ecommerce.Models.DbModel;
using Ecommerce.Models.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Twilio.Clients;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Types;

namespace Ecommerce.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly IConfiguration _configuration;
        private readonly ITwilioRestClient _client;
        private readonly IHttpContextAccessor _context;

        public UserRepository( ITwilioRestClient client, IHttpContextAccessor context, IConfiguration configuration)
        {    
            _client = client;
            _context = context;
            _configuration = configuration;
        }

        protected string Generate_otp()
        {
            char[] charArr = "0123456789".ToCharArray();
            string strrandom = string.Empty;
            Random objran = new Random();
            for (int i = 0; i < 6; i++)
            {
                int pos = objran.Next(1, charArr.Length);
                if (!strrandom.Contains(charArr.GetValue(pos).ToString())) strrandom += charArr.GetValue(pos);
                else i--;
            }
            return strrandom;
        }

        public UserGender GetGender(int id)
        {
            try
            {
                EcommerceContext db = new EcommerceContext();

                var gender = db.UserGenders.FirstOrDefault(x=>x.Id==id);

                return gender;

            }catch(Exception)
            {
                return null;
            }  
        }
        
        public UserRole GetRole(int id)
        {
            try
            {
                EcommerceContext db = new EcommerceContext();

                var role = db.UserRoles.FirstOrDefault(x => x.Id == id);

                return role;

            }
            catch (Exception)
            {
                return null;
            }

        }

        public UserRole GetUserRole(int userId)
        {
            try
            {
                EcommerceContext db = new EcommerceContext();

                var role = db.UserRoleMappings.FirstOrDefault(x => x.UserId == userId);

                return GetRole(role.RoleId);

            }
            catch (Exception)
            {
                return null;
            }
        }

        public string UserSignUp(SignUpModel user)
        {
            try
            {
                EcommerceContext db = new EcommerceContext();

                var tempuser = new User()
                {
                    UserName = user.UserName,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    CountryCode = user.CountryCode,
                    Phone = user.Phone,
                    Email = user.Email,
                    Password = Password.HashEncrypt(user.Password),
                    GenderId = user.GenderId,
                    IsVerified = false
                };

                var tempRole = new UserRoleMapping()
                {
                    UserId = tempuser.Id,
                    RoleId = user.RoleId
                };

                string newnum = "+"+user.CountryCode + user.Phone;
                
                var sentotp=ResendOtp(newnum);

                tempuser.UserRoleMappings.Add(tempRole);
                db.Users.Add(tempuser);
                var response = db.SaveChanges();

                
                _context.HttpContext.Session.SetInt32("Id", tempuser.Id);

            }
            catch(Exception ex)
            {
                return "Error occurred: " + ex.Message.ToString();
            }

            return "User Created";
        }

        public bool VerifyUser(string userotp)
        {
            var otp = _context.HttpContext.Session.GetString("otp");
            var userId = _context.HttpContext.Session.GetInt32("Id");

            var dbcontext = new EcommerceContext();
            var res = dbcontext.Users.First(x => x.Id == userId);

            if (userId != null && userotp == otp)
            {

                res.IsVerified = true;
                dbcontext.Update(res);
                dbcontext.SaveChanges();
                return true;
            }

            return false;
        }

        public string ResendOtp(string number)
        {           
            if (number != null)
            {
                string otp = Generate_otp();
                var message = MessageResource.Create(
                           to: new PhoneNumber(number),
                           from: new PhoneNumber("+13854815314"),
                           body: otp,
                           client: _client
                    );
                _context.HttpContext.Session.SetString("otp", otp);

                return "Otp Send successfully";
            }
            return "Can not resend otp";
        }

        public UserDetailsModel Login(LoginModel credentials)
        {
            UserDetailsModel newuser = new UserDetailsModel();
            try
            {
                if (credentials != null && !string.IsNullOrWhiteSpace(credentials.UserName) && !string.IsNullOrWhiteSpace(credentials.Password))
                {
                    EcommerceContext db = new EcommerceContext();

                    var user = db.Users.First(x => x.UserName == credentials.UserName && x.Password == Password.HashEncrypt(credentials.Password));

                    if (user == null)
                    {
                        return newuser;
                    }

                    var jwt = _configuration.GetSection("Jwt").Get<Jwt>();

                    if (user != null)
                    {
                        var claims = new[]
                        {
                            new Claim(JwtRegisteredClaimNames.Sub,jwt.Subject),
                            new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()),
                            new Claim(JwtRegisteredClaimNames.Iat,DateTime.UtcNow.ToString()),
                            new Claim("Id",user.Id.ToString()),
                            new Claim("UserName",user.UserName),
                            new Claim("Password",user.Password)
                        };

                        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwt.key));

                        var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                        var token = new JwtSecurityToken(
                                jwt.Issuer,
                                jwt.Audience,
                                claims,
                                expires: DateTime.Now.AddMinutes(10),
                                signingCredentials: signIn
                                );


                        if (user.IsVerified == false)
                        {
                            string number = "+" + user.CountryCode + user.Phone;
                            ResendOtp(number);
                            _context.HttpContext.Session.SetInt32("Id", user.Id);
                            newuser.token = new JwtSecurityTokenHandler().WriteToken(token);
                            return newuser;
                        }

                        newuser = new UserDetailsModel() {
                            UserName = user.UserName,
                            FirstName = user.FirstName,
                            LastName = user.LastName,
                            Email = user.Email,
                            Phone = "+" + user.CountryCode + user.Phone,
                            Gender = GetGender(user.GenderId),
                            isVerified = user.IsVerified,
                            Role = GetUserRole(user.Id),
                            token = new JwtSecurityTokenHandler().WriteToken(token)
                        };
                    }

                    
                }
            }catch(Exception ex)
            {
                newuser.ExceptionMessage = ex.ToString();
                return newuser; 
            }

            return newuser;
        }

        public string AddUserAddress(int userId, AddressModel userAddress)
        {
            EcommerceContext db = new EcommerceContext();

            try
            {
                var user = db.Users.First(x => x.Id==userId);

                if (user != null)
                {
                    Address address = new Address() {
                        AddressLine1 = userAddress.AddressLine1,
                        AddressLine2 = userAddress.AddressLine2,
                        City = userAddress.City,
                        State = userAddress.State,
                        Country = userAddress.Country,
                        Postalcode = userAddress.Postalcode,
                        UserId = userId
                    };

                    user.Addresses.Add(address);
                    var response = db.SaveChanges();
                }
                
            }catch(Exception ex)
            {
                return ex.ToString();
            }

            return "Address Added Successfully";
        }
    }
}

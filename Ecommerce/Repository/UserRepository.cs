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
        private readonly IRefreshTokenGenerator _refreshTokenGenerator;

        public UserRepository(ITwilioRestClient client, IHttpContextAccessor context, IConfiguration configuration, IRefreshTokenGenerator refreshTokenGenerator)
        {
            _client = client;
            _context = context;
            _configuration = configuration;
            _refreshTokenGenerator = refreshTokenGenerator;
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
            EcommerceContext db = new EcommerceContext();
            try
            {
                var gender = db.UserGenders.FirstOrDefault(x => x.Id == id);

                return gender;

            }
            catch (Exception)
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
                    IsVerified = false,
                    IsActive = true
                };

                var tempRole = new UserRoleMapping()
                {
                    UserId = tempuser.Id,
                    RoleId = user.RoleId
                };

                string newnum = "+" + user.CountryCode + user.Phone;

                tempuser.UserRoleMappings.Add(tempRole);
                db.Users.Add(tempuser);



                var sentOtp = SendOtp(newnum);

                _context.HttpContext.Session.SetInt32("Id", tempuser.Id);
                var response = db.SaveChanges();
                return "User Created";

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message.ToString());
                //return "Error occurred: " + ex.Message.ToString();
            }

        }


        public bool VerifyUser(string userotp)
        {

            EcommerceContext db = new EcommerceContext();
            var otp = _context.HttpContext.Session.GetString("otp");
            var userId = _context.HttpContext.Session.GetInt32("Id");


            var res = db.Users.First(x => x.Id == userId);

            if (userId != null && userotp == otp)
            {

                res.IsVerified = true;
                db.Update(res);
                db.SaveChanges();
                return true;
            }

            return false;
        }


        public bool SendOtp(string number)
        {
            string otp = Generate_otp();
            var message = MessageResource.Create(
                       to: new PhoneNumber(number),
                       from: new PhoneNumber("+13854815314"),
                       body: otp,
                       client: _client
                );
            _context.HttpContext.Session.SetString("otp", otp);

            return true;
        }


        public UserDetailsModel Login(LoginModel credentials)
        {
            EcommerceContext db = new EcommerceContext();
            UserDetailsModel newuser = new UserDetailsModel();
            try
            {
                if (credentials != null && !string.IsNullOrWhiteSpace(credentials.UserName) && !string.IsNullOrWhiteSpace(credentials.Password))
                {


                    var user = db.Users.First(x => x.UserName == credentials.UserName && x.Password == Password.HashEncrypt(credentials.Password));

                    if (user.IsActive == false)
                    {
                        throw new Exception("Your account is Deactivated Please Reactivate Your Account");
                    }

                    if (user == null)
                    {
                        throw new Exception("Invalid UserName or Password");
                    }

                    var jwt = _configuration.GetSection("Jwt").Get<Jwt>();
                    var userRoleMapping = db.UserRoleMappings.First(x => x.UserId == user.Id);
                    var userRole = db.UserRoles.Where(x => x.Id == userRoleMapping.RoleId);



                    if (user != null)
                    {
                        var claims = new List<Claim>
                        {
                            new Claim(JwtRegisteredClaimNames.Sub,jwt.Subject),
                            new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()),
                            new Claim(JwtRegisteredClaimNames.Iat,DateTime.UtcNow.ToString()),
                            new Claim("Id",user.Id.ToString()),
                            new Claim("UserName",user.UserName),
                            new Claim("Password",user.Password),

                        };

                        foreach (var role in userRole)
                        {
                            claims.Add(new Claim(ClaimTypes.Role, role.Role));
                        }


                        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwt.key));

                        var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                        var token = new JwtSecurityToken(
                                jwt.Issuer,
                                jwt.Audience,
                                claims,
                                expires: DateTime.Now.AddMinutes(5),
                                signingCredentials: signIn
                                );


                        if (user.IsVerified == false)
                        {
                            string number = "+" + user.CountryCode + user.Phone;
                            SendOtp(number);
                            _context.HttpContext.Session.SetInt32("Id", user.Id);
                            newuser.Token = new JwtSecurityTokenHandler().WriteToken(token);
                            return newuser;
                        }

                        newuser = new UserDetailsModel()
                        {
                            UserName = user.UserName,
                            FirstName = user.FirstName,
                            LastName = user.LastName,
                            Email = user.Email,
                            Phone = "+" + user.CountryCode + user.Phone,
                            Gender = GetGender(user.GenderId),
                            isVerified = user.IsVerified,
                            Role = GetUserRole(user.Id),
                            Token = new JwtSecurityTokenHandler().WriteToken(token),
                            RefreshToken = _refreshTokenGenerator.GenerateToken()

                        };
                    }

                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return newuser;
        }


        public string AddUserAddress(int userId, AddressModel userAddress)
        {
            EcommerceContext db = new EcommerceContext();

            try
            {
                var user = db.Users.First(x => x.Id == userId);

                if (user != null)
                {
                    Address address = new Address()
                    {
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

            }
            catch (Exception ex)
            {
                return ex.ToString();
            }

            return "Address Added Successfully";
        }


        public List<UserDetailsModel> GetAllUsers()
        {

            EcommerceContext db = new EcommerceContext();
            List<UserDetailsModel> list = new List<UserDetailsModel>();
            try
            {
                foreach (User user in db.Users)
                {
                    var printuser = new UserDetailsModel()
                    {
                        UserName = user.UserName,
                        FirstName = user.FirstName,
                        LastName = user.LastName,
                        Email = user.Email,
                        Gender = GetGender(user.GenderId),
                        Phone = user.Phone,
                        Role = GetUserRole(user.Id),
                        isVerified = user.IsVerified,
                    };
                    list.Add(printuser);
                }

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return list;
        }


        public UserDetailsModel GetUserById(int id)
        {
            try
            {
                EcommerceContext db = new EcommerceContext();
                var user = db.Users.Where(x => x.Id == id).FirstOrDefault();
                var temp = new UserDetailsModel()
                {
                    UserName = user.UserName,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Phone = user.Phone,
                    Email = user.Email,
                    Gender = GetGender(user.GenderId),
                    Role = GetUserRole(user.Id),
                    IsActive = user.IsActive,
                    isVerified = user.IsVerified
                };
                return temp;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        public List<AddressModel> GetUserAddresses(int id)
        {
            EcommerceContext db = new EcommerceContext();

            try
            {
                var userAddress = db.Addresses.Where(x => x.UserId == id);

                if (userAddress == null)
                {
                    throw new Exception("No Address Found");
                }

                List<AddressModel> list = new List<AddressModel>();

                foreach (var add in userAddress)
                {
                    var Address = new AddressModel()
                    {
                        AddressLine1 = add.AddressLine1,
                        AddressLine2 = add.AddressLine2,
                        City = add.City,
                        State = add.State,
                        Country = add.Country,
                        Postalcode = add.Postalcode
                    };

                    list.Add(Address);
                }

                return list;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        public bool DeactivateUser(int id, string password)
        {
            EcommerceContext db = new EcommerceContext();
            try
            {
                var user = db.Users.First(x => x.Id == id);

                if (user == null)
                {
                    throw new Exception("User doesn't exist");
                }

                if (user.Password == Password.HashEncrypt(password))
                {
                    db.Users.Remove(user);
                    db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return true;
        }
    }
}

using Ecommerce.Models.DbModel;

namespace Ecommerce.Models.ViewModel
{
    public class UserDetailsModel
    {
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public UserGender Gender { get; set; }
        public UserRole Role { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public bool isVerified { get; set; }
        public bool IsActive { get; set; }
        public string ExceptionMessage { get; set; }
        public string Token { get; set; }
        public string RefreshToken { get; set; }
    }
}

using System;
using System.Collections.Generic;

#nullable disable

namespace Ecommerce.Models.DbModel
{
    public partial class User
    {
        public User()
        {
            Addresses = new HashSet<Address>();
            Comments = new HashSet<Comment>();
            OrderDetails = new HashSet<OrderDetail>();
            UserProductMappings = new HashSet<UserProductMapping>();
            UserRoleMappings = new HashSet<UserRoleMapping>();
        }

        public int Id { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int GenderId { get; set; }
        public int CountryCode { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        public virtual UserGender Gender { get; set; }
        public virtual CartTable CartTable { get; set; }
        public virtual Wishlist Wishlist { get; set; }
        public virtual ICollection<Address> Addresses { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
        public virtual ICollection<UserProductMapping> UserProductMappings { get; set; }
        public virtual ICollection<UserRoleMapping> UserRoleMappings { get; set; }
    }
}

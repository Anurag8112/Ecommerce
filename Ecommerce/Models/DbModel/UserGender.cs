using System;
using System.Collections.Generic;

#nullable disable

namespace Ecommerce.Models.DbModel
{
    public partial class UserGender
    {
        public UserGender()
        {
            Users = new HashSet<User>();
        }

        public int Id { get; set; }
        public string Gender { get; set; }

        public virtual ICollection<User> Users { get; set; }
    }
}

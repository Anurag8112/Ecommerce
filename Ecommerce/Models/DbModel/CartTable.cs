using System;
using System.Collections.Generic;

#nullable disable

namespace Ecommerce.Models
{
    public partial class CartTable
    {
        public CartTable()
        {
            Carts = new HashSet<Cart>();
        }

        public int Id { get; set; }
        public int UserId { get; set; }

        public virtual User User { get; set; }
        public virtual ICollection<Cart> Carts { get; set; }
    }
}

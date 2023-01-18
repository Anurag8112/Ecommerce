using System;
using System.Collections.Generic;

#nullable disable

namespace Ecommerce.Models.DbModel
{
    public partial class OrderDetail
    {
        public OrderDetail()
        {
            OrderItems = new HashSet<OrderItem>();
            UserWarehouseOrderDetailsMappings = new HashSet<UserWarehouseOrderDetailsMapping>();
        }

        public int Id { get; set; }
        public int UserId { get; set; }
        public int Total { get; set; }
        public int PaymentId { get; set; }
        public DateTime CreatedOn { get; set; }
        public int AddressId { get; set; }

        public virtual Address Address { get; set; }
        public virtual PaymentDetail Payment { get; set; }
        public virtual User User { get; set; }
        public virtual ICollection<OrderItem> OrderItems { get; set; }
        public virtual ICollection<UserWarehouseOrderDetailsMapping> UserWarehouseOrderDetailsMappings { get; set; }
    }
}

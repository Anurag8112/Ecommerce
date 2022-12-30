using System;
using System.Collections.Generic;

#nullable disable

namespace Ecommerce.Models
{
    public partial class PaymentDetail
    {
        public int Id { get; set; }
        public int Amount { get; set; }
        public string Status { get; set; }
        public byte[] Time { get; set; }

        public virtual OrderDetail OrderDetail { get; set; }
    }
}

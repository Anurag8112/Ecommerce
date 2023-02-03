using System;
using System.Collections.Generic;

#nullable disable

namespace Ecommerce.Models.DbModel
{
    public partial class PaymentDetail
    {
        public int Id { get; set; }
        public int Amount { get; set; }
        public string Currency { get; set; }
        public string TransectionId { get; set; }
        public DateTime CreatedOn { get; set; }

        public virtual OrderDetail OrderDetail { get; set; }
    }
}

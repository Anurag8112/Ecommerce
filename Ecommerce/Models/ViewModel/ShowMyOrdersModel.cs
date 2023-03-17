using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce.Models.ViewModel
{
    public class ShowMyOrdersModel
    {
        public int OrderId { get; set; }
        public int UserId { get; set; }
        public int TotalAmount { get; set; }
        public PaymentDetails PaymentDetails { get; set; }
        public DeliveryAddress Address { get; set; }
        public DateTime DateAndTime { get; set; }
        public OrderItems OrderItems { get; set; }
    }

    public class OrderItems
    {
        public int OrderItemId { get; set; }
        public ShowProduct Product { get; set; }
        public int Quantity { get; set; }
        public DateTime DateAndTime { get; set;}
    }

    public class DeliveryAddress
    {
        public int AddressId { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string PostalCode { get; set; }
        public string Country { get; set; }
    }

    public class PaymentDetails
    {
        public int PaymentId { get; set; }
        public int Amount { get; set; }
        public string Currency { get; set; }
        public string TransectionId { get; set; }
        public DateTime DateAndTime { get; set; }
    }
}

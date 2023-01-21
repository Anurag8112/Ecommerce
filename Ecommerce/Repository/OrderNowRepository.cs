using Ecommerce.Interface;
using Ecommerce.Models.DbModel;
using Ecommerce.Models.ViewModel;
using System;
using System.Linq;

namespace Ecommerce.Repository
{
    public class OrderNowRepository : IOrderRepository
    {
        public bool OrderNow(OrderModel model)
        {
            try
            {
                EcommerceContext db = new EcommerceContext();
                var Product = db.ProductDetails.FirstOrDefault(x => x.ProdId == model.ProdId && x.SizeId==model.SizeId && x.ColorId==model.ColorId);

                var IsOutofStock = db.InventryItems.FirstOrDefault(x => x.ProductDetailId == Product.Id);
                if (IsOutofStock.ProductCount == 0)
                {
                    throw new Exception("Product Is Out Of Stock");
                }
                if (Product == null)
                {
                    throw new Exception("Invalid Product Id");
                }
                if (model.PaymentStatus != "Success")
                {
                    throw new Exception("Payment is Unsuccessful.Please Try Again");
                }
                var PaymentDetails = new PaymentDetail()
                {
                    Amount = Product.Price * model.Quantity,
                    Status = model.PaymentStatus,
                    TransectionId=model.TransectionId,
                    CreatedOn = DateTime.Now
                };
                var OrderDetails = new OrderDetail()
                {
                    UserId = model.UserId,
                    Total = Product.Price * model.Quantity,
                    CreatedOn = DateTime.Now,
                    AddressId = model.AddressId,
                    Payment=PaymentDetails
                };
                var OrderItems = new OrderItem()
                {
                    OrderId = OrderDetails.Id,
                    ProductId = model.ProdId,
                    Quantity = model.Quantity,
                    CreatedOn = DateTime.Now,
                };
                var inventry = db.InventryItems.FirstOrDefault(x => x.ProductDetailId == Product.Id);
                var WarehouseOrderMapping = new WarehouseOrderDetailsMapping()
                {
                      WarehouseId=inventry.WarehouseId,
                      OrderDetailId= OrderDetails.Id,
                      OrderDetail= OrderDetails
                };
                db.WarehouseOrderDetailsMappings.Add(WarehouseOrderMapping);
                OrderDetails.OrderItems.Add(OrderItems);
                db.OrderDetails.Add(OrderDetails);
                inventry.ProductCount--;
                db.InventryItems.Update(inventry);
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}

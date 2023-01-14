using Ecommerce.Interface;
using Ecommerce.Models.DbModel;
using Ecommerce.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Ecommerce.Repository
{
    public class CartRepository : ICartRepository
    {
        public bool AddToCart(CartModel model)
        {
            try
            {
                EcommerceContext db = new EcommerceContext();

                var IsCartExist = db.CartTables.FirstOrDefault(x => x.UserId == model.UserId);
                var isValidUser = db.Users.FirstOrDefault(x => x.Id == model.UserId);
                var isValidProduct = db.ProductDetails.FirstOrDefault(x => x.Id == model.ProductDetailId);

                if (IsCartExist != null)
                {
                    var CartItemCount = db.Carts.Count(x => x.CartId == IsCartExist.Id);

                    if (CartItemCount == 15)
                    {
                        throw new Exception("Your Cart has been full");
                    }
                }

                if (isValidUser == null)
                {
                    throw new Exception("Invalid UserId");
                }

                if (isValidProduct == null)
                {
                    throw new Exception("Invalid ProductId");
                }

                if (IsCartExist == null)
                {
                    var tempCart = new CartTable()
                    {
                        UserId = model.UserId,
                    };

                    var CartItem = new Cart()
                    {
                        ProdId = model.ProductDetailId,
                        CartId = tempCart.Id
                    };

                    tempCart.Carts.Add(CartItem);
                    db.CartTables.Add(tempCart);
                    db.SaveChanges();
                    return true;
                }
                else
                {
                    var IsCartItemExist = db.Carts.FirstOrDefault(x => x.ProdId == model.ProductDetailId && x.CartId == IsCartExist.Id);
                    if (IsCartItemExist == null)
                    {
                        var CartItems = new Cart()
                        {

                            ProdId = model.ProductDetailId,
                            CartId = IsCartExist.Id
                        };

                        db.Carts.Add(CartItems);
                        db.SaveChanges();

                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool RemoveFromCart(CartModel model)
        {
            try
            {
                EcommerceContext db = new EcommerceContext();
                var IsCartExist = db.CartTables.FirstOrDefault(x => x.UserId == model.UserId);
                var isValidUser = db.Wishlists.FirstOrDefault(x => x.UserId == model.UserId);
                var isValidProduct = db.Carts.FirstOrDefault(x => x.ProdId == model.ProductDetailId && IsCartExist.Id == x.CartId);


                if (IsCartExist == null)
                {
                    throw new Exception("Wishlist is Empty");
                }
                var CartItemCount = db.Carts.Count(x => x.CartId == IsCartExist.Id);

                if (isValidUser == null)
                {
                    throw new Exception("Invalid UserId");
                }

                if (isValidProduct == null)
                {
                    throw new Exception("Invalid ProductId");
                }

                if (CartItemCount > 0)
                {
                    var removeProductFromCart = db.Carts.FirstOrDefault(x => x.ProdId == model.ProductDetailId && x.CartId == IsCartExist.Id);

                    db.Carts.Remove(removeProductFromCart);
                    db.SaveChanges();
                    CartItemCount--;
                }

                if (CartItemCount == 0)
                {
                    db.CartTables.Remove(IsCartExist);
                    db.SaveChanges();
                }

                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<ShowProduct> ShowCartItems()
        {
            throw new NotImplementedException();
        }
    }
}

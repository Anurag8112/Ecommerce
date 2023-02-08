using Ecommerce.Interface;
using Ecommerce.Models.DbModel;
using Ecommerce.Models.ViewModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Ecommerce.Repository
{
    public class CartRepository : ICartRepository
    {
        private readonly ILogger<CartRepository> _logger;
        public CartRepository(ILogger<CartRepository> logger)
        {
            _logger = logger;
        }

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
                        _logger.LogError("---------------Cart has been full-----------------");
                        throw new Exception("Your Cart has been full");
                    }
                }

                if (isValidUser == null)
                {
                    _logger.LogError("---------------Invalid UserId-----------------");
                    throw new Exception("Invalid UserId");
                }

                if (isValidProduct == null)
                {
                    _logger.LogError("---------------Invalid ProductId-----------------");
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
                        CartId = tempCart.Id,
                        Quantity = model.Quantity
                    };

                    tempCart.Carts.Add(CartItem);
                    db.CartTables.Add(tempCart);
                    db.SaveChanges();
                    _logger.LogInformation("---------------Item add to cart-----------------");
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
                            CartId = IsCartExist.Id,
                            Quantity = model.Quantity
                        };

                        db.Carts.Add(CartItems);
                        db.SaveChanges();
                        _logger.LogInformation("---------------Item add to cart-----------------");
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

        public bool RemoveFromCart(DeleteCartItem model)
        {
            try
            {
                EcommerceContext db = new EcommerceContext();
                var IsCartExist = db.CartTables.FirstOrDefault(x => x.UserId == model.UserId);
                var isValidUser = db.CartTables.FirstOrDefault(x => x.UserId == model.UserId);
                var isValidProduct = db.Carts.FirstOrDefault(x => x.ProdId == model.ProductDetailId && IsCartExist.Id == x.CartId);


                if (IsCartExist == null)
                {
                    _logger.LogError("---------------WishList is Empty-----------------");
                    throw new Exception("Wishlist is Empty");
                }
                var CartItemCount = db.Carts.Count(x => x.CartId == IsCartExist.Id);

                if (isValidUser == null)
                {
                    _logger.LogError("---------------Invalid UserId-----------------");
                    throw new Exception("Invalid UserId");
                }

                if (isValidProduct == null)
                {
                    _logger.LogError("---------------Invalid ProductId-----------------");
                    throw new Exception("Invalid ProductId");
                }

                if (CartItemCount > 0)
                {
                    var removeProductFromCart = db.Carts.FirstOrDefault(x => x.ProdId == model.ProductDetailId && x.CartId == IsCartExist.Id);

                    db.Carts.Remove(removeProductFromCart);
                    db.SaveChanges();
                    CartItemCount--;
                    _logger.LogInformation("-------------Inventry Updated Successfully-------------");
                }

                if (CartItemCount == 0)
                {
                    db.CartTables.Remove(IsCartExist);
                    db.SaveChanges();
                    _logger.LogInformation("-----------------------Item Removed From Cart------------------");
                }

                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<ShowProduct> ShowCartItems(int userId)
        {
            try
            {
                EcommerceContext db = new EcommerceContext();
                List<ShowProduct> ListOfCartItem = new List<ShowProduct>();

                var IsValidUser = db.Users.FirstOrDefault(x => x.Id == userId);

                if (IsValidUser == null)
                {
                    throw new Exception("-----Invalid User Id-----");
                }

                var CartTable = db.CartTables.FirstOrDefault(x => x.UserId == userId);

                var Cart = db.Carts.Where(x => x.CartId == CartTable.Id).Include(x => x.Prod).Include(x => x.Prod.Prod).Include(x => x.Prod.Prod.ProductImages)
                                                                        .Include(x=>x.Prod.Prod.Brand).Include(x=>x.Prod.Prod.CategoryL1).Include(x=>x.Prod.Prod.CategoryL2)
                                                                        .Include(x=>x.Prod.Prod.CategoryL3).Include(x=>x.Prod.Size).Include(x=>x.Prod.Color);

                var ProductList = db.ProductImages.Select(x => x).ToList();
                foreach (var product in Cart)
                {
                    var productimage = ProductList.Where(x => x.ProdId == product.Prod.Prod.ProdId).ToList();
                    List<string> productList = new List<string>();
                    productList.Clear();
                    foreach (var image in productimage)
                    {
                        productList.Add(image.Image);
                    }

                    var ShowProduct = new ShowProduct()
                    {
                        UserId = userId,
                        UserName = IsValidUser.UserName,
                        productData = new ProductData()
                        {
                            productName = product.Prod.Prod.ProdName,
                            productDesc = product.Prod.Prod.ProdDescription,
                            brand = new Brands()
                            {
                                brandName = product.Prod.Prod.Brand.BrandName,
                            },
                            categoryL1 = new CategoryL1()
                            {
                                categoryL1 = product.Prod.Prod.CategoryL1.CategoryL1,
                            },
                            categoryL2 = new CategoryL2()
                            {
                                categoryL2 = product.Prod.Prod.CategoryL2.CategoryL2,
                            },
                            categoryL3 = new CategoryL3()
                            {
                                categoryL3 = product.Prod.Prod.CategoryL3.CategoryL3,
                            },
                            productDetail = new ProdDetail()
                            {
                                price = product.Prod.Price,
                                productColor = new ProductColors()
                                {
                                    colorName = product.Prod.Color.Color1,
                                },
                                productSize = new ProductSizes()
                                {
                                    sizeName = product.Prod.Size.Size1,
                                },
                            },
                            productImage = new ProductImages()
                            {
                                image = productList,
                            },
                        }
                    };
                    ListOfCartItem.Add(ShowProduct);
                }
                return ListOfCartItem;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.InnerException.ToString());
            }
        }
    }
}

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
    public class WishlistRepository : IWishlistRepository
    {
        private readonly ILogger<WishlistRepository> _logger;

        public WishlistRepository(ILogger<WishlistRepository> logger)
        {
            _logger = logger;
        }

        public bool AddToWishlist(WishlistModel model)
        {
            try
            {
                EcommerceContext db = new EcommerceContext();
                _logger.LogInformation("-----DB Connection Established-----");
                var IsWishlistExist = db.Wishlists.FirstOrDefault(x => x.UserId == model.UserId);
                var isValidUser = db.Users.FirstOrDefault(x => x.Id == model.UserId);
                var isValidProduct = db.ProductDetails.FirstOrDefault(x => x.Id == model.ProductDetailsId);
                if (IsWishlistExist != null)
                {
                    var WishlistItemCount = db.WishlistItems.Count(x => x.WishlistId == IsWishlistExist.Id);

                    if (WishlistItemCount == 15)
                    {
                        _logger.LogError("-----Wishlist has been full-----");
                        throw new Exception("Your Wishlish has been full");
                    }
                }

                if (isValidUser == null)
                {
                    _logger.LogError("-----Invalid User Id-----");
                    throw new Exception("Invalid UserId");
                }

                if (isValidProduct == null)
                {
                    _logger.LogError("-----Invalid Product Id-----");
                    throw new Exception("Invalid ProductId");
                }

                if (IsWishlistExist == null)
                {
                    var tempWishlist = new Wishlist()
                    {
                        UserId = model.UserId,

                    };

                    var WishlistItems = new WishlistItem()
                    {
                        ProdDetailId = model.ProductDetailsId,
                        WishlistId = tempWishlist.Id
                    };

                    tempWishlist.WishlistItems.Add(WishlistItems);
                    db.Wishlists.Add(tempWishlist);
                    db.SaveChanges();
                    _logger.LogInformation("-----Product Added to Wishlist-----");
                    return true;
                }
                else
                {
                    var IsWishlistItemExist = db.WishlistItems.FirstOrDefault(x => x.ProdDetailId == model.ProductDetailsId && x.WishlistId == IsWishlistExist.Id);
                    if (IsWishlistItemExist == null)
                    {
                        var WishlistItems = new WishlistItem()
                        {
                            ProdDetailId = model.ProductDetailsId,
                            WishlistId = IsWishlistExist.Id
                        };

                        db.WishlistItems.Add(WishlistItems);
                        db.SaveChanges();
                        _logger.LogInformation("-----Product Added to Wishlist-----");
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
                _logger.LogError(ex.InnerException.ToString());
                throw new Exception(ex.Message);
            }
        }

        public bool RemoveFromWishlist(WishlistModel model)
        {
            try
            {
                EcommerceContext db = new EcommerceContext();
                _logger.LogInformation("-----DB Connection Established-----");
                var IsWishlistExist = db.Wishlists.FirstOrDefault(x => x.UserId == model.UserId);
                var isValidUser = db.Wishlists.FirstOrDefault(x => x.UserId == model.UserId);
                var isValidProduct = db.WishlistItems.FirstOrDefault(x => x.ProdDetailId == model.ProductDetailsId && IsWishlistExist.Id == x.WishlistId);


                if (IsWishlistExist == null)
                {
                    _logger.LogError("-----Wishlist is Empty-----");
                    throw new Exception("Wishlist is Empty");
                }
                var WishlistItemCount = db.WishlistItems.Count(x => x.WishlistId == IsWishlistExist.Id);

                if (isValidUser == null)
                {
                    _logger.LogError("-----Invalid User Id-----");
                    throw new Exception("Invalid UserId");
                }

                if (isValidProduct == null)
                {
                    _logger.LogError("-----Invalid Product Id-----");
                    throw new Exception("Invalid ProductId");
                }

                if (WishlistItemCount > 0)
                {
                    var removeProductFromWishlist = db.WishlistItems.FirstOrDefault(x => x.ProdDetailId == model.ProductDetailsId && x.WishlistId == IsWishlistExist.Id);

                    db.WishlistItems.Remove(removeProductFromWishlist);
                    db.SaveChanges();
                    _logger.LogInformation("-----Product Removed From Wishlist-----");
                    WishlistItemCount--;
                }

                if (WishlistItemCount == 0)
                {
                    db.Wishlists.Remove(IsWishlistExist);
                    db.SaveChanges();
                    _logger.LogInformation("-----Product Removed From Wishlist");
                }

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.InnerException.ToString());
                throw new Exception(ex.Message);
            }

        }

        public List<ShowProduct> ShowMyWishlist(int userId)
        {
            try
            {
                EcommerceContext db = new EcommerceContext();
                List<ShowProduct> ListOfWishlistItem = new List<ShowProduct>();

                var IsValidUser = db.Users.FirstOrDefault(x => x.Id == userId);

                if (IsValidUser == null)
                {
                    throw new Exception("-----Invalid User Id-----");
                }

                var WishList = db.Wishlists.FirstOrDefault(x => x.UserId == userId);

                var WishListItem = db.WishlistItems.Where(x => x.WishlistId == WishList.Id).Include(x => x.ProdDetail).Include(x => x.ProdDetail.Prod).Include(x => x.ProdDetail.Prod.ProductImages)
                                                                        .Include(x => x.ProdDetail.Prod.Brand).Include(x => x.ProdDetail.Prod.CategoryL1).Include(x => x.ProdDetail.Prod.CategoryL2)
                                                                        .Include(x => x.ProdDetail.Prod.CategoryL3).Include(x => x.ProdDetail.Size).Include(x => x.ProdDetail.Color);

                var ProductList = db.ProductImages.Select(x => x).ToList();
                foreach (var product in WishListItem)
                {
                    var productimage = ProductList.Where(x => x.ProdId == product.ProdDetail.Prod.ProdId).ToList();
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
                            productName = product.ProdDetail.Prod.ProdName,
                            productDesc = product.ProdDetail.Prod.ProdDescription,
                            brand = new Brands()
                            {
                                brandName = product.ProdDetail.Prod.Brand.BrandName,
                            },
                            categoryL1 = new CategoryL1()
                            {
                                categoryL1 = product.ProdDetail.Prod.CategoryL1.CategoryL1,
                            },
                            categoryL2 = new CategoryL2()
                            {
                                categoryL2 = product.ProdDetail.Prod.CategoryL2.CategoryL2,
                            },
                            categoryL3 = new CategoryL3()
                            {
                                categoryL3 = product.ProdDetail.Prod.CategoryL3.CategoryL3,
                            },
                            productDetail = new ProdDetail()
                            {
                                price = product.ProdDetail.Price,
                                productColor = new ProductColors()
                                {
                                    colorName = product.ProdDetail.Color.Color1,
                                },
                                productSize = new ProductSizes()
                                {
                                    sizeName = product.ProdDetail.Size.Size1,
                                },
                            },
                            productImage = new ProductImages()
                            {
                                image = productList,
                            },
                        }
                    };
                    ListOfWishlistItem.Add(ShowProduct);
                }
                return ListOfWishlistItem;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.InnerException.ToString());
            }
        }
    }
}

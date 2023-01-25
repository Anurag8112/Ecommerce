using Ecommerce.Interface;
using Ecommerce.Models.DbModel;
using Ecommerce.Models.ViewModel;
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
                _logger.LogInformation("-----DB COnnection Established-----");
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

        public List<ShowProduct> ShowMyWishlist(ShowWishlist model)
        {
            try
            {
                EcommerceContext db = new EcommerceContext();
                _logger.LogInformation("-----Db Connection Established-----");
                List<ShowProduct> wishlist = new List<ShowProduct>();

                _logger.LogInformation("-----Wishlist Retrieved-----");
                return wishlist;

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.InnerException.ToString());
                throw new Exception(ex.Message);
            }
        }
    }
}

using Ecommerce.Interface;
using Ecommerce.Models.DbModel;
using Ecommerce.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Ecommerce.Repository
{
    public class WishlistRepository : IWishlistRepository
    {
        public bool AddToWishlist(WishlistModel model)
        {
            try
            {
                EcommerceContext db = new EcommerceContext();

                var IsWishlistExist = db.Wishlists.FirstOrDefault(x => x.UserId == model.UserId);
                var isValidUser = db.Users.FirstOrDefault(x => x.Id == model.UserId);
                var isValidProduct = db.ProductDetails.FirstOrDefault(x => x.Id == model.ProductDetailsId);
                if (IsWishlistExist != null)
                {
                    var WishlistItemCount = db.WishlistItems.Count(x => x.WishlistId == IsWishlistExist.Id);

                    if (WishlistItemCount == 15)
                    {
                        throw new Exception("Your Wishlish has been full");
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

        public bool RemoveFromWishlist(WishlistModel model)
        {
            try
            {
                EcommerceContext db = new EcommerceContext();
                var IsWishlistExist = db.Wishlists.FirstOrDefault(x => x.UserId == model.UserId);
                var isValidUser = db.Wishlists.FirstOrDefault(x => x.UserId == model.UserId);
                var isValidProduct = db.WishlistItems.FirstOrDefault(x => x.ProdDetailId == model.ProductDetailsId && IsWishlistExist.Id == x.WishlistId);


                if (IsWishlistExist == null)
                {
                    throw new Exception("Wishlist is Empty");
                }
                var WishlistItemCount = db.WishlistItems.Count(x => x.WishlistId == IsWishlistExist.Id);

                if (isValidUser == null)
                {
                    throw new Exception("Invalid UserId");
                }

                if (isValidProduct == null)
                {
                    throw new Exception("Invalid ProductId");
                }

                if (WishlistItemCount > 0)
                {
                    var removeProductFromWishlist = db.WishlistItems.FirstOrDefault(x => x.ProdDetailId == model.ProductDetailsId && x.WishlistId == IsWishlistExist.Id);

                    db.WishlistItems.Remove(removeProductFromWishlist);
                    db.SaveChanges();
                    WishlistItemCount--;
                }

                if (WishlistItemCount == 0)
                {
                    db.Wishlists.Remove(IsWishlistExist);
                    db.SaveChanges();
                }

                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        public List<ShowProduct> ShowMyWishlist(ShowWishlist model)
        {
            try
            {
                EcommerceContext db = new EcommerceContext();
                List<ShowProduct> wishlist = new List<ShowProduct>();

                return wishlist;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}

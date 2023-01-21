using Ecommerce.Interface;
using Ecommerce.Models.DbModel;
using Ecommerce.Models.ViewModel;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Ecommerce.Repository
{
    public class ProductRepository : IProductRepository
    {
        public readonly IUserRepository _userRepository;
        public ProductRepository(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public CategoryLevel1 GetCategoryL1(int id)
        {
            try
            {
                EcommerceContext db = new EcommerceContext();

                var categoryL1 = db.CategoryLevel1s.First(x => x.Id == id);

                return categoryL1;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public CategoryLevel2 GetCategoryL2(int id)
        {
            try
            {
                EcommerceContext db = new EcommerceContext();
                var categoryL2 = db.CategoryLevel2s.First(x => x.Id == id);
                return categoryL2;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        public CategoryLevel3 GetCategoryL3(int id)
        {
            try
            {
                EcommerceContext db = new EcommerceContext();
                var categoryL3 = db.CategoryLevel3s.First(x => x.Id == id);
                return categoryL3;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public Brand GetBrandName(int id)
        {
            try
            {
                EcommerceContext db = new EcommerceContext();
                var Brand = db.Brands.First(x => x.Id == id);
                return Brand;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public Size GetSizeById(int id)
        {
            try
            {
                EcommerceContext db = new EcommerceContext();
                var Size = db.Sizes.First(x => x.Id == id);
                return Size;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public Color GetColorById(int id)
        {
            try
            {
                EcommerceContext db = new EcommerceContext();
                var Color = db.Colors.First(x => x.Id == id);
                return Color;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool AddProduct(ProductModel product)
        {
            try
            {
                EcommerceContext db = new EcommerceContext();

                var tempProduct = new Product()
                {
                    ProdName = product.ProductName,
                    ProdDescription = product.ProductDescription,
                    CategoryL1id = product.CategoryLevel1Id,
                    CategoryL2id = product.CategoryLevel2Id,
                    CategoryL3id = product.CategoryLevel3Id,
                    BrandId = product.BrandId
                };

                foreach (var image in product.Images)
                {
                    var productImage = new ProductImage()
                    {
                        ProdId = tempProduct.ProdId,
                        Image = image,
                    };
                    tempProduct.ProductImages.Add(productImage);
                }

                var productDetails = new ProductDetail()
                {
                    Price = product.Price,
                    SizeId = product.SizeId,
                    ColorId = product.SizeId,
                    ProdId = tempProduct.ProdId
                };

                var userProductMapping = new UserProductMapping()
                {
                    ProdId = tempProduct.ProdId,
                    UserId = product.UserId
                };

                var inventryItem = new InventryItem()
                {
                    ProductDetailId = productDetails.ProdId,
                    WarehouseId = product.WarehouseId,
                    ProductCount = product.TotalStock,
                    ProductDetail=productDetails
                };

                productDetails.InventryItems.Add(inventryItem);
                tempProduct.ProductDetails.Add(productDetails);
                tempProduct.UserProductMappings.Add(userProductMapping);
                db.Products.Add(tempProduct);
                db.SaveChanges();

                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public string GetImageById(int id)
        {
            try
            {
                EcommerceContext db = new EcommerceContext();
                var imageId = db.ProductImages.First(x => x.ImgId == id);
                return imageId.Image;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<ShowProduct> ShowAllProducts()
        {
            try
            {
                EcommerceContext db = new EcommerceContext();
                List<ShowProduct> list = new List<ShowProduct>();

                var productMapping = db.UserProductMappings.ToList();
                var productData = db.Products.ToList();
                var productDetails = db.ProductDetails.ToList();
                var categoryL1 = db.CategoryLevel1s.ToList();
                var categoryL2 = db.CategoryLevel2s.ToList();
                var categoryL3 = db.CategoryLevel3s.ToList();
                var brands = db.Brands.ToList();
                var sizes = db.Sizes.ToList();
                var color = db.Colors.ToList();
                var images = db.ProductImages.ToList();

                foreach (var product in db.Products)
                {
                    var oneUserData = productMapping.First(x => x.ProdId == product.ProdId);
                    var oneProductData = productData.First(x => x.ProdId == product.ProdId);
                    var oneProductDetails = productDetails.First(x => x.ProdId == product.ProdId);
                    var oneProductCategoryL1 = categoryL1.First(x => x.Id == oneProductData.CategoryL1id);
                    var oneProductCategoryL2 = categoryL2.First(x => x.Id == oneProductData.CategoryL2id);
                    var oneProductCategoryL3 = categoryL3.First(x => x.Id == oneProductData.CategoryL3id);
                    var oneProductBrand = brands.First(x => x.Id == oneProductData.BrandId);
                    var oneProductSize = sizes.First(x => x.Id == oneProductDetails.SizeId);
                    var oneProductColor = color.First(x => x.Id == oneProductDetails.ColorId);
                    var oneProductImage = images.Where(x => x.ProdId == product.ProdId);
                    List<string> ProductImageString = new List<string>();

                    foreach (var image in oneProductImage)
                    {
                        ProductImageString.Add(image.Image);
                    }

                    var allProducts = new ShowProduct()
                    {
                        UserId = oneUserData.UserId,
                        UserName = _userRepository.GetUserById(oneUserData.UserId).UserName,
                        productData = new ProductData()
                        {
                            productName = oneProductData.ProdName,
                            productDesc = oneProductData.ProdDescription,
                            productDetail = new ProdDetail()
                            {
                                price = oneProductDetails.Price,
                                productColor = new ProductColors()
                                {
                                    colorName = oneProductColor.Color1
                                },
                                productSize = new ProductSizes()
                                {
                                    sizeName = oneProductSize.Size1
                                },
                            },
                            brand = new Brands()
                            {
                                brandName = oneProductBrand.BrandName
                            },
                            categoryL1 = new CategoryL1()
                            {
                                categoryL1 = oneProductCategoryL1.CategoryL1
                            },
                            categoryL2 = new CategoryL2()
                            {
                                categoryL2 = oneProductCategoryL2.CategoryL2
                            },
                            categoryL3 = new CategoryL3()
                            {
                                categoryL3 = oneProductCategoryL3.CategoryL3
                            },
                            productImage = new ProductImages()
                            {
                                image = ProductImageString
                            },
                        }
                    };
                    list.Add(allProducts);
                }

                return list;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<ShowProduct> ShowMyProducts(int userId)
        {
            try
            {
                EcommerceContext db = new EcommerceContext();


                var myProduct = from userMapping in db.UserProductMappings
                                join product in db.Products
                                on userMapping.ProdId equals product.ProdId
                                into productGroup
                                from products in productGroup
                                join brand in db.Brands
                                on products.BrandId equals brand.Id
                                join categoryL1 in db.CategoryLevel1s
                                on products.CategoryL1id equals categoryL1.Id
                                into completeProduct
                                from compProd in completeProduct
                                join categoryL2 in db.CategoryLevel2s
                                on products.CategoryL2id equals categoryL2.Id
                                into withCategoryL2
                                from categoryL2 in withCategoryL2
                                join categoryL3 in db.CategoryLevel3s
                                on products.CategoryL3id equals categoryL3.Id
                                into withCategoryL3
                                from categoryL3 in withCategoryL3
                                join productDetail in db.ProductDetails
                                on products.ProdId equals productDetail.ProdId
                                into withProdDetails
                                from details in withProdDetails
                                join color in db.Colors
                                on details.ColorId equals color.Id
                                into withcolor
                                from color in withcolor
                                join size in db.Sizes
                                on details.SizeId equals size.Id
                                into withsize
                                from size in withsize
                                join image in db.ProductImages
                                on products.ProdId equals image.ProdId
                                into withImage
                                from image in withImage
                                where userMapping.UserId == userId
                                select new { products, brand, compProd, categoryL2, categoryL3, details, color, size, img = withImage.Where(x => x.ProdId == products.ProdId) };


                List<ShowProduct> showProductList = new List<ShowProduct>();

                var user = db.Users.First(x => x.Id == userId);

                foreach (var product in myProduct)
                {
                    List<string> images = new List<string>();

                    foreach (var image in product.img)
                    {
                        images.Add(image.Image);
                    }

                    var completeProduct = new ShowProduct()
                    {

                        UserId = user.Id,
                        UserName = user.UserName,

                        productData = new ProductData()
                        {
                            productName = product.products.ProdName,
                            productDesc = product.products.ProdDescription,
                            productDetail = new ProdDetail()
                            {
                                price = product.details.Price,
                                productColor = new ProductColors()
                                {
                                    colorName = product.color.Color1
                                },
                                productSize = new ProductSizes()
                                {
                                    sizeName = product.size.Size1
                                },
                            },
                            brand = new Brands()
                            {
                                brandName = product.brand.BrandName
                            },
                            categoryL1 = new CategoryL1()
                            {
                                categoryL1 = product.compProd.CategoryL1
                            },
                            categoryL2 = new CategoryL2()
                            {
                                categoryL2 = product.categoryL2.CategoryL2
                            },
                            categoryL3 = new CategoryL3()
                            {
                                categoryL3 = product.categoryL3.CategoryL3
                            },
                            productImage = new ProductImages()
                            {
                                image = images
                            }
                        }
                    };
                    showProductList.Add(completeProduct);
                }

                return showProductList;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool DeleteProduct(DeleteProductModel model)
        {
            try
            {
                EcommerceContext db = new EcommerceContext();
                var deleteMapping = db.UserProductMappings.First(x => x.UserId == model.UserId && x.ProdId == model.ProdId);

                db.UserProductMappings.Remove(deleteMapping);
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public string UpdateProduct(int UserId, ProductModel product)
        {
            throw new NotImplementedException();
        }

        public List<ShowComments> ShowComments(CommentModel model)
        {
            try
            {
                EcommerceContext db = new EcommerceContext();
                var Comments = db.Comments.Where(x => x.ProdId == model.ProdId).Include(x => x.User);


                List<ShowComments> CommentList = new List<ShowComments>();

                if (Comments == null)
                {
                    throw new Exception("There Is No Comment");
                }
                else
                {
                    foreach (var Comment in Comments)
                    {
                        var comment = new ShowComments()
                        {
                            User = Comment.User.UserName,
                            Comment = Comment.Comment1
                        };
                        CommentList.Add(comment);
                    }

                    return CommentList;
                }

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}

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
    public class ProductRepository : IProductRepository
    {
        private readonly ILogger<ProductRepository> _logger;
        public readonly IUserRepository _userRepository;
        public ProductRepository(IUserRepository userRepository, ILogger<ProductRepository> logger)
        {
            _logger = logger;
            _userRepository = userRepository;
        }

        public CategoryLevel1 GetCategoryL1(int id)
        {
            try
            {
                EcommerceContext db = new EcommerceContext();
                _logger.LogInformation("----------DB Connection Established-----------");

                var categoryL1 = db.CategoryLevel1s.First(x => x.Id == id);

                _logger.LogInformation("----------Category Level 1 Retrieved---------");
                return categoryL1;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.InnerException.ToString());
                throw new Exception(ex.Message);
            }
        }

        public CategoryLevel2 GetCategoryL2(int id)
        {
            try
            {
                EcommerceContext db = new EcommerceContext();
                _logger.LogInformation("----------DB Connection Established-----------");
                var categoryL2 = db.CategoryLevel2s.First(x => x.Id == id);
                _logger.LogInformation("----------Category Level 2 Retrieved---------");
                return categoryL2;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.InnerException.ToString());
                throw new Exception(ex.Message);
            }
        }

        public CategoryLevel3 GetCategoryL3(int id)
        {
            try
            {
                EcommerceContext db = new EcommerceContext();
                _logger.LogInformation("----------DB Connection Established-----------");
                var categoryL3 = db.CategoryLevel3s.First(x => x.Id == id);
                _logger.LogInformation("----------Category Level 3 Retrieved---------");
                return categoryL3;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.InnerException.ToString());
                throw new Exception(ex.Message);
            }
        }

        public Brand GetBrandName(int id)
        {
            try
            {
                EcommerceContext db = new EcommerceContext();
                _logger.LogInformation("----------DB Connection Established-----------");
                var Brand = db.Brands.First(x => x.Id == id);
                _logger.LogInformation("----------Brand Name Retrieved---------");
                return Brand;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.InnerException.ToString());
                throw new Exception(ex.Message);
            }
        }

        public Size GetSizeById(int id)
        {
            try
            {
                EcommerceContext db = new EcommerceContext();
                _logger.LogInformation("----------DB Connection Established-----------");
                var Size = db.Sizes.First(x => x.Id == id);
                _logger.LogInformation("----------Size Retrieved---------");
                return Size;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.InnerException.ToString());
                throw new Exception(ex.Message);
            }
        }

        public Color GetColorById(int id)
        {
            try
            {
                EcommerceContext db = new EcommerceContext();
                _logger.LogInformation("----------DB Connection Established-----------");
                var Color = db.Colors.First(x => x.Id == id);
                _logger.LogInformation("----------Color Retrieved---------");
                return Color;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.InnerException.ToString());
                throw new Exception(ex.Message);
            }
        }

        public bool AddProduct(ProductModel product)
        {
            try
            {
                EcommerceContext db = new EcommerceContext();
                _logger.LogInformation("----------DB Connection Established-----------");
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
                _logger.LogInformation("----------Product Added-----------");
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.InnerException.ToString());
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

                var IsValiduser = db.Users.FirstOrDefault(x => x.Id == userId);
                if (IsValiduser == null)
                {
                    _logger.LogError("-----Invalid User Id-----");
                    throw new Exception("Invalid User Id");
                }

                var UserProdMapping = db.UserProductMappings.Where(x => x.UserId == userId);

                List<ShowProduct> ProductList = new List<ShowProduct>();

                foreach (var product in UserProdMapping
                                     .Include(x=>x.Prod)
                                     .Include(x=>x.Prod.ProductDetails)
                                     .Include(x=>x.Prod.ProductImages))
                {

                    var oneImage = product.Prod.ProductImages.Where(x => x.ProdId == product.ProdId);
                    List<String> ListOfImage = new List<string>();

                    foreach (var image in oneImage)
                    {
                        ListOfImage.Add(image.Image);
                    }

                    var x = product;
                    var showProducts = new ShowProduct()
                    {
                        UserId=userId,
                        UserName = IsValiduser.UserName,
                        productData = new ProductData()
                        {
                            productName = product.Prod.ProdName,
                            productDesc = product.Prod.ProdDescription,
                            productDetail = new ProdDetail()
                            {
                                price = product.Prod.ProductDetails.FirstOrDefault(x => x.ProdId == product.Prod.ProdId).ProdId,
                                productColor= new ProductColors()
                                {
                                    colorName=GetColorById(product.Prod.ProductDetails.FirstOrDefault(x => x.ProdId == product.Prod.ProdId).ColorId).Color1,
                                },
                                productSize=new ProductSizes()
                                {
                                    sizeName=GetSizeById(product.Prod.ProductDetails.FirstOrDefault(x=>x.ProdId==product.Prod.ProdId).SizeId).Size1,
                                }
                            },
                            brand=new Brands()
                            {
                                brandName=GetBrandName(product.Prod.BrandId).BrandName,
                            },
                            categoryL1=new CategoryL1()
                            {
                                categoryL1=GetCategoryL1(product.Prod.CategoryL1id).CategoryL1,
                            },
                            categoryL2=new CategoryL2()
                            {
                                categoryL2=GetCategoryL2(product.Prod.CategoryL2id).CategoryL2,
                            },
                            categoryL3=new CategoryL3()
                            {
                                categoryL3=GetCategoryL3(product.Prod.CategoryL3id).CategoryL3,
                            },
                            productImage=new ProductImages()
                            {
                                image= ListOfImage,
                            }
                        }
                    };
                    ProductList.Add(showProducts);
                }
                return ProductList;
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

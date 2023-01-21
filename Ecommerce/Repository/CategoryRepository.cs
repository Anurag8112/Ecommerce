using Ecommerce.Interface;
using Ecommerce.Models.DbModel;
using Ecommerce.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Ecommerce.Repository
{
    public class CategoryRepository : ICategoryRepository
    {
        public bool AddCategoryL1(CategoryModelL1 model)
        {
            try
            {
                EcommerceContext db = new EcommerceContext();

                var categoryL1 = new CategoryLevel1()
                {
                    CategoryL1 = model.CategoryL1Name
                };

                db.CategoryLevel1s.Add(categoryL1);
                db.SaveChanges();
                return true;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool AddCategoryL2(CategoryModelL2 model)
        {
            try
            {
                EcommerceContext db = new EcommerceContext();

                var categoryL2 = new CategoryLevel2()
                {
                    CategoryL1Id = model.CategoryL1Id,
                    CategoryL2 = model.CategoryL2Name
                };

                db.CategoryLevel2s.Add(categoryL2);
                db.SaveChanges();
                return true;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool AddCategoryL3(CategoryModelL3 model)
        {
            try
            {
                EcommerceContext db = new EcommerceContext();

                var categoryL3 = new CategoryLevel3()
                {
                    CategoryL2Id = model.CategoryL2Id,
                    CategoryL3 = model.CategoryL3Name
                };

                db.CategoryLevel3s.Add(categoryL3);
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool RemoveCategoryL1(RemoveCategoryModel model)
        {
            try
            {
                EcommerceContext db = new EcommerceContext();

                var category = db.CategoryLevel1s.FirstOrDefault(x => x.Id == model.Id);
                if (category == null)
                {
                    throw new Exception("Invalid Category Id");
                }

                var noFurtherCategories = db.CategoryLevel2s.FirstOrDefault(x => x.CategoryL1Id == category.Id);

                if (noFurtherCategories == null)
                {
                    db.CategoryLevel1s.Remove(category);
                    db.SaveChanges();
                    return true;
                }
                else
                {
                    throw new Exception("This Category Contains Sub-Category. First remove all sub-category");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool RemoveCategoryL2(RemoveCategoryModel model)
        {
            try
            {
                EcommerceContext db = new EcommerceContext();

                var category = db.CategoryLevel2s.FirstOrDefault(x => x.Id == model.Id);
                if (category == null)
                {
                    throw new Exception("Invalid Category Id");
                }

                var noFurtherCategories = db.CategoryLevel3s.FirstOrDefault(x => x.CategoryL2Id == category.Id);

                if (noFurtherCategories == null)
                {
                    db.CategoryLevel2s.Remove(category);
                    db.SaveChanges();
                    return true;
                }
                else
                {
                    throw new Exception("This Category Contains Sub-Category. First remove all sub-category");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool RemoveCategoryL3(RemoveCategoryModel model)
        {
            try
            {
                EcommerceContext db = new EcommerceContext();

                var category = db.CategoryLevel3s.FirstOrDefault(x => x.Id == model.Id);

                if (category == null)
                {
                    throw new Exception("Invalid Category Id");
                }

                var noFurtherProducts = db.Products.FirstOrDefault(x => x.CategoryL3id == category.Id);

                if (noFurtherProducts == null)
                {
                    db.CategoryLevel3s.Remove(category);
                    db.SaveChanges();
                    return true;
                }
                else
                {
                    throw new Exception("This Category Contains Products. First remove all Products");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<ShowCategoryL1Model> ShowCategoryL1()
        {
            EcommerceContext db = new EcommerceContext();

            List<ShowCategoryL1Model> CategoryList = new List<ShowCategoryL1Model>();

            var AllCategory = db.CategoryLevel1s.Select(x => new ShowCategoryL1Model { CategoryL1Id = x.Id, CategoryL1Name = x.CategoryL1 });

            foreach (var category in AllCategory)
            {
                CategoryList.Add(category);
            }

            foreach (var category in CategoryList)
            {
                category.ProductCount = db.Products.Count(x => category.CategoryL1Id == x.CategoryL1id);
            }

            return CategoryList;

        }

        public List<ShowCategoryL2Model> ShowCategoryL2()
        {
            EcommerceContext db = new EcommerceContext();

            List<ShowCategoryL2Model> CategoryList = new List<ShowCategoryL2Model>();

            var AllCategory = db.CategoryLevel2s.Select(x => new ShowCategoryL2Model { CategoryL2Id = x.Id, CategoryL2Name = x.CategoryL2 });

            foreach (var category in AllCategory)
            {
                CategoryList.Add(category);
            }

            foreach (var category in CategoryList)
            {
                category.ProductCount = db.Products.Count(x => category.CategoryL2Id == x.CategoryL2id);
            }

            return CategoryList;
        }

        public List<ShowCategoryL3Model> ShowCategoryL3()
        {
            EcommerceContext db = new EcommerceContext();

            List<ShowCategoryL3Model> CategoryList = new List<ShowCategoryL3Model>();

            var AllCategory = db.CategoryLevel3s.Select(x => new ShowCategoryL3Model { CategoryL3Id = x.Id, CategoryL3Name = x.CategoryL3 });

            foreach (var category in AllCategory)
            {
                CategoryList.Add(category);
            }

            foreach (var category in CategoryList)
            {
                category.ProductCount = db.Products.Count(x => category.CategoryL3Id == x.CategoryL3id);
            }

            return CategoryList;
        }
    }
}

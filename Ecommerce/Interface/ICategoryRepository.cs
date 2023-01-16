﻿using Ecommerce.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce.Interface
{
    public interface ICategoryRepository
    {
        public bool AddCategoryL1(CategoryModelL1 model);
        public bool AddCategoryL2(CategoryModelL2 model);
        public bool AddCategoryL3(CategoryModelL3 model);

        public bool RemoveCategoryL1(RemoveCategoryModel model);
        public bool RemoveCategoryL2(RemoveCategoryModel model);
        public bool RemoveCategoryL3(RemoveCategoryModel model);

        public List<ShowCategoryL1Model> ShowCategoryL1();
        public List<ShowCategoryL2Model> ShowCategoryL2();
        public List<ShowCategoryL3Model> ShowCategoryL3();

    }
}
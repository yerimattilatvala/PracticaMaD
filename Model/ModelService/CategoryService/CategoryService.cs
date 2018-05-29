﻿using Es.Udc.DotNet.ModelUtil.Transactions;
using Es.Udc.DotNet.PracticaMaD.Model.CategoryDao;
using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Es.Udc.DotNet.PracticaMaD.Model.ModelService.CategoryService
{
    public class CategoryService : ICategoryService
    {
        [Inject]
        public ICategoryDao CategoryDao { private get; set; }

        [Transactional]
        public List<Category> GetAllCategories()
        {
            List<Category> categories = CategoryDao.GetAllElements();
            return categories;
           /* List<string> categoriesName = new List<string>();

            for (int i = 0; i < categories.Count; i++)
            {
                categoriesName.Add(categories.ElementAt(i).name);
            }

            return categoriesName;*/
        }
    }
}

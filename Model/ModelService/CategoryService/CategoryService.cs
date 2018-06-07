using Es.Udc.DotNet.ModelUtil.Transactions;
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
        public List<CategoryDetails> GetAllCategories()
        {
            List<Category> categories = CategoryDao.GetAllElements();
            List<CategoryDetails> categoriesDTO = new List<CategoryDetails>();
            foreach(Category cat in categories)
            {
                CategoryDetails categoryDetails = new CategoryDetails(cat.categoryId, cat.name);
                categoriesDTO.Add(categoryDetails);
            }
            return categoriesDTO;
        }

        public CategoryDetails GetCategory(long categoryId)
        {
            Category category= CategoryDao.Find(categoryId);
            CategoryDetails categoryDetails = new CategoryDetails(category.categoryId, category.name);
            return categoryDetails;
        }
    }
}

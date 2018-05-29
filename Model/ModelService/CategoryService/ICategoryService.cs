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
    public interface ICategoryService
    {
        [Inject]
        ICategoryDao CategoryDao { set; }

        [Transactional]
        List<Category> GetAllCategories();

    }
}

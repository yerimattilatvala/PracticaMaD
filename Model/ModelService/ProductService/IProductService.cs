using Es.Udc.DotNet.ModelUtil.Transactions;
using Es.Udc.DotNet.PracticaMaD.Model.CategoryDao;
using Es.Udc.DotNet.PracticaMaD.Model.ProductDao;
using Es.Udc.DotNet.PracticaMaD.Model.TagDao;
using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Es.Udc.DotNet.PracticaMaD.Model.ModelService.ProductService
{
    public interface IProductService
    {
        [Inject]
        IProductDao ProductDao { set; }

        [Inject]
        ICategoryDao CategoryDao { set; }

        [Inject]
        ITagDao TagDao { set; }

        [Transactional]
        List<ProductDetails> FindByKeywords(String keywords);

        [Transactional]
        List<ProductDetails> FindByKeywords(String keywords,long categoryId);

        [Transactional]
        List<ProductDetails> FindByTag(long tagId);
    }
}

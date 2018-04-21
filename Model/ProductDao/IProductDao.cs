using Es.Udc.DotNet.ModelUtil.Dao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Es.Udc.DotNet.PracticaMaD.Model.ProductDao
{
    public interface IProductDao : IGenericDao<Product, Int64>
    {
        List<Product> FindByKeywords(String name, long category);
    }
}

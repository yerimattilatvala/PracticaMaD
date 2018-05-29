using Es.Udc.DotNet.ModelUtil.Dao;
using Es.Udc.DotNet.ModelUtil.Exceptions;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Es.Udc.DotNet.PracticaMaD.Model.ProductDao
{
    public class ProductDaoEntityFramework :
        GenericDaoEntityFramework<Product, Int64>, IProductDao
    {
        public List<Product> FindByKeywords(String name, long category,int startIndex,int count)
        {
            List<Product> productList= new List<Product>();

            DbSet<Product> products = Context.Set<Product>();
            if (category == -1)
            {
                string sqlQuery = "Select * FROM Product where name LIKE @name";
                DbParameter productNameParameter =
                    new System.Data.SqlClient.SqlParameter("name", name);
                productList = Context.Database.SqlQuery<Product>(sqlQuery, productNameParameter).Skip(startIndex).Take(count).ToList<Product>();
            } else
            {
                string sqlQuery = "Select * FROM Product INNER JOIN Category ON Product.categoryId = Category.categoryId  where Product.name LIKE @name AND Category.name=@category";
                DbParameter productNameParameter =
                    new System.Data.SqlClient.SqlParameter("name", name);
                DbParameter categoryNameParameter =
                    new System.Data.SqlClient.SqlParameter("category", category);
                productList = Context.Database.SqlQuery<Product>(sqlQuery, productNameParameter, categoryNameParameter).Skip(startIndex).Take(count).ToList<Product>();
            }


            if (!productList.Any())
                throw new InstanceNotFoundException(name,
                    typeof(Product).FullName);

            return productList;
        }

        public int GetNumberOfProductsByKeywords(string name, long category)
        {
            int result = 0;

            DbSet<Product> products = Context.Set<Product>();
            if (category == -1)
            {
                string sqlQuery = "Select * FROM Product where name LIKE @name";
                DbParameter productNameParameter =
                    new System.Data.SqlClient.SqlParameter("name", name);
                result = Context.Database.SqlQuery<Product>(sqlQuery, productNameParameter).ToList<Product>().Count;
            }
            else
            {
                string sqlQuery = "Select * FROM Product INNER JOIN Category ON Product.categoryId = Category.categoryId  where Product.name LIKE @name AND Category.name=@category";
                DbParameter productNameParameter =
                    new System.Data.SqlClient.SqlParameter("name", name);
                DbParameter categoryNameParameter =
                    new System.Data.SqlClient.SqlParameter("category", category);
                result = Context.Database.SqlQuery<Product>(sqlQuery, productNameParameter, categoryNameParameter).ToList<Product>().Count;
            }
            return result;
        }
    }
}

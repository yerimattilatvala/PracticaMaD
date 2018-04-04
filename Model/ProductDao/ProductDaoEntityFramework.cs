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
    class ProductDaoEntityFramework :
        GenericDaoEntityFramework<Product, Int64>, IProductDao
    {
        public Product FindByName(string name)
        {
            Product product = null;

            DbSet<Product> products = Context.Set<Product>();
            string sqlQuery = "Select * FROM Product where name=@name";
            DbParameter productNameParameter =
                new System.Data.SqlClient.SqlParameter("name", name);

            product = Context.Database.SqlQuery<Product>(sqlQuery, productNameParameter).FirstOrDefault<Product>();

            if (product == null)
                throw new InstanceNotFoundException(name,
                    typeof(UserProfile).FullName);

            return product;
        }

        public Product FindByNameAndCategory(string name, string category)
        {
            Product product = null;

            DbSet<Product> products = Context.Set<Product>();
            string sqlQuery = "Select * FROM Product INNER JOIN Category ON Product.categoryId = Category.categoryId  where Product.name=@name AND Category.name=@category";
            DbParameter productNameParameter =
                new System.Data.SqlClient.SqlParameter("name", name);
            DbParameter categoryNameParameter =
                new System.Data.SqlClient.SqlParameter("category", category);

            product = Context.Database.SqlQuery<Product>(sqlQuery, productNameParameter,categoryNameParameter).FirstOrDefault<Product>();

            if (product == null)
                throw new InstanceNotFoundException(name,
                    typeof(UserProfile).FullName);

            return product;
        }
    }
}

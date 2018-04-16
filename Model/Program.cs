using Es.Udc.DotNet.PracticaMaD.Model.ProductDao;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace Es.Udc.DotNet.PracticaMaD.Model
{
    class Program
    {
        static void Main(string[] args)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["madDatabaseEntities"].ToString();
            DbContext dbContext = new System.Data.Entity.DbContext(connectionString);

            using (TransactionScope transaction = new TransactionScope())
            {
                ProductDaoEntityFramework p = new ProductDaoEntityFramework();
                p.Context = dbContext;
                List<Product> productS = p.FindByKeywords("ACDC",null);
                
                    Console.WriteLine(productS[1].name);
                transaction.Complete();
            }
        }
    }
}

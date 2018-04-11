using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using Es.Udc.DotNet.PracticaMaD.Model.UserProfileDao;
using Es.Udc.DotNet.PracticaMaD.Model.ProductDao;
using Es.Udc.DotNet.PracticaMaD.Model;

namespace Model
{
    public class Program
    {
        static void Main(string[] args)
        {

            string connectionString = ConfigurationManager.ConnectionStrings["madDatabaseEntities"].ToString();
            DbContext dbContext = new System.Data.Entity.DbContext(connectionString);

            using (TransactionScope transaction = new TransactionScope())
            {


                UserProfile user2 = new UserProfile();
                user2.loginName = "adrian";
                user2.lastName = "a";
                user2.firstName = "b";
                user2.enPassword = "1234";
                user2.email = "asdas";
                user2.country = "es";
                user2.language = "as";
                user2.postalAddress = 35555;



                Console.WriteLine("/*  ************ Table per Type ************* */");
                UserProfileDaoEntityFramework p = new UserProfileDaoEntityFramework();

                p.Context = dbContext;

                p.Create(user2);

                UserProfile user = p.Find(2);



                if (user != null)
                    Console.WriteLine(user.loginName);
                else
                    Console.WriteLine("ERROR");

                /*Product p = null;

                ProductDaoEntityFramework product = new ProductDaoEntityFramework();
                product.Context = dbContext;

                p = product.FindByName("acdc");

                //p = product.FindByNameAndCategory("acdc","CD");

                if (p != null)
                    Console.WriteLine(p.name);
                else
                    Console.WriteLine("ERROR");
                List<Order> orders = null;

                OrderDaoEntityFramework query = new OrderDaoEntityFramework();
                query.Context = dbContext;
                orders = query.FindByUserId(1);
                if (orders != null)
                      for (int i = 1; i <= orders.Count; i++)
                      {
                          Console.WriteLine(orders.ElementAt(i).orderId);
                      }
                    Console.WriteLine(orders.Count);
                else
                    Console.WriteLine("ERROR"); */
                 transaction.Complete();

            }

        }
    }
}

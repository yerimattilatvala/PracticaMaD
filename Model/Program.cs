﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using Es.Udc.DotNet.PracticaMaD.Model.UserProfileDao;
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


                Console.WriteLine("/*  ************ Table per Type ************* */");
                UserProfileDaoEntityFramework p = new UserProfileDaoEntityFramework();

                p.Context = dbContext;

                Es.Udc.DotNet.PracticaMaD.Model.UserProfile user = p.FindByLoginName("adrian");

                if (user != null)
                    Console.WriteLine(user.loginName);
                else
                    Console.WriteLine("ERROR");
                transaction.Complete();

            }

        }
    }
}

using Es.Udc.DotNet.ModelUtil.Dao;
using Es.Udc.DotNet.ModelUtil.Exceptions;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Es.Udc.DotNet.PracticaMaD.Model.OrderDao
{
    class OrderDaoEntityFramework :
        GenericDaoEntityFramework<Order, Int64>, IOrderDao
    {
        public List<Order> FindByUserId(long userId)
        {
            List<Order> Orders = null;

            //DbSet<Order> ordersByUser = Context.Set<Order>();

            string sqlQuery = "Select * FROM Orders where usrId=@userId";
            DbParameter userIdParameter =
                new System.Data.SqlClient.SqlParameter("userId", userId);

            Orders = Context.Database.SqlQuery<Order>(sqlQuery, userIdParameter).ToList<Order>();

            if (Orders == null)
                throw new InstanceNotFoundException(userId,
                    typeof(Order).FullName);

            return Orders;
        }
    }
}

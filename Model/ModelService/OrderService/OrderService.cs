using Es.Udc.DotNet.ModelUtil.Transactions;
using Es.Udc.DotNet.PracticaMaD.Model.CardDao;
using Es.Udc.DotNet.PracticaMaD.Model.ModelService.Exceptions;
using Es.Udc.DotNet.PracticaMaD.Model.OrderDao;
using Es.Udc.DotNet.PracticaMaD.Model.OrderLineDao;
using Es.Udc.DotNet.PracticaMaD.Model.ProductDao;
using Es.Udc.DotNet.PracticaMaD.Model.UserProfileDao;
using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Es.Udc.DotNet.PracticaMaD.Model.ModelService.OrderService
{
    public class OrderService : IOrderService
    {
        [Inject]
        public ICardDao CardDao { private get; set; }

        [Inject]
        public IUserProfileDao UserProfileDao { private get; set; }

        [Inject]
        public IProductDao ProductDao { private get; set; }

        [Inject]
        public IOrderLineDao OrderLineDao { private get; set; }

        [Inject]
        public IOrderDao OrderDao { private get; set; }

        [Transactional]
        public long GenerateOrder(long usrId, long idCard, int postalAddress, List<ProductDetails> productList)
        {
            Order order = new Order();
            order.usrId = usrId;
            order.idCard = idCard;
            order.orderDate = DateTime.Now;
            order.postalAddress = postalAddress;
            OrderDao.Create(order);
            long orderId = order.orderId;
            for (int i = 0; i < productList.Count; i++)
            {
                long productId = productList.ElementAt(i).productId;
                Product product = ProductDao.Find(productId);
                int numberOfUnits = productList.ElementAt(i).numberOfUnits;
                int numberOfRemainingUnits = product.numberOfUnits - productList.ElementAt(i).numberOfUnits;
                if (numberOfRemainingUnits < 0)
                    throw new InsuficientNumberOfUnitsException(product.name, numberOfRemainingUnits);
                product.numberOfUnits = numberOfRemainingUnits;
                ProductDao.Update(product);
                OrderLine orderLine = new OrderLine();
                orderLine.orderId = orderId;
                orderLine.productId = productId;
                orderLine.numberOfUnits = numberOfUnits;
                orderLine.unitPrize = product.prize;
                OrderLineDao.Create(orderLine);
            }
            return orderId;
        }

        [Transactional]
        public List<OrderDetails> ViewOrdersByUser(long usrId)
        {
            UserProfile User = UserProfileDao.Find(usrId);

            List<OrderDetails> ordersDetails = new List<OrderDetails>();

            List<Order> orders = User.Orders.ToList();

            for (int i = 0; i < orders.Count; i++)
            {
                List<OrderLine> orderLines = orders.ElementAt(i).OrderLines.ToList();

                List<OrderLineDetails> orderLinesDetails = new List<OrderLineDetails>();
                for (int j = 0; j < orderLines.Count; j++)
                {
                    string productName = orderLines.ElementAt(j).Product.name;
                    int numberOfUnits = orderLines.ElementAt(j).numberOfUnits;
                    float unitPrize = (float)orderLines.ElementAt(j).unitPrize;
                    OrderLineDetails orderLine = new OrderLineDetails(orders.ElementAt(i).orderId, productName, numberOfUnits, unitPrize);
                    orderLinesDetails.Add(orderLine);
                }
                long orderId = orders.ElementAt(i).orderId;
                string cardNumber = CardDao.Find(orders.ElementAt(i).idCard).cardNumber;
                int postalAddress = orders.ElementAt(i).postalAddress;
                DateTime orderDate = orders.ElementAt(i).orderDate;
                ordersDetails.Add(new OrderDetails(orderId, usrId, cardNumber, postalAddress, orderDate, orderLinesDetails));
            }
            return ordersDetails;
        }

        public OrderDetails FindOrder(long orderId)
        {
            Order order = OrderDao.Find(orderId);
            OrderDetails orderDetails = new OrderDetails(order.orderId,order.usrId,CardDao.Find(order.idCard).cardNumber,order.postalAddress,order.orderDate);
            return orderDetails;
        }
    }
}

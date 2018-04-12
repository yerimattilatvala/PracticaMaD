using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Es.Udc.DotNet.PracticaMaD.Model.ProductDao;
using Es.Udc.DotNet.ModelUtil.Exceptions;
using Es.Udc.DotNet.PracticaMaD.Model.ModelService.Exceptions;
using Es.Udc.DotNet.PracticaMaD.Model.OrderLineDao;
using Es.Udc.DotNet.PracticaMaD.Model.OrderDao;

namespace Es.Udc.DotNet.PracticaMaD.Model.ModelService
{
    class ModelService : IModelService
    {

        public IProductDao ProductDao { get; set; }
        public IOrderLineDao OrderLineDao { get; set; }
        public IOrderDao  OrderDao { get; set; }

        public long GenerateOrder(long usrId, int cardNumber, int postalAddress, List<ProductDetails> productList)
        {
            Order order = new Order();
            order.usrId = usrId;
            order.cardNumber = cardNumber;
            order.orderDate = BitConverter.GetBytes(new DateTime().Ticks);
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
                    throw new NumberOfUnitsException(product.name, numberOfRemainingUnits);
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
    }
}

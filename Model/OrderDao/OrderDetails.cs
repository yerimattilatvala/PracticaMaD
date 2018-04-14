using Es.Udc.DotNet.PracticaMaD.Model.OrderLineDao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Es.Udc.DotNet.PracticaMaD.Model.OrderDao
{
    public class OrderDetails
    {
        #region Properties region
        private long OrderId;

        private long UsrId;

        private int CardNumber;

        private int PostalAddress;

        private DateTime OrderDate;

        private List<OrderLineDetails> OrderLines;
        #endregion
        public OrderDetails(long orderId,long usrId,int cardNumber, int postalAddress,DateTime orderDate,List<OrderLineDetails> orderLines)
        {
            this.OrderId = orderId;
            this.UsrId = usrId;
            this.CardNumber = cardNumber;
            this.PostalAddress = postalAddress;
            this.OrderDate = orderDate;
            this.OrderLines = orderLines;
        }

        public OrderDetails(long orderId, long usrId, int cardNumber, int postalAddress, DateTime orderDate)
        {
            this.OrderId = orderId;
            this.UsrId = usrId;
            this.CardNumber = cardNumber;
            this.PostalAddress = postalAddress;
            this.OrderDate = orderDate;
        }
    }
}

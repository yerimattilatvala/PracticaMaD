using Es.Udc.DotNet.PracticaMaD.Model.OrderLineDao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Es.Udc.DotNet.PracticaMaD.Model.OrderDao
{
    [Serializable()]
    public class OrderDetails
    {
        #region Properties region
        public long OrderId { get; }

        public long UsrId { get; }

        public string CardNumber { get; }

        public int PostalAddress { get; }

        public DateTime OrderDate { get; }

        public List<OrderLineDetails> OrderLines { get; }
        
        public int NumberOfProducts { get; }
        #endregion
        public OrderDetails(long orderId,long usrId,string cardNumber, int postalAddress,DateTime orderDate,List<OrderLineDetails> orderLines)
        {
            this.OrderId = orderId;
            this.UsrId = usrId;
            this.CardNumber = cardNumber;
            this.PostalAddress = postalAddress;
            this.OrderDate = orderDate;
            this.OrderLines = orderLines;
            this.NumberOfProducts = this.OrderLines.Count;
        }

        public OrderDetails(long orderId, long usrId, string cardNumber, int postalAddress, DateTime orderDate)
        {
            this.OrderId = orderId;
            this.UsrId = usrId;
            this.CardNumber = cardNumber;
            this.PostalAddress = postalAddress;
            this.OrderDate = orderDate;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Es.Udc.DotNet.PracticaMaD.Model.OrderDao
{
    class OrderDetails
    {
        long orderId;
        long usrId;
        int cardNumber;
        int postalAddress;
        DateTime orderDate;
        OrderDetails(long orderId,long usrId,int cardNumber, int postalAddress,DateTime orderDate)
        {
            this.orderId = orderId;
            this.usrId = usrId;
            this.cardNumber = cardNumber;
            this.postalAddress = postalAddress;
            this.orderDate = orderDate;
        }
    }
}

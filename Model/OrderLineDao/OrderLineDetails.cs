using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Es.Udc.DotNet.PracticaMaD.Model.OrderLineDao
{

    public class OrderLineDetails
    {
        #region Properties region
        private string ProductName;

        private long OrderId;

        private int NumberOfUnits;

        private float UnitaryPrize;
        #endregion
        public OrderLineDetails(long orderId, string productName,int numberOfUnits, float unitaryPrize)
        {
            this.OrderId = orderId;
            this.ProductName = productName;
            this.NumberOfUnits = numberOfUnits;
            this.UnitaryPrize = unitaryPrize;
        }
    }
}

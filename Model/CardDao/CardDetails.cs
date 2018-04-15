using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Es.Udc.DotNet.PracticaMaD.Model.CardDao
{
    [Serializable()]
    public class CardDetails
    {
        #region Properties region
        public int CardNumber { get; }

        public int VerficationCode { get; }

        public DateTime ExpirateTime { get; }

        public string CardType { get; }
        
        #endregion

        public CardDetails(int cardNumber, int verificationCode, DateTime expirateTime, string cardType)
        {
            this.CardNumber = cardNumber;
            this.VerficationCode = verificationCode;
            this.ExpirateTime = expirateTime;
            this.CardType = cardType;
        }
    }
}

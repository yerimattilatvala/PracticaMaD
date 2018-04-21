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
        public string CardNumber { get; }

        public int VerificationCode { get; }

        public DateTime ExpirateTime { get; }

        public string CardType { get; }
        
        #endregion

        public CardDetails(string cardNumber, int verificationCode, DateTime expirateTime, string cardType)
        {
            this.CardNumber = cardNumber;
            this.VerificationCode = verificationCode;
            this.ExpirateTime = expirateTime;
            this.CardType = cardType;
        }
    }
}

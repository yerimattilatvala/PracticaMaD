using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Es.Udc.DotNet.PracticaMaD.Model.ModelService.Exceptions
{
    public class IncorrectCardNumberFormatException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the 
        /// <see cref="IncorrectCardNumberFormatException"/> class.
        /// </summary>
        /// <param name="loginName"><c>loginName</c> that causes the error.</param>
        public IncorrectCardNumberFormatException(String cardNumber)
            : base("Wrong card number format exception => cardNumber = " + cardNumber)
        {
            this.CardNumber = cardNumber;
        }

        /// <summary>
        /// Stores the Card number of the exception
        /// </summary>
        /// <value>The card number.</value>
        public String CardNumber { get; private set; }
    }
}

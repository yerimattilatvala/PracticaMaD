using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Es.Udc.DotNet.PracticaMaD.Model.ModelService.Exceptions
{
    class NumberOfUnitsException : Exception
    {
        public NumberOfUnitsException(String name, int numberOfUnitsRemaining)
            : base("Number of units exceded exception => Product = " + name + " || Number of units remaining = " + numberOfUnitsRemaining)
        {
        }
    }
}

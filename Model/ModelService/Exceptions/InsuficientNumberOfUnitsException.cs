using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Es.Udc.DotNet.PracticaMaD.Model.ModelService.Exceptions
{
    public class InsuficientNumberOfUnitsException : Exception
    {
        public InsuficientNumberOfUnitsException(String name, int numberOfUnitsRemaining)
            : base(name + " " + numberOfUnitsRemaining)
        {
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Es.Udc.DotNet.PracticaMaD.Model.ModelService.Exceptions
{
    public class DuplicateEmailException : Exception
    {
        public DuplicateEmailException(String email)
            : base("Duplicate email exception => email = " + email)
        {
            this.email = email;
        }
    public String email { get; private set; }
}
}

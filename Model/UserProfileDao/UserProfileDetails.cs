using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Es.Udc.DotNet.PracticaMaD.Model.UserProfileDao
{
    [Serializable()]
    public class UserProfileDetails
    {
        #region Properties region
        public String FirstName { get; private set; }

        public String Lastname { get; private set; }

        public String Email { get; private set; }

        public string Language { get; private set; }

        public string Country { get; private set; }

        public int PostalAddress { get; private set; }
        #endregion
        public UserProfileDetails(String firstName, String lastName,
            String email, String language, String country,int postalAddress)
        {
            this.FirstName = firstName;
            this.Lastname = lastName;
            this.Email = email;
            this.Language = language;
            this.Country = country;
            this.PostalAddress = postalAddress;
        }
    }
}

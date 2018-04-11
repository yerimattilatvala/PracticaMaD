using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Es.Udc.DotNet.PracticaMaD.Model.ProductDao
{
    public class ProductDetails
    {
        private string name;
        private string category;
        private DateTime registerDate;
        private double prize;

        ProductDetails(string name, string category, DateTime registerDate, double prize)
        {
            this.name = name;
            this.category = category;
            this.registerDate = registerDate;
            this.prize = prize;

        }


    }
}

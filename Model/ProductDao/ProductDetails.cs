using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Es.Udc.DotNet.PracticaMaD.Model.ProductDao
{
    [Serializable()]
    public class ProductDetails
    {
        public string name;
        public long category;
        public DateTime registerDate;
        public double prize;
        public long productId { get; }
        public int numberOfUnits { get; }

        public ProductDetails(string name, long category, DateTime registerDate, double prize)
        {
            this.name = name;
            this.category = category;
            this.registerDate = registerDate;
            this.prize = prize;
        }

        public ProductDetails(long productId, int numberOfUnits)
        {
            this.productId = productId;
            this.numberOfUnits = numberOfUnits;
        }


    }
}

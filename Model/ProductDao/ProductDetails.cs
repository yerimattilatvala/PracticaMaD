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
        public string name { get; }
        public string category { get; }
        public DateTime registerDate { get; }
        public double prize { get; }
        public long productId { get; }
        public int numberOfUnits { get; }
        public ProductDetails(string name, string category, DateTime registerDate, double prize, long productId)
        {
            this.name = name;
            this.category = category;
            this.registerDate = registerDate;
            this.prize = prize;
            this.productId = productId;
        }
    }
}

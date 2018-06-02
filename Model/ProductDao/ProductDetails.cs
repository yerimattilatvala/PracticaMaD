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
        //se necesita el set para ir actualizanndo las unidades del carrito.
        public int numberOfUnits { get; set; }
        public bool forGift { get; set; }
        public ProductDetails(string name, string category, DateTime registerDate, double prize, long productId,int numberOfUnits,bool gift)
        {
            this.name = name;
            this.category = category;
            this.registerDate = registerDate;
            this.prize = prize;
            this.productId = productId;
            this.numberOfUnits = numberOfUnits;
            this.forGift = gift;
        }

        public override bool Equals(object obj)
        {
            var details = obj as ProductDetails;
            return details != null &&
                   productId == details.productId;
        }

        public override int GetHashCode()
        {
            return -1857272791 + productId.GetHashCode();
        }

    }
}

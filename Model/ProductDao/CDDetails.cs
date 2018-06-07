using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Es.Udc.DotNet.PracticaMaD.Model.ProductDao
{
    [Serializable()]
    public class CDDetails:ProductDetails
    {
        public string title { get; }
        public string artist { get; }
        public string topic { get; }
        public int songs { get; }
        public CDDetails(string name, long categoryId,string categoryName, DateTime registerDate, double prize, long productId, int numberOfUnits, bool gift, string title,
            string artist, string topic, int songs) : base(name, categoryId,categoryName, registerDate, prize, productId, numberOfUnits, gift)
        {
            this.title = title;
            this.artist = artist;
            this.topic = topic;
            this.songs = songs;
        }
    }
}

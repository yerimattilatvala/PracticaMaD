using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Es.Udc.DotNet.PracticaMaD.Model.ProductDao
{
    [Serializable()]
    public class MovieDetails:ProductDetails
    {
        public string title { get; }
        public string director { get; }
        public string summary { get; }
        public string topic { get; }
        public int duration { get; }
        public MovieDetails(string name, long categoryId,string categoryName, DateTime registerDate, double prize, long productId, int numberOfUnits, bool gift,string title,
            string director,string summary,string topic,int duration): base(name, categoryId,categoryName,registerDate,prize,productId,numberOfUnits,gift)
        {
            this.title = title;
            this.director = director;
            this.summary = summary;
            this.topic = topic;
            this.duration = duration;
        }
    }
}

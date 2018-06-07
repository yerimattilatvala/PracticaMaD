using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Es.Udc.DotNet.PracticaMaD.Model.ProductDao
{
    [Serializable()]
    public class BookDetails:ProductDetails
    {
        public string title { get; }
        public string author { get; }
        public string summary { get; }
        public string topic { get; }
        public int pages { get; }
        public BookDetails(string name, long categoryId,string categoryName, DateTime registerDate, double prize, long productId, int numberOfUnits, bool gift, string title,
            string author, string summary, string topic, int pages) : base(name, categoryId,categoryName, registerDate, prize, productId, numberOfUnits, gift)
        {
            this.title = title;
            this.author = author;
            this.summary = summary;
            this.topic = topic;
            this.pages = pages;
        }
    }
}

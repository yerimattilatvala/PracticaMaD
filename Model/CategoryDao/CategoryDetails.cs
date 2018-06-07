using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Es.Udc.DotNet.PracticaMaD.Model.CategoryDao
{
    [Serializable()]
    public class CategoryDetails
    {
        public string name { get; }
        public long categoryId { get; }

        public CategoryDetails(long categoryId,string name)
        {
            this.categoryId = categoryId;
            this.name = name;
        }
    }
}

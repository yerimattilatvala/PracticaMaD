using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Es.Udc.DotNet.PracticaMaD.Model.TagDao
{
    [Serializable]
    public class TagDetails
    {
        public long tagId { get; }

        public string name { get; }

        public long timesUsed { get; }

        public TagDetails(long tagId,string name, long times)
        {
            this.name = name;
            this.timesUsed = times;
            this.tagId = tagId;
        }
    }
}

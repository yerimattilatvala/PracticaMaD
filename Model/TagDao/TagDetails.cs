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
        public string name { get; }

        public long timesUsed { get; }

        public TagDetails(string name, long times)
        {
            this.name = name;
            this.timesUsed = times;
        }
    }
}

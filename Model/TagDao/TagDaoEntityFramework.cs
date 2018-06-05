using Es.Udc.DotNet.ModelUtil.Dao;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Es.Udc.DotNet.PracticaMaD.Model.TagDao
{
    public class TagDaoEntityFramework : GenericDaoEntityFramework<Tag, Int64>, ITagDao
    {
        public List<Tag> GetMostPopularTags(int n)
        {
            List<Tag> tags = null;

            string sqlQuery = "Select top " +n.ToString() +" * FROM Tag Order by timesUsed desc";

            tags = Context.Database.SqlQuery<Tag>(sqlQuery).ToList<Tag>();

            return tags;
        }
    }
}

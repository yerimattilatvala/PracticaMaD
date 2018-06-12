using Es.Udc.DotNet.ModelUtil.Dao;
using Es.Udc.DotNet.ModelUtil.Exceptions;
using System;
using System.Collections.Generic;
using System.Data.Common;
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

        public Tag GetTagByName(string tagName)
        {
            Tag tag = null;
            string sqlQuery = "Select * FROM Tag where name = @name";
            DbParameter tagNameParameter =
                new System.Data.SqlClient.SqlParameter("name", tagName);
            tag = Context.Database.SqlQuery<Tag>(sqlQuery, tagNameParameter).FirstOrDefault<Tag>();

            if(tag==null)
                throw new InstanceNotFoundException(tagName,
                    typeof(Tag).FullName);

            return tag;
        }
    }
}

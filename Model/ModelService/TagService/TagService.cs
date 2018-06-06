using Es.Udc.DotNet.ModelUtil.Transactions;
using Es.Udc.DotNet.PracticaMaD.Model.ProductDao;
using Es.Udc.DotNet.PracticaMaD.Model.TagDao;
using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Es.Udc.DotNet.PracticaMaD.Model.ModelService.TagService
{
    public class TagService : ITagService
    {
        [Inject]
        public ITagDao TagDao { private get; set; }

        [Inject]
        public IProductDao ProductDao { private get; set; }

        [Transactional]
        public List<TagDetails> GetAllTags()
        {
            List<TagDetails> tags = new List<TagDetails>();

            List<Tag> tagList = TagDao.GetAllElements();

            for(int i = 0; i < tagList.Count; i++)
            {
                string name = tagList.ElementAt(i).name;
                long times = tagList.ElementAt(i).timesUsed;
                long tagId = tagList.ElementAt(i).tagId;
                tags.Add(new TagDetails(tagId, name, times));
            }

            return tags;
        }

        [Transactional]
        public List<TagDetails> GetMostPopularTags(int n)
        {
            List<TagDetails> tags = new List<TagDetails>();

            List<Tag> tagList = TagDao.GetMostPopularTags(n);

            for (int i = 0; i < tagList.Count; i++)
            {
                string name = tagList.ElementAt(i).name;
                long times = tagList.ElementAt(i).timesUsed;
                long tagId = tagList.ElementAt(i).tagId;
                tags.Add(new TagDetails(tagId,name, times));
            }

            return tags;
        }

        [Transactional]
        public void TagProduct(long productId, long tagId)
        {
            Product product = ProductDao.Find(productId);

            Tag tag = TagDao.Find(tagId);

            tag.timesUsed += 1;
            tag.Products.Add(product);
            TagDao.Update(tag);

            /*product.Tags.Add(tag);
            ProductDao.Update(product);*/
        }
    }
}

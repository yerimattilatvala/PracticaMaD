using Es.Udc.DotNet.ModelUtil.Exceptions;
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

            if (!product.Tags.Contains(tag))
                tag.Products.Add(product);

            tag.timesUsed += 1;
            TagDao.Update(tag);
        }

        [Transactional]
        public List<TagDetails> GetTagsByProduct(long productId)
        {
            Product product = ProductDao.Find(productId);
            List<TagDetails> tags = new List<TagDetails>();
            List<Tag> productTags = product.Tags.ToList();

            for (int i = 0; i < productTags.Count; i++)
                tags.Add(new TagDetails(productTags.ElementAt(i).tagId, productTags.ElementAt(i).name, productTags.ElementAt(i).timesUsed));
            return tags;
        }

        [Transactional]
        public long AddNewTag(TagDetails newTag)
        {
            Tag tag = null;
            try
            {
                Tag t = TagDao.GetTagByName(newTag.name);
                throw new DuplicateInstanceException(newTag.name,
                    typeof(Tag).FullName);
            } catch(InstanceNotFoundException)
            {
                tag = new Tag();
                tag.name = newTag.name;
                tag.timesUsed = 0;
                TagDao.Create(tag);
            }

            return tag.tagId;
        }

        public TagDetails FinTagById(long tagId)
        {
            Tag tag = null;
            TagDetails tagDetail = null;
            try
            {
                tag = TagDao.Find(tagId);
                tagDetail = new TagDetails(tag.tagId,tag.name,tag.timesUsed);
            } catch(InstanceNotFoundException e)
            {
                throw new InstanceNotFoundException(tagId, "Tag no encontrada");
            }

            return tagDetail;
        }

        public void UntagProduct(long productId, long tagId)
        {
            Product product = null;
            try
            {
                product = ProductDao.Find(productId);
            }
            catch (InstanceNotFoundException)
            {
                throw new InstanceNotFoundException(productId, "Producto no encontrado");
            }
            for(int i = 0; i< product.Tags.Count;i++)
            {
                if (product.Tags.ElementAt(i).tagId == tagId)
                {
                    product.Tags.Remove(TagDao.Find(tagId));
                    ProductDao.Update(product);
                }
            }
        }
    }
}

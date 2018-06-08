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
    public interface ITagService
    {
        [Inject]
        ITagDao TagDao { set; }

        [Inject]
        IProductDao ProductDao { set; }

        [Transactional]
        List<TagDetails> GetAllTags();

        [Transactional]
        List<TagDetails> GetMostPopularTags(int n);

        [Transactional]
        void TagProduct(long productId, long tagId);

        [Transactional]
        List<TagDetails> GetTagsByProduct(long productId);

        [Transactional]
        long AddNewTag(TagDetails newTag);

        [Transactional]
        TagDetails FinTagById(long tagId);
    }
}

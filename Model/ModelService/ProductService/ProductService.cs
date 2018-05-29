using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Es.Udc.DotNet.PracticaMaD.Model.ProductDao;
using Es.Udc.DotNet.ModelUtil.Transactions;
using Ninject;
using Es.Udc.DotNet.PracticaMaD.Model.TagDao;
using Es.Udc.DotNet.PracticaMaD.Model.CategoryDao;

namespace Es.Udc.DotNet.PracticaMaD.Model.ModelService.ProductService
{
    public class ProductService : IProductService
    {
        [Inject]
        public IProductDao ProductDao { private get; set; }

        [Inject]
        public ICategoryDao CategoryDao { private get; set; }

        [Inject]
        public ITagDao TagDao { private get; set; }

        [Transactional]
        public List<ProductDetails> FindByKeywords(string keywords,int startIndex, int count)
        {
            List<ProductDetails> products = new List<ProductDetails>();
            string productName = keywords;

            List<Product> productList = ProductDao.FindByKeywords(keywords, -1,startIndex,count);

            for (int i = 0; i < productList.Count; i++)
            {
                string name = productList.ElementAt(i).name;
                string category = CategoryDao.Find(productList.ElementAt(i).categoryId).name;
                DateTime registerDate = productList.ElementAt(i).registerDate;
                double prize = productList.ElementAt(i).prize;
                products.Add(new ProductDetails(name, category, registerDate, prize));
            }
            return products;
        }

        [Transactional]
        public List<ProductDetails> FindByKeywords(string keywords, long categoryId,int startIndex,int count)
        {
            List<ProductDetails> products = new List<ProductDetails>();
            string productName = keywords;

            List<Product> productList = ProductDao.FindByKeywords(keywords, categoryId,startIndex,count);

            for (int i = 0; i < productList.Count; i++)
            {
                string name = productList.ElementAt(i).name;
                string category = CategoryDao.Find(productList.ElementAt(i).categoryId).name; DateTime registerDate = productList.ElementAt(i).registerDate;
                double prize = productList.ElementAt(i).prize;
                products.Add(new ProductDetails(name, category, registerDate, prize));
            }
            return products;
        }

        [Transactional]
        public List<ProductDetails> FindByTag(long tagId)
        {
            List<ProductDetails> products = new List<ProductDetails>();

            List<Product> productList = TagDao.Find(tagId).Products.ToList<Product>();

            for (int i = 0; i < productList.Count; i++)
            {
                string name = productList.ElementAt(i).name;
                string category = CategoryDao.Find(productList.ElementAt(i).categoryId).name; DateTime registerDate = productList.ElementAt(i).registerDate;
                double prize = productList.ElementAt(i).prize;
                products.Add(new ProductDetails(name, category, registerDate, prize));
            }
            return products;
        }
        [Transactional]
        public int getNumberOfProductsByKeywords(string keywords)
        {
            return ProductDao.GetNumberOfProductsByKeywords(keywords, -1);
        }
        [Transactional]
        public int getNumberOfProductsByKeywords(string keywords, long categoryId)
        {
            return ProductDao.GetNumberOfProductsByKeywords(keywords, categoryId);
        }
    }
}

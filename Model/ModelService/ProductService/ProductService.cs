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
using Es.Udc.DotNet.ModelUtil.Exceptions;
using Es.Udc.DotNet.PracticaMaD.Model.OrderDao;

namespace Es.Udc.DotNet.PracticaMaD.Model.ModelService.ProductService
{
    public class ProductService : IProductService
    {
        [Inject]
        public IProductDao ProductDao { private get; set; }

        [Inject]
        public IOrderDao OrderDao { private get; set; }

        [Inject]
        public ICategoryDao CategoryDao { private get; set; }

        [Inject]
        public ITagDao TagDao { private get; set; }

        [Transactional]
        public List<ProductDetails> FindByKeywords(string keywords,int startIndex, int count)
        {
            List<ProductDetails> products = new List<ProductDetails>();
            string productName = keywords;
            try
            {
                List<Product> productList = ProductDao.FindByKeywords(keywords, -1, startIndex, count);

                for (int i = 0; i < productList.Count; i++)
                {
                    long productId = productList.ElementAt(i).productId;
                    string name = productList.ElementAt(i).name;
                    long categoryId = productList.ElementAt(i).categoryId;
                    string categoryName = CategoryDao.Find(productList.ElementAt(i).categoryId).name;
                    DateTime registerDate = productList.ElementAt(i).registerDate;
                    double prize = productList.ElementAt(i).prize;
                    int numberOfUnits = productList.ElementAt(i).numberOfUnits;
                    products.Add(new ProductDetails(name, categoryId,categoryName, registerDate, prize, productId, numberOfUnits,false));
                }
#pragma warning disable CS0168 // La variable 'e' se ha declarado pero nunca se usa
            }catch (InstanceNotFoundException e)
#pragma warning restore CS0168 // La variable 'e' se ha declarado pero nunca se usa
            {

            }
            return products;
        }

        [Transactional]
        public List<ProductDetails> FindByKeywords(string keywords, long categoryId,int startIndex,int count)
        {
            List<ProductDetails> products = new List<ProductDetails>();

            try
            {
                List<Product> productList = ProductDao.FindByKeywords(keywords, categoryId, startIndex, count);
                for (int i = 0; i < productList.Count; i++)
                {
                    long productId = productList.ElementAt(i).productId;
                    string name = productList.ElementAt(i).name;
                    string categoryName = CategoryDao.Find(productList.ElementAt(i).categoryId).name;
                    DateTime registerDate = productList.ElementAt(i).registerDate;
                    double prize = productList.ElementAt(i).prize;
                    int numberOfUnits = productList.ElementAt(i).numberOfUnits;

                    products.Add(new ProductDetails(name, categoryId,categoryName, registerDate, prize, productId, numberOfUnits, false));
                }
            }catch (InstanceNotFoundException e)
            {

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
                long productId = productList.ElementAt(i).productId;
                string name = productList.ElementAt(i).name;
                long categoryId = productList.ElementAt(i).categoryId;
                string categoryName = CategoryDao.Find(productList.ElementAt(i).categoryId).name;
                DateTime registerDate = productList.ElementAt(i).registerDate;
                double prize = productList.ElementAt(i).prize;
                int numberOfUnits = productList.ElementAt(i).numberOfUnits;

                products.Add(new ProductDetails(name, categoryId,categoryName, registerDate, prize,productId,numberOfUnits,false));
            }
            return products;
        }
        [Transactional]
        public ProductDetails FindProduct(long id)
        {
            Product product;
            product = ProductDao.Find(id);
            long categoryId = product.categoryId;
            string categoryName = CategoryDao.Find(product.categoryId).name;

            ProductDetails productDetails;
            if(product is Movie)
            {
                Movie mov = product as Movie;
                productDetails = new MovieDetails(product.name, categoryId,categoryName, product.registerDate, product.prize, product.productId, product.numberOfUnits, false, mov.title, mov.director, mov.summary, mov.topic, mov.duration);
            }else if(product is Book)
            {
                Book book = product as Book;
                productDetails = new BookDetails(product.name, categoryId,categoryName, product.registerDate, product.prize, product.productId, product.numberOfUnits, false, book.title, book.author, book.summary, book.topic, book.pages);
            }else if (product is CD)
            {
                CD cd = product as CD;
                productDetails = new CDDetails(product.name, categoryId,categoryName, product.registerDate, product.prize, product.productId, product.numberOfUnits, false, cd.title, cd.artist, cd.topic, cd.songs);
            }
            else {
                productDetails = new ProductDetails(product.name, categoryId,categoryName, product.registerDate, product.prize, product.productId, product.numberOfUnits, false);
            }
            return productDetails;
        }

        

        [Transactional]
        public int getNumberOfProductsByKeywords(string keywords)
        {
            int result = 0;
            try
            {
                result = ProductDao.GetNumberOfProductsByKeywords(keywords, -1);
#pragma warning disable CS0168 // La variable 'e' se ha declarado pero nunca se usa
            }catch(InstanceNotFoundException e)
#pragma warning restore CS0168 // La variable 'e' se ha declarado pero nunca se usa
            {

            }
            return result;
        }
        [Transactional]
        public int getNumberOfProductsByKeywords(string keywords, long categoryId)
        {
            int result = 0;
            try
            {
                return ProductDao.GetNumberOfProductsByKeywords(keywords, categoryId);
#pragma warning disable CS0168 // La variable 'e' se ha declarado pero nunca se usa
            }catch(InstanceNotFoundException e)
#pragma warning restore CS0168 // La variable 'e' se ha declarado pero nunca se usa
            {

            }
            return result;
        }

        [Transactional]
        public List<ProductDetails> GetOrderLineProductsByOrderId(long orderId)
        {
            Order order = OrderDao.Find(orderId);
            List<ProductDetails> productsDetails = new List<ProductDetails>();

            for (int i = 0; i < order.OrderLines.Count; i++)
            {
                Product p = ProductDao.Find(order.OrderLines.ElementAt(i).productId);
                string categoryName = CategoryDao.Find(p.categoryId).name;
                ProductDetails productDetails = new ProductDetails(p.name, p.categoryId,categoryName, p.registerDate, order.OrderLines.ElementAt(i).unitPrize, p.productId, order.OrderLines.ElementAt(i).numberOfUnits);
                productsDetails.Add(productDetails);
            }

            return productsDetails;
        }

    }
}

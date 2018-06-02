﻿using System;
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
            try
            {
                List<Product> productList = ProductDao.FindByKeywords(keywords, -1, startIndex, count);

                for (int i = 0; i < productList.Count; i++)
                {
                    long productId = productList.ElementAt(i).productId;
                    string name = productList.ElementAt(i).name;
                    string category = CategoryDao.Find(productList.ElementAt(i).categoryId).name;
                    DateTime registerDate = productList.ElementAt(i).registerDate;
                    double prize = productList.ElementAt(i).prize;
                    int numberOfUnits = productList.ElementAt(i).numberOfUnits;
                    products.Add(new ProductDetails(name, category, registerDate, prize, productId, numberOfUnits,false));
                }
            }catch (InstanceNotFoundException e)
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
                    string category = CategoryDao.Find(productList.ElementAt(i).categoryId).name; DateTime registerDate = productList.ElementAt(i).registerDate;
                    double prize = productList.ElementAt(i).prize;
                    int numberOfUnits = productList.ElementAt(i).numberOfUnits;

                    products.Add(new ProductDetails(name, category, registerDate, prize, productId, numberOfUnits, false));
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
                string category = CategoryDao.Find(productList.ElementAt(i).categoryId).name;
                DateTime registerDate = productList.ElementAt(i).registerDate;
                double prize = productList.ElementAt(i).prize;
                int numberOfUnits = productList.ElementAt(i).numberOfUnits;

                products.Add(new ProductDetails(name, category, registerDate, prize,productId,numberOfUnits,false));
            }
            return products;
        }
        [Transactional]
        public ProductDetails FindProduct(long id)
        {
            Product product;
            product = ProductDao.Find(id);
            string category = CategoryDao.Find(product.categoryId).name;
            ProductDetails productDetails = new ProductDetails(product.name,category,product.registerDate,product.prize,product.productId,product.numberOfUnits,false);
            return productDetails;
        }

        [Transactional]
        public int getNumberOfProductsByKeywords(string keywords)
        {
            int result = 0;
            try
            {
                result = ProductDao.GetNumberOfProductsByKeywords(keywords, -1);
            }catch(InstanceNotFoundException e)
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
            }catch(InstanceNotFoundException e)
            {

            }
            return result;
        }
    }
}

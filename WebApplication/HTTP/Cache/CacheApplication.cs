using Es.Udc.DotNet.PracticaMaD.Model.ProductDao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;

namespace Es.Udc.DotNet.PracticaMaD.WebApplication.HTTP.Cache
{
    public class CacheApplication
    {
        public static List<string> keys = new List<string>();

        public static MemoryCache cache = new MemoryCache("MyTestCache");

        private static CacheItemPolicy cachePolicy = new CacheItemPolicy { SlidingExpiration = new TimeSpan(2, 0, 0) };

        public static bool IsContainKey(string key)
        {
            return cache.Contains(key);
        }

        public static void StoreItemsCache(string key, List<ProductDetails> searchProducts)
        {
            if (!cache.Contains(key) && searchProducts.Count>0)
            {
                keys.Add(key);
                cache.Add(key, searchProducts, cachePolicy);
                DeleteOlderItem();
            }
        }

        public static List<ProductDetails> GetCacheItem(string key)
        {
            List<ProductDetails> searchProducts = null;

            if (cache.Contains(key))
                searchProducts = cache.Get(key) as List<ProductDetails>;

            return searchProducts;
        }

        private static void DeleteOlderItem()
        {
            if (cache.Count() > 5)
            {
                cache.Remove(keys[0]);
                keys.RemoveAt(0);
            }
        }

    }
}
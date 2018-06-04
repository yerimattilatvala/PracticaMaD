using System;
using System.Collections.Generic;
using Es.Udc.DotNet.PracticaMaD.Model.ProductDao;
using Ninject;
using Es.Udc.DotNet.PracticaMaD.Model.OrderDao;
using Es.Udc.DotNet.PracticaMaD.Model.ModelService.ProductService;
using System.Linq;
using Es.Udc.DotNet.PracticaMaD.Model.CategoryDao;

namespace Es.Udc.DotNet.PracticaMaD.Model.ModelService.OrderLineService
{
    public class OrderLineService : IOrderLineService
    {

        [Inject]
        public IOrderDao OrderDao { private get; set; }

        [Inject]
        public IProductDao ProductDao { private get; set; }

        [Inject]
        public ICategoryDao CategoryDao { private get; set; }

        public List<ProductDetails> GetOrderLineProductsByOrderId(long orderId)
        {
            Order order = OrderDao.Find(orderId);
            List<ProductDetails> productsDetails = new List<ProductDetails>();

            for(int i= 0; i< order.OrderLines.Count; i++)
            {
                Product p = ProductDao.Find(order.OrderLines.ElementAt(i).productId);
                ProductDetails productDetails = new ProductDetails(p.name,CategoryDao.Find(p.categoryId).name,p.registerDate, order.OrderLines.ElementAt(i).unitPrize, p.productId, order.OrderLines.ElementAt(i).numberOfUnits);
                productsDetails.Add(productDetails);
            }

            return productsDetails;
        }
    }
}

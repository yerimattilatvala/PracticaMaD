using Es.Udc.DotNet.ModelUtil.Transactions;
using Es.Udc.DotNet.PracticaMaD.Model.CategoryDao;
using Es.Udc.DotNet.PracticaMaD.Model.OrderDao;
using Es.Udc.DotNet.PracticaMaD.Model.ProductDao;
using Ninject;
using System.Collections.Generic;

namespace Es.Udc.DotNet.PracticaMaD.Model.ModelService.OrderLineService
{
    public interface IOrderLineService
    {
        [Inject]
        IOrderDao OrderDao { set; }

        [Inject]
        IProductDao ProductDao { set; }

        [Inject]
        ICategoryDao CategoryDao { set; }

        [Transactional]
        List<ProductDetails> GetOrderLineProductsByOrderId(long orderId);
    }
}

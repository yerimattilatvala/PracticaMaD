using Es.Udc.DotNet.ModelUtil.Transactions;
using Es.Udc.DotNet.PracticaMaD.Model.CardDao;
using Es.Udc.DotNet.PracticaMaD.Model.OrderDao;
using Es.Udc.DotNet.PracticaMaD.Model.OrderLineDao;
using Es.Udc.DotNet.PracticaMaD.Model.ProductDao;
using Es.Udc.DotNet.PracticaMaD.Model.UserProfileDao;
using Ninject;
using System.Collections.Generic;

namespace Es.Udc.DotNet.PracticaMaD.Model.ModelService.OrderService
{
    public interface IOrderService
    {
        [Inject]
        IUserProfileDao UserProfileDao { set; }

        [Inject]
        ICardDao CardDao { set; }

        [Inject]
        IOrderDao OrderDao { set; }

        [Inject]
        IProductDao ProductDao { set; }

        [Inject]
        IOrderLineDao OrderLineDao { set; }

        [Transactional]
        OrderDetails GenerateOrder(long userProfileId, long idCard, int postalAddress, List<ProductDetails> productList);

        [Transactional]
        List<OrderDetails> ViewOrdersByUser(long usrId);
    }
}

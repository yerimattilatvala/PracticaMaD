using Es.Udc.DotNet.ModelUtil.Transactions;
using Es.Udc.DotNet.PracticaMaD.Model.CardDao;
using Es.Udc.DotNet.PracticaMaD.Model.OrderDao;
using Es.Udc.DotNet.PracticaMaD.Model.OrderLineDao;
using Es.Udc.DotNet.PracticaMaD.Model.ProductDao;
using Es.Udc.DotNet.PracticaMaD.Model.UserProfileDao;
using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Es.Udc.DotNet.PracticaMaD.Model.ModelService
{
    public interface IModelService
    {
        [Inject]
        IUserProfileDao UserProfileDao { set; }

        [Inject]
        ICardDao CardDao { set; }

        [Inject]
        IProductDao ProductDao { set; }

        [Inject]
        IOrderDao OrderDao { set; }

        [Inject]
        IOrderLineDao OrderLineDao { set; }

        [Transactional]
        OrderDetails GenerateOrder(long userProfileId, int cardNumber,int postalAddress,List<ProductDetails> productList);

        [Transactional]
        List<OrderDetails> ViewOrdersByUser(long usrId);

        [Transactional]
        List<ProductDetails> FindByKeywords(string keywords);

        [Transactional]
        long RegisterUser(String loginName, String clearPassword,
            UserProfileDetails userProfileDetails);

        [Transactional]
        LoginResult Login(String loginName, String password,
            Boolean passwordIsEncrypted);

        [Transactional]
        UserProfileDetails FindUserProfileDetails(long userProfileId);

        [Transactional]
        void UpdateUserProfileDetails(long userProfileId,
            UserProfileDetails userProfileDetails);

        [Transactional]
        void ChangePassword(long userProfileId, String oldClearPassword,
            String newClearPassword);

        [Transactional]
        void AddCreditCard(long userProfileId, CardDetails newCard);

        [Transactional]
        List<CardDetails> ViewCardsByUser(long userProfileId);

        [Transactional]
        void ChangeDefaultCard(long userProfileId, int cardNumber);

    }
}

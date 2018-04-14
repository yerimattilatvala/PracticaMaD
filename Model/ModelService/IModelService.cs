using Es.Udc.DotNet.PracticaMaD.Model.CardDao;
using Es.Udc.DotNet.PracticaMaD.Model.OrderDao;
using Es.Udc.DotNet.PracticaMaD.Model.ProductDao;
using Es.Udc.DotNet.PracticaMaD.Model.UserProfileDao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Es.Udc.DotNet.PracticaMaD.Model.ModelService
{
    interface IModelService
    {

        OrderDetails GenerateOrder(long userProfileId, int cardNumber,int postalAddress,List<ProductDetails> productList);

        List<OrderDetails> ViewOrdersByUser(long usrId);

        List<ProductDetails> FindByKeywords(string keywords);

        long RegisterUser(String loginName, String clearPassword,
            UserProfileDetails userProfileDetails);
        
        LoginResult Login(String loginName, String password,
            Boolean passwordIsEncrypted);
        
        UserProfileDetails FindUserProfileDetails(long userProfileId);
        
        void UpdateUserProfileDetails(long userProfileId,
            UserProfileDetails userProfileDetails);

        void ChangePassword(long userProfileId, String oldClearPassword,
            String newClearPassword);

        void AddCreditCard(long userProfileId, CardDetails newCard);

        List<CardDetails> ViewCardsByUser(long userProfileId);

        void ChangeDefaultCard(long userProfileId, int cardNumber);

    }
}

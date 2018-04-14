using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Es.Udc.DotNet.PracticaMaD.Model.ProductDao;
using Es.Udc.DotNet.ModelUtil.Exceptions;
using Es.Udc.DotNet.PracticaMaD.Model.ModelService.Exceptions;
using Es.Udc.DotNet.PracticaMaD.Model.OrderLineDao;
using Es.Udc.DotNet.PracticaMaD.Model.OrderDao;
using Es.Udc.DotNet.PracticaMaD.Model.UserProfileDao;
using Es.Udc.DotNet.PracticaMaD.Model.Util;

namespace Es.Udc.DotNet.PracticaMaD.Model.ModelService
{
    class ModelService : IModelService
    {

        private IProductDao ProductDao;
        private IOrderLineDao OrderLineDao;
        private IOrderDao OrderDao;
        private IUserProfileDao UserProfileDao;

        public OrderDetails GenerateOrder(long usrId, int cardNumber, int postalAddress, List<ProductDetails> productList)
        {
            Order order = new Order();
            order.usrId = usrId;
            order.cardNumber = cardNumber;
            order.orderDate = DateTime.Now;
            order.postalAddress = postalAddress;
            OrderDao.Create(order);
            long orderId = order.orderId;
            for (int i = 0; i < productList.Count; i++)
            {
                long productId = productList.ElementAt(i).productId;
                Product product = ProductDao.Find(productId);
                int numberOfUnits = productList.ElementAt(i).numberOfUnits;
                int numberOfRemainingUnits = product.numberOfUnits - productList.ElementAt(i).numberOfUnits;
                if (numberOfRemainingUnits < 0)
                    throw new InsuficientNumberOfUnitsException(product.name, numberOfRemainingUnits);
                product.numberOfUnits = numberOfRemainingUnits;
                ProductDao.Update(product);
                OrderLine orderLine = new OrderLine();
                orderLine.orderId = orderId;
                orderLine.productId = productId;
                orderLine.numberOfUnits = numberOfUnits;
                orderLine.unitPrize = product.prize;
                OrderLineDao.Create(orderLine);
            }
            return new OrderDetails(orderId,usrId,cardNumber,order.postalAddress,order.orderDate);
        }

        public List<OrderDetails> ViewOrdersByUser(long usrId)
        {
            UserProfile User = UserProfileDao.Find(usrId);

            List<OrderDetails> ordersDetails = new List<OrderDetails>();

            List<Order> orders = User.Orders.ToList();

            for (int i = 0; i< orders.Count; i++)
            {
                List<OrderLine> orderLines = orders.ElementAt(i).OrderLines.ToList();

                List<OrderLineDetails> orderLinesDetails = new List<OrderLineDetails>();
                for (int j = 0; j<orderLines.Count; j++)
                {
                    string productName = orderLines.ElementAt(j).Product.name;
                    int numberOfUnits = orderLines.ElementAt(j).numberOfUnits;
                    float unitPrize = (float)orderLines.ElementAt(j).unitPrize;
                    OrderLineDetails orderLine = new OrderLineDetails(orders.ElementAt(i).orderId,productName,numberOfUnits,unitPrize);
                    orderLinesDetails.Add(orderLine);
                }
                long orderId = orders.ElementAt(i).orderId;
                int cardNumber = orders.ElementAt(i).cardNumber;
                int postalAddress = orders.ElementAt(i).postalAddress;
                DateTime orderDate = orders.ElementAt(i).orderDate;
                ordersDetails.Add(new OrderDetails(orderId,usrId,cardNumber,postalAddress,orderDate,orderLinesDetails));
            }
            return ordersDetails;
        }


        public List<ProductDetails> FindByKeywords(string keywords)
        {
            List<ProductDetails> products = new List<ProductDetails>();
            /*HAY QUE TOKENIZAR PERO PARA IR PROBANDO SIRVE*/
            string productName = keywords;

            List<Product> productList = ProductDao.FindByKeywords(keywords,"");

            for(int i = 0; i<productList.Count; i++)
            {
                string name = productList.ElementAt(i).name;
                string category = productList.ElementAt(i).Category.name;
                DateTime registerDate = productList.ElementAt(i).registerDate;
                double prize = productList.ElementAt(i).prize;
                products.Add(new ProductDetails(name,category,registerDate,prize));
            }
            return products;
        }

        public long RegisterUser(string loginName, string clearPassword,
            UserProfileDetails userProfileDetails)
        {

            try
            {
                UserProfileDao.FindByLoginName(loginName);

                throw new DuplicateInstanceException(loginName,
                    typeof(UserProfile).FullName);

            }
            catch (InstanceNotFoundException)
            {
                String encryptedPassword = PasswordEncrypter.Crypt(clearPassword);

                UserProfile userProfile = new UserProfile();

                userProfile.loginName = loginName;
                userProfile.enPassword = encryptedPassword;
                userProfile.firstName = userProfileDetails.FirstName;
                userProfile.lastName = userProfileDetails.Lastname;
                userProfile.email = userProfileDetails.Email;
                userProfile.language = userProfileDetails.Language;
                userProfile.country = userProfileDetails.Country;

                UserProfileDao.Create(userProfile);

                return userProfile.usrId;

            }

        }

        /// <exception cref="InstanceNotFoundException"/>
        /// <exception cref="IncorrectPasswordException"/>        
        public LoginResult Login(string loginName, string password, bool passwordIsEncrypted)
        {
            UserProfile userProfile = UserProfileDao.FindByLoginName(loginName);

            String storedPassword = userProfile.enPassword;

            if (passwordIsEncrypted)
            {
                if (!password.Equals(storedPassword))
                {
                    throw new IncorrectPasswordException(loginName);
                }
            }
            else
            {
                if (!PasswordEncrypter.IsClearPasswordCorrect(password,
                        storedPassword))
                {
                    throw new IncorrectPasswordException(loginName);
                }
            }

            return new LoginResult(userProfile.usrId, userProfile.firstName,
                storedPassword, userProfile.language, userProfile.country);
        }

        /// <exception cref="InstanceNotFoundException"/>
        public UserProfileDetails FindUserProfileDetails(long userProfileId)
        {
            UserProfile userProfile = UserProfileDao.Find(userProfileId);

            UserProfileDetails userProfileDetails =
                new UserProfileDetails(userProfile.firstName,
                    userProfile.lastName, userProfile.email,
                    userProfile.language, userProfile.country,userProfile.postalAddress);

            return userProfileDetails;
        }

        /// <exception cref="InstanceNotFoundException"/>
        public void UpdateUserProfileDetails(long userProfileId,
            UserProfileDetails userProfileDetails)
        {
            UserProfile userProfile =
                UserProfileDao.Find(userProfileId);

            userProfile.firstName = userProfileDetails.FirstName;
            userProfile.lastName = userProfileDetails.Lastname;
            userProfile.email = userProfileDetails.Email;
            userProfile.language = userProfileDetails.Language;
            userProfile.country = userProfileDetails.Country;
            UserProfileDao.Update(userProfile);
        }

        /// <exception cref="IncorrectPasswordException"/>
        /// <exception cref="InstanceNotFoundException"/>
        public void ChangePassword(long userProfileId, string oldClearPassword,
            string newClearPassword)
        {
            UserProfile userProfile = UserProfileDao.Find(userProfileId);
            String storedPassword = userProfile.enPassword;

            if (!PasswordEncrypter.IsClearPasswordCorrect(oldClearPassword,
                storedPassword))
            {
                throw new IncorrectPasswordException(userProfile.loginName);
            }

            userProfile.enPassword =
                PasswordEncrypter.Crypt(newClearPassword);

            UserProfileDao.Update(userProfile);
        }
    }
}

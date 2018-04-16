using System;
using System.Collections.Generic;
using System.Linq;
using Es.Udc.DotNet.PracticaMaD.Model.ProductDao;
using Es.Udc.DotNet.ModelUtil.Exceptions;
using Es.Udc.DotNet.PracticaMaD.Model.ModelService.Exceptions;
using Es.Udc.DotNet.PracticaMaD.Model.OrderLineDao;
using Es.Udc.DotNet.PracticaMaD.Model.OrderDao;
using Es.Udc.DotNet.PracticaMaD.Model.UserProfileDao;
using Es.Udc.DotNet.PracticaMaD.Model.Util;
using Es.Udc.DotNet.PracticaMaD.Model.CardDao;
using Ninject;
using Es.Udc.DotNet.ModelUtil.Transactions;

namespace Es.Udc.DotNet.PracticaMaD.Model.ModelService
{
    public class ModelService : IModelService
    {
        [Inject]
        public IUserProfileDao UserProfileDao { private get; set; }
        [Inject]
        public IProductDao ProductDao { private get; set; }

        [Inject]
        public IOrderLineDao OrderLineDao { private get; set; }

        [Inject]
        public IOrderDao OrderDao { private get; set; }

        [Inject]
        public ICardDao CardDao { private get; set; }

        [Transactional]
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

        [Transactional]
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


        [Transactional]
        public List<ProductDetails> FindByKeywords(string keywords)
        {
            List<ProductDetails> products = new List<ProductDetails>();
            /*HAY QUE TOKENIZAR PERO PARA IR PROBANDO SIRVE*/
            string productName = keywords;

            List<Product> productList = ProductDao.FindByKeywords(keywords,null);

            for(int i = 0; i<productList.Count; i++)
            {
                string name = productList.ElementAt(i).name;
                long category = productList.ElementAt(i).categoryId;
                DateTime registerDate = productList.ElementAt(i).registerDate;
                double prize = productList.ElementAt(i).prize;
                products.Add(new ProductDetails(name,category,registerDate,prize));
            }
            return products;
        }

        [Transactional]
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
                userProfile.postalAddress = userProfileDetails.PostalAddress;

                UserProfileDao.Create(userProfile);

                return userProfile.usrId;

            }

        }

        [Transactional]
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

        [Transactional]
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

        [Transactional]
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

        [Transactional]
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

        [Transactional]
        public void AddCard(long userProfileId, CardDetails newCard)
        {
            UserProfile UserProfile = UserProfileDao.Find(userProfileId);

            Card creditCard = new Card();
            creditCard.usrId = UserProfile.usrId;
            creditCard.cardNumber = newCard.CardNumber;
            creditCard.verificationCode = newCard.VerificationCode;
            creditCard.expirationDate = newCard.ExpirateTime;
            creditCard.cardType = newCard.CardType;

            if (!UserProfile.Cards.Any())
                creditCard.defaultCard = true;
            else
                creditCard.defaultCard = false;

            CardDao.Create(creditCard);
        }

        [Transactional]
        public List<CardDetails> ViewCardsByUser(long userProfileId)
        {
            List<CardDetails> userCards = new List<CardDetails>();

            List<Card> cards = UserProfileDao.Find(userProfileId).Cards.ToList<Card>();

            for(int i = 0; i< cards.Count; i++)
            {
                int cardNumber = cards.ElementAt(i).cardNumber;
                int cv = cards.ElementAt(i).verificationCode;
                DateTime expirationDate = cards.ElementAt(i).expirationDate;
                string type = cards.ElementAt(i).cardType;
                userCards.Add(new CardDetails(cardNumber,cv,expirationDate,type));
            }
            return userCards;
        }

        [Transactional]
        public void ChangeDefaultCard(long userProfileId, int cardNumber)
        {
            UserProfile userProfile = UserProfileDao.Find(userProfileId);

            List<Card> userCards = userProfile.Cards.ToList<Card>();

            for(int i = 0; i< userCards.Count; i++)
            {
                if(userCards.ElementAt(i).defaultCard == true)
                {
                    userCards.ElementAt(i).defaultCard = false;
                    CardDao.Update(userCards.ElementAt(i));
                }
            }

            Card card = CardDao.Find(cardNumber);
            card.defaultCard = true;
            CardDao.Update(card);
        }
    }
}
